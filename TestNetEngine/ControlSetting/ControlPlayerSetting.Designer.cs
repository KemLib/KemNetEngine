namespace TestNetEngine.ControlSetting
{
    partial class ControlPlayerSetting
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
            label_message_per_frame = new Label();
            textbox_message_per_frame = new TextBox();
            control_socket_setting = new ControlSocketSetting();
            button_default = new Button();
            SuspendLayout();
            // 
            // label_message_per_frame
            // 
            label_message_per_frame.Location = new Point(3, 3);
            label_message_per_frame.Name = "label_message_per_frame";
            label_message_per_frame.Size = new Size(113, 23);
            label_message_per_frame.TabIndex = 0;
            label_message_per_frame.Text = "Message/Frame";
            label_message_per_frame.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textbox_message_per_frame
            // 
            textbox_message_per_frame.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_message_per_frame.Location = new Point(122, 3);
            textbox_message_per_frame.Name = "textbox_message_per_frame";
            textbox_message_per_frame.Size = new Size(175, 23);
            textbox_message_per_frame.TabIndex = 1;
            // 
            // control_socket_setting
            // 
            control_socket_setting.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            control_socket_setting.Location = new Point(0, 61);
            control_socket_setting.MinimumSize = new Size(250, 180);
            control_socket_setting.Name = "control_socket_setting";
            control_socket_setting.Size = new Size(300, 180);
            control_socket_setting.TabIndex = 3;
            // 
            // button_default
            // 
            button_default.Location = new Point(122, 32);
            button_default.Name = "button_default";
            button_default.Size = new Size(75, 23);
            button_default.TabIndex = 2;
            button_default.Text = "Default";
            button_default.UseVisualStyleBackColor = true;
            button_default.Click += Button_Default_Click;
            // 
            // ControlPlayerSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button_default);
            Controls.Add(control_socket_setting);
            Controls.Add(label_message_per_frame);
            Controls.Add(textbox_message_per_frame);
            MinimumSize = new Size(250, 250);
            Name = "ControlPlayerSetting";
            Size = new Size(300, 250);
            Load += ControlPlayerSetting_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_message_per_frame;
        private TextBox textbox_message_per_frame;
        private ControlSocketSetting control_socket_setting;
        private Button button_default;
    }
}
