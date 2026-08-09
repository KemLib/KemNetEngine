namespace TestNetEngine.ControlSetting
{
    partial class ControlListenSetting
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
            label_backlog = new Label();
            textbox_backlog = new TextBox();
            control_ip_end_point = new ControlIpEndPoint();
            button_default = new Button();
            SuspendLayout();
            // 
            // label_backlog
            // 
            label_backlog.Location = new Point(3, 96);
            label_backlog.Name = "label_backlog";
            label_backlog.Size = new Size(113, 23);
            label_backlog.TabIndex = 1;
            label_backlog.Text = "Backlog Capacity";
            label_backlog.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textbox_backlog
            // 
            textbox_backlog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_backlog.Location = new Point(122, 96);
            textbox_backlog.Name = "textbox_backlog";
            textbox_backlog.Size = new Size(175, 23);
            textbox_backlog.TabIndex = 2;
            // 
            // control_ip_end_point
            // 
            control_ip_end_point.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            control_ip_end_point.Location = new Point(0, 0);
            control_ip_end_point.MinimumSize = new Size(250, 90);
            control_ip_end_point.Name = "control_ip_end_point";
            control_ip_end_point.Size = new Size(300, 90);
            control_ip_end_point.TabIndex = 0;
            // 
            // button_default
            // 
            button_default.Location = new Point(122, 125);
            button_default.Name = "button_default";
            button_default.Size = new Size(75, 23);
            button_default.TabIndex = 3;
            button_default.Text = "Default";
            button_default.UseVisualStyleBackColor = true;
            button_default.Click += Button_Default_Click;
            // 
            // ControlListenSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button_default);
            Controls.Add(control_ip_end_point);
            Controls.Add(label_backlog);
            Controls.Add(textbox_backlog);
            MinimumSize = new Size(250, 155);
            Name = "ControlListenSetting";
            Size = new Size(300, 155);
            Load += ControlListenSetting_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_backlog;
        private TextBox textbox_backlog;
        private ControlIpEndPoint control_ip_end_point;
        private Button button_default;
    }
}
