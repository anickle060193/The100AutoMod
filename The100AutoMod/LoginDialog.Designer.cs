namespace The100AutoMod
{
    partial class LoginDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiUsernameLabel = new System.Windows.Forms.Label();
            this.uiPasswordLabel = new System.Windows.Forms.Label();
            this.uiUsernameText = new System.Windows.Forms.TextBox();
            this.uiPasswordText = new System.Windows.Forms.TextBox();
            this.uiCancelButton = new System.Windows.Forms.Button();
            this.uiOkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uiUsernameLabel
            // 
            this.uiUsernameLabel.AutoSize = true;
            this.uiUsernameLabel.Location = new System.Drawing.Point(12, 15);
            this.uiUsernameLabel.Name = "uiUsernameLabel";
            this.uiUsernameLabel.Size = new System.Drawing.Size(58, 13);
            this.uiUsernameLabel.TabIndex = 0;
            this.uiUsernameLabel.Text = "Username:";
            // 
            // uiPasswordLabel
            // 
            this.uiPasswordLabel.AutoSize = true;
            this.uiPasswordLabel.Location = new System.Drawing.Point(12, 41);
            this.uiPasswordLabel.Name = "uiPasswordLabel";
            this.uiPasswordLabel.Size = new System.Drawing.Size(56, 13);
            this.uiPasswordLabel.TabIndex = 1;
            this.uiPasswordLabel.Text = "Password:";
            // 
            // uiUsernameText
            // 
            this.uiUsernameText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiUsernameText.Location = new System.Drawing.Point(76, 12);
            this.uiUsernameText.Name = "uiUsernameText";
            this.uiUsernameText.Size = new System.Drawing.Size(254, 20);
            this.uiUsernameText.TabIndex = 2;
            // 
            // uiPasswordText
            // 
            this.uiPasswordText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiPasswordText.Location = new System.Drawing.Point(76, 38);
            this.uiPasswordText.Name = "uiPasswordText";
            this.uiPasswordText.Size = new System.Drawing.Size(254, 20);
            this.uiPasswordText.TabIndex = 3;
            this.uiPasswordText.UseSystemPasswordChar = true;
            // 
            // uiCancelButton
            // 
            this.uiCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancelButton.Location = new System.Drawing.Point(255, 64);
            this.uiCancelButton.Name = "uiCancelButton";
            this.uiCancelButton.Size = new System.Drawing.Size(75, 23);
            this.uiCancelButton.TabIndex = 4;
            this.uiCancelButton.Text = "Cancel";
            this.uiCancelButton.UseVisualStyleBackColor = true;
            // 
            // uiOkButton
            // 
            this.uiOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uiOkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uiOkButton.Location = new System.Drawing.Point(174, 64);
            this.uiOkButton.Name = "uiOkButton";
            this.uiOkButton.Size = new System.Drawing.Size(75, 23);
            this.uiOkButton.TabIndex = 5;
            this.uiOkButton.Text = "OK";
            this.uiOkButton.UseVisualStyleBackColor = true;
            // 
            // LoginDialog
            // 
            this.AcceptButton = this.uiOkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uiCancelButton;
            this.ClientSize = new System.Drawing.Size(342, 94);
            this.ControlBox = false;
            this.Controls.Add(this.uiOkButton);
            this.Controls.Add(this.uiCancelButton);
            this.Controls.Add(this.uiPasswordText);
            this.Controls.Add(this.uiUsernameText);
            this.Controls.Add(this.uiPasswordLabel);
            this.Controls.Add(this.uiUsernameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LoginDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label uiUsernameLabel;
        private System.Windows.Forms.Label uiPasswordLabel;
        private System.Windows.Forms.TextBox uiUsernameText;
        private System.Windows.Forms.TextBox uiPasswordText;
        private System.Windows.Forms.Button uiCancelButton;
        private System.Windows.Forms.Button uiOkButton;
    }
}