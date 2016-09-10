using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Module.NPOI
{
    /// <summary>
    /// 快速读Excel
    /// </summary>
    public class ReadExcel
    {
        #region ctor cfg
        /// <summary>
        /// 是否依据Excel标题列顺序 与 模型属性顺序 产生对应关系
        /// </summary>
        private readonly bool _isOrderNum;

        public ReadExcel(bool isOrderNum)
        {
            _isOrderNum = isOrderNum;
        }

        public ReadExcel()
        {

        }
        #endregion



        public List<T> ReadTo<T>(Stream fs, bool isXls, int sheetAt = 0) where T : new()
        {
            //1. workbook
            //2. headers
            //3. content
            using (fs)
            {
                var workbook = isXls
                        ? (IWorkbook)new HSSFWorkbook(fs) : new XSSFWorkbook(fs);
                var sheet = workbook.GetSheetAt(sheetAt);
                var headers = sheet.GetRow(0).Cells.Select(x => new ReadHeader
                {
                    Title = x.ToString(),
                    TitleOrderId = x.ColumnIndex,
                    PropOrderId = GetPropOrderId<T>(x.ToString())
                }).ToList();
                var list = new List<T>();
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    var obj = new T();
                    row.Cells.ForEach(x =>
                    {
                        PropertyInfo prop;
                        if (_isOrderNum)
                            prop = typeof(T).GetProperties()[x.ColumnIndex];
                        else
                            prop = typeof(T).GetProperties()[headers.First(y => y.TitleOrderId == x.ColumnIndex).PropOrderId];
                        SetPropValue(obj, prop, x.ToString());
                    });
                    list.Add(obj);
                }

                return list;
            }
        }

        private void SetPropValue(object obj, PropertyInfo prop, string value)
        {
            if (prop.PropertyType.IsEnum)
            {
                value = InvokeHelp.InvokeGenericMethod(new[] { value }, typeof(InvokeHelp), "GetEnumValue", prop.PropertyType) as string;
                if (value != null)
                {
                    var en = Enum.Parse(prop.PropertyType, value);
                    prop.SetValue(obj, en);
                }
            }
            else
            {
                var newVal = Convert.ChangeType(value, prop.PropertyType);
                prop.SetValue(obj, newVal);
            }
        }

        private int GetPropOrderId<T>(string name)
        {
            var props = typeof(T).GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                if (_isOrderNum)
                    return -1;
                var nameAttr =
                    prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as
                        DisplayNameAttribute;
                //1. DisplayName
                //2. Prop Name
                if (nameAttr != null && nameAttr.DisplayName == name)
                    return i;
                if (name == prop.Name)
                    return i;
            }
            return -1;
        }
    }

    public class ReadHeader
    {
        /// <summary>
        /// Excel 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Title 对应的顺序
        /// 从0开始
        /// </summary>
        public int TitleOrderId { get; set; }

        /// <summary>
        /// T 属性 对应的顺序
        /// 从0开始
        /// </summary>
        public int PropOrderId { get; set; }
    }
}
