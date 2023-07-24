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
        /// ͨ��uipanelName���� ��ȡ��UIType
        /// </summary>
        /// <param name="uiPanelName"></param>
        public UIType GetUIType(string uiPanelName)
        {
            //Ҫ���ص�uiType
            UIType uiType = null;
            //����������û�и�key
            if(!_uiTypes.TryGetValue(uiPanelName,out uiType))
            {
                //ʵ����һ���µ�uiType
                uiType=new UIType(JsonDataManager.Instance.FindPnaelPath(uiPanelName));
                //��ӵ��ֵ�
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

