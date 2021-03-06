﻿using CefSharp;
using CefSharp.WinForms;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The100AutoMod.Properties;

namespace The100AutoMod
{
    abstract class The100Browser : ChromiumWebBrowser, IThe100BrowserControl
    {
        private static readonly ILog LOG = LogManager.GetLogger( typeof( The100Browser ) );

        private static readonly String THE100_LOGIN_URL = "https://www.the100.io/login";
        private static readonly String THE100_AFTER_LOGIN_URL = "https://www.the100.io/users/264831/dashboard";

        public new event EventHandler<FrameLoadEndEventArgs> FrameLoadEnd;
        public event EventHandler<LoginPromptEventArgs> LoginPrompt;
        public event EventHandler LoggedIn;

        private bool _loginAttempted;

        public The100Browser() : base( "" )
        {
            base.FrameLoadEnd += The100Browser_FrameLoadEnd;

            this.MenuHandler = new The100BrowserContextMenuHandler();
        }

        private void The100Browser_FrameLoadEnd( object sender, FrameLoadEndEventArgs e )
        {
            LOG.DebugInject( "FrameLoadEnd - URL: {Url} - Status Code: {HttpStatusCode}", e );

            if( e.Url == THE100_LOGIN_URL )
            {
                LOG.Debug( "FrameLoadEnd - Login" );

                LoginPromptEventArgs loginPromptEventArgs = new LoginPromptEventArgs( _loginAttempted );
                OnLoginPrompt( loginPromptEventArgs );
                if( !loginPromptEventArgs.CancelLogin )
                {
                    SendLogin( loginPromptEventArgs.Username, loginPromptEventArgs.Password );
                    _loginAttempted = true;
                }
            }
            else if( e.Url == THE100_AFTER_LOGIN_URL )
            {
                LOG.Debug( "FrameLoadEnd - After Login" );

                OnLoggedIn( EventArgs.Empty );
            }
            else
            {
                OnFrameLoadEnd( e );
            }
        }

        protected virtual void OnFrameLoadEnd( FrameLoadEndEventArgs e )
        {
            FrameLoadEnd?.Invoke( this, e );
        }

        protected virtual void OnLoginPrompt( LoginPromptEventArgs e )
        {
            LoginPrompt?.Invoke( this, e );
        }

        protected virtual void OnLoggedIn( EventArgs e )
        {
            LoggedIn?.Invoke( this, e );
        }

        private void SendLogin( String username, String password )
        {
            var auth = new
            {
                username = username,
                password = password
            };
            this.ExecuteScriptAsync( Resources.The100Browser_LoginScript.Inject( auth ) );
        }

        public void StartLogin()
        {
            this.Load( THE100_LOGIN_URL );
        }
    }
}
