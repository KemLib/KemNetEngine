namespace TestNetEngine
{
    partial class ControlListLog
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
            CheckBox_EndScroll = new CheckBox();
            ListBox_Log = new ListBox();
            Btn_Clear = new Button();
            CheckBox_Enable = new CheckBox();
            SuspendLayout();
            // 
            // CheckBox_EndScroll
            // 
            CheckBox_EndScroll.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CheckBox_EndScroll.AutoSize = true;
            CheckBox_EndScroll.Checked = true;
            CheckBox_EndScroll.CheckState = CheckState.Checked;
            CheckBox_EndScroll.Location = new Point(38, 177);
            CheckBox_EndScroll.Name = "CheckBox_EndScroll";
            CheckBox_EndScroll.Size = new Size(78, 19);
            CheckBox_EndScroll.TabIndex = 2;
            CheckBox_EndScroll.Text = "End Scroll";
            CheckBox_EndScroll.UseVisualStyleBackColor = true;
            // 
            // ListBox_Log
            // 
            ListBox_Log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ListBox_Log.FormattingEnabled = true;
            ListBox_Log.HorizontalScrollbar = true;
            ListBox_Log.Location = new Point(3, 3);
            ListBox_Log.Name = "ListBox_Log";
            ListBox_Log.Size = new Size(194, 139);
            ListBox_Log.TabIndex = 0;
            // 
            // Btn_Clear
            // 
            Btn_Clear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Btn_Clear.Location = new Point(122, 174);
            Btn_Clear.Name = "Btn_Clear";
            Btn_Clear.Size = new Size(75, 23);
            Btn_Clear.TabIndex = 3;
            Btn_Clear.Text = "Clear";
            Btn_Clear.UseVisualStyleBackColor = true;
            Btn_Clear.Click += BtnClick_Clear;
            // 
            // CheckBox_Enable
            // 
            CheckBox_Enable.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CheckBox_Enable.AutoSize = true;
            CheckBox_Enable.Checked = true;
            CheckBox_Enable.CheckState = CheckState.Checked;
            CheckBox_Enable.Location = new Point(136, 149);
            CheckBox_Enable.Name = "CheckBox_Enable";
            CheckBox_Enable.Size = new Size(61, 19);
            CheckBox_Enable.TabIndex = 1;
            CheckBox_Enable.Text = "Enable";
            CheckBox_Enable.UseVisualStyleBackColor = true;
            // 
            // ControlListLog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CheckBox_Enable);
            Controls.Add(Btn_Clear);
            Controls.Add(ListBox_Log);
            Controls.Add(CheckBox_EndScroll);
            MinimumSize = new Size(200, 200);
            Name = "ControlListLog";
            Size = new Size(200, 200);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox CheckBox_EndScroll;
        private ListBox ListBox_Log;
        private Button Btn_Clear;
        private CheckBox CheckBox_Enable;
    }
}
