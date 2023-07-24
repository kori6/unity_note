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
            //��ȡ��ǰģ��������Ӷ���
            allChild = GetComponentsInChildren<Transform>();

            //�޸ĵ�ǰģ�������
            gameObject.name = gameObject.name.Remove(gameObject.name.Length - 7);


            //�����п��õ�uiԪ�����
            AddWidgetBahaviour();

        }
        public virtual void Start()
        {

        }

        #region Set Widgets

        private void AddWidgetBahaviour()
        {
            //
            //�������е��Ӷ���
            for (int i = 0; i < allChild.Length; i++)
            {
                //�������еı��
                for (int j = 0; j < SystemDefine.WIDGET_TOKEN.Length; j++)
                {
                    //�жϵ�ǰԪ�������ǲ����Ըñ����Ϊ���ƽ�β��
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
            //��Ԫ�����UIWidgetBase���
            UIWidgetBase uIWidgetBase = allChild[index].gameObject.AddComponent<UIWidgetBase>();
            //���ø�Ԫ����ģ���ǵ�ǰģ��
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
        /// ���뵱ǰģ��ִ�иú���
        /// </summary>
        public virtual void OnEnter()
        {
            _canvasGroup.blocksRaycasts = true;
        }



        /// <summary>
        /// ����ͣģ��ʱ
        /// </summary>
        public virtual void OnPause()
        {
            _canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// ��ģ��ظ�ʱ
        /// </summary>
        public virtual void OnResume()
        {
            _canvasGroup.blocksRaycasts = true;
        }
        /// <summary>
        /// ���뿪��ģ��ִ�иú���
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
