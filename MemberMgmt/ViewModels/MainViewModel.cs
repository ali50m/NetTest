using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MemberMgmt.IRepositories;
using MemberMgmt.Models;
using MemberMgmt.Services;
using System;
using System.Collections.ObjectModel;
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
            _vbarapi.addCodeFormat((byte)1);
            _vbarapi.backlight(false);
            CardInfo = cardInfo;
            SearchInfo = searchInfo;
            if (IsInDesignMode)
            {
                LoadData("");
            }
            else
            {

            }
        }

        public CardInfoViewModel CardInfo { get; private set; }
        public SearchInfoViewModel SearchInfo { get; private set; }
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
                        //var str1 = await _cardInfoService.Test(str);
                        MessageBox.Show(str);
                        //LoadData(str);
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
                sResult = Encoding.UTF8.GetString(buffer, 0, size);
            }
            else
            {
                sResult = null;
            }
            return sResult;
        }
        async void LoadData(string qrCode)
        {
            Info info = await _cardInfoService.GetOne(qrCode);
            CardInfo.CardNum = info.Card.CardNum;
            CardInfo.Name = info.Card.Name;
            CardInfo.StartDate = info.Card.StartTime.ToString("yyyy-MM-dd");
            CardInfo.EndDate = info.Card.EndTime.ToString("yyyy-MM-dd");
            CardInfo.SeatInfos.Clear();
            info.Seats.ForEach(m => CardInfo.SeatInfos.Add(m.Row + m.RowIndex + m.SiteInfo));
        }

        public override void Cleanup()
        {
            base.Cleanup();
            _vbarapi.disConnected();
        }
    }
    class CardInfoViewModel : ViewModelBase
    {
        public CardInfoViewModel()
        {
            SeatInfos = new ObservableCollection<string>();
        }
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
        public ObservableCollection<String> SeatInfos { get; private set; }

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