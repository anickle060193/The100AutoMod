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

        private static readonly String THE100_CHAT_URL = "https://www.the100.io/groups/268/chatroom";

        private The100ChatBrowser _chatBrowser;
        private The100ModBrowser _modBrowser;

        public The100AutoModForm()
        {
            LOG.Debug( "Constructor" );
            InitializeComponent();
            InitializeChromium();
            InitializeNotifyIcon();
            InitializeFromSettings();

            this.Resize += The100AutoModForm_Resize;
            this.ResizeEnd += The100AutoModForm_ResizeEnd;
            this.FormClosing += The100AutoModForm_FormClosing;
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

            _chatBrowser = new The100ChatBrowser( THE100_CHAT_URL );
            _chatBrowser.Dock = DockStyle.Fill;
            _chatBrowser.LoginPrompt += Browser_LoginPrompt;
            _chatBrowser.LoggedIn += ChatBrowser_LoggedIn;
            _chatBrowser.ChatMessageReceived += ChatBrowser_ChatMessageReceived;
            _chatBrowser.IsBrowserInitializedChanged += ChatBrowser_IsBrowserInitializedChanged;
            uiBrowserSplitContainer.Panel2.Controls.Add( _chatBrowser );

            _modBrowser = new The100ModBrowser();
            _modBrowser.Dock = DockStyle.Fill;
            _modBrowser.LoginPrompt += Browser_LoginPrompt;
            uiBrowserSplitContainer.Panel1.Controls.Add( _modBrowser );
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

            _chatBrowser.Dispose();
            _modBrowser.Dispose();
        }

        private void Browser_LoginPrompt( object sender, LoginPromptEventArgs e )
        {
            if( !e.RePrompt )
            {
                e.Username = Settings.Default.The100Username;
                e.Password = Settings.Default.The100Password;
            }
            else
            {
                this.Invoke( (Action<LoginPromptEventArgs>)PromptLogin, e );
            }
        }

        private void ChatBrowser_LoggedIn( object sender, EventArgs e )
        {
            _chatBrowser.GotoChat();
            _modBrowser.StartLogin();
        }

        private void ChatBrowser_ChatMessageReceived( object sender, ChatMessageReceivedEventArgs e )
        {
            LOG.DebugInject( "ChatMessageReceived - {Message}", e.Message );
            this.UiBeginInvoke( (Action<ChatMessage>)AppendMessage, e.Message );
        }

        private void ChatBrowser_IsBrowserInitializedChanged( object sender, IsBrowserInitializedChangedEventArgs e )
        {
            _chatBrowser.StartLogin();
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

        private void PromptLogin( LoginPromptEventArgs e )
        {
            ShowMe();
            LoginDialog login = new LoginDialog( Settings.Default.The100Username, Settings.Default.The100Password );
            if( login.ShowDialog() == DialogResult.OK )
            {
                Settings.Default.The100Username = login.Username;
                Settings.Default.The100Password = login.Password;
                Settings.Default.Save();

                e.Username = Settings.Default.The100Username;
                e.Password = Settings.Default.The100Password;
            }
            else
            {
                e.CancelLogin = true;
            }
        }

        private void AppendMessage( ChatMessage m )
        {
            uiChat.AppendText( "{Username}: {Content}".Inject( m ) + Environment.NewLine );
            uiChat.SelectionStart = uiChat.TextLength;
            uiChat.ScrollToCaret();
        }
    }
}
