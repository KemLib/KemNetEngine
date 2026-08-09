namespace TestNetEngine.ControlSetting
{
    partial class ControlSocketSetting
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
            textbox_send_buffer_size = new TextBox();
            label_send_buffer_size = new Label();
            label_send_timeout = new Label();
            textbox_send_timeout = new TextBox();
            label_riceive_buffer_size = new Label();
            textbox_riceive_buffer_size = new TextBox();
            label_receive_timeout = new Label();
            textbox_receive_timeout = new TextBox();
            checkbox_no_delay = new CheckBox();
            button_default = new Button();
            SuspendLayout();
            // 
            // textbox_send_buffer_size
            // 
            textbox_send_buffer_size.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_send_buffer_size.Location = new Point(122, 3);
            textbox_send_buffer_size.Name = "textbox_send_buffer_size";
            textbox_send_buffer_size.Size = new Size(175, 23);
            textbox_send_buffer_size.TabIndex = 1;
            // 
            // label_send_buffer_size
            // 
            label_send_buffer_size.Location = new Point(3, 3);
            label_send_buffer_size.Name = "label_send_buffer_size";
            label_send_buffer_size.Size = new Size(113, 23);
            label_send_buffer_size.TabIndex = 0;
            label_send_buffer_size.Text = "Send BufferSize";
            label_send_buffer_size.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_send_timeout
            // 
            label_send_timeout.Location = new Point(3, 32);
            label_send_timeout.Name = "label_send_timeout";
            label_send_timeout.Size = new Size(113, 23);
            label_send_timeout.TabIndex = 2;
            label_send_timeout.Text = "Send Timeout";
            label_send_timeout.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textbox_send_timeout
            // 
            textbox_send_timeout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_send_timeout.Location = new Point(122, 32);
            textbox_send_timeout.Name = "textbox_send_timeout";
            textbox_send_timeout.Size = new Size(175, 23);
            textbox_send_timeout.TabIndex = 3;
            // 
            // label_riceive_buffer_size
            // 
            label_riceive_buffer_size.Location = new Point(3, 61);
            label_riceive_buffer_size.Name = "label_riceive_buffer_size";
            label_riceive_buffer_size.Size = new Size(113, 23);
            label_riceive_buffer_size.TabIndex = 4;
            label_riceive_buffer_size.Text = "Receive BufferSize";
            label_riceive_buffer_size.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textbox_riceive_buffer_size
            // 
            textbox_riceive_buffer_size.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_riceive_buffer_size.Location = new Point(122, 61);
            textbox_riceive_buffer_size.Name = "textbox_riceive_buffer_size";
            textbox_riceive_buffer_size.Size = new Size(175, 23);
            textbox_riceive_buffer_size.TabIndex = 5;
            // 
            // label_receive_timeout
            // 
            label_receive_timeout.Location = new Point(3, 90);
            label_receive_timeout.Name = "label_receive_timeout";
            label_receive_timeout.Size = new Size(113, 23);
            label_receive_timeout.TabIndex = 6;
            label_receive_timeout.Text = "Receive Timeout";
            label_receive_timeout.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textbox_receive_timeout
            // 
            textbox_receive_timeout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_receive_timeout.Location = new Point(122, 90);
            textbox_receive_timeout.Name = "textbox_receive_timeout";
            textbox_receive_timeout.Size = new Size(175, 23);
            textbox_receive_timeout.TabIndex = 7;
            // 
            // checkbox_no_delay
            // 
            checkbox_no_delay.Location = new Point(122, 119);
            checkbox_no_delay.Name = "checkbox_no_delay";
            checkbox_no_delay.Size = new Size(104, 24);
            checkbox_no_delay.TabIndex = 8;
            checkbox_no_delay.Text = "No Delay";
            checkbox_no_delay.UseVisualStyleBackColor = true;
            // 
            // button_default
            // 
            button_default.Location = new Point(122, 149);
            button_default.Name = "button_default";
            button_default.Size = new Size(75, 23);
            button_default.TabIndex = 9;
            button_default.Text = "Default";
            button_default.UseVisualStyleBackColor = true;
            button_default.Click += ButtonDefault_Click;
            // 
            // ControlSocketSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button_default);
            Controls.Add(checkbox_no_delay);
            Controls.Add(label_receive_timeout);
            Controls.Add(textbox_receive_timeout);
            Controls.Add(label_riceive_buffer_size);
            Controls.Add(textbox_riceive_buffer_size);
            Controls.Add(label_send_timeout);
            Controls.Add(textbox_send_timeout);
            Controls.Add(label_send_buffer_size);
            Controls.Add(textbox_send_buffer_size);
            MinimumSize = new Size(250, 180);
            Name = "ControlSocketSetting";
            Size = new Size(300, 180);
            Load += ControlSocketSetting_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textbox_send_buffer_size;
        private Label label_send_buffer_size;
        private Label label_send_timeout;
        private TextBox textbox_send_timeout;
        private Label label_riceive_buffer_size;
        private TextBox textbox_riceive_buffer_size;
        private Label label_receive_timeout;
        private TextBox textbox_receive_timeout;
        private CheckBox checkbox_no_delay;
        private Button button_default;
    }
}
