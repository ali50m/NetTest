using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MemberMgmt.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MemberMgmt.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        readonly CardInfoService _cardInfoService;
        readonly BarCodeHook _barCode;
        public MainViewModel(CardInfoService cardInfoService, BarCodeHook barCode, CardInfoViewModel cardInfo, SearchInfoViewModel searchInfo)
        {
            _cardInfoService = cardInfoService;
            _barCode = barCode;
            CardInfo = cardInfo;
            SearchInfo = searchInfo;
            if (IsInDesignMode)
            {
                LoadData();
            }
            else
            {
                //_barCode.BarCodeEvent += args => { MessageBox.Show(args.BarCode); };
                //_barCode.Start();
            }
        }

        public CardInfoViewModel CardInfo { get; }
        public SearchInfoViewModel SearchInfo { get; }

        RelayCommand _openCardCommand;

        public RelayCommand OpenCardCommand
        {
            get
            {
                return _openCardCommand ?? (_openCardCommand = new RelayCommand(() =>
                {
                    MessageBox.Show("开卡");
                }));
            }
        }
        void LoadData()
        {
            Models.CardInfo cardInfo = _cardInfoService.GetOne();
            CardInfo.CardNum = cardInfo.CardNum;
            CardInfo.Name = cardInfo.Name;
            CardInfo.IdCardNum = cardInfo.IdCardNum;
        }

        public override void Cleanup()
        {
            base.Cleanup();
            _barCode.Stop();
        }
    }
    class CardInfoViewModel : ViewModelBase
    {
        String _cardNum;
        public String CardNum
        {
            get { return _cardNum; }
            set { Set(ref _cardNum, value); }
        }
        String _name;
        public String Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }
        String _idCardNum;
        public String IdCardNum
        {
            get { return _idCardNum; }
            set { Set(ref _idCardNum, value); }
        }
        String _mobile;
        public String Mobile
        {
            get { return _mobile; }
            set { Set(ref _mobile, value); }
        }
        String _gender;
        public String Gender
        {
            get { return _gender; }
            set { Set(ref _gender, value); }
        }
        String _state;
        public String State
        {
            get { return _state; }
            set { Set(ref _state, value); }
        }
        String _cardType;
        public String CardType
        {
            get { return _cardType; }
            set { Set(ref _cardType, value); }
        }
        String _startDate;
        public String StartDate
        {
            get { return _startDate; }
            set { Set(ref _startDate, value); }
        }
        String _endDate;
        public String EndDate
        {
            get { return _endDate; }
            set { Set(ref _endDate, value); }
        }
        RelayCommand _scanQRCodeCommand;

        public RelayCommand ScanQRCodeCommand
        {
            get
            {
                return _scanQRCodeCommand ?? (_scanQRCodeCommand = new RelayCommand(() =>
                {
                    MessageBox.Show("扫码");
                }));
            }
        }
    }
    class SearchInfoViewModel : ViewModelBase
    {
        String _name;
        public String Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }
        String _mobile;
        public String Mobile
        {
            get { return _mobile; }
            set { Set(ref _mobile, value); }
        }
        RelayCommand _searchCommand;

        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(() =>
                {
                    MessageBox.Show("搜索");
                }));
            }
        }
    }
}