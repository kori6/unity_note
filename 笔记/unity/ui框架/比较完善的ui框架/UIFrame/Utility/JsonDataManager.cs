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
        //panel�����������
        JsonPanelMode panelData;
        //panel�����������ת�����ֵ�
        private Dictionary<int, Dictionary<string, string>> panelDataDic;
        #endregion

        //Json����
        private void ParsePanelData()
        {
            //��ȡ�����ı���Դ
            TextAsset panelConfig = AssetsManager.Instance.GetAsset(SystemDefine.PenelConfigPath) as TextAsset;
            //��panel�������ļ����н���
            panelData = JsonUtility.FromJson<JsonPanelMode>(panelConfig.text);

            //��paneldataת��Ϊ����������ֵ�
            for (int i = 0; i < panelData.AllData.Length; i++)
            {
                //���һ������id��һ���ֵ�
                Dictionary<string,string> crtDic= new Dictionary<string, string>();
                panelDataDic.Add(i, crtDic);

                //������ǰ����emit������panel����
                for(int j = 0; j< panelData.AllData[i].Data.Length; j++)
                {
                    //��panelNameΪkey��panelPathΪvalue���д洢
                    crtDic.Add(panelData.AllData[i].Data[j].PanelName, panelData.AllData[i].Data[j].PanelPath);
                }


            }
            
        }



        /// <summary>
        /// ͨ��panel���Ʒ���panel��Դ·��
        /// </summary>
        /// <param name="panelPath"></param>
        /// <returns></returns>
       public string FindPnaelPath(string panelName,int sceneID=(int)SystemDefine.SceneID.MainScene)
        {
            
            if (!panelDataDic.ContainsKey(sceneID))
                return "1";
            if (!panelDataDic[sceneID].ContainsKey(panelName))
                return "2";
            //���id��panelname���ֵ��ж����� ��ֱ�ӷ���
            return panelDataDic[sceneID][panelName];
        }
    }
}

