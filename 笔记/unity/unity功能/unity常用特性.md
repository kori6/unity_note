# 特性

## Multiline

在string前面加入这个特性之后可以多行输入/换行输入

## RequireComponent

依赖于某个组件，在一个组件上添加这个特性之后，再次添加这个组件就会自动添加他所依赖的组件

## AttributeUsage 自定义特性类

预定义特性 AttributeUsage 描述了如何使用一个自定义特性类。它规定了特性可应用到的项目的类型。

规定该特性的语法如下：

```csharp
[AttributeUsage(
   validon,
   AllowMultiple=allowmultiple,
   Inherited=inherited
)]
```

validon：自定义特性的对象，可以是类、方法、属性等对象（默认值是 AttributeTargets.All）
AllowMultiple：是否允许被多次使用（默认值为false：单用的）
Inherited：是否可被派生类继承（默认值为false：不能）

## DisallowMultipleComponent 禁止添加多个

这个特性是unity中使用的，在脚本的前面加上这个类之后，同一个物体只能添加一个脚本
