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
        /// 获取资源
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <returns>资源对象</returns>
        public Object GetAsset(string path)
        {
            //要返回的资源
            Object assetObj = null;
            //如果缓存中没有该资源
            if (!assetsCache.ContainsKey(path))
            {
                //通过resources加载资源
                assetObj =  Resources.Load(path);
                //将资源存进缓存
                assetsCache.Add(path, assetObj);
            }
            else
            {
                //从缓存中加载资源
                assetObj= assetsCache[path];
            }
            return assetObj;
            
        }


    }
}

