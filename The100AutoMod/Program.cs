using CefSharp;
using log4net;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;

namespace The100AutoMod
{
    static class Program
    {
        private static readonly ILog LOG = LogManager.GetLogger( typeof( Program ) );

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LOG.Debug( "Application Start" );

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;

            CefSharpSettings.ShutdownOnExit = false;

            CefSettings settings = new CefSettings()
            {
                BrowserSubprocessPath = @"x86\CefSharp.BrowserSubprocess.exe"
            };
            Cef.Initialize( settings, true, null );

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new The100AutoModForm() );

            Cef.Shutdown();

            LOG.Debug( "Application End" );
        }

        private static void Application_ThreadException( object sender, ThreadExceptionEventArgs e )
        {
            LOG.Error( "Application_ThreadException", e.Exception );
        }

        private static void CurrentDomain_FirstChanceException( object sender, FirstChanceExceptionEventArgs e )
        {
            LOG.Error( "CurrentDomain_FirstChangeException", e.Exception );
        }
    }
}
