# 1.transform中的方法

## 基本用法

```csharp
        //Posion
        transform.DOMove(Vector3.forward, 2);
        transform.DOMoveX(114, 514);
        transform.DOLocalMove(Vector3.forward, 2);

        //Rotation
        transform.DORotate(new Vector3(90,90,90), 2);
        transform.DOLookAt(Vector3.one, 2);

        //Scalc
        transform.DOScale(Vector3.one * 2,2);
```

## 模拟小球落地弹起的效果

```csharp
        // 模拟小球落地弹起的效果
        // 第一个参数为运动的方向
        // 第二个参数为持续时间
        // 第三个参数为弹的次数
        // 第四个参数取值范围是0-1 表示会不会运到到起始点的反方向
        transform.DOPunchPosition(new Vector3(0, 1, 0), 2, 2, 0f);

        // shake 震动
        // 第一个参数 持续时间 第二个震动力的大小 第三个 震动次数 第四个 随机性
        transform.DOShakePosition(2, Vector3.one*5, 10, 90);

        // blend 混合 与domove的区别：domove是移动到某个坐标 blend是在当前的坐标上增加 增量 
        transform.DOBlendableMoveBy(Vector3.one, 2);
        transform.DOBlendableRotateBy(Vector3.one, 2);
        transform.DOBlendableScaleBy(Vector3.one, 2);
```

## 改变材质的方法

```csharp
 //material 材质 //获取材质
        Material material = GetComponent<MeshRenderer>().material;
        //改变颜色 （颜色，时间）
        //material.DOColor(Color.blue, 2);

        //改变透明度 （结束时的值，时间
        //material.DOFade(0, 2f);

        //渐变颜色 （gradient：渐变曲线 ，时间//不应用α值（透明度
        //material.DOGradientColor(gradient, 2);

        //如果同时执行多个改变颜色 只会执行最新的

        //颜色混合 (一混合变黑色？？？ 不懂
        //material.DOBlendableColor(Color.blue, 2);
        //material.DOBlendableColor(Color.green, 2);
```

## 摄像机常用方法

```csharp
//Camera
        //Camera camera =GetComponent<Camera>();
        //改变宽高比
        //第一个参数为宽：高 第二个参数为时间
        //camera.DOAspect(0.1f, 2);

        //改变相机背景色 参数1 改变后的颜色 ；参数2 时间
        //camera.DOColor(Color.red,2);


        //改变视场FOV（视域 可以用来做放大镜的效果/狙击枪瞄准镜
        //参数1：改变后的值；参数2：时间
        //camera.DOFieldOfView(1, 5);
        //改变相机正交模式下的size
        //camera.DOOrthoSize();


        //改变相机显示的像素 实现一个屏幕显示多个相机内容(有点bug 不知道为啥
        //camera.DOPixelRect(new Rect(0, 0, 1, 1), 2);//通过像素
        //camera.DORect();//通过百分比

        //晃动 实现受伤时屏幕晃动 
        //参数1：时间 参数2：力度 参数3：颤抖 参数4：随机性
        //camera.DOShakePosition(2, 2, 10, 90);
```

## text常用方法

```csharp
//text
        //TMP_Text text = GetComponent<TMP_Text>();
        ////改变颜色 （颜色，时间）
        //text.DOColor(Color.red, 2);

        ////改变透明度 （时间，最终值）
        //text.DOFade(2, 0);

        ////颜色混合 （颜色，时间）
        //text.DOBlendableColor(Color.red, 2);

        //Text text1=GetComponent<Text>();
        ////实现打字效果 （tmptext暂时没有
        //text1.DOText("asdasd", 5);

          //创建一个队列 将执行的语句添加到队列中，可以按顺序一个一个执行eg：同时写两个domove时，执行执行最新的，将两个move放到队列中时，会先执行第一个 再执行第二个
        //创建一个队列
        //Sequence quence = DOTween.Sequence();
        ////添加 参数类型为Tween //do方法的返回值都为Tween，所以直接传入do方法
        //quence.Append(transform.DOMove(Vector3.one, 2));//0-2
        //quence.Join(transform.DOScale(Vector3.one * 5, 2));
        //quence.AppendInterval(2);//延时两秒//2-4
        //quence.Append(transform.DOMove(Vector3.one*2, 2));//4-6
        //quence.Join(transform.DOScale(Vector3.one, 2));
```

## 队列常用方法

