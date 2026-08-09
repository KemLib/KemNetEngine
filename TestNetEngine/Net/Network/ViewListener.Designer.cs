namespace TestNetEngine.Net.Network
{
    partial class ViewListener
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
            control_list_log = new ControlListLog();
            button_stop = new Button();
            button_start = new Button();
            control_listen_setting = new TestNetEngine.ControlSetting.ControlListenSetting();
            control_socket_setting = new TestNetEngine.ControlSetting.ControlSocketSetting();
            SuspendLayout();
            // 
            // control_list_log
            // 
            control_list_log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            control_list_log.BackColor = SystemColors.ControlLight;
            control_list_log.Location = new Point(12, 12);
            control_list_log.MinimumSize = new Size(200, 200);
            control_list_log.Name = "control_list_log";
            control_list_log.Size = new Size(254, 487);
            control_list_log.TabIndex = 4;
            // 
            // button_stop
            // 
            button_stop.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button_stop.Location = new Point(447, 476);
            button_stop.Name = "button_stop";
            button_stop.Size = new Size(75, 23);
            button_stop.TabIndex = 3;
            button_stop.Text = "Stop";
            button_stop.UseVisualStyleBackColor = true;
            button_stop.Click += Button_Stop_Click;
            // 
            // button_start
            // 
            button_start.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button_start.Location = new Point(366, 476);
            button_start.Name = "button_start";
            button_start.Size = new Size(75, 23);
            button_start.TabIndex = 2;
            button_start.Text = "Start";
            button_start.UseVisualStyleBackColor = true;
            button_start.Click += Button_Start_Click;
            // 
            // control_listen_setting
            // 
            control_listen_setting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            control_listen_setting.BackColor = SystemColors.ControlLight;
            control_listen_setting.Location = new Point(272, 12);
            control_listen_setting.MinimumSize = new Size(250, 155);
            control_listen_setting.Name = "control_listen_setting";
            control_listen_setting.Size = new Size(250, 155);
            control_listen_setting.TabIndex = 0;
            // 
            // control_socket_setting
            // 
            control_socket_setting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            control_socket_setting.BackColor = SystemColors.ControlLight;
            control_socket_setting.Location = new Point(272, 173);
            control_socket_setting.MinimumSize = new Size(250, 180);
            control_socket_setting.Name = "control_socket_setting";
            control_socket_setting.Size = new Size(250, 180);
            control_socket_setting.TabIndex = 1;
            // 
            // ViewListener
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 511);
            Controls.Add(control_socket_setting);
            Controls.Add(control_listen_setting);
            Controls.Add(button_start);
            Controls.Add(button_stop);
            Controls.Add(control_list_log);
            MinimumSize = new Size(550, 550);
            Name = "ViewListener";
            Text = "Listener Net";
            FormClosing += ViewListener_FormClosing;
            Load += ClientConnectView_Load;
            ResumeLayout(false);
        }

        #endregion
        private ControlListLog control_list_log;
        private Button button_stop;
        private Button button_start;
        private ControlSetting.ControlListenSetting control_listen_setting;
        private ControlSetting.ControlSocketSetting control_socket_setting;
    }
}