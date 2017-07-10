using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using System.Text.RegularExpressions;
using log4net;
using The100AutoMod.Properties;

namespace The100AutoMod
{
    public partial class ModBrowserControl : UserControl, IThe100BrowserControl
    {
        private static readonly String THE100_MOD_URL = "https://www.the100.io/groups/268/edit";

        private The100ModChatBrowser ModChatBrowser { get; set; }
        private The100ModUtilBrowser ModUtilBrowser { get; set; }

        public event EventHandler LoggedIn;
        public event EventHandler<LoginPromptEventArgs> LoginPrompt;

        public ModBrowserControl()
        {
            InitializeComponent();

            ModChatBrowser = new The100ModChatBrowser( this );
            ModChatBrowser.Dock = DockStyle.Fill;
            ModChatBrowser.LoginPrompt += ( sender, e ) => this.OnLoginPrompt( e );
            ModChatBrowser.LoggedIn += ( sender, e ) => this.OnLoggedIn( e );
            uiSplitContainer.Panel1.Controls.Add( ModChatBrowser );

            ModUtilBrowser = new The100ModUtilBrowser( this );
            ModUtilBrowser.Dock = DockStyle.Fill;
            ModUtilBrowser.LoginPrompt += ( sender, e ) => this.OnLoginPrompt( e );
            ModUtilBrowser.LoggedIn += ( sender, e ) => ModChatBrowser.StartLogin();
            uiSplitContainer.Panel2.Controls.Add( ModUtilBrowser );

            this.Disposed += ModBrowserControl_Disposed;
        }

        public void StartLogin()
        {
            ModUtilBrowser.StartLogin();
        }

        private void ModBrowserControl_Disposed( object sender, EventArgs e )
        {
            ModChatBrowser.Dispose();
            ModUtilBrowser.Dispose();
        }

        protected virtual void OnLoggedIn( EventArgs e )
        {
            LoggedIn?.Invoke( this, e );
        }

        protected virtual void OnLoginPrompt( LoginPromptEventArgs e )
        {
            LoginPrompt?.Invoke( this, e );
        }

        class The100ModUtilBrowser : The100ChatBrowser
        {
            private static readonly ILog LOG = LogManager.GetLogger( typeof( The100ModUtilBrowser ) );

            private static readonly String THE100_MOD_AFTER_UPDATE_URL = "https://www.the100.io/groups/268";
            private static readonly String DELTA_COMPANY_633_HELPER_URL = "https://deltacompany633.com/?bot=true";
            private static readonly String DELTA_COMPANY_633_HELPER_XUR_URL = "https://deltacompany633.com/?bot=true&xur={xur}";
            private static readonly String DELTA_COMPANY_633_HELPER_RESULTS_HTML = "https://deltacompany633.com/formatter.html";

            private readonly ModBrowserControl _owner;
            private readonly The100ModBoundObject _boundMod = new The100ModBoundObject();

            private String _updateHtml = null;
            private bool _afterUpdate = false;

            internal event EventHandler HomePageUpdated;

            internal bool Updating { get; private set; }

            public The100ModUtilBrowser( ModBrowserControl owner ) : base( THE100_MOD_URL )
            {
                _owner = owner;

                _boundMod.HomePageHtmlReceived += BoundMod_HomePageHtmlReceived;
                this.RegisterAsyncJsObject( "The100BoundMod", _boundMod );
            }

            private void BoundMod_HomePageHtmlReceived( object sender, HomePageHtmlReceivedEventArgs e )
            {
                _updateHtml = e.HomePageHtml;
                Updating = !String.IsNullOrEmpty( _updateHtml );

                this.Load( THE100_MOD_URL );
            }

            internal void UpdateHomePage( String xurLocation = null )
            {
                if( !Updating )
                {
                    Updating = true;

                    this.SendChatMessage( "Starting update..." );

                    if( String.IsNullOrWhiteSpace( xurLocation ) )
                    {
                        this.Load( DELTA_COMPANY_633_HELPER_URL );
                    }
                    else
                    {
                        this.Load( DELTA_COMPANY_633_HELPER_XUR_URL.Inject( new { xur = xurLocation } ) );
                    }
                }
            }

            protected virtual void OnHomePageUpdated( EventArgs e )
            {
                HomePageUpdated?.Invoke( this, e );
            }

            protected override async void OnFrameLoadEnd( FrameLoadEndEventArgs e )
            {
                if( e.Url == THE100_MOD_URL )
                {
                    if( Updating )
                    {
                        String script = Resources.The100ModBrowser_SetHomePageHtml.Inject( new { homePageHtml = _updateHtml } );
                        this.ExecuteScriptAsync( script );

                        Updating = false;
                        _updateHtml = null;
                        _afterUpdate = true;
                    }
                    else if( _afterUpdate )
                    {
                        _afterUpdate = false;

                        await Task.Delay( 2000 );

                        this.SendChatMessage( "Update done" );
                    }
                }
                else if( e.Url == THE100_MOD_AFTER_UPDATE_URL )
                {
                    if( _afterUpdate )
                    {
                        this.Load( THE100_MOD_URL );
                    }
                }
                else if( e.Url == DELTA_COMPANY_633_HELPER_URL )
                {
                    LOG.Debug( "FrameLoadEnd - D633 Helper" );
                }
                else if( e.Url.StartsWith( DELTA_COMPANY_633_HELPER_RESULTS_HTML ) )
                {
                    LOG.Debug( "FrameLoadEnd - D633 Helper Results" );

                    this.ExecuteScriptAsync( Resources.The100ModBrowser_GetHomePageHtml );
                }

                base.OnFrameLoadEnd( e );
            }