```csharp
 //在队列中插入一个 
        //参数1：插入的时间点（时间,第几秒） 参数2：方法
        //eg：插入第0秒 直接播放  
        //可以插入一个不存在的时间，例如插入100s 会等到第100s再播放
        //quence.Insert(0, transform.DOScale(Vector3.one*5, 2));
        //quence.Insert(4, transform.DOScale(Vector3.one , 2));

        //插入可以实现在一个时间节点同时播放两个不同的动画（或者使用join）
        //eg：在移动的过程中放大缩小


        //预添加 不知道有啥用 执行顺序不同
        //quence.Prepend(transform.DOMove(Vector3.up, 2));


        //回调常用来在执行到某个地方的时候执行某个方法
        //执行回调函数 在第5s的时候
        //quence.InsertCallback(5, InsertCallBack);
        //在当前位置插入
        //quence.AppendCallback(AppendCallBack);
        ////预添加调用回调
        //quence.PrependCallback(PrependCallback);
        private void PrependCallback()
    {
        Debug.Log("PrependCallback");
    }

    private void AppendCallBack()
    {
        Debug.Log("AppendCallBack");
    }

    private void InsertCallBack()
    {
        Debug.Log("InsertCallBack");
    }
```

## 参数

```csharp
//设置参数
        //setLoops 设置循环 （循环次数（-1就一直循环），tpye为类型）
        //yoyo：来回变化 Restart:从头开始  Incremental:增量运动，一直往一个方向走
        //transform.DOMove(Vector3.one, 2).SetLoops(-1, LoopType.Incremental);

        //从某个点运动到起始点From
        //transform.DOMove(Vector3.one, 2).From(true);

        //延迟3秒执行 设置延迟
        //transform.DOMove(Vector3.one, 2).SetDelay(3);

        ////将运动时间变成运动速度
        //transform.DOMove(Vector3.one, 2).SetSpeedBased();

        ////变成相对运动//增量运动
        //transform.DOMove(Vector3.one, 2).SetRecyclable();

```

## 运动曲线 设置速度的变化曲线

```csharp
//transform.DOMove(Vector3.one, 2).SetEase(Ease.Flash);
```

## 回调函数

```csharp
////在动画播放完成之后执行
        //transform.DOMove(Vector3.one, 2).OnComplete(() => { Debug.Log("回调"); });
        ////动画被杀死时
        //transform.DOMove(Vector3.one, 2).OnKill();
        ////在动画播放时（暂停重新开始播放时也会执行
        //transform.DOMove(Vector3.one, 2).OnPlay();
        ////在动画暂停时
        //transform.DOMove(Vector3.one, 2).OnPause();
        ////在动画开始时(只执行一次
        //transform.DOMove(Vector3.one, 2).OnStart();
        ////每轮循环都会执行
        //transform.DOMove(Vector3.one, 2).OnStepComplete();
        ////动画执行帧内执行
        //transform.DOMove(Vector3.one, 2).OnUpdate();
        ////在动画重新播放时？？？
        //transform.DOMove(Vector3.one, 2).OnRewind();

```

## 控制函数 控制动画播放之类的

```csharp
//暂停 和下面doplay配合使用
        //transform.DOPause();
        //开始
        //transform.DOPlay();
        //重新开始？点了没反应
        //transform.DORestart();
        //倒播 返回起始点 瞬间完成
        //transform.DORewind();
        //平滑的倒播
        //transform.DOSmoothRewind();
        //杀死动画 直接不播放
        //transform.DOKill();
        //翻转动画 类似倒放
        //transform.DOFlip();
        //跳转到某个时间点 跳转过去之后是否播放动画
        //transform.DOGoto(1, true);
        //倒放
        //transform.DOPlayBackwards();
        //暂停的时候调用就开始，开始的时候调用就暂停（主打一个叛逆
        //transform.DOTogglePause();

         //获取动画开始的时间节点
        // var tween= transform.DOMove(Vector3.one, 2);
        //Debug.Log(tween.fullPosition);
        //

        //获取循环了几次
        //var tweener = transform.DOMove(Vector3.one, 1).SetLoops(3);
        //Debug.Log(tweener.CompletedLoops());

        //返回所有暂停的动画
        //var list = DOTween.PausedTweens();
        //返回所有正在播放的动画
        //var list = DOTween.PlayingTweens();
        //收集id为指定输入的id的动画
        //var list = DOTween.TweensById("ID");
        //是否正在运行动画
        //DOTween.IsTweening(transform);
        //统计正在播放的动画有几个（延时状态下的也会计算
        //DOTween.TotalPlayingTweens();

        //
        var tweener = transform.DOMove(Vector3.one, 1);
        //获取动画持续的时间（如果为true 则包含循环的时间，如果为false 则只计算一次
        //是否包含循环
        //tweener.Duration();

        //当前已经完成的时间
        //是否包含循环
        //tweener.Elapsed();

```
