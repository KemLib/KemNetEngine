namespace TestNetEngine.ControlSetting
{
    partial class ControlIpEndPoint
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
            label_ip = new Label();
            textbox_ip = new TextBox();
            label_port = new Label();
            textbox_port = new TextBox();
            button_default = new Button();
            SuspendLayout();
            // 
            // label_ip
            // 
            label_ip.Location = new Point(3, 3);
            label_ip.Name = "label_ip";
            label_ip.Size = new Size(113, 23);
            label_ip.TabIndex = 0;
            label_ip.Text = "Ip Address";
            label_ip.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textbox_ip
            // 
            textbox_ip.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_ip.Location = new Point(122, 3);
            textbox_ip.Name = "textbox_ip";
            textbox_ip.Size = new Size(175, 23);
            textbox_ip.TabIndex = 1;
            // 
            // label_port
            // 
            label_port.Location = new Point(3, 32);
            label_port.Name = "label_port";
            label_port.Size = new Size(113, 23);
            label_port.TabIndex = 2;
            label_port.Text = "Port";
            label_port.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textbox_port
            // 
            textbox_port.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_port.Location = new Point(122, 32);
            textbox_port.Name = "textbox_port";
            textbox_port.Size = new Size(175, 23);
            textbox_port.TabIndex = 3;
            // 
            // button_default
            // 
            button_default.Location = new Point(122, 61);
            button_default.Name = "button_default";
            button_default.Size = new Size(75, 23);
            button_default.TabIndex = 4;
            button_default.Text = "Default";
            button_default.UseVisualStyleBackColor = true;
            button_default.Click += Button_Default_Click;
            // 
            // ControlIpEndPoint
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button_default);
            Controls.Add(label_port);
            Controls.Add(textbox_port);
            Controls.Add(label_ip);
            Controls.Add(textbox_ip);
            MinimumSize = new Size(250, 90);
            Name = "ControlIpEndPoint";
            Size = new Size(300, 90);
            Load += ControlIpEndPoint_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_ip;
        private TextBox textbox_ip;
        private Label label_port;
        private TextBox textbox_port;
        private Button button_default;
    }
}
