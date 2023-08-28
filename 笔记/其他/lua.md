# 基本语法

## lua可以使用的数据类型

![lua数据类型](../../图片/lua数据类型.png
)

## 变量

lua在定义变量是不需要声明数据类型，直接使用变量名=值的格式就可以，变量默认为nil

## 循环

### while循环

```lua
while(condition(条件))
do
    statements(循环体)
end
```

 statements(循环体语句) 可以是一条或多条语句，condition(条件) 可以是任意表达式，在 condition(条件) 为 true 时执行循环体语句。

### for循环

#### 数值for循环

```lua
for var=exp1,exp2,exp3 do  
    <执行体>  
end 
```

>var 从 exp1 变化到 exp2，每次变化以 exp3 为步长递增 var，并执行一次 "执行体"。exp3 是可选的，如果不指定，默认为1。

#### 泛型for循环

```lua
--打印数组a的所有值  
a = {"one", "two", "three"}
for i, v in ipairs(a) do
    print(i, v)
end 
```

>i是数组索引值，v是对应索引的数组元素值。ipairs是Lua提供的一个迭代器函数，用来迭代数组。

### Lua repeat...until 循环

```lua
repeat
   statements
until( condition )
```

>Lua 编程语言中 repeat...until 循环语句不同于 for 和 while循环，for 和 while 循环的条件语句在当前循环执行开始时判断，而 repeat...until 循环的条件语句在当前循环结束后判断。

## Lua 流程控制

```lua
if(0)
then
    print("0 为 true")
end
```

## lua函数定义

格式如下

```lua
optional_function_scope function function_name( argument1, argument2, argument3..., argumentn)
    function_body
    return result_params_comma_separated
end
```

解析：

- optional_function_scope: 该参数是可选的指定函数是全局函数还是局部函数，未设置该参数默认为全局函数，如果你需要设置函数为局部函数需要使用关键字 local。

- function_name: 指定函数名称。

- argument1, argument2, argument3..., argumentn: 函数参数，多个参数以逗号隔开，函数也可以不带参数。

- function_body: 函数体，函数中需要执行的代码语句块。

- result_params_comma_separated: 函数返回值，Lua语言函数可以返回多个值，每个值以逗号隔开。

## lua的模块

引入其他模块,引入之后可以访问这个模块的内容

```lua
require("<模块名>")
```
