## 说明

- 根据扩展名自动解析使用xls和xlsx

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

- 支持DataAnnotations
    - [NotMapped]
    - [DisplayName("header名称")]
    - [DisplayName("sheet名称")]
    - [DisplayFormat(DataFormatString = "yyyy年MM月dd日")]

