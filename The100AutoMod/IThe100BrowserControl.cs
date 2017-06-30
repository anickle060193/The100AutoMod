using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The100AutoMod
{
    public interface IThe100BrowserControl
    {
        event EventHandler<LoginPromptEventArgs> LoginPrompt;
        event EventHandler LoggedIn;

        void StartLogin();
    }

    public class LoginPromptEventArgs : EventArgs
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public bool CancelLogin { get; set; }
        public bool RePrompt { get; private set; }

        public LoginPromptEventArgs( bool rePrompt )
        {
            Username = "";
            Password = "";
            CancelLogin = false;
            RePrompt = rePrompt;
        }
    }
}
