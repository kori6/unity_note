using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrame
{
    public class AssetsManager : Singleton<AssetsManager>
    {
        private AssetsManager()
        {
            assetsCache = new Dictionary<string, Object>();
        }

        private Dictionary<string, Object> assetsCache;

        /// <summary>
        /// ��ȡ��Դ
        /// </summary>
        /// <param name="path">��Դ·��</param>
        /// <returns>��Դ����</returns>
        public Object GetAsset(string path)
        {
            //Ҫ���ص���Դ
            Object assetObj = null;
            //���������û�и���Դ
            if (!assetsCache.ContainsKey(path))
            {
                //ͨ��resources������Դ
                assetObj =  Resources.Load(path);
                //����Դ�������
                assetsCache.Add(path, assetObj);
            }
            else
            {
                //�ӻ����м�����Դ
                assetObj= assetsCache[path];
            }
            return assetObj;
            
        }


    }
}

