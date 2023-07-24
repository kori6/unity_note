using System.Collections.Generic;
using UIFrame;
using UnityEngine;

namespace UIFrame
{
    public class UImanager : Singleton<UImanager>
    {
        private UImanager()
        {
            uiModules = new Dictionary<UIType, UIModuleBase>();
            _canvas = GameObject.Find("Canvas").transform;
            uiModulesStack=new Stack<UIModuleBase>();
            uiWidgets=new Dictionary<string, Dictionary<string, UIWidgetBase>>();
        }
        //ÿ�����ּ�·������Ӧһ��uiģ���Ԥ������� ����ǰ�������е�uiģ��
        private Dictionary<UIType, UIModuleBase> uiModules;
        //����ǰ�������е�UIԪ��
        private Dictionary<string, Dictionary<string, UIWidgetBase>> uiWidgets;
        //uiģ���ջ�洢�ռ�
        private Stack<UIModuleBase> uiModulesStack;
    

        //��ǰ�����еĻ���
        private Transform _canvas;

        #region UI Module GameObject
        /// <summary>
        /// ͨ��uitype��ȡ��type����Ӧ��ģ����Ϸ�������ϵ�UIModuleBase���
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        private UIModuleBase GetUIModule(UIType uiType)
        {
            UIModuleBase crtModule = null;
            //����ֵ���û�и�ģ��
            if (!uiModules.TryGetValue(uiType, out crtModule))
            {
                //���ɸ�ģ��
                crtModule = InstantiateUIModule(AssetsManager.Instance.GetAsset(uiType.Path) as GameObject);
                //����ģ����ӵ��ֵ���
                uiModules.Add(uiType, crtModule);
            }
            else if (crtModule == null)
            {
                //���ɸ�ģ��
                crtModule = InstantiateUIModule(AssetsManager.Instance.GetAsset(uiType.Path) as GameObject);
                //����ģ����µ��ֵ���
            }
            
            return crtModule;
        }
        private UIModuleBase InstantiateUIModule(GameObject prefab)
        {
            //���ɵ�ǰģ��
            GameObject cetModuleObj = GameObject.Instantiate(prefab);

            //���ø�����Ϊ����
            cetModuleObj.transform.SetParent(_canvas, false);
            return cetModuleObj.GetComponent<UIModuleBase>();
        }
        #endregion
        #region UI Module Stack

        /// <summary>
        /// ͨ��panelName��ȡģ����󣬲�ѹջ
        /// </summary>
        /// <param name="uiPanelName"></param>
        public void PushUI(string uiPanelName)
        {
            //��ȡuitype
            UIType _uiType=UITypeManager.Instance.GetUIType(uiPanelName);
            //��ȡUIModulebase
            UIModuleBase _moduleBase= GetUIModule(_uiType);
            if(uiModulesStack.Count!=0)
            {
                //��ʱջ���Ĵ��ڽ�����ͣ״̬
                uiModulesStack.Peek().OnPause();
            }
            //�´���ѹջ
            uiModulesStack.Push(_moduleBase);
            //�´���ִ��onenter
            _moduleBase.OnEnter();
        }


        public void PopUI()
        {
            //
            if(uiModulesStack.Count!=0)
            {
                //��ǰջ��Ԫ�س�ջ����ִ���뿪�Ļص�����
                uiModulesStack.Pop().OnExit();
            }
            //ջ��Ԫ�س�ջ��ջ�ڻ���û��Ԫ��
            if (uiModulesStack.Count != 0)
            {
                uiModulesStack.Peek().OnResume();
            }

            }
        #endregion
        #region UI Widgets Modele (UN)Register
        /// <summary>
        /// ע��uiģ��
        /// </summary>
        /// <param name="moduleName">ģ������</param>
        public void RegisterUIModuleToUIWidgets(string moduleName)
        {
            if (!uiWidgets.ContainsKey(moduleName))
            {
                //���ֵ����Ԫ��
                uiWidgets.Add(moduleName, new Dictionary<string, UIWidgetBase>());
            }
            else
            {
                Debug.LogWarning("��ģ���Ѿ����� �����ٴ����");
            }
        }
        public void UnRegisterUIModuleFromUIWidgets(string moduleName)
        {
            if (uiWidgets.ContainsKey(moduleName))
            {
                //���ֵ����Ƴ���Ԫ��
                uiWidgets.Remove(moduleName);
            }
            else
            {
                Debug.LogWarning("�޷�ȡ��ע��");
            }
        }



        #endregion
        #region UI Widgets Add/Remove
        /// <summary>
        /// ���Ԫ��
        /// </summary>
        /// <param name="moduleName">ģ������</param>
        /// <param name="widgetName">Ԫ������</param>
        /// <param name="UIWidgetBase">Ԫ������</param>
        public void AddUIWidget(string moduleName,string widgetName,UIWidgetBase uIWidgetGet)
        {
            //���ģ�鲻���ڣ����ģ��
            RegisterUIModuleToUIWidgets(moduleName);
            //����ֵ����Ѿ����ڸ�Ԫ��
            if (uiWidgets[moduleName].ContainsKey(widgetName))
            {
                Debug.LogWarning("��Ԫ���Ѿ����ֵ��д���");
            }
            else
            {
                uiWidgets[moduleName].Add(widgetName, uIWidgetGet);
            }
        }/// <summary>
         /// �Ƴ�Ԫ��
         /// </summary>
         /// <param name="moduleName">ģ������</param>
         /// <param name="widgetName">Ԫ������</param>
        public void RemoveUIWidget(string moduleName, string widgetName)
        {
            if (uiWidgets[moduleName].ContainsKey(widgetName))
            {
                uiWidgets[moduleName].Remove(widgetName);
            }
            else
            {
                Debug.LogWarning("��Ԫ��������");
            }
        }
        #endregion
        #region Find Widget
        /// <summary>
        /// ��ȡĳ��ģ���Ԫ��
        /// </summary>
        /// <param name="moduleName">ģ������</param>
        /// <param name="widgetName">Ԫ��</param>
        public UIWidgetBase FindWidget(string moduleName, string widgetName)
        {
            //���ģ�鲻���� ע��ģ��
            RegisterUIModuleToUIWidgets(moduleName);

            //
            UIWidgetBase uiWidget = null;
            //���Ի�ȡ��Ԫ�������û�л�ȡ�� ����null
            uiWidgets[moduleName].TryGetValue(widgetName, out uiWidget);
            //���ؽ��
            return uiWidget;
        }


    }


        #endregion
}



