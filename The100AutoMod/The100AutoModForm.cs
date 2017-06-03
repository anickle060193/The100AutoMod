using CefSharp;
using CefSharp.WinForms;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using The100AutoMod.Properties;

namespace The100AutoMod
{
    public partial class The100AutoModForm : Form
    {
        private static readonly ILog LOG = LogManager.GetLogger( typeof( The100AutoModForm ) );

        private static readonly String THE100_LOGIN_URL = "https://www.the100.io/login";
        private static readonly String THE100_AFTER_LOGIN_URL = "https://www.the100.io/users/264831/dashboard";
        private static readonly String THE100_CHAT_URL = "https://www.the100.io/groups/268/chatroom";

        private The100AutoModBoundObject _bound;
        private ChromiumWebBrowser _browser;
        private bool _loginAttempted;

        public The100AutoModForm()
        {
            LOG.Debug( "Constructor" );
            InitializeComponent();
            InitializeBound();
            InitializeChromium();
            InitializeNotifyIcon();
            InitializeFromSettings();

            _loginAttempted = false;

            this.ResizeEnd += The100AutoModForm_ResizeEnd;
            this.FormClosing += The100AutoModForm_FormClosing;

            _toggleDevToolsMenuItem.Click += ToggleDevToolsMenuItem_Click;
            _exitMenuItem.Click += ExitMenuItem_Click;
        }

        private void InitializeBound()
        {
            _bound = new The100AutoModBoundObject();
            _bound.MessageReceived += Bound_NewMessageReceived;
        }

        private void InitializeChromium()
        {
            var initInfo = new
            {
                chromium = Cef.ChromiumVersion,
                cef = Cef.CefVersion,
                cefsharp = Cef.CefSharpVersion,
                environment = Environment.Is64BitProcess ? "x64" : "x86"
            };
            LOG.DebugInject( "InitializeChromium - Chromium: {chromium}, CEF: {cef}, CefSharp: {cefsharp}, Environment: {environment}", initInfo );

            _browser = new ChromiumWebBrowser( THE100_LOGIN_URL );
            this.Controls.Add( _browser );
            _browser.Dock = DockStyle.Fill;
            _browser.BringToFront();

            _browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;
            _browser.FrameLoadEnd += Browser_FrameLoadEnd;
            
            _browser.RegisterAsyncJsObject( "The100AutoMod", _bound );
        }

        private void InitializeNotifyIcon()
        {
            _notifyIcon.Icon = this.Icon;

            _notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            _openNotifyIconMenuItem.Click += OpenNotifyIconMenuItem_Click;
            _exitNotifyIconMenuItem.Click += ExitNotifyIconMenuItem_Click;
        }

        private void NotifyIcon_DoubleClick( object sender, EventArgs e )
        {
            LOG.Debug( "NotifyIcon - Double Click" );

            this.Show();
            _notifyIcon.Visible = false;
        }

        private void OpenNotifyIconMenuItem_Click( object sender, EventArgs e )
        {
            LOG.Debug( "NotifyIcon - Open" );

            this.Show();
            _notifyIcon.Visible = false;
        }

        private void ExitNotifyIconMenuItem_Click( object sender, EventArgs e )
        {
            LOG.Debug( "NotifyIcon - Exit" );

            Application.Exit();
        }

        private void InitializeFromSettings()
        {
            if( !Settings.Default.The100AutoModFormLocation.IsEmpty )
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = Settings.Default.The100AutoModFormLocation;
            }

            if( !Settings.Default.The100AutoModFormSize.IsEmpty )
            {
                this.Size = Settings.Default.The100AutoModFormSize;
            }

            if( Settings.Default.The100AutoModFormMaximized )
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void The100AutoModForm_ResizeEnd( object sender, EventArgs e )
        {
            if( this.WindowState == FormWindowState.Maximized )
            {
                Settings.Default.The100AutoModFormMaximized = true;
                Settings.Default.The100AutoModFormSize = this.RestoreBounds.Size;
                Settings.Default.The100AutoModFormLocation = this.RestoreBounds.Location;
            }
            else if( this.WindowState == FormWindowState.Minimized )
            {
                Settings.Default.The100AutoModFormMaximized = false;
                Settings.Default.The100AutoModFormSize = this.RestoreBounds.Size;
                Settings.Default.The100AutoModFormLocation = this.RestoreBounds.Location;
            }
            else
            {
                Settings.Default.The100AutoModFormMaximized = false;
                Settings.Default.The100AutoModFormSize = this.Size;
                Settings.Default.The100AutoModFormLocation = this.Location;
            }

            Settings.Default.Save();
        }

        private void The100AutoModForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            LOG.DebugInject( "FormClosing - Reason: {CloseReason}", e );

            if( e.CloseReason == CloseReason.UserClosing )
            {
                LOG.Debug( "FormClosing - Cancelling Close" );

                e.Cancel = true;
                this.Hide();
                _notifyIcon.Visible = true;
                return;
            }

            _browser.Dispose();
        }

        private void ToggleDevToolsMenuItem_Click( object sender, EventArgs e )
        {
            if( _browser.IsBrowserInitialized )
            {
                _browser.ShowDevTools();
            }
        }

        private void ExitMenuItem_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

        private void Bound_NewMessageReceived( object sender, MessageReceivedEventArgs e )
        {
            LOG.DebugInject( "NewMessageReceived - {Message}", e );
        }

        private void Browser_IsBrowserInitializedChanged( object sender, IsBrowserInitializedChangedEventArgs e )
        {
            LOG.DebugInject( "Browser_IsBrowserInitializedChanged - IsInitialized: {IsBrowserInitialized}", e );

            _toggleDevToolsMenuItem.Enabled = e.IsBrowserInitialized;
        }

        private void Browser_FrameLoadEnd( object sender, FrameLoadEndEventArgs e )
        {
            LOG.DebugInject( "Browser_FrameLoadEnd - URL: {Url} - Status Code: {HttpStatusCode}", e );

            if( e.Url == THE100_LOGIN_URL )
            {
                LOG.Debug( "Browser_FrameLoadEnd - Login" );

                if( _loginAttempted )
                {
                    this.UiBeginInvoke( (Action)PromptLogin );
                }
                else
                {
                    _loginAttempted = true;

                    SendLogin();
                }
            }
            else if( e.Url == THE100_AFTER_LOGIN_URL )
            {
                LOG.Debug( "Browser_FrameLoadEnd - After Login" );

                _browser.Load( THE100_CHAT_URL );
            }
            else if( e.Url == THE100_CHAT_URL )
            {
                LOG.Debug( "Browser_FrameLoaded - Chat" );

                _browser.ExecuteScriptAsync( Resources.CreateChatListenerScript );
            }
        }

        private void SendLogin()
        {
            var auth = new
            {
                username = Settings.Default.The100Username,
                password = Settings.Default.The100Password
            };
            _browser.ExecuteScriptAsync( Resources.LoginScript.Inject( auth ) );
        }

        private void PromptLogin()
        {
            LoginDialog login = new LoginDialog( Settings.Default.The100Username, Settings.Default.The100Password );
            if( login.ShowDialog() == DialogResult.OK )
            {
                Settings.Default.The100Username = login.Username;
                Settings.Default.The100Password = login.Password;
                Settings.Default.Save();

                SendLogin();
            }
        }
    }
}
