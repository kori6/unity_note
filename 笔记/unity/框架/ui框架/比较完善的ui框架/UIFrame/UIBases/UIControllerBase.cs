namespace UIFrame
{
    /// <summary>
    /// רע��дUI�е�ҵ���߼�
    /// </summary>
    public class UIControllerBase
    {
        //��ǰ������ģ��
        protected UIModuleBase crtModule;

        public void ControllerInit(UIModuleBase moduleBase)
        {
            crtModule = moduleBase;
            //����
            ControllerStart();

        }
        protected virtual void ControllerStart()
        {

        }

    }

}
