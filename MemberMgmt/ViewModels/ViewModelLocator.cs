using Autofac;
using Autofac.Extras.CommonServiceLocator;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System.Reflection;


namespace MemberMgmt.ViewModels
{
    class ViewModelLocator
    {

        static ViewModelLocator()
        {
            var builder = new ContainerBuilder();
            if (ViewModelBase.IsInDesignModeStatic)
            {
                //设计时数据访问层使用假数据
                builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).InNamespace("MemberMgmt.DesignRepositories").AsImplementedInterfaces();
            }
            else
            {
                //运行时数据访问层从WebApi获取数据
                builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).InNamespace("MemberMgmt.Repositories").AsImplementedInterfaces();
            }
            //注册业务层类
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).InNamespace("MemberMgmt.Services").AsSelf();
            //注册ViewModels
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).InNamespace("MemberMgmt.ViewModels").Except<ViewModelLocator>().AsSelf();
            //服务定位器设置为由Autofac实现
            var locator = new AutofacServiceLocator(builder.Build());
            ServiceLocator.SetLocatorProvider(() => locator);
        }
        public ViewModelLocator()
        {

        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
  
        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            
        }
    }
}