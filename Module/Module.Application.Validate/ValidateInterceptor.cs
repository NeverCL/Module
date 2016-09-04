using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Castle.DynamicProxy;
using System.ComponentModel.DataAnnotations;
using Castle.Core.Internal;

namespace Module.Application.Validate
{
    public class ValidateInterceptor : IInterceptor
    {
        private readonly List<ValidationResult> _validationErrors;

        public ValidateInterceptor()
        {
            _validationErrors = new List<ValidationResult>();
        }

        public void Intercept(IInvocation invocation)
        {
            ValidateArgs(invocation.Arguments);
            invocation.Proceed();
        }

        private void ValidateArgs(object[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                ValidateObjectRecursively(args[i]);
            }

            if (_validationErrors.Any())
            {
                //foreach (var validationResult in _validationErrors)
                //{
                //    Console.WriteLine("{0}:{1}", validationResult.MemberNames.FirstOrDefault(), validationResult.ErrorMessage);
                //}
                var rst = _validationErrors.First();
                throw new Exception(rst.ErrorMessage);
            }

            foreach (var parameterValue in args)
            {
                if (parameterValue is IShouldNormalize)
                {
                    (parameterValue as IShouldNormalize).Normalize();
                }
            }
        }

        /// <summary>
        /// ComponentModel验证
        /// </summary>
        /// <param name="validatingObject"></param>
        private void ValidateObjectRecursively(object validatingObject)
        {
            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                var validationAttributes = property.Attributes.OfType<ValidationAttribute>().ToArray();
                if (validationAttributes.IsNullOrEmpty())
                {
                    continue;
                }

                var validationContext = new ValidationContext(validatingObject)
                {
                    DisplayName = property.Name,
                    MemberName = property.Name
                };

                foreach (var attribute in validationAttributes)
                {
                    var result = attribute.GetValidationResult(property.GetValue(validatingObject), validationContext);
                    if (result != null)
                    {
                        _validationErrors.Add(result);
                    }
                }
            }

            if (validatingObject is ICustomValidate)
            {
                ((ICustomValidate) validatingObject).AddValidationErrors(_validationErrors);
            }
        }
    }
}
