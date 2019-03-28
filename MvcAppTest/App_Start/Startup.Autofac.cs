using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Reflection;

namespace MvcAppTest
{
    public partial class Startup
    {
        static IContainer _autofacContainer;
        static IContainer AutofacContainer
        {
            get
            {
                if (_autofacContainer == null)
                {
                    var builder = new ContainerBuilder();
                    RegisterWeb(builder);
                    _autofacContainer = builder.Build();
                }
                return _autofacContainer;
            }
        }


        static void RegisterWeb(ContainerBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            //注册MVC、WebApi和SignalR
            builder.RegisterApiControllers(assembly).OnActivated(h => h.Context.InjectUnsetProperties(h.Instance));
        }
    }
}