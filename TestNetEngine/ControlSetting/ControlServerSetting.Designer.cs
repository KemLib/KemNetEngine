namespace TestNetEngine.ControlSetting
{
    partial class ControlServerSetting
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
            control_listen_setting = new ControlListenSetting();
            control_player_setting = new ControlPlayerSetting();
            label_limit_player = new Label();
            textbox_limit_player = new TextBox();
            label_client_per_frame = new Label();
            textbox_client_per_frame = new TextBox();
            button_default = new Button();
            SuspendLayout();
            // 
            // control_listen_setting
            // 
            control_listen_setting.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            control_listen_setting.Location = new Point(0, 0);
            control_listen_setting.MinimumSize = new Size(250, 0);
            control_listen_setting.Name = "control_listen_setting";
            control_listen_setting.Size = new Size(300, 155);
            control_listen_setting.TabIndex = 0;
            // 
            // control_player_setting
            // 
            control_player_setting.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            control_player_setting.Location = new Point(0, 248);
            control_player_setting.MinimumSize = new Size(250, 250);
            control_player_setting.Name = "control_player_setting";
            control_player_setting.Size = new Size(300, 250);
            control_player_setting.TabIndex = 5;
            // 
            // label_limit_player
            // 
            label_limit_player.Location = new Point(3, 161);
            label_limit_player.Name = "label_limit_player";
            label_limit_player.Size = new Size(113, 23);
            label_limit_player.TabIndex = 1;
            label_limit_player.Text = "Limit Player";
            label_limit_player.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textbox_limit_player
            // 
            textbox_limit_player.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_limit_player.Location = new Point(122, 161);
            textbox_limit_player.Name = "textbox_limit_player";
            textbox_limit_player.Size = new Size(175, 23);
            textbox_limit_player.TabIndex = 2;
            // 
            // label_client_per_frame
            // 
            label_client_per_frame.Location = new Point(3, 190);
            label_client_per_frame.Name = "label_client_per_frame";
            label_client_per_frame.Size = new Size(113, 23);
            label_client_per_frame.TabIndex = 3;
            label_client_per_frame.Text = "Client/Frame";
            label_client_per_frame.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textbox_client_per_frame
            // 
            textbox_client_per_frame.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textbox_client_per_frame.Location = new Point(122, 190);
            textbox_client_per_frame.Name = "textbox_client_per_frame";
            textbox_client_per_frame.Size = new Size(175, 23);
            textbox_client_per_frame.TabIndex = 4;
            // 
            // button_default
            // 
            button_default.Location = new Point(122, 219);
            button_default.Name = "button_default";
            button_default.Size = new Size(75, 23);
            button_default.TabIndex = 4;
            button_default.Text = "Default";
            button_default.UseVisualStyleBackColor = true;
            button_default.Click += Button_Default_Click;
            // 
            // ControlServerSetting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button_default);
            Controls.Add(label_client_per_frame);
            Controls.Add(textbox_client_per_frame);
            Controls.Add(label_limit_player);
            Controls.Add(textbox_limit_player);
            Controls.Add(control_player_setting);
            Controls.Add(control_listen_setting);
            MinimumSize = new Size(250, 510);
            Name = "ControlServerSetting";
            Size = new Size(300, 510);
            Load += ControlServerSetting_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ControlListenSetting control_listen_setting;
        private ControlPlayerSetting control_player_setting;
        private Label label_limit_player;
        private TextBox textbox_limit_player;
        private Label label_client_per_frame;
        private TextBox textbox_client_per_frame;
        private Button button_default;
    }
}
