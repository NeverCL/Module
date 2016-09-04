# 说明

- [官方log4net module](http://autofac.readthedocs.io/en/latest/examples/log4net.html)

# 使用

Nuget package [available](https://www.nuget.org/packages/Module.NLog), to install run the following command;

```csharp
Install-Package Module.NLog
```

**builder.Build()前执行**

```csharp
builder.RegisterModule(new NLogModule());
```

**调用Log方法前执行**

```csharp
LogConfig.ConfigureFile();//内置的FileCfg
```
