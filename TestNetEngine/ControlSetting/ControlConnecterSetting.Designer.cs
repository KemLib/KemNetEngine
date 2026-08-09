namespace TestNetEngine.ControlSetting
{
    partial class ControlConnecterSetting
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            control_local_ip_end_point = new ControlIpEndPoint();
            checkbox_local_end_Point = new CheckBox();
            combobox_remote_end_point = new ComboBox();
            label_send_buffer_size = new Label();
            label_local_end_point = new Label();
            control_remote_ip_end_point = new ControlIpEndPoint();
            control_remote_dns_end_point = new ControlDnsEndPoint();
            SuspendLayout();
            // 
            // control_local_ip_end_point
            // 
            control_local_ip_end_point.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            control_local_ip_end_point.Enabled = false;
            control_local_ip_end_point.Location = new Point(0, 35);
            control_local_ip_end_point.MinimumSize = new Size(250, 90);
            control_local_ip_end_point.Name = "control_local_ip_end_point";
            control_local_ip_end_point.Size = new Size(300, 90);
            control_local_ip_end_point.TabIndex = 2;
            // 
            // checkbox_local_end_Point
            // 
            checkbox_local_end_Point.AutoSize = true;
            checkbox_local_end_Point.Location = new Point(122, 12);
            checkbox_local_end_Point.Name = "checkbox_local_end_Point";
            checkbox_local_end_Point.Size = new Size(61, 19);
            checkbox_local_end_Point.TabIndex = 1;
            checkbox_local_end_Point.Text = "Enable";
            checkbox_local_end_Point.UseVisualStyleBackColor = true;
            checkbox_local_end_Point.CheckedChanged += Checkbox_Local_End_Point_CheckedChanged;
            // 
            // combobox_remote_end_point
            // 
            combobox_remote_end_point.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            combobox_remote_end_point.DropDownStyle = ComboBoxStyle.DropDownList;
            combobox_remote_end_point.FlatStyle = FlatStyle.System;
            combobox_remote_end_point.FormattingEnabled = true;
            combobox_remote_end_point.Items.AddRange(new object[] { "Ip", "Dns" });
            combobox_remote_end_point.Location = new Point(122, 131);
            combobox_remote_end_point.Name = "combobox_remote_end_point";
            combobox_remote_end_point.Size = new Size(175, 23);
            combobox_remote_end_point.TabIndex = 4;
            combobox_remote_end_point.SelectedIndexChanged += Combobox_Remote_End_Point_SelectedIndexChanged;
            // 
            // label_send_buffer_size
            // 
            label_send_buffer_size.Location = new Point(3, 131);
            label_send_buffer_size.Name = "label_send_buffer_size";
            label_send_buffer_size.Size = new Size(113, 23);
            label_send_buffer_size.TabIndex = 3;
            label_send_buffer_size.Text = "Remote EndPoint";
            label_send_buffer_size.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_local_end_point
            // 
            label_local_end_point.Location = new Point(3, 9);
            label_local_end_point.Name = "label_local_end_point";
            label_local_end_point.Size = new Size(113, 23);
            label_local_end_point.TabIndex = 0;
            label_local_end_point.Text = "Local EndPoint";
            label_local_end_point.TextAlign = ContentAlignment.MiddleRight;
            // 
            // control_remote_ip_end_point
            // 
            control_remote_ip_end_point.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            control_remote_ip_end_point.Location = new Point(0, 160);
            control_remote_ip_end_point.MinimumSize = new Size(250, 90);
            control_remote_ip_end_point.Name = "control_remote_ip_end_point";
            control_remote_ip_end_point.Size = new Size(300, 90);
            control_remote_ip_end_point.TabIndex = 5;
            // 
            // control_remote_dns_end_point
            // 
            control_remote_dns_end_point.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            control_remote_dns_end_point.Location = new Point(0, 160);
            control_remote_dns_end_point.MinimumSize = new Size(250, 90);
            control_remote_dns_end_point.Name = "control_remote_dns_end_point";
            control_remote_dns_end_point.Size = new Size(300, 90);
            control_remote_dns_end_point.TabIndex = 8;
            // 
            // ControlConnecterSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(control_remote_ip_end_point);
            Controls.Add(label_local_end_point);
            Controls.Add(label_send_buffer_size);
            Controls.Add(combobox_remote_end_point);
            Controls.Add(checkbox_local_end_Point);
            Controls.Add(control_local_ip_end_point);
            Controls.Add(control_remote_dns_end_point);
            MinimumSize = new Size(250, 250);
            Name = "ControlConnecterSetting";
            Size = new Size(300, 250);
            Load += ControlClientSetting_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ControlIpEndPoint control_local_ip_end_point;
        private CheckBox checkbox_local_end_Point;
        private ComboBox combobox_remote_end_point;
        private Label label_send_buffer_size;
        private Label label_local_end_point;
        private ControlIpEndPoint control_remote_ip_end_point;
        private ControlDnsEndPoint control_remote_dns_end_point;
    }
}
