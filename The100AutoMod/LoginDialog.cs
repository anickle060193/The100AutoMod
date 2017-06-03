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
                return _usernameText.Text;
            }

            private set
            {
                _usernameText.Text = value;
            }
        }

        public String Password
        {
            get
            {
                return _passwordText.Text;
            }

            private set
            {
                _passwordText.Text = value;
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
