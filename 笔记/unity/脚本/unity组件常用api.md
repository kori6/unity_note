# 组件常用api

## Nav Mesh Agent(导航组件)

### agent.SetDestination() 导航到目标点

public bool SetDestination (Vector3 target);
target 要导航到的目标点.
如果可以导航到返回true,不可以返回false

### agent.desiredVelocity（） 导航的期望速度

### agent.remainingDistance 导航剩余距离

## Vector3(三维向量)

### Vector3.Project（） 计算投影

public static Vector3 Project (Vector3 vector, Vector3 onNormal);
将向量投影到另一个向量上。
要理解向量投影，请想象 onNormal 位于一条指向其方向的线上。 这条线上有一个最靠近 vector 尖端的点。 投影不过是重新缩放 /onNormal/，以使其到达线上的那个点。
![投影](../../../图片/投影.png)

## animator（动画组件）

### void OnAnimatorMove()回调函数

>用于处理动画移动以修改根运动的回调。
该回调在处理完状态机和动画后 （但在 OnAnimatorIK 之前）的每个帧中调用。

### animator.deltaPosition 计算根动画位移

>获取上一个已计算帧的化身位置增量。
每帧的位移，除以时间等于速度。

### OnAnimatorIK(int layerIndex) 回调函数

在即将调用IK动画之前执行，可以用来设置IK目标的位置和权重

### IK动画使用步骤

1.先设置某一个动画层是否启用IK
![IK设置](../../../图片/IK设置.png)
2.在代码中先设置iK权重（表示开启不开启）

```csharp
animator.SetIKPositionWeight(部位，权重)
```

>public void SetIKPositionWeight (AvatarIKGoal goal, float value);
goal 设置的 AvatarIKGoal。
LeftFoot 左脚。
RightFoot 右脚。
LeftHand 左手。
RightHand 右手。

例如：
设置右手的位置权重为1：
animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
3.设置ik的位置

```csharp
animator.SetIKPosition(AvatarIKGoal.RightHand, transform.posion);
```

括号中参数为 (部位，ik注视点)
4.重复步骤2-3，设置旋转、看向的点
SetIKRotationWeight()
SetLookAtWeight()
SetIKRotation()
SetLookAtPosition()：主要表现为看向指定方向（转头）
