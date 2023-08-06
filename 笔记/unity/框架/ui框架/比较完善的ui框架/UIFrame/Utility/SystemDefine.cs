using System.Linq.Expressions;

namespace UIFrame
{
    public static class SystemDefine
    {
        #region Conciguration Path
        public const string PenelConfigPath = "UIPanelConfig";

        #endregion
        #region Scene ID
        public enum SceneID
        {
            MainScene=0
        }

        #endregion
        #region Widget Token
        public static string[] WIDGET_TOKEN = new string[] { "_F" ,"_S","_T"};

        #endregion
    }
}