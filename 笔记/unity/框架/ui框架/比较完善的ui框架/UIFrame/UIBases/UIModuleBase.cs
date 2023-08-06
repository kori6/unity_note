using System;
using UIFrame;
using Unity.VisualScripting;
using UnityEngine;

namespace UIFrame
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIModuleBase : MonoBehaviour
    {
        protected CanvasGroup _canvasGroup;

        private Transform[] allChild;
        public virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            //获取当前模块的所有子对象
            allChild = GetComponentsInChildren<Transform>();

            //修改当前模块的名称
            gameObject.name = gameObject.name.Remove(gameObject.name.Length - 7);


            //给所有可用的ui元件添加
            AddWidgetBahaviour();

        }
        public virtual void Start()
        {

        }

        #region Set Widgets

        private void AddWidgetBahaviour()
        {
            //
            //遍历所有的子对象
            for (int i = 0; i < allChild.Length; i++)
            {
                //遍历所有的标记
                for (int j = 0; j < SystemDefine.WIDGET_TOKEN.Length; j++)
                {
                    //判断当前元件对象是不是以该标记作为名称结尾的
                    if (allChild[i].name.EndsWith(SystemDefine.WIDGET_TOKEN[j]))
                    {
                        AddComponentForWidget(i);
                    }
                }
                // allChild[i].name 
            }
        }
        protected virtual void AddComponentForWidget(int index)
        {
            //给元件添加UIWidgetBase组件
            UIWidgetBase uIWidgetBase = allChild[index].gameObject.AddComponent<UIWidgetBase>();
            //设置该元件的模块是当前模块
            uIWidgetBase.UIWidgetInit(this);
        }


        #endregion

        #region Controller Bind
        protected void BindController(UIControllerBase controllerBase)
        {
            controllerBase.ControllerInit(this);
        }

        #endregion


        #region State call
        /// <summary>
        /// 进入当前模块执行该函数
        /// </summary>
        public virtual void OnEnter()
        {
            _canvasGroup.blocksRaycasts = true;
        }



        /// <summary>
        /// 当暂停模块时
        /// </summary>
        public virtual void OnPause()
        {
            _canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// 当模块回复时
        /// </summary>
        public virtual void OnResume()
        {
            _canvasGroup.blocksRaycasts = true;
        }
        /// <summary>
        /// 当离开该模块执行该函数
        /// </summary>
        public virtual void OnExit()
        {
            _canvasGroup.blocksRaycasts = false;
        }

        #endregion

        #region Find Widget
        public UIWidgetBase FindCurrentModuleWidget(string widgetName)
        {
            return UImanager.Instance.FindWidget(name, widgetName);
        }
        #endregion
    }
}
