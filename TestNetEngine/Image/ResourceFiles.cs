namespace TestNetEngine.Image
{
    internal static class ResourceFiles
    {
        #region Properties
        private const string PATH_KNET_ICON = "Image\\KNet_Icon.ico",
            PATH_KNET_PIC = "Image\\KNet_Icon.png";
        #endregion

        #region Construction

        #endregion

        #region Method
        public static Icon Load_IconKNet()
        {
            return new Icon(PATH_KNET_ICON);
        }
        public static Bitmap Load_PictureKNet()
        {
            return new Bitmap(PATH_KNET_PIC);
        }
        #endregion
    }
}
