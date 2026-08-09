using TestNetEngine.Image;

namespace TestNetEngine
{
    public partial class MainForm : Form
    {
        #region Properties
        private const string PROCESS_EXPLORER_NAME = "explorer.exe";
        #endregion

        #region Construction
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Ui Event
        private void MainForm_Shown(object sender, EventArgs e)
        {
            Icon = ResourceFiles.Load_IconKNet();
        }
        private void MenuItemClick_OpenExplore(object sender, EventArgs e)
        {
            FileInfo fileInfo = new(Application.ExecutablePath);
            if (fileInfo.Exists && fileInfo.DirectoryName != null)
                System.Diagnostics.Process.Start(PROCESS_EXPLORER_NAME, fileInfo.DirectoryName);
        }
        private void MenuItemClick_Exit(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Net
        private void MenuItem_Net_Listen_Click(object sender, EventArgs e)
        {
            Form form = new Net.Network.ViewListener
            {
                MdiParent = this
            };
            form.Show();
        }
        private void MenuItem_Net_Connect_Click(object sender, EventArgs e)
        {
            Form form = new Net.Network.ViewConnecter
            {
                MdiParent = this
            };
            form.Show();
        }
        private void MenuItem_Net_Server_Click(object sender, EventArgs e)
        {
            Form form = new Net.Server.ViewServer
            {
                MdiParent = this
            };
            form.Show();
        }
        #endregion
    }
}
