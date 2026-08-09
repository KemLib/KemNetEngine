namespace TestNetEngine.Net.Network
{
    partial class ViewClient
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
            button_disconnect = new Button();
            button_send = new Button();
            control_list_log = new ControlListLog();
            control_list_message = new ControlListLog();
            panel_send = new Panel();
            button_send_random_name = new Button();
            combobox_send_name = new ComboBox();
            label_send_name = new Label();
            combobox_send_length = new ComboBox();
            combobox_send_count = new ComboBox();
            label_send_count = new Label();
            label_send_length = new Label();
            panel_send.SuspendLayout();
            SuspendLayout();
            // 
            // button_disconnect
            // 
            button_disconnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button_disconnect.Location = new Point(12, 426);
            button_disconnect.Name = "button_disconnect";
            button_disconnect.Size = new Size(250, 23);
            button_disconnect.TabIndex = 11;
            button_disconnect.Text = "Disconnect";
            button_disconnect.UseVisualStyleBackColor = true;
            button_disconnect.Click += Button_Disconnect_Click;
            // 
            // button_send
            // 
            button_send.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button_send.Location = new Point(176, 124);
            button_send.Name = "button_send";
            button_send.Size = new Size(75, 23);
            button_send.TabIndex = 8;
            button_send.Text = "Send";
            button_send.UseVisualStyleBackColor = true;
            button_send.Click += Button_Send_Click;
            // 
            // control_list_log
            // 
            control_list_log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            control_list_log.BackColor = SystemColors.ControlLight;
            control_list_log.Location = new Point(12, 12);
            control_list_log.MinimumSize = new Size(200, 200);
            control_list_log.Name = "control_list_log";
            control_list_log.Size = new Size(250, 408);
            control_list_log.TabIndex = 9;
            // 
            // control_list_message
            // 
            control_list_message.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            control_list_message.BackColor = SystemColors.ControlLight;
            control_list_message.Location = new Point(268, 12);
            control_list_message.MinimumSize = new Size(200, 200);
            control_list_message.Name = "control_list_message";
            control_list_message.Size = new Size(254, 281);
            control_list_message.TabIndex = 10;
            // 
            // panel_send
            // 
            panel_send.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel_send.BackColor = SystemColors.ControlLight;
            panel_send.Controls.Add(button_send_random_name);
            panel_send.Controls.Add(combobox_send_name);
            panel_send.Controls.Add(label_send_name);
            panel_send.Controls.Add(combobox_send_length);
            panel_send.Controls.Add(combobox_send_count);
            panel_send.Controls.Add(label_send_count);
            panel_send.Controls.Add(label_send_length);
            panel_send.Controls.Add(button_send);
            panel_send.Location = new Point(268, 299);
            panel_send.Name = "panel_send";
            panel_send.Size = new Size(254, 150);
            panel_send.TabIndex = 0;
            // 
            // button_send_random_name
            // 
            button_send_random_name.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button_send_random_name.Location = new Point(136, 38);
            button_send_random_name.Name = "button_send_random_name";
            button_send_random_name.Size = new Size(115, 23);
            button_send_random_name.TabIndex = 3;
            button_send_random_name.Text = "Random Name";
            button_send_random_name.UseVisualStyleBackColor = true;
            button_send_random_name.Click += Button_Send_Random_Name_Click;
            // 
            // combobox_send_name
            // 
            combobox_send_name.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            combobox_send_name.DropDownStyle = ComboBoxStyle.DropDownList;
            combobox_send_name.FlatStyle = FlatStyle.System;
            combobox_send_name.FormattingEnabled = true;
            combobox_send_name.Items.AddRange(new object[] { "Hung", "Huy", "Thanh", "Son", "Hai", "Tien", "Nguyen", "Nga", "Bi", "Bien", "Chien", "Chinh", "Tra" });
            combobox_send_name.Location = new Point(109, 9);
            combobox_send_name.Name = "combobox_send_name";
            combobox_send_name.Size = new Size(142, 23);
            combobox_send_name.TabIndex = 2;
            // 
            // label_send_name
            // 
            label_send_name.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label_send_name.Location = new Point(3, 8);
            label_send_name.Name = "label_send_name";
            label_send_name.Size = new Size(100, 23);
            label_send_name.TabIndex = 1;
            label_send_name.Text = "Name";
            label_send_name.TextAlign = ContentAlignment.MiddleRight;
            // 
            // combobox_send_length
            // 
            combobox_send_length.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            combobox_send_length.DropDownStyle = ComboBoxStyle.DropDownList;
            combobox_send_length.FlatStyle = FlatStyle.System;
            combobox_send_length.FormattingEnabled = true;
            combobox_send_length.Items.AddRange(new object[] { "1", "8", "16", "32", "64", "128", "256", "512", "1024", "8192", "16384", "32768", "65536", "131072" });
            combobox_send_length.Location = new Point(109, 95);
            combobox_send_length.Name = "combobox_send_length";
            combobox_send_length.Size = new Size(142, 23);
            combobox_send_length.TabIndex = 7;
            // 
            // combobox_send_count
            // 
            combobox_send_count.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            combobox_send_count.DropDownStyle = ComboBoxStyle.DropDownList;
            combobox_send_count.FlatStyle = FlatStyle.System;
            combobox_send_count.FormattingEnabled = true;
            combobox_send_count.Items.AddRange(new object[] { "1", "5", "10", "50", "100", "500", "1000", "5000", "10000", "50000", "100000" });
            combobox_send_count.Location = new Point(109, 67);
            combobox_send_count.Name = "combobox_send_count";
            combobox_send_count.Size = new Size(142, 23);
            combobox_send_count.TabIndex = 5;
            // 
            // label_send_count
            // 
            label_send_count.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label_send_count.Location = new Point(3, 66);
            label_send_count.Name = "label_send_count";
            label_send_count.Size = new Size(100, 23);
            label_send_count.TabIndex = 4;
            label_send_count.Text = "Count";
            label_send_count.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_send_length
            // 
            label_send_length.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label_send_length.Location = new Point(3, 95);
            label_send_length.Name = "label_send_length";
            label_send_length.Size = new Size(100, 23);
            label_send_length.TabIndex = 6;
            label_send_length.Text = "Length";
            label_send_length.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ViewClient
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 461);
            Controls.Add(panel_send);
            Controls.Add(control_list_message);
            Controls.Add(control_list_log);
            Controls.Add(button_disconnect);
            MinimumSize = new Size(550, 500);
            Name = "ViewClient";
            Text = "Client Net";
            FormClosing += ViewClient_FormClosing;
            Load += ViewClient_Load;
            panel_send.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button_disconnect;
        private Button button_send;
        private ControlListLog control_list_log;
        private ControlListLog control_list_message;
        private Panel panel_send;
        private Label label_send_length;
        private ComboBox combobox_send_length;
        private ComboBox combobox_send_count;
        private Label label_send_count;
        private ComboBox combobox_send_name;
        private Label label_send_name;
        private Button button_send_random_name;
    }
}