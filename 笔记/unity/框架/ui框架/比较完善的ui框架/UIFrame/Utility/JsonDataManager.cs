using System.Collections.Generic;
using UIFrame;
using UnityEngine;

namespace UIFrame
{
    public class JsonDataManager : Singleton<JsonDataManager>
    {
       private JsonDataManager() 
       {
            panelDataDic = new Dictionary<int, Dictionary<string, string>>();
            ParsePanelData();
       }
        #region Saved Structure
        //panel解析后的数据
        JsonPanelMode panelData;
        //panel解析后的数据转换成字典
        private Dictionary<int, Dictionary<string, string>> panelDataDic;
        #endregion

        //Json解析
        private void ParsePanelData()
        {
            //获取配置文本资源
            TextAsset panelConfig = AssetsManager.Instance.GetAsset(SystemDefine.PenelConfigPath) as TextAsset;
            //将panel的配置文件进行解析
            panelData = JsonUtility.FromJson<JsonPanelMode>(panelConfig.text);

            //将paneldata转换为方便检索的字典
            for (int i = 0; i < panelData.AllData.Length; i++)
            {
                //添加一个场景id和一个字典
                Dictionary<string,string> crtDic= new Dictionary<string, string>();
                panelDataDic.Add(i, crtDic);

                //遍历当前场景emit的所有panel数据
                for(int j = 0; j< panelData.AllData[i].Data.Length; j++)
                {
                    //以panelName为key，panelPath为value进行存储
                    crtDic.Add(panelData.AllData[i].Data[j].PanelName, panelData.AllData[i].Data[j].PanelPath);
                }


            }
            
        }



        /// <summary>
        /// 通过panel名称返回panel资源路径
        /// </summary>
        /// <param name="panelPath"></param>
        /// <returns></returns>
       public string FindPnaelPath(string panelName,int sceneID=(int)SystemDefine.SceneID.MainScene)
        {
            
            if (!panelDataDic.ContainsKey(sceneID))
                return "1";
            if (!panelDataDic[sceneID].ContainsKey(panelName))
                return "2";
            //如果id和panelname在字典中都存在 则直接返回
            return panelDataDic[sceneID][panelName];
        }
    }
}

