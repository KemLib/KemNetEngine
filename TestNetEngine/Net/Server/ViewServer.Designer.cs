namespace TestNetEngine.Net.Server
{
    partial class ViewServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            control_server_setting = new TestNetEngine.ControlSetting.ControlServerSetting();
            button_start = new Button();
            button_stop = new Button();
            control_list_log = new ControlListLog();
            timer_log_update = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // control_server_setting
            // 
            control_server_setting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            control_server_setting.BackColor = SystemColors.ControlLight;
            control_server_setting.Location = new Point(272, 12);
            control_server_setting.MinimumSize = new Size(250, 510);
            control_server_setting.Name = "control_server_setting";
            control_server_setting.Size = new Size(250, 510);
            control_server_setting.TabIndex = 0;
            // 
            // button_start
            // 
            button_start.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button_start.Location = new Point(366, 536);
            button_start.Name = "button_start";
            button_start.Size = new Size(75, 23);
            button_start.TabIndex = 1;
            button_start.Text = "Start";
            button_start.UseVisualStyleBackColor = true;
            button_start.Click += Button_Start_Click;
            // 
            // button_stop
            // 
            button_stop.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button_stop.Location = new Point(447, 536);
            button_stop.Name = "button_stop";
            button_stop.Size = new Size(75, 23);
            button_stop.TabIndex = 2;
            button_stop.Text = "Stop";
            button_stop.UseVisualStyleBackColor = true;
            button_stop.Click += Button_Stop_Click;
            // 
            // control_list_log
            // 
            control_list_log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            control_list_log.Location = new Point(12, 12);
            control_list_log.MinimumSize = new Size(200, 200);
            control_list_log.Name = "control_list_log";
            control_list_log.Size = new Size(254, 547);
            control_list_log.TabIndex = 3;
            // 
            // timer_log_update
            // 
            timer_log_update.Interval = 50;
            timer_log_update.Tick += Timer_Log_Update_Tick;
            // 
            // ViewServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 571);
            Controls.Add(control_list_log);
            Controls.Add(button_start);
            Controls.Add(button_stop);
            Controls.Add(control_server_setting);
            MinimumSize = new Size(550, 610);
            Name = "ViewServer";
            Text = "Server Net";
            FormClosing += ViewServer_FormClosing;
            Load += ViewServer_Load;
            ResumeLayout(false);
        }

        #endregion

        private ControlSetting.ControlServerSetting control_server_setting;
        private Button button_start;
        private Button button_stop;
        private ControlListLog control_list_log;
        private System.Windows.Forms.Timer timer_log_update;
    }
}