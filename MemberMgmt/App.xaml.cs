using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MemberMgmt
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                //LogHelper.Instance.Logger.Error(e.Exception, "UI线程全局异常");
                e.Handled = true;
            }
            catch (Exception ex)
            {
                //LogHelper.Instance.Logger.Error(ex, "不可恢复的UI线程全局异常");
                MessageBox.Show("应用程序发生不可恢复的异常，将要退出！");
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    //LogHelper.Instance.Logger.Error(exception, "非UI线程全局异常");
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Instance.Logger.Error(ex, "不可恢复的非UI线程全局异常");
                MessageBox.Show("应用程序发生不可恢复的异常，将要退出！");
            }
        }
    }
}
