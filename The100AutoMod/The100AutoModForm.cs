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

        private ChromiumWebBrowser uiBrowser;

        private The100AutoModBoundObject _bound;
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

            this.Resize += The100AutoModForm_Resize;
            this.ResizeEnd += The100AutoModForm_ResizeEnd;
            this.FormClosing += The100AutoModForm_FormClosing;

            uiToggleDevToolsMenuItem.Click += UiToggleDevToolsMenuItem_Click;
            uiExitMenuItem.Click += UiExitMenuItem_Click;
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

            uiBrowser = new ChromiumWebBrowser( THE100_LOGIN_URL );
            uiSplitLayout.Panel1.Controls.Add( uiBrowser );
            uiBrowser.Dock = DockStyle.Fill;
            uiBrowser.BringToFront();

            uiBrowser.IsBrowserInitializedChanged += UiBrowser_IsBrowserInitializedChanged;
            uiBrowser.FrameLoadEnd += UiBrowser_FrameLoadEnd;
            
            uiBrowser.RegisterAsyncJsObject( "The100AutoMod", _bound );
        }

        private void InitializeNotifyIcon()
        {
            uiNotifyIcon.Icon = this.Icon;

            uiNotifyIcon.DoubleClick += UiNotifyIcon_DoubleClick;

            uiOpenNotifyIconMenuItem.Click += UiOpenNotifyIconMenuItem_Click;
            uiExitNotifyIconMenuItem.Click += UiExitNotifyIconMenuItem_Click;
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

        private void UiNotifyIcon_DoubleClick( object sender, EventArgs e )
        {
            LOG.Debug( "NotifyIcon - Double Click" );

            this.ShowMe();
        }

        private void UiOpenNotifyIconMenuItem_Click( object sender, EventArgs e )
        {
            LOG.Debug( "NotifyIcon - Open" );

            this.ShowMe();
        }

        private void UiExitNotifyIconMenuItem_Click( object sender, EventArgs e )
        {
            LOG.Debug( "NotifyIcon - Exit" );

            this.Close();
        }

        private void The100AutoModForm_Resize( object sender, EventArgs e )
        {
            if( this.WindowState == FormWindowState.Minimized )
            {
                this.Hide();
                uiNotifyIcon.Visible = true;
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

            uiBrowser.Dispose();
        }

        private void UiToggleDevToolsMenuItem_Click( object sender, EventArgs e )
        {
            if( uiBrowser.IsBrowserInitialized )
            {
                uiBrowser.ShowDevTools();
            }
        }

        private void UiExitMenuItem_Click( object sender, EventArgs e )
        {
            this.Close();
        }

        private void Bound_NewMessageReceived( object sender, MessageReceivedEventArgs e )
        {
            LOG.DebugInject( "NewMessageReceived - {Message}", e );

            this.UiBeginInvoke( (Action<Message>)AppendMessage, e.Message );
        }

        private void UiBrowser_IsBrowserInitializedChanged( object sender, IsBrowserInitializedChangedEventArgs e )
        {
            LOG.DebugInject( "Browser_IsBrowserInitializedChanged - IsInitialized: {IsBrowserInitialized}", e );

            uiToggleDevToolsMenuItem.Enabled = e.IsBrowserInitialized;
        }

        private void UiBrowser_FrameLoadEnd( object sender, FrameLoadEndEventArgs e )
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

                uiBrowser.Load( THE100_CHAT_URL );
            }
            else if( e.Url == THE100_CHAT_URL )
            {
                LOG.Debug( "Browser_FrameLoaded - Chat" );

                uiBrowser.ExecuteScriptAsync( Resources.CreateChatListenerScript );
            }
        }

        private void ShowMe()
        {
            this.Show();
            if( this.WindowState == FormWindowState.Minimized )
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.BringToFront();
            uiNotifyIcon.Visible = false;
        }

        private void SendLogin()
        {
            var auth = new
            {
                username = Settings.Default.The100Username,
                password = Settings.Default.The100Password
            };
            uiBrowser.ExecuteScriptAsync( Resources.LoginScript.Inject( auth ) );
        }

        private void PromptLogin()
        {
            ShowMe();
            LoginDialog login = new LoginDialog( Settings.Default.The100Username, Settings.Default.The100Password );
            if( login.ShowDialog() == DialogResult.OK )
            {
                Settings.Default.The100Username = login.Username;
                Settings.Default.The100Password = login.Password;
                Settings.Default.Save();

                SendLogin();
            }
        }

        private void AppendMessage( Message m )
        {
            uiChat.AppendText( "{Username}: {Content}".Inject( m ) + Environment.NewLine );
            uiChat.SelectionStart = uiChat.TextLength;
            uiChat.ScrollToCaret();
        }
    }
}
