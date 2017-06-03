namespace The100AutoMod
{
    partial class The100AutoModForm
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
            this.components = new System.ComponentModel.Container();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this._notifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._openNotifyIconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exitNotifyIconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._optionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._toggleDevToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuStrip.SuspendLayout();
            this._notifyIconContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._optionsMenu});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(984, 24);
            this._menuStrip.TabIndex = 0;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _notifyIcon
            // 
            this._notifyIcon.ContextMenuStrip = this._notifyIconContextMenu;
            this._notifyIcon.Text = "the100.io AutoMod";
            // 
            // _notifyIconContextMenu
            // 
            this._notifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openNotifyIconMenuItem,
            this._exitNotifyIconMenuItem});
            this._notifyIconContextMenu.Name = "_notifyIconContextMenu";
            this._notifyIconContextMenu.Size = new System.Drawing.Size(104, 48);
            // 
            // _openNotifyIconMenuItem
            // 
            this._openNotifyIconMenuItem.Name = "_openNotifyIconMenuItem";
            this._openNotifyIconMenuItem.Size = new System.Drawing.Size(103, 22);
            this._openNotifyIconMenuItem.Text = "Open";
            // 
            // _exitNotifyIconMenuItem
            // 
            this._exitNotifyIconMenuItem.Name = "_exitNotifyIconMenuItem";
            this._exitNotifyIconMenuItem.Size = new System.Drawing.Size(103, 22);
            this._exitNotifyIconMenuItem.Text = "Exit";
            // 
            // _optionsMenu
            // 
            this._optionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toggleDevToolsMenuItem,
            this._exitMenuItem});
            this._optionsMenu.Name = "_optionsMenu";
            this._optionsMenu.Size = new System.Drawing.Size(61, 20);
            this._optionsMenu.Text = "Options";
            // 
            // _toggleDevToolsMenuItem
            // 
            this._toggleDevToolsMenuItem.Name = "_toggleDevToolsMenuItem";
            this._toggleDevToolsMenuItem.Size = new System.Drawing.Size(164, 22);
            this._toggleDevToolsMenuItem.Text = "Toggle Dev Tools";
            // 
            // _exitMenuItem
            // 
            this._exitMenuItem.Name = "_exitMenuItem";
            this._exitMenuItem.Size = new System.Drawing.Size(164, 22);
            this._exitMenuItem.Text = "Exit";
            // 
            // The100AutoModForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this._menuStrip);
            this.MainMenuStrip = this._menuStrip;
            this.Name = "The100AutoModForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "the100.io AutoMod";
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this._notifyIconContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private System.Windows.Forms.ContextMenuStrip _notifyIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem _openNotifyIconMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _exitNotifyIconMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _optionsMenu;
        private System.Windows.Forms.ToolStripMenuItem _toggleDevToolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _exitMenuItem;
    }
}

