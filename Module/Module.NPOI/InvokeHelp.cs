using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Module.NPOI
{
    public static class InvokeHelp
    {
        public static object InvokeGenericMethod(object[] args, Type classType, string methodName, Type genericType, object obj = null)
        {
            var mets = classType.GetMethods();
            MethodInfo mi = classType.GetMethod(methodName);
            MethodInfo miConstructed = mi.MakeGenericMethod(genericType);
            return miConstructed.Invoke(obj, args);
        }

        public static string GetEnumName<T>(object value)
        {
            var membs = typeof(T).GetFields();
            foreach (var item in membs)
            {
                if (item.Name == value.ToString())
                {
                    var attr = item.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;

                    if (attr != null)
                        return attr.Name;
                    var descAttr = item.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                    if (descAttr != null)
                        return descAttr.Description;
                }
            }
            return null;
        }

        public static string GetEnumValue<T>(string value)
        {
            var membs = typeof(T).GetFields();
            foreach (var item in membs)
            {
                var attrName = string.Empty;
                var attr = item.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;

                if (attr != null)
                    attrName = attr.Name;
                else
                {
                    var descAttr = item.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                    if (descAttr != null)
                        attrName = descAttr.Description;
                }

                if (string.IsNullOrEmpty(attrName) && item != membs.First())
                {
                    return value;
                }
                if (attrName == value)
                {
                    return item.Name;
                }
            }
            return "0";
        }
    }
}
