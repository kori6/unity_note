using UIFrame;
using UIInterface;
using UnityEngine;

namespace UIFrame
{
    public class UIWidgetBase : UIMono
    {
        //当前元件所处的模块
        private UIModuleBase currentModule;
        public void UIWidgetInit(UIModuleBase uiModuleBase)
        {
            currentModule=uiModuleBase;
            Debug.Log(currentModule.name + "/" + name);
            //将当前元件添加到uimanager中的字典中
            UImanager.Instance.AddUIWidget(currentModule.name, name, this);
        }
        
        protected virtual void OnDestroy()
        {
            //将当前元件从UIManager中的字典中移除
            UImanager.Instance.RemoveUIWidget(currentModule.name, name);
        }
    }

}
