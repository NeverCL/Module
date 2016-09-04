## 说明

- Nuget安装
```
Install-Package Module.NPOI
```

- 3w条数据导出耗时:1s

- 3w条数据序列化耗时:1s

- 根据扩展名自动解析使用xls和xlsx

- 支持DataAnnotations
    - [NotMapped]
    - [DisplayName("header名称")]
    - [DisplayName("sheet名称")]
    - [Display(Name = "enum名称")]
	- [System.ComponentModel.DescriptionAttribute("enum名称")]
    - [DisplayFormat(DataFormatString = "yyyy年MM月dd日")]

```C#
    [DisplayName("sheet名称")]
    public class Model
    {
        [NotMapped]
        public int Id { get; set; }

        [DisplayName("姓名")]
        public string Name { get; set; }

		[DisplayName("状态")]
        public Statu Statu { get; set; }

        [DisplayName("创建时间")]
        [DisplayFormat(DataFormatString = "yyyy年MM月dd日")]
        public DateTime Time { get; set; }
    }

	public enum Statu
    {
        [Display(Name = "激活")]
        Active,
        [System.ComponentModel.DescriptionAttribute("关闭")]
        Close
    }
```

- 单个sheet导出
```C#
new List<Model>().WriteExcel("2.xls");
```

- 多个sheet批量导出
```C#
WriteExcelHelp.WriteBulkExcel("1.xls", data, data, data);//data = new List<Model>()
```

- 单个sheet导入
```C#
var data = "2.xls".ReadExcel<Model>();
```

- 多个sheet批量导入
```C#
var dataList = ReadExcelHelp.ReadBulkExcel("3.xls", new List<Model>(), new List<Model>(), new List<Model>());
```



