using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The100AutoMod
{
    public partial class LoginDialog : Form
    {
        public String Username
        {
            get
            {
                return uiUsernameText.Text;
            }

            private set
            {
                uiUsernameText.Text = value;
            }
        }

        public String Password
        {
            get
            {
                return uiPasswordText.Text;
            }

            private set
            {
                uiPasswordText.Text = value;
            }
        }

        public LoginDialog( String username, String password )
        {
            InitializeComponent();

            Username = username;
            Password = password;
        }
    }
}
