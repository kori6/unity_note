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

## 元表

1.元表是用来储存元方法的
2.元表是来改变表的默认行为的，例如当访问一个不存在的值时/给一个不存在的值赋值时/两个表相加等行为
3.元表的键是原方法的名称，值是函数

## 元方法

### __index

1.是用来改变获取表中不存在的数据时的方法
2.如果__index={} 是一个表而不是一个方法，当获取不存在的值时就会来__index里面找，如果这里是一个方法

```lua
__index = function(mytable, key)
    if key == "key2" then
      return "metatablevalue"
    else
      return nil
    end
  end
```

当获取不存在的值时就会调用这个方法。
可以实现的效果有当有地方获取不存在的值时输出一个错误信息

### __newindex

和上面相反，这个是给不存在的键赋值时会调用的
一般情况下直接给一个表赋值时会创建出新的键值对
但如果设置了元表，并且元表中有__newindex={}
那么新创建的键值对会直接到了元表中的__newindex中的这个表中
如果这里是方法，则和上面一样，会调用上面的方法，如果想要正常赋值
则可以使用rawset(t,k,v)

```lua
__newindex = function(table, key, value)
        print("Assigning value '" .. value .. "' to key '" .. key .. "'.")
        rawset(table, key, value)  -- 使用rawset来进行实际的赋值操作
    end
```

如果这里使用t[k]=v来直接进行赋值，则会发生递归，会一直调用这个方法，直到栈溢出
