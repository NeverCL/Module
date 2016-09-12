using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
                //headers
                var cells = sheet.GetRow(0).Cells;
                var headers = new List<HeaderInfo>();
                var type = typeof(T);
                var props = type.GetProperties();
                for (int i = 0; i < cells.Count; i++)
                {
                    var cell = cells[i];
                    if (_isOrderNum && i < props.Length)
                    {
                        var prop = props[i];
                        headers.Add(new HeaderInfo
                        {
                            Title = cell.ToString(),
                            TitleOrderId = i,
                            Property = prop
                        });
                    }
                    else
                    {
                        var prop = GetPropByName(props, cell.ToString());
                        if (prop != null)
                        {
                            headers.Add(new HeaderInfo
                            {
                                Title = cell.ToString(),
                                TitleOrderId = i,
                                Property = prop
                            });
                        }
                    }
                }

                //content
                var list = new List<T>();
                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    var row = sheet.GetRow(i);
                    var obj = new T();
                    foreach (var headerInfo in headers)
                    {
                        var prop = headerInfo.Property;
                        if (headerInfo.TitleOrderId >= row.Cells.Count)
                            continue;
                        var cell = row.Cells[headerInfo.TitleOrderId];
                        SetPropValue(obj, prop, cell.CellType == CellType.Numeric ? cell.DateCellValue.ToString() : cell.ToString());
                    }
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

        private PropertyInfo GetPropByName(PropertyInfo[] props, string name)
        {
            //var props = type.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                if (GetPropAttrName(prop) == name)
                    return prop;
            }
            return null;
        }

        protected virtual string GetPropAttrName(PropertyInfo prop)
        {
            var displayAttr =
                prop.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as
                        DisplayAttribute;
            if (displayAttr != null)
            {
                return displayAttr.Name;
            }

            var displayNameAttr =
                     prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as
                         DisplayNameAttribute;
            if (displayNameAttr != null)
            {
                return displayNameAttr.DisplayName;
            }

            var descriptionAttr =
                     prop.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as
                         DescriptionAttribute;
            if (descriptionAttr != null)
            {
                return descriptionAttr.Description;
            }

            return string.Empty;
        }

        protected virtual bool GetTypeAttrName(Type type, string name)
        {
            var displayAttr =
                type.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as
                        DisplayAttribute;
            if (displayAttr != null && displayAttr.Name == name)
            {
                return true;
            }

            var displayNameAttr =
                     type.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as
                         DisplayNameAttribute;
            if (displayNameAttr != null && displayNameAttr.DisplayName == name)
            {
                return true;
            }

            var descriptionAttr =
                     type.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as
                         DescriptionAttribute;
            if (descriptionAttr != null && descriptionAttr.Description == name)
            {
                return true;
            }

            var typeName = type.Name;
            if (typeName == name)
            {
                return true;
            }

            return false;
        }

    }
}