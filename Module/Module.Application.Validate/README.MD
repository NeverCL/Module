# 说明


- DataAnnotations

- ICustomValidate

- IShouldNormalize


# 使用

Nuget package [available](https://www.nuget.org/packages/Module.Application.Validate), to install run the following command;

```csharp
Install-Package Module.Application.Validate
```

**builder.Build()前只执行一次**

```csharp
builder.RegisterType<ValidateInterceptor>();
```

**RegisterType后(需要拦截的Type)**

```csharp
().EnableInterfaceInterceptors().InterceptedBy(typeof(ValidateInterceptor));
```
