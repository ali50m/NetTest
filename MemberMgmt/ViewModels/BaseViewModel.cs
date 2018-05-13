using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows;

namespace MemberMgmt.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        public Window Window { get; set; }


        RelayCommand _dropMoveCommand;
        public RelayCommand DropMoveCommand
        {
            get
            {
                return _dropMoveCommand ?? (_dropMoveCommand = new RelayCommand(() =>
                {
                    if (Window==null)
                    {
                        throw new NullReferenceException("请先给ViewModel的Window属性赋值。");
                    }
                    Window.DragMove();
                }));
            }
        }
        RelayCommand _closeWindowCommand;
        public RelayCommand CloseWindowCommand
        {
            get
            {
                return _closeWindowCommand ?? (_closeWindowCommand = new RelayCommand(() =>
                {
                    if (Window == null)
                    {
                        throw new NullReferenceException("请先给ViewModel的Window属性赋值。");
                    }
                    Window.Close();
                }));
            }
        }

    }
}
