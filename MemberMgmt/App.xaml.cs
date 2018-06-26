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
                MessageBox.Show(string.Format("UI线程全局异常：{0}", e.Exception));
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("应用程序发生不可恢复的异常，将要退出！异常：{0}",ex));
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    MessageBox.Show(string.Format("UI线程全局异常：{0}", exception));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("应用程序发生不可恢复的异常，将要退出！异常：{0}", ex));
            }
        }
    }
}
