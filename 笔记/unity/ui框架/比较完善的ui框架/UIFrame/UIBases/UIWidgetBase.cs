using UIFrame;
using UIInterface;
using UnityEngine;

namespace UIFrame
{
    public class UIWidgetBase : UIMono
    {
        //��ǰԪ��������ģ��
        private UIModuleBase currentModule;
        public void UIWidgetInit(UIModuleBase uiModuleBase)
        {
            currentModule=uiModuleBase;
            Debug.Log(currentModule.name + "/" + name);
            //����ǰԪ����ӵ�uimanager�е��ֵ���
            UImanager.Instance.AddUIWidget(currentModule.name, name, this);
        }
        
        protected virtual void OnDestroy()
        {
            //����ǰԪ����UIManager�е��ֵ����Ƴ�
            UImanager.Instance.RemoveUIWidget(currentModule.name, name);
        }
    }

}
