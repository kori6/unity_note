using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrame//单例框架
{
    public class Singleton<T> where T : class
    {
        //单例对象
        private static T _singleton;

        //获取单例
        public static T Instance
        {
            get
            {
                if (_singleton == null)
                {
                    //通过反射的方式 实例化一个对象出来
                    //这样，派生的单例类中必须有一个私有的 无参的构造函数
                    _singleton = (T)Activator.CreateInstance(typeof(T), true);
                }
                return _singleton;
            }
        }
    }

}
