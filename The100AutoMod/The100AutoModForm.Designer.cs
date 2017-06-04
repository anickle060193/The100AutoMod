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
            this.uiMenuStrip = new System.Windows.Forms.MenuStrip();
            this.uiOptionsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.uiToggleDevToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.uiNotifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uiOpenNotifyIconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiExitNotifyIconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiSplitLayout = new System.Windows.Forms.SplitContainer();
            this.uiChat = new System.Windows.Forms.TextBox();
            this.uiMenuStrip.SuspendLayout();
            this.uiNotifyIconContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitLayout)).BeginInit();
            this.uiSplitLayout.Panel2.SuspendLayout();
            this.uiSplitLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiMenuStrip
            // 
            this.uiMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiOptionsMenu});
            this.uiMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.uiMenuStrip.Name = "uiMenuStrip";
            this.uiMenuStrip.Size = new System.Drawing.Size(984, 24);
            this.uiMenuStrip.TabIndex = 0;
            // 
            // uiOptionsMenu
            // 
            this.uiOptionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiToggleDevToolsMenuItem,
            this.uiExitMenuItem});
            this.uiOptionsMenu.Name = "uiOptionsMenu";
            this.uiOptionsMenu.Size = new System.Drawing.Size(61, 20);
            this.uiOptionsMenu.Text = "Options";
            // 
            // uiToggleDevToolsMenuItem
            // 
            this.uiToggleDevToolsMenuItem.Name = "uiToggleDevToolsMenuItem";
            this.uiToggleDevToolsMenuItem.Size = new System.Drawing.Size(164, 22);
            this.uiToggleDevToolsMenuItem.Text = "Toggle Dev Tools";
            // 
            // uiExitMenuItem
            // 
            this.uiExitMenuItem.Name = "uiExitMenuItem";
            this.uiExitMenuItem.Size = new System.Drawing.Size(164, 22);
            this.uiExitMenuItem.Text = "Exit";
            // 
            // uiNotifyIcon
            // 
            this.uiNotifyIcon.ContextMenuStrip = this.uiNotifyIconContextMenu;
            this.uiNotifyIcon.Text = "the100.io AutoMod";
            // 
            // uiNotifyIconContextMenu
            // 
            this.uiNotifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uiOpenNotifyIconMenuItem,
            this.uiExitNotifyIconMenuItem});
            this.uiNotifyIconContextMenu.Name = "_notifyIconContextMenu";
            this.uiNotifyIconContextMenu.Size = new System.Drawing.Size(104, 48);
            // 
            // uiOpenNotifyIconMenuItem
            // 
            this.uiOpenNotifyIconMenuItem.Name = "uiOpenNotifyIconMenuItem";
            this.uiOpenNotifyIconMenuItem.Size = new System.Drawing.Size(103, 22);
            this.uiOpenNotifyIconMenuItem.Text = "Open";
            // 
            // uiExitNotifyIconMenuItem
            // 
            this.uiExitNotifyIconMenuItem.Name = "uiExitNotifyIconMenuItem";
            this.uiExitNotifyIconMenuItem.Size = new System.Drawing.Size(103, 22);
            this.uiExitNotifyIconMenuItem.Text = "Exit";
            // 
            // uiSplitLayout
            // 
            this.uiSplitLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiSplitLayout.Location = new System.Drawing.Point(0, 24);
            this.uiSplitLayout.Name = "uiSplitLayout";
            // 
            // uiSplitLayout.Panel2
            // 
            this.uiSplitLayout.Panel2.Controls.Add(this.uiChat);
            this.uiSplitLayout.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.uiSplitLayout.Size = new System.Drawing.Size(984, 637);
            this.uiSplitLayout.SplitterDistance = 700;
            this.uiSplitLayout.TabIndex = 1;
            // 
            // uiChat
            // 
            this.uiChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiChat.Location = new System.Drawing.Point(3, 3);
            this.uiChat.Multiline = true;
            this.uiChat.Name = "uiChat";
            this.uiChat.ReadOnly = true;
            this.uiChat.Size = new System.Drawing.Size(274, 631);
            this.uiChat.TabIndex = 0;
            // 
            // The100AutoModForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.uiSplitLayout);
            this.Controls.Add(this.uiMenuStrip);
            this.MainMenuStrip = this.uiMenuStrip;
            this.Name = "The100AutoModForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "the100.io AutoMod";
            this.uiMenuStrip.ResumeLayout(false);
            this.uiMenuStrip.PerformLayout();
            this.uiNotifyIconContextMenu.ResumeLayout(false);
            this.uiSplitLayout.Panel2.ResumeLayout(false);
            this.uiSplitLayout.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitLayout)).EndInit();
            this.uiSplitLayout.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip uiMenuStrip;
        private System.Windows.Forms.NotifyIcon uiNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip uiNotifyIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem uiOpenNotifyIconMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uiExitNotifyIconMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uiOptionsMenu;
        private System.Windows.Forms.ToolStripMenuItem uiToggleDevToolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uiExitMenuItem;
        private System.Windows.Forms.SplitContainer uiSplitLayout;
        private System.Windows.Forms.TextBox uiChat;
    }
}

