namespace TestNetEngine.Net.Network
{
    partial class ViewConnecter
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
            control_client_setting = new TestNetEngine.ControlSetting.ControlConnecterSetting();
            control_list_log = new ControlListLog();
            button_cancel = new Button();
            button_connect = new Button();
            control_socket_setting = new TestNetEngine.ControlSetting.ControlSocketSetting();
            SuspendLayout();
            // 
            // control_client_setting
            // 
            control_client_setting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            control_client_setting.BackColor = SystemColors.ControlLight;
            control_client_setting.Location = new Point(272, 12);
            control_client_setting.MinimumSize = new Size(250, 250);
            control_client_setting.Name = "control_client_setting";
            control_client_setting.Size = new Size(250, 250);
            control_client_setting.TabIndex = 0;
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
            // button_cancel
            // 
            button_cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button_cancel.Location = new Point(447, 476);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(75, 23);
            button_cancel.TabIndex = 3;
            button_cancel.Text = "Cancel";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += Button_Cancel_Click;
            // 
            // button_connect
            // 
            button_connect.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button_connect.Location = new Point(366, 476);
            button_connect.Name = "button_connect";
            button_connect.Size = new Size(75, 23);
            button_connect.TabIndex = 2;
            button_connect.Text = "Connect";
            button_connect.UseVisualStyleBackColor = true;
            button_connect.Click += Button_Connect_Click;
            // 
            // control_socket_setting
            // 
            control_socket_setting.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            control_socket_setting.BackColor = SystemColors.ControlLight;
            control_socket_setting.Location = new Point(272, 268);
            control_socket_setting.MinimumSize = new Size(250, 180);
            control_socket_setting.Name = "control_socket_setting";
            control_socket_setting.Size = new Size(250, 180);
            control_socket_setting.TabIndex = 1;
            // 
            // ViewConnecter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 511);
            Controls.Add(control_socket_setting);
            Controls.Add(button_connect);
            Controls.Add(button_cancel);
            Controls.Add(control_list_log);
            Controls.Add(control_client_setting);
            MinimumSize = new Size(550, 550);
            Name = "ViewConnecter";
            Text = "Connecter Net";
            FormClosing += ViewConnecter_FormClosing;
            Load += ClientConnectView_Load;
            ResumeLayout(false);
        }

        #endregion

        private ControlSetting.ControlConnecterSetting control_client_setting;
        private ControlListLog control_list_log;
        private Button button_cancel;
        private Button button_connect;
        private ControlSetting.ControlSocketSetting control_socket_setting;
    }
}