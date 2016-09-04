using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Module.Application.Validate.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var builder = new ContainerBuilder();

            // Register individual components

            builder.RegisterType<TaskRepository>().As<ITaskRepository>().EnableInterfaceInterceptors().InterceptedBy(typeof(ValidateInterceptor));
            builder.RegisterType<ValidateInterceptor>();

            var container = builder.Build();

            var task = container.Resolve<ITaskRepository>();

            task.Create(new CreateInput {Id = "1", Name = "1234" });
        }
    }

    public class CreateInput : IShouldNormalize, ICustomValidate
    {
        [Required]
        public string Id { get; set; }

        [MinLength(3)]
        public string Name { get; set; }

        public void Normalize()
        {
            Name += DateTime.Now;
        }

        public void AddValidationErrors(List<ValidationResult> results)
        {
            if (Name == "123")
            {
                results.Add(new ValidationResult("不允许为123"));
            }
        }
    }
    public class TaskRepository : ITaskRepository
    {
        public void Create(CreateInput input)
        {
            Console.WriteLine("创建了...");
        }
    }

    public interface ITaskRepository
    {
        void Create(CreateInput input);
    }
}