            private class HomePageHtmlReceivedEventArgs : EventArgs
            {
                public String HomePageHtml { get; private set; }

                public HomePageHtmlReceivedEventArgs( String homePageHtml )
                {
                    HomePageHtml = homePageHtml;
                }
            }

            private class The100ModBoundObject
            {
                public event EventHandler<HomePageHtmlReceivedEventArgs> HomePageHtmlReceived;

                public void OnHomePageHtmlReceived( String homePageHtml )
                {
                    HomePageHtmlReceived?.Invoke( this, new HomePageHtmlReceivedEventArgs( homePageHtml ) );
                }
            }
        }

        class The100ModChatBrowser : The100ChatBrowser
        {
            private static readonly ILog LOG = LogManager.GetLogger( typeof( The100ModChatBrowser ) );

            private static readonly Regex CHAT_MENTION_UPDATE = new Regex( @"^\s*@D633_Automod\s+Update(?:\s+Xur:\s*(?<xur>[^\s].*?))?\s*$" );

            private readonly ModBrowserControl _owner;
            private readonly Timer _updateTimer = new Timer();

            private DateTime _lastUpdateCheck = DateTime.UtcNow;
            private bool _loadedModChat = false;
            private bool _loggedIn = false;

            public The100ModChatBrowser( ModBrowserControl owner ) : base( THE100_MOD_URL )
            {
                _owner = owner;

                this.ChatMessageReceived += The100ModBrowser_ChatMessageReceived;

                _updateTimer.Tick += UpdateTimer_Tick;
                _updateTimer.Interval = (int)TimeSpan.FromMinutes( 1 ).TotalMilliseconds;
                _updateTimer.Start();
            }

            protected override void Dispose( bool disposing )
            {
                base.Dispose( disposing );

                if( disposing )
                {
                    _updateTimer.Dispose();
                }
            }

            protected override void OnLoggedIn( EventArgs e )
            {
                base.OnLoggedIn( e );

                if( _loggedIn && !_loadedModChat )
                {
                    this.UiBeginInvoke( (Action)( () =>
                    {
                        MessageBox.Show( "Failed to load Mod Chat. Verify still a mod." );
                        Application.Exit();
                    } ) );
                    return;
                }

                _loggedIn = true;

                this.Load( THE100_MOD_URL );

                _owner.ModUtilBrowser.HomePageUpdated += ModUtilBrowser_HomePageUpdated;
            }

            protected override void OnFrameLoadEnd( FrameLoadEndEventArgs e )
            {
                if( e.Url == THE100_MOD_URL )
                {
                    LOG.DebugFormat( "OnFrameLoaded - {0}", e.Url );

                    _loadedModChat = true;
                }

                base.OnFrameLoadEnd( e );
            }

            private void The100ModBrowser_ChatMessageReceived( object sender, ChatMessageReceivedEventArgs e )
            {
                Match m = CHAT_MENTION_UPDATE.Match( e.Message.Content );
                if( m.Success )
                {
                    if( m.Groups[ "xur" ].Success )
                    {
                        _owner.ModUtilBrowser.UpdateHomePage( m.Groups[ "xur" ].Value );
                    }
                    else
                    {
                        _owner.ModUtilBrowser.UpdateHomePage();
                    }
                }
            }

            private void UpdateTimer_Tick( object sender, EventArgs e )
            {
                if( !_owner.ModUtilBrowser.Updating )
                {
                    DateTime utcNow = DateTime.UtcNow;
                    if( GetResetTimes( _lastUpdateCheck ).Any( resetTime => resetTime > _lastUpdateCheck && resetTime <= utcNow ) )
                    {
                        _owner.ModUtilBrowser.UpdateHomePage();
                    }
                    _lastUpdateCheck = utcNow;
                }
            }

            private void ModUtilBrowser_HomePageUpdated( object sender, EventArgs e )
            {
                _lastUpdateCheck = DateTime.UtcNow;
            }

            private DateTime[] GetResetTimes( DateTime utcStart )
            {
                DateTime startOfWeek = utcStart.StartOfWeek( DayOfWeek.Sunday );

                DateTime weeklyReset = startOfWeek.Next( DayOfWeek.Tuesday, 9, 30 );
                DateTime trials = startOfWeek.Next( DayOfWeek.Friday, 17, 30 );
                DateTime xurArrival = startOfWeek.Next( DayOfWeek.Friday, 9, 30 );
                DateTime xurDeparture = startOfWeek.Next( DayOfWeek.Sunday, 9, 30 );

                return new[] { weeklyReset, trials, xurArrival, xurDeparture };
            }
        }
    }
}
