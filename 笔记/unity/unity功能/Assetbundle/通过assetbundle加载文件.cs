using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
public class NormolLoad : MonoBehaviour
{
    [Header("本地路径")]
    public string localPath = "AssetBundles";
    [Header("资源的bundle名称")]
    public string loadAsseBundleName = "model";
    [Header("资源的文件名称")]
    public string loadAssetFileName = "Model";

    [Header("版本号")]
    public int version = 1;
    //资源名单
    private string manifestName;


    //资源列表说明文件的全路径
    private string manifestFullPath;
    private void Start()
    {
        LoadByFile();
        //StartCoroutine(LoadByWWW());
        //StartCoroutine(LoadByWebRequest());
    }   
    /// <summary>
    /// 通过文件加载
    /// </summary>
    private void LoadByFile()
    {
        if (localPath.Contains(@"\"))
        {
            manifestName = localPath.Substring(localPath.LastIndexOf("/") + 1);
        }
        else
        {
            manifestName = localPath;
        }
        manifestFullPath = Application.dataPath + "/" + localPath + "/" + manifestName; 
        //加载manifest文件bundle
        AssetBundle manifestBundle= AssetBundle.LoadFromFile(manifestFullPath);

        //加载真正的manifest文件
        AssetBundleManifest manifestFile= manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        //获取要加载的资源的所有依赖
        string[] depandencies= manifestFile.GetAllDependencies(loadAsseBundleName);

        //卸载
       manifestBundle.Unload(false);

        AssetBundle[] depandenciesBundle=new AssetBundle[depandencies.Length];

        for(int i = 0; i < depandencies.Length; i++)
        {
            //拼凑依赖文件的全路径
            string depPath = Application.dataPath + "/" + localPath + "/" + depandencies[i];
            //加载依赖文件
            depandenciesBundle[i]=AssetBundle.LoadFromFile (depPath);
        }
        //for (int i = 0; i < depandenciesBundle.Length; i++)
        //{
        //    //卸载
        //    depandenciesBundle[i].Unload(false);
        //}

        //拼凑真正需要加载的资源的全路径
        string realAssetPath=Application.dataPath + "/" + localPath+"/"+loadAsseBundleName;

        //加载
        AssetBundle realbunle= AssetBundle.LoadFromFile(realAssetPath);
        Debug.Log(realbunle);
        //load预设体
        UnityEngine.Object realAsset= realbunle.LoadAsset(loadAssetFileName);
        

        if(realAsset is GameObject)
        {
            Instantiate(realAsset,Vector3.zero, Quaternion.identity);
        }
        realbunle.Unload(false);


    }

    private IEnumerator LoadByWWW()
    {
        if (localPath.Contains(@"\"))
        {
            manifestName = localPath.Substring(localPath.LastIndexOf("/") + 1);
        }
        else
        {
            manifestName = localPath;
        }
        manifestFullPath = Application.dataPath + "/" + localPath + "/" + manifestName;
        //www对象
        WWW www = null;

        www= WWW.LoadFromCacheOrDownload("file:///" + manifestFullPath, version);

        yield return www;   

        //加载manifest文件bundle
        AssetBundle manifestBundle = www.assetBundle;

        //加载真正的manifest文件
        AssetBundleManifest manifestFile = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        //获取要加载的资源的所有依赖
        string[] depandencies = manifestFile.GetAllDependencies(loadAsseBundleName);

        AssetBundle[] depandenciesBundle = new AssetBundle[depandencies.Length];

        for (int i = 0; i < depandencies.Length; i++)
        {
            //拼凑依赖文件的全路径
            string depPath = Application.dataPath + "/" + localPath + "/" + depandencies[i];

            www = WWW.LoadFromCacheOrDownload("file:///" + depPath, version);
            //等待下载
            yield return www;
            //加载依赖文件
            depandenciesBundle[i] = www.assetBundle;
        }

        //拼凑真正需要加载的资源的全路径
        string realAssetPath = Application.dataPath + "/" + localPath + "/" + loadAsseBundleName;

        //加载
        AssetBundle realbunle = AssetBundle.LoadFromFile(realAssetPath);
        Debug.Log(realbunle);
        //load预设体
        UnityEngine.Object realAsset = realbunle.LoadAsset(loadAssetFileName);


        if (realAsset is GameObject)
        {
            Instantiate(realAsset, Vector3.zero, Quaternion.identity);
        }
        yield return null;
    }


    private IEnumerator LoadByWebRequest()
    {
        if (localPath.Contains(@"\"))
        {
            manifestName = localPath.Substring(localPath.LastIndexOf("/") + 1);
        }
        else
        {
            manifestName = localPath;
        }
        manifestFullPath = Application.dataPath + "/" + localPath + "/" + manifestName;
        
        //通过webrequest加载
        UnityWebRequest request=UnityWebRequest.Get(manifestFullPath);

        yield return request.SendWebRequest();

        //加载manifest文件bundle
        AssetBundle manifestBundle = DownloadHandlerAssetBundle.GetContent(request);

        //加载真正的manifest文件
        AssetBundleManifest manifestFile = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        //获取要加载的资源的所有依赖
        string[] depandencies = manifestFile.GetAllDependencies(loadAsseBundleName);

        AssetBundle[] depandenciesBundle = new AssetBundle[depandencies.Length];

        for (int i = 0; i < depandencies.Length; i++)
        {
            //拼凑依赖文件的全路径
            string depPath = Application.dataPath + "/" + localPath + "/" + depandencies[i];

            //通过webrequest加载
             request = UnityWebRequest.Get(depPath);
            yield return request.SendWebRequest();
            //加载依赖文件
            depandenciesBundle[i] = DownloadHandlerAssetBundle.GetContent(request);
        }

        //拼凑真正需要加载的资源的全路径
        string realAssetPath = Application.dataPath + "/" + localPath + "/" + loadAsseBundleName;
        //通过webrequest加载
        request = UnityWebRequest.Get(realAssetPath);
        yield return request.SendWebRequest();
        //加载
        AssetBundle realbunle = DownloadHandlerAssetBundle.GetContent(request);
        Debug.Log(realbunle);
        //load预设体
        UnityEngine.Object realAsset = realbunle.LoadAsset(loadAssetFileName);


        if (realAsset is GameObject)
        {
            Instantiate(realAsset, Vector3.zero, Quaternion.identity);
        }
        yield return null;
    }
}
