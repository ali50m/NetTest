using System;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MvcAppTest.Startup))]

namespace MvcAppTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureWebApi(app);
        }

        /// <summary>
        /// 配置WebApi
        /// </summary>
        /// <param name="app"></param>
        static void ConfigureWebApi(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration
            {
                //依赖注入
                DependencyResolver = new AutofacWebApiDependencyResolver(AutofacContainer),
            };
            //过滤器配置
            //HttpConfiguration.Filters.Add(new MyAuthorizeAttribute());
            WebApiConfig.Register(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }

    }
}
