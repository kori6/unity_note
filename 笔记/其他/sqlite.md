# sqlite使用步骤和封装方法

## 1.使用步骤

### 1.创建数据库

可以直接在本地手动创建一个数据库，记录好创建数据库的地址

### 2.连接数据库

>1 SqliteConnection 表示与数据库的连接
要连接数据库，首先先声明一个SqliteConnection connection
实例化 connection

```csharp
connection = new SqliteConnection(new SqliteConnectionStringBuilder() { DataSource = dbPath }.ToString());
``````

括号内传入的是字符串地址字符串格式为

```dotnetcli
string path="DataSource = C:\Program Files (x86)"
``````

>2 打开数据库
 connection.Open();

### 3.对数据库进行操作

声明一个要执行的sql语句的字段
SqliteCommand sqliteCommand;
sqliteCommand = new SqliteCommand(connection);
括号里表示刚才连接的数据库

#### 增 删  改

>1 sqliteCommand.CommandText="输入要执行的sql语句"
sqliteCommand.ExecuteNonQuery();执行sql语句
>2.关闭连接
IDispose接口可以通过Using关键字实现使用后立刻销毁，因此，Dispose适合只在方法中调用一次SqlConnection对象，而Close更适合SqlConnection在关闭后可能需要再次打开的情况。

#### 查

ExecuteReader()执⾏SQL语句，返回所有查询到的结果
（SqliteDataReader）
![read](../../图片/sqliteRead().png)

## 封装

```csharp
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEditor;
using UnityEngine;

public class SqlDbCommand :sqlDbConnect
{
    /// <summary>
    /// 表示要对数据库执行sql语句
    /// </summary>
    SqliteCommand sqliteCommand;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dbPath"></param>
    public SqlDbCommand(string dbPath):base(dbPath) 
    {
                                    //命令使用的连接。
        sqliteCommand = new SqliteCommand(connection);
    }
    #region 表管理
    public int CreateTable<T>()where T : class
    {
        //if (IsTableCreated<T>())
        //{
        //    return -1;
        //}
        //typeof返回T的类型 相当于给type赋值类型
        var type=typeof(T);
        //返回的是类的名字，相当于赋值表名
        var typeName=type.Name;
        //String 对象不可变。 每次使用 System.String 类中的方法之一，都要在内存中新建字符串对象，这就需要为新对象分配新空间。 在需要重复修改字符串的情况下，与新建 String 对象关联的开销可能会非常大。 若要修改字符串（而不新建对象），可以使用 System.Text.StringBuilder 类。 例如，如果在循环中将许多字符串连接在一起，使用 StringBuilder 类可以提升性能。
        var sb =new StringBuilder();
        //将信息追加到当前 StringBuilder 的末尾。
        sb.Append( $"create TABLE {typeName}(");
        //返回为当前 Type 的所有公共属性。
        var properties=type.GetProperties();
        foreach(var p in properties)
        {
            //检索应用于程序集、模块、类型成员或方法参数的指定类型的自定义属性。
            var attribute = p.GetCustomAttribute<ModeHelp>();
            if (attribute.IsCreated)
            {
                sb.Append($"{attribute.FieldName} {attribute.Type} ");
                if(attribute.IsPrimaryKey)
                {
                    sb.Append(" primary key ");
                }
                if(attribute.IsCanBeNull)
                {
                    sb.Append(" null ");
                }
                else
                {
                    sb.Append(" not null ");
                }
                sb.Append(",");
            }
        }
        //从当前 StringBuilder 中删除指定数量的字符。
        sb.Remove(sb.Length-1,1);
        sb.Append(")");

        sqliteCommand.CommandText= sb.ToString();
        return sqliteCommand.ExecuteNonQuery();
    }
    public int DeleteTable<T>()where T : BaseDate
    {
        var sql = $"drop table {typeof(T).Name}";
        sqliteCommand.CommandText = sql;
        return sqliteCommand.ExecuteNonQuery();
    }
    public bool IsTableCreated<T>()where T:BaseDate
    {
        var sql = $"SELECT count(*) FROM sqlite_master WHERE type= 'table' AND name='{typeof(T).Name}'";
        sqliteCommand.CommandText = sql;
        var dr= sqliteCommand.ExecuteReader();
        if(dr!=null&& dr.Read())
        {
            var temp = Convert.ToInt32(dr[dr.GetName(0)]);
            if (temp == 1)
            {
                return true;
            }
            return false;
        }
        return false;
    }
    #endregion
    #region 添加表中的数据
    //新增
    public int Insert<T>(T t)where T : class
    {
        if (t == default(T))
        {

            Debug.Log("Insert()参数错误");
            return -1;
        }
        var type=typeof(T);
        StringBuilder stringBuilder= new StringBuilder();
        stringBuilder.Append($"INSERT INTO {type.Name} (");
        var property=type.GetProperties();
        foreach( var p in property)
        {
            if(p.GetCustomAttribute<ModeHelp>().IsCreated)
            {
                stringBuilder.Append($"{p.GetCustomAttribute<ModeHelp>().FieldName}");
                stringBuilder.Append(",");

            }
        }
        stringBuilder.Remove(stringBuilder.Length-1,1);
        stringBuilder.Append(") VALUES (");
        foreach (var p in property)
        {
            if (p.GetCustomAttribute<ModeHelp>().IsCreated)
            {
                if (p.GetCustomAttribute<ModeHelp>().Type == "string")
                {
                    stringBuilder.Append($" ' {p.GetValue(t)} ' ");
                }
                else
                {
                    stringBuilder.Append(p.GetValue(t));
                }
                
                stringBuilder.Append(",");
            }
        }
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append(")");
        sqliteCommand.CommandText = stringBuilder.ToString();

        return sqliteCommand.ExecuteNonQuery();
    }
    public int Insert<T>(List<T> tList) where T : class
    {
        if (tList == null || tList.Count == 0)
        {

            Debug.Log("Insert()参数错误");
            return -1;
        }
        var type = typeof(T);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"INSERT INTO {type.Name} (");
        var property = type.GetProperties();
        foreach (var p in property)
        {
            if (p.GetCustomAttribute<ModeHelp>().IsCreated)
            {
                stringBuilder.Append($"{p.GetCustomAttribute<ModeHelp>().FieldName}");
                stringBuilder.Append(",");

            }
        }
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append(") VALUES ");
        foreach (var t in tList)
        {
            stringBuilder.Append("( ");
            foreach (var p in property)
            {
                if (p.GetCustomAttribute<ModeHelp>().IsCreated)
                {
                    if (p.GetCustomAttribute<ModeHelp>().Type == "string")
                    {
                        stringBuilder.Append($" ' {p.GetValue(t)} ' ");
                    }
                    else
                    {
                        stringBuilder.Append(p.GetValue(t));
                    }

                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append("),");
        }
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        sqliteCommand.CommandText = stringBuilder.ToString();
        return sqliteCommand.ExecuteNonQuery();
    }
    #endregion
    #region 删除表中的数据
    //删除
    public int DeleteById<T>(int id)
    {
        var sql = $"DELETE FROM {typeof(T).Name} WHERE ID = {id}";
        sqliteCommand.CommandText= sql;
        return sqliteCommand.ExecuteNonQuery();
    }
    public int DeleteByIds<T>(List<int> ids)
    {
        var count = 0;
        foreach (var id in ids)
        {
            count += DeleteById<T>(id);
        }
        
        return count;
    }
    #endregion
    #region 改
    //更新
    public int Update<T>(T t) where T : BaseDate
    {
        if(t == default(T))
        {

            Debug.Log("Update()参数错误");
            return -1;
        }
        var type = typeof(T);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append($"UPDATE {type.Name} set ");
        var propertys =type.GetProperties();
        foreach (var p in propertys)
        {
            if(p.GetCustomAttribute<ModeHelp>().IsCreated)
            {
                stringBuilder.Append($"{p.GetCustomAttribute<ModeHelp>().FieldName}=");
                if (p.GetCustomAttribute<ModeHelp>().Type == "string")
                {
                    stringBuilder.Append($" ' {p.GetValue(t)} ' ");
                }
                else
                {
                    stringBuilder.Append(p.GetValue(t));
                }
                stringBuilder.Append(',');
            }
        }
        stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append($" where Id={t.Id} AND UNAME = {GlobalParameter.Instance.uname} ");
        sqliteCommand.CommandText = stringBuilder.ToString();
        return sqliteCommand.ExecuteNonQuery();
    }
    public int Update<T>(List<T> tList) where T : BaseDate
    {
        if (tList == null || tList.Count == 0)
        {
            Debug.Log("Update(list)参数错误");
            return -1;
        }
        int count = 0;
        foreach (var t in tList)
        {
            count+=Update(t);
        }
        return count;
    }
    #endregion
    #region 查
    public T SelectById<T>(int id)where T : BaseDate
    {
        var type = typeof(T);
        var sql = $"SELECT * FROM {type.Name} where Id = {id}";
        sqliteCommand.CommandText=sql;
        var dr=sqliteCommand.ExecuteReader();
        if (dr != null&&dr.Read())
        {
            return DataReaderToData<T>(dr);

        }
        return default(T);
    }

    
    public List<T> SelectBySql<T>(string sqlWhere="") where T : BaseDate
    {
        var ret = new List<T>();
        string sql;
        var type = typeof(T);
        if (string.IsNullOrEmpty(sqlWhere))
        {
             sql = $"SELECT * FROM {type.Name} ";
        }
        else
        {
            sql = $"SELECT * FROM {type.Name} where {sqlWhere}";
        }
        
        
        sqliteCommand.CommandText = sql;
        var dr = sqliteCommand.ExecuteReader();
        if (dr != null)
        {
            while (dr.Read())
            {
                ret.Add(DataReaderToData<T>(dr));
            }

        }
        return ret;
    }
    public List<T> SelectByJoin<T>() where T : BaseDate
    {
        var ret = new List<T>();
        string sql;
        var type = typeof(T);
        
        sql = $"SELECT * FROM {type.Name} INNER JOIN UserTable ON {type.Name}.UNAME=UserTable.UNAME ";
        


        sqliteCommand.CommandText = sql;
        var dr = sqliteCommand.ExecuteReader();
        if (dr != null)
        {
            while (dr.Read())
            {
                ret.Add(DataReaderToData<T>(dr));
            }

        }
        return ret;
    }
    private T DataReaderToData<T>(SqliteDataReader dr) where T : BaseDate
    {
        try
        {
            List<string> dieldName = new List<string>() ;
            for (int i = 0; i < dr.FieldCount; i++)
            {
                dieldName.Add(dr.GetName(i));
            }
            var type = typeof(T);
            T data = Activator.CreateInstance<T>();
            var properties=type.GetProperties();

            foreach (var p in properties)
            {
                if(!p.CanWrite)continue; 
                var fieldName =p.GetCustomAttribute<ModeHelp>().FieldName;
                if (fieldName.Contains(fieldName)&&p.GetCustomAttribute<ModeHelp>().IsCreated)
                {
                    p.SetValue(data, dr[fieldName]);
                }
            }
            return data;
        }
        catch (Exception)
        {

            Debug.Log($"DataReaderToData()转换出错“{typeof(T).Name}");
            return null;
        }
    }
    #endregion
    public void CloseDB()
    {
        connection.Close();
    }
}

``````
