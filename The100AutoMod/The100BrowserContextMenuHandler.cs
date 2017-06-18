using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The100AutoMod
{
    class The100BrowserContextMenuHandler : IContextMenuHandler
    {
        enum CustomCefMenuCommand
        {
            _First = CefMenuCommand.UserFirst,
            ShowDevTools,
            CloseDevTools
        }

        public void OnBeforeContextMenu( IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model )
        {
            if( model.Count > 0 )
            {
                model.AddSeparator();
            }

            if( browser.GetHost().HasDevTools )
            {
                model.AddItem( (CefMenuCommand)CustomCefMenuCommand.CloseDevTools, "Close DevTools" );
            }
            else
            {
                model.AddItem( (CefMenuCommand)CustomCefMenuCommand.ShowDevTools, "Show DevTools" );
            }
        }

        public bool OnContextMenuCommand( IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags )
        {
            switch( (CustomCefMenuCommand)commandId )
            {
                case CustomCefMenuCommand.ShowDevTools:
                    browser.ShowDevTools();
                    return true;

                case CustomCefMenuCommand.CloseDevTools:
                    browser.CloseDevTools();
                    return true;

                default:
                    return false;
            }
        }

        public void OnContextMenuDismissed( IWebBrowser browserControl, IBrowser browser, IFrame frame )
        {
        }

        public bool RunContextMenu( IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback )
        {
            return false;
        }
    }
}
