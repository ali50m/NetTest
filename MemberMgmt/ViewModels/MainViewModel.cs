using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MemberMgmt.Services;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MemberMgmt.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        readonly CardInfoService _cardInfoService;
        readonly Vbarapi _vbarapi;
        public MainViewModel(CardInfoService cardInfoService, Vbarapi vbarapi, CardInfoViewModel cardInfo, SearchInfoViewModel searchInfo)
        {
            _cardInfoService = cardInfoService;
            _vbarapi = vbarapi;
            _vbarapi.openDevice(1);
            _vbarapi.backlight(false);
            CardInfo = cardInfo;
            SearchInfo = searchInfo;
            if (IsInDesignMode)
            {
                LoadData();
            }
            else
            {

            }
        }

        public CardInfoViewModel CardInfo { get; }
        public SearchInfoViewModel SearchInfo { get; }
        bool _scanQrCodeEnable = true;
        public bool ScanQrCodeEnable
        {
            get { return _scanQrCodeEnable; }
            set { Set(ref _scanQrCodeEnable, value); }
        }

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
        RelayCommand _scanQrCodeCommand;

        public RelayCommand ScanQrCodeCommand
        {
            get
            {
                return _scanQrCodeCommand ?? (_scanQrCodeCommand = new RelayCommand(async () =>
                {
                    ScanQrCodeEnable = false;
                    var str = await Task.Run(() =>
                    {
                        _vbarapi.backlight(true);
                        _vbarapi.beepControl(3);
                        var s = "";
                        for (var i = 0; i < 500; i++)
                        {
                            s = Decoder();
                            if (s != null)
                            {
                                _vbarapi.beepControl(1);
                                break;
                            }
                            Thread.Sleep(50);
                        }
                        _vbarapi.backlight(false);
                        ScanQrCodeEnable = true;
                        return s;
                    });
                    if (!string.IsNullOrEmpty(str))
                    {
                        LoadData();
                    }
                }));
            }
        }
        string Decoder()
        {
            byte[] result;
            string sResult = null;
            int size;
            if (_vbarapi.getResultStr(out result, out size))
            {
                string msg = System.Text.Encoding.Default.GetString(result);
                byte[] buffer = Encoding.UTF8.GetBytes(msg);
                sResult = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }
            else
            {
                sResult = null;
            }
            return sResult;
        }
        async void LoadData()
        {
            Models.CardInfo cardInfo = await _cardInfoService.GetOne();
            CardInfo.CardNum = cardInfo.CardNum;
            CardInfo.Name = cardInfo.Name;
            CardInfo.IdCardNum = cardInfo.IdCardNum;
        }

        public override void Cleanup()
        {
            base.Cleanup();
            _vbarapi.disConnected();
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