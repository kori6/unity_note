using System;

namespace UIFrame
{
    [Serializable]
    public class JsonPanelMode 
    {
        public SceneDataModel[] AllData;
    }
    [Serializable]
    public class SceneDataModel
    {
        public string SceneName;
        public PanelDataModel[] Data;
    }
    [Serializable]
    public class PanelDataModel
    {
        public string PanelName;
        public string PanelPath;
    }
}

