using System.Text;

namespace TestNetEngine
{
    public partial class ControlListLog : UserControl
    {
        #region Properties

        #endregion

        #region Construction
        public ControlListLog()
        {
            InitializeComponent();
        }
        #endregion

        #region Ui Event
        private void BtnClick_Clear(object sender, EventArgs e)
        {
            ListBox_Log.Items.Clear();
        }
        #endregion

        #region Method
        public void AddLog(string log)
        {
            if (!CheckBox_Enable.Checked)
                return;
            //
            ListBox_Log.Items.Add(log);
            //
            if (CheckBox_EndScroll.Checked)
            {
                int visibleItems = ListBox_Log.ClientSize.Height / ListBox_Log.ItemHeight;
                ListBox_Log.TopIndex = Math.Max(ListBox_Log.Items.Count - visibleItems + 1, 0);
            }
        }
        public void AddLog(byte[]? data)
        {
            if (!CheckBox_Enable.Checked)
                return;
            //
            if (data == null || data.Length == 0)
            {
                AddLog(string.Empty);
                return;
            }
            string log = Encoding.UTF8.GetString(data);
            AddLog(log);
        }
        #endregion
    }
}
