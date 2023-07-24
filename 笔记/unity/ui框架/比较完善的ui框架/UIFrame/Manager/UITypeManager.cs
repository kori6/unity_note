using UnityEngine;
using UIFrame;
using System.Collections.Generic;

namespace UIFrame
{
    public class UITypeManager : Singleton<UITypeManager>
    {
        private UITypeManager()
        {
            _uiTypes = new Dictionary<string, UIType>();
        }
        private Dictionary<string, UIType> _uiTypes;

        /// <summary>
        /// 通过uipanelName名称 获取其UIType
        /// </summary>
        /// <param name="uiPanelName"></param>
        public UIType GetUIType(string uiPanelName)
        {
            //要返回的uiType
            UIType uiType = null;
            //如果缓存池中没有该key
            if(!_uiTypes.TryGetValue(uiPanelName,out uiType))
            {
                //实例化一个新的uiType
                uiType=new UIType(JsonDataManager.Instance.FindPnaelPath(uiPanelName));
                //添加到字典
                _uiTypes.Add(uiPanelName, uiType);
            }
            else
            {
                uiType = _uiTypes[uiPanelName];
            }
            return uiType;
        }
    }
}

