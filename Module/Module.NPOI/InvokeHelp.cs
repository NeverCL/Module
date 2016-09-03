using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Module.NPOI
{
    public static class InvokeHelp
    {
        public static object InvokeStaticGenericMethod(object[] args, Type classType, string methodName, Type genericType)
        {
            MethodInfo mi = classType.GetMethod(methodName);
            MethodInfo miConstructed = mi.MakeGenericMethod(genericType);
            return miConstructed.Invoke(null, args);
        }

    }
}
