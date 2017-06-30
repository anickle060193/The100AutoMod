using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using The100AutoMod.Properties;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace The100AutoMod
{
    class The100ModBrowser : The100ChatBrowser
    {
        private static readonly ILog LOG = LogManager.GetLogger( typeof( The100ModBrowser ) );

        private static readonly String THE100_MOD_URL = "https://www.the100.io/groups/268/edit";
        private static readonly String THE100_MOD_AFTER_UPDATE_URL = "https://www.the100.io/groups/268";
        private static readonly String DELTA_COMPANY_633_HELPER_URL = "https://deltacompany633.com/?bot=true";
        private static readonly String DELTA_COMPANY_633_HELPER_XUR_URL = "https://deltacompany633.com/?bot=true&xur={xur}";
        private static readonly String DELTA_COMPANY_633_HELPER_RESULTS_HTML = "https://deltacompany633.com/formatter.html";

        private static readonly Regex CHAT_MENTION_UPDATE = new Regex( @"^\s*@D633_Automod\s+Update(?:\s+Xur:\s*(?<xur>[^\s].*?))?\s*$" );

        private readonly The100ModBoundObject _boundMod = new The100ModBoundObject();
        private readonly Timer _updateTimer = new Timer();

        private DateTime _lastUpdateCheck = DateTime.UtcNow;
        private bool _updating = false;
        private String _updateHtml = null;
        private bool _afterUpdate = false;

        public The100ModBrowser() : base( THE100_MOD_URL )
        {
            _boundMod.HomePageHtmlReceived += BoundMod_HomePageHtmlReceived;
            this.RegisterAsyncJsObject( "The100BoundMod", _boundMod );

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

            this.Load( THE100_MOD_URL );
        }

        protected override async void OnFrameLoadEnd( FrameLoadEndEventArgs e )
        {
            if( e.Url == THE100_MOD_URL )
            {
                if( _updating )
                {
                    String script = Resources.The100ModBrowser_SetHomePageHtml.Inject( new { homePageHtml = _updateHtml } );
                    this.ExecuteScriptAsync( script );

                    _lastUpdateCheck = DateTime.UtcNow;
                    _updating = false;
                    _updateHtml = null;
                    _afterUpdate = true;
                }
                else if( _afterUpdate )
                {
                    _afterUpdate = false;

                    await Task.Delay( 2000 );

                    this.SendChatMessage( "Update done." );
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

        private void BoundMod_HomePageHtmlReceived( object sender, HomePageHtmlReceivedEventArgs e )
        {
            _updateHtml = e.HomePageHtml;
            _updating = !String.IsNullOrEmpty( _updateHtml );

            this.Load( THE100_MOD_URL );
        }

        private void The100ModBrowser_ChatMessageReceived( object sender, ChatMessageReceivedEventArgs e )
        {
            Match m = CHAT_MENTION_UPDATE.Match( e.Message.Content );
            if( m.Success )
            {
                if( m.Groups[ "xur" ].Success )
                {
                    this.UpdateHomePage( m.Groups[ "xur" ].Value );
                }
                else
                {
                    this.UpdateHomePage();
                }
            }
        }

        private void UpdateTimer_Tick( object sender, EventArgs e )
        {
            if( !_updating )
            {
                DateTime utcNow = DateTime.UtcNow;
                if( GetResetTimes( _lastUpdateCheck ).Any( resetTime => resetTime > _lastUpdateCheck && resetTime <= utcNow ) )
                {
                    this.UpdateHomePage();
                }
                _lastUpdateCheck = utcNow;
            }
        }

        private void UpdateHomePage( String xurLocation = null )
        {
            if( !_updating )
            {
                _updating = true;

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

        private DateTime[] GetResetTimes( DateTime utcStart )
        {
            DateTime startOfWeek = utcStart.StartOfWeek( DayOfWeek.Sunday );

            DateTime weeklyReset = startOfWeek.Next( DayOfWeek.Tuesday, 9, 30 );
            DateTime trials = startOfWeek.Next( DayOfWeek.Friday, 17, 30 );
            DateTime xurArrival = startOfWeek.Next( DayOfWeek.Friday, 9, 30 );
            DateTime xurDeparture = startOfWeek.Next( DayOfWeek.Sunday, 9, 30 );

            return new[] { weeklyReset, trials, xurArrival, xurDeparture };
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
}
