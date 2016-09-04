using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;

namespace Module.NLog.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNLog()
        {
            var builder = new ContainerBuilder();

            // Register individual components
            //builder.RegisterInstance(new TaskRepository())
            //       .As<ITaskRepository>();
            builder.RegisterType<TaskRepository>().As<ITaskRepository>();
            builder.RegisterModule(new NLogModule());

            var container = builder.Build();

            var task = container.Resolve<ITaskRepository>();

            LogConfig.ConfigureFile();
            task.Create();

        }
    }


    public class TaskRepository : ITaskRepository
    {
        public ILogger Logger1 { get; set; }
        public TaskRepository(ILogger logger2)
        {
            if (logger2 == null)
            {
                throw new Exception("logger2 is null");
            }
        }
        public void Create()
        {
            if (Logger1 == null)
            {
                throw new Exception("Logger1 is null");
            }
            Logger1.Debug("test");
            Console.WriteLine("创建了...");
        }
    }

    public interface ITaskRepository
    {
        void Create();
    }
}
