using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrame//�������
{
    public class Singleton<T> where T : class
    {
        //��������
        private static T _singleton;

        //��ȡ����
        public static T Instance
        {
            get
            {
                if (_singleton == null)
                {
                    //ͨ������ķ�ʽ ʵ����һ���������
                    //�����������ĵ������б�����һ��˽�е� �޲εĹ��캯��
                    _singleton = (T)Activator.CreateInstance(typeof(T), true);
                }
                return _singleton;
            }
        }
    }

}
