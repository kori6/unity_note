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
        //每个名字及路径都对应一个ui模块的预制体对象 管理当前场景所有得ui模块
        private Dictionary<UIType, UIModuleBase> uiModules;
        //管理当前场景所有的UI元件
        private Dictionary<string, Dictionary<string, UIWidgetBase>> uiWidgets;
        //ui模块的栈存储空间
        private Stack<UIModuleBase> uiModulesStack;
    

        //当前场景中的画布
        private Transform _canvas;

        #region UI Module GameObject
        /// <summary>
        /// 通过uitype获取该type所对应的模块游戏对象身上的UIModuleBase组件
        /// </summary>
        /// <param name="uiType"></param>
        /// <returns></returns>
        private UIModuleBase GetUIModule(UIType uiType)
        {
            UIModuleBase crtModule = null;
            //如果字典中没有该模块
            if (!uiModules.TryGetValue(uiType, out crtModule))
            {
                //生成该模块
                crtModule = InstantiateUIModule(AssetsManager.Instance.GetAsset(uiType.Path) as GameObject);
                //将该模块添加到字典中
                uiModules.Add(uiType, crtModule);
            }
            else if (crtModule == null)
            {
                //生成该模块
                crtModule = InstantiateUIModule(AssetsManager.Instance.GetAsset(uiType.Path) as GameObject);
                //将该模块更新到字典中
            }
            
            return crtModule;
        }
        private UIModuleBase InstantiateUIModule(GameObject prefab)
        {
            //生成当前模块
            GameObject cetModuleObj = GameObject.Instantiate(prefab);

            //设置父物体为画布
            cetModuleObj.transform.SetParent(_canvas, false);
            return cetModuleObj.GetComponent<UIModuleBase>();
        }
        #endregion
        #region UI Module Stack

        /// <summary>
        /// 通过panelName获取模块对象，并压栈
        /// </summary>
        /// <param name="uiPanelName"></param>
        public void PushUI(string uiPanelName)
        {
            //获取uitype
            UIType _uiType=UITypeManager.Instance.GetUIType(uiPanelName);
            //获取UIModulebase
            UIModuleBase _moduleBase= GetUIModule(_uiType);
            if(uiModulesStack.Count!=0)
            {
                //此时栈顶的窗口进入暂停状态
                uiModulesStack.Peek().OnPause();
            }
            //新窗口压栈
            uiModulesStack.Push(_moduleBase);
            //新窗口执行onenter
            _moduleBase.OnEnter();
        }


        public void PopUI()
        {
            //
            if(uiModulesStack.Count!=0)
            {
                //当前栈顶元素出栈，并执行离开的回调方法
                uiModulesStack.Pop().OnExit();
            }
            //栈顶元素出栈后，栈内还有没有元素
            if (uiModulesStack.Count != 0)
            {
                uiModulesStack.Peek().OnResume();
            }

            }
        #endregion
        #region UI Widgets Modele (UN)Register
        /// <summary>
        /// 注册ui模块
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        public void RegisterUIModuleToUIWidgets(string moduleName)
        {
            if (!uiWidgets.ContainsKey(moduleName))
            {
                //向字典添加元素
                uiWidgets.Add(moduleName, new Dictionary<string, UIWidgetBase>());
            }
            else
            {
                Debug.LogWarning("该模块已经存在 无需再次添加");
            }
        }
        public void UnRegisterUIModuleFromUIWidgets(string moduleName)
        {
            if (uiWidgets.ContainsKey(moduleName))
            {
                //从字典中移除该元素
                uiWidgets.Remove(moduleName);
            }
            else
            {
                Debug.LogWarning("无法取消注册");
            }
        }



        #endregion
        #region UI Widgets Add/Remove
        /// <summary>
        /// 添加元件
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="widgetName">元件名称</param>
        /// <param name="UIWidgetBase">元件对象</param>
        public void AddUIWidget(string moduleName,string widgetName,UIWidgetBase uIWidgetGet)
        {
            //如果模块不存在，添加模块
            RegisterUIModuleToUIWidgets(moduleName);
            //如果字典中已经存在该元件
            if (uiWidgets[moduleName].ContainsKey(widgetName))
            {
                Debug.LogWarning("该元件已经在字典中存在");
            }
            else
            {
                uiWidgets[moduleName].Add(widgetName, uIWidgetGet);
            }
        }/// <summary>
         /// 移除元件
         /// </summary>
         /// <param name="moduleName">模块名称</param>
         /// <param name="widgetName">元件名称</param>
        public void RemoveUIWidget(string moduleName, string widgetName)
        {
            if (uiWidgets[moduleName].ContainsKey(widgetName))
            {
                uiWidgets[moduleName].Remove(widgetName);
            }
            else
            {
                Debug.LogWarning("该元件不存在");
            }
        }
        #endregion
        #region Find Widget
        /// <summary>
        /// 获取某个模块的元件
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="widgetName">元件</param>
        public UIWidgetBase FindWidget(string moduleName, string widgetName)
        {
            //如果模块不存在 注册模块
            RegisterUIModuleToUIWidgets(moduleName);

            //
            UIWidgetBase uiWidget = null;
            //尝试获取该元件，如果没有获取到 返回null
            uiWidgets[moduleName].TryGetValue(widgetName, out uiWidget);
            //返回结果
            return uiWidget;
        }


    }


        #endregion
}



