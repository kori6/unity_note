# 资源打包

## BuildPipeline.BuildAssetBundles()

1.先将需要打包的资源在assetBundle中设置好名称，这个方法会自动检测设置好的文件

```csharp
 BuildPipeline.BuildAssetBundles(outputPath,/*ab包输出路径*/
     BuildAssetBundleOptions.ChunkBasedCompression,/*压缩模式*/
     BuildTarget.StandaloneWindows64/*输出平台*/);
```

传入的参数分别为 输出路径、压缩模式、输出平台三部分
其中最关键的部分为压缩模式，常用的压缩模式有三种
![压缩模式](../../../图片/assetbundle压缩模式.png)
