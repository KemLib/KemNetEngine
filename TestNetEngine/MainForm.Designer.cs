namespace TestNetEngine
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip_1 = new MenuStrip();
            MenuItem_File = new ToolStripMenuItem();
            MenuItem_File_OpenExplore = new ToolStripMenuItem();
            toolStrip_File_Separator_1 = new ToolStripSeparator();
            MenuItem_File_Exit = new ToolStripMenuItem();
            MenuItem_Net = new ToolStripMenuItem();
            MenuItem_Net_Listen = new ToolStripMenuItem();
            MenuItem_Net_Connect = new ToolStripMenuItem();
            MenuItem_Net_Separator_1 = new ToolStripSeparator();
            MenuItem_Net_Server = new ToolStripMenuItem();
            menuStrip_1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip_1
            // 
            menuStrip_1.Items.AddRange(new ToolStripItem[] { MenuItem_File, MenuItem_Net });
            menuStrip_1.Location = new Point(0, 0);
            menuStrip_1.Name = "menuStrip_1";
            menuStrip_1.Size = new Size(1184, 24);
            menuStrip_1.TabIndex = 1;
            menuStrip_1.Text = "menuStrip_1";
            // 
            // MenuItem_File
            // 
            MenuItem_File.DropDownItems.AddRange(new ToolStripItem[] { MenuItem_File_OpenExplore, toolStrip_File_Separator_1, MenuItem_File_Exit });
            MenuItem_File.Name = "MenuItem_File";
            MenuItem_File.Size = new Size(37, 20);
            MenuItem_File.Text = "File";
            // 
            // MenuItem_File_OpenExplore
            // 
            MenuItem_File_OpenExplore.Name = "MenuItem_File_OpenExplore";
            MenuItem_File_OpenExplore.Size = new Size(144, 22);
            MenuItem_File_OpenExplore.Text = "Open Explore";
            MenuItem_File_OpenExplore.Click += MenuItemClick_OpenExplore;
            // 
            // toolStrip_File_Separator_1
            // 
            toolStrip_File_Separator_1.Name = "toolStrip_File_Separator_1";
            toolStrip_File_Separator_1.Size = new Size(141, 6);
            // 
            // MenuItem_File_Exit
            // 
            MenuItem_File_Exit.Name = "MenuItem_File_Exit";
            MenuItem_File_Exit.Size = new Size(144, 22);
            MenuItem_File_Exit.Text = "Exit";
            MenuItem_File_Exit.Click += MenuItemClick_Exit;
            // 
            // MenuItem_Net
            // 
            MenuItem_Net.DropDownItems.AddRange(new ToolStripItem[] { MenuItem_Net_Listen, MenuItem_Net_Connect, MenuItem_Net_Separator_1, MenuItem_Net_Server });
            MenuItem_Net.Name = "MenuItem_Net";
            MenuItem_Net.Size = new Size(38, 20);
            MenuItem_Net.Text = "Net";
            // 
            // MenuItem_Net_Listen
            // 
            MenuItem_Net_Listen.Name = "MenuItem_Net_Listen";
            MenuItem_Net_Listen.Size = new Size(180, 22);
            MenuItem_Net_Listen.Text = "Listen";
            MenuItem_Net_Listen.Click += MenuItem_Net_Listen_Click;
            // 
            // MenuItem_Net_Connect
            // 
            MenuItem_Net_Connect.Name = "MenuItem_Net_Connect";
            MenuItem_Net_Connect.Size = new Size(180, 22);
            MenuItem_Net_Connect.Text = "Connect";
            MenuItem_Net_Connect.Click += MenuItem_Net_Connect_Click;
            // 
            // MenuItem_Net_Separator_1
            // 
            MenuItem_Net_Separator_1.Name = "MenuItem_Net_Separator_1";
            MenuItem_Net_Separator_1.Size = new Size(177, 6);
            // 
            // MenuItem_Net_Server
            // 
            MenuItem_Net_Server.Name = "MenuItem_Net_Server";
            MenuItem_Net_Server.Size = new Size(180, 22);
            MenuItem_Net_Server.Text = "Server";
            MenuItem_Net_Server.Click += MenuItem_Net_Server_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 711);
            Controls.Add(menuStrip_1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip_1;
            MinimumSize = new Size(600, 400);
            Name = "MainForm";
            Text = "Test KNet";
            Shown += MainForm_Shown;
            menuStrip_1.ResumeLayout(false);
            menuStrip_1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip_1;
        private ToolStripMenuItem MenuItem_File;
        private ToolStripMenuItem MenuItem_File_OpenExplore;
        private ToolStripSeparator toolStrip_File_Separator_1;
        private ToolStripMenuItem MenuItem_File_Exit;
        private ToolStripMenuItem MenuItem_Net;
        private ToolStripMenuItem MenuItem_Net_Listen;
        private ToolStripMenuItem MenuItem_Net_Connect;
        private ToolStripSeparator MenuItem_Net_Separator_1;
        private ToolStripMenuItem MenuItem_Net_Server;
    }
}
