using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Application.Validate
{
    /// <summary>
    /// 初始化数据接口
    /// 在验证通过后,自动执行
    /// </summary>
    public interface IShouldNormalize
    {
        void Normalize();
    }
}
