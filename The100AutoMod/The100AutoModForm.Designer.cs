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
            this.uiNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.uiNotifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uiOpenNotifyIconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiExitNotifyIconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiSplitLayout = new System.Windows.Forms.SplitContainer();
            this.uiBrowserTabs = new System.Windows.Forms.TabControl();
            this.uiChatTab = new System.Windows.Forms.TabPage();
            this.uiModTab = new System.Windows.Forms.TabPage();
            this.uiChat = new System.Windows.Forms.TextBox();
            this.uiNotifyIconContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitLayout)).BeginInit();
            this.uiSplitLayout.Panel1.SuspendLayout();
            this.uiSplitLayout.Panel2.SuspendLayout();
            this.uiSplitLayout.SuspendLayout();
            this.uiBrowserTabs.SuspendLayout();
            this.SuspendLayout();
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
            this.uiSplitLayout.Location = new System.Drawing.Point(0, 0);
            this.uiSplitLayout.Name = "uiSplitLayout";
            // 
            // uiSplitLayout.Panel1
            // 
            this.uiSplitLayout.Panel1.Controls.Add(this.uiBrowserTabs);
            // 
            // uiSplitLayout.Panel2
            // 
            this.uiSplitLayout.Panel2.Controls.Add(this.uiChat);
            this.uiSplitLayout.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.uiSplitLayout.Size = new System.Drawing.Size(984, 661);
            this.uiSplitLayout.SplitterDistance = 700;
            this.uiSplitLayout.TabIndex = 1;
            // 
            // uiBrowserTabs
            // 
            this.uiBrowserTabs.Controls.Add(this.uiChatTab);
            this.uiBrowserTabs.Controls.Add(this.uiModTab);
            this.uiBrowserTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiBrowserTabs.Location = new System.Drawing.Point(0, 0);
            this.uiBrowserTabs.Name = "uiBrowserTabs";
            this.uiBrowserTabs.SelectedIndex = 0;
            this.uiBrowserTabs.Size = new System.Drawing.Size(700, 661);
            this.uiBrowserTabs.TabIndex = 0;
            // 
            // uiChatTab
            // 
            this.uiChatTab.Location = new System.Drawing.Point(4, 22);
            this.uiChatTab.Name = "uiChatTab";
            this.uiChatTab.Padding = new System.Windows.Forms.Padding(3);
            this.uiChatTab.Size = new System.Drawing.Size(692, 635);
            this.uiChatTab.TabIndex = 0;
            this.uiChatTab.Text = "Chat";
            this.uiChatTab.UseVisualStyleBackColor = true;
            // 
            // uiModTab
            // 
            this.uiModTab.Location = new System.Drawing.Point(4, 22);
            this.uiModTab.Name = "uiModTab";
            this.uiModTab.Padding = new System.Windows.Forms.Padding(3);
            this.uiModTab.Size = new System.Drawing.Size(692, 611);
            this.uiModTab.TabIndex = 1;
            this.uiModTab.Text = "Mod";
            this.uiModTab.UseVisualStyleBackColor = true;
            // 
            // uiChat
            // 
            this.uiChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiChat.Location = new System.Drawing.Point(3, 3);
            this.uiChat.Multiline = true;
            this.uiChat.Name = "uiChat";
            this.uiChat.ReadOnly = true;
            this.uiChat.Size = new System.Drawing.Size(274, 655);
            this.uiChat.TabIndex = 0;
            // 
            // The100AutoModForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.uiSplitLayout);
            this.Name = "The100AutoModForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "the100.io AutoMod";
            this.uiNotifyIconContextMenu.ResumeLayout(false);
            this.uiSplitLayout.Panel1.ResumeLayout(false);
            this.uiSplitLayout.Panel2.ResumeLayout(false);
            this.uiSplitLayout.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitLayout)).EndInit();
            this.uiSplitLayout.ResumeLayout(false);
            this.uiBrowserTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon uiNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip uiNotifyIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem uiOpenNotifyIconMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uiExitNotifyIconMenuItem;
        private System.Windows.Forms.SplitContainer uiSplitLayout;
        private System.Windows.Forms.TextBox uiChat;
        private System.Windows.Forms.TabControl uiBrowserTabs;
        private System.Windows.Forms.TabPage uiChatTab;
        private System.Windows.Forms.TabPage uiModTab;
    }
}

