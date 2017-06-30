namespace The100AutoMod
{
    partial class ModBrowserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiBrowserTabControl = new System.Windows.Forms.TabControl();
            this.uiModBrowserTab = new System.Windows.Forms.TabPage();
            this.uiBrowserTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiBrowserTabControl
            // 
            this.uiBrowserTabControl.Controls.Add(this.uiModBrowserTab);
            this.uiBrowserTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiBrowserTabControl.Location = new System.Drawing.Point(0, 0);
            this.uiBrowserTabControl.Name = "uiBrowserTabControl";
            this.uiBrowserTabControl.SelectedIndex = 0;
            this.uiBrowserTabControl.Size = new System.Drawing.Size(400, 400);
            this.uiBrowserTabControl.TabIndex = 0;
            // 
            // uiModBrowserTab
            // 
            this.uiModBrowserTab.Location = new System.Drawing.Point(4, 22);
            this.uiModBrowserTab.Name = "uiModBrowserTab";
            this.uiModBrowserTab.Padding = new System.Windows.Forms.Padding(3);
            this.uiModBrowserTab.Size = new System.Drawing.Size(392, 374);
            this.uiModBrowserTab.TabIndex = 0;
            this.uiModBrowserTab.Text = "Mod Browser";
            this.uiModBrowserTab.UseVisualStyleBackColor = true;
            // 
            // ModBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uiBrowserTabControl);
            this.Name = "ModBrowserControl";
            this.Size = new System.Drawing.Size(400, 400);
            this.uiBrowserTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl uiBrowserTabControl;
        private System.Windows.Forms.TabPage uiModBrowserTab;
    }
}
