using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Application.Validate
{
    /// <summary>
    /// 自定义验证接口
    /// </summary>
    public interface ICustomValidate
    {
        void AddValidationErrors(List<ValidationResult> results);
    }
}
