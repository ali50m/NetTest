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
using System.Windows.Media.Imaging;

namespace MemberMgmt.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        readonly CardInfoService _cardInfoService;
        readonly Vbarapi _vbarapi;
        readonly Reader _rfidReader;
        public MainViewModel(CardInfoService cardInfoService, Vbarapi vbarapi, Reader rfidReader, CardInfoViewModel cardInfo, SearchInfoViewModel searchInfo)
        {
            _cardInfoService = cardInfoService;
            _vbarapi = vbarapi;
            _vbarapi.openDevice(1);
            _vbarapi.addCodeFormat((byte)1);
            _vbarapi.backlight(false);
            _rfidReader = rfidReader;
            CardInfo = cardInfo;
            SearchInfo = searchInfo;
            if (IsInDesignMode)
            {
                //LoadData("");
            }
            else
            {
                Message = string.Format("服务器基础地址：{0}", Properties.Settings.Default.ApiBaseUrl);
            }
        }

        public CardInfoViewModel CardInfo { get; private set; }
        public SearchInfoViewModel SearchInfo { get; private set; }
        String _message;
        /// <summary>
        /// 操作完成的信息
        /// </summary>
        public String Message
        {
            get { return _message; }
            set { Set(ref _message, value); }
        }
        //string _rfidMemberNo;
        ////Rfid里存的会员号
        //public String RfidMemberNo
        //{
        //    get { return _rfidMemberNo; }
        //    set { Set(ref _rfidMemberNo, value); }
        //}

        bool _scanQrCodeEnable = true;
        /// <summary>
        /// 可否扫码
        /// </summary>
        public bool ScanQrCodeEnable
        {
            get { return _scanQrCodeEnable; }
            set { Set(ref _scanQrCodeEnable, value); this.RaisePropertyChanged(() => WriteCardEnable); }
        }
        /// <summary>
        /// 可否写卡
        /// </summary>
        public bool WriteCardEnable
        {
            get { return ScanQrCodeEnable && !string.IsNullOrWhiteSpace(CardInfo?.CardNum); }
        }
        string scanQrCodeFunc = "query";
        /// <summary>
        /// 扫码要做的操作，查询或消费
        /// </summary>
        public string ScanQrCodeFunc
        {
            get { return scanQrCodeFunc; }
            set { Set(ref scanQrCodeFunc, value); }
        }
        //TODO 可以用ListBox定制模板来放置两个单选框
        RelayCommand<string> _scanQrCodeFuncCommand;
        /// <summary>
        /// 开卡
        /// </summary>
        public RelayCommand<string> ScanQrCodeFuncCommand
        {
            get
            {
                return _scanQrCodeFuncCommand ?? (_scanQrCodeFuncCommand = new RelayCommand<string>(funcStr =>
                {
                    ScanQrCodeFunc = funcStr;
                }));
            }
        }

        RelayCommand _openCardCommand;
        /// <summary>
        /// 开卡命令
        /// </summary>
        public RelayCommand OpenCardCommand
        {
            get
            {
                return _openCardCommand ?? (_openCardCommand = new RelayCommand(async () =>
                {
                    if (string.IsNullOrWhiteSpace(CardInfo.Id))
                    {
                        Message = "无会员ID";
                        return;
                    }
                    var info = await _cardInfoService.EditState(CardInfo.Id,1);
                    Message = info.Message;
                }));
            }
        }
        RelayCommand _scanQrCodeCommand;
        /// <summary>
        /// 扫码命令
        /// </summary>
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
                        Info info = null;
                        switch (ScanQrCodeFunc)
                        {
                            case "query":
                                info = await _cardInfoService.GetOne(str, false);//8820180633
                                break;
                            case "consume":
                                info = await _cardInfoService.GetOne(str, true);
                                break;
                        }
                        if (info != null)
                        {
                            LoadData(info);
                        }
                    }
                }));
            }
        }
        RelayCommand _readCardCommand;
        public RelayCommand ReadCardCommand
        {
            get
            {
                return _readCardCommand ?? (_readCardCommand = new RelayCommand(async () =>
                {
                    ScanQrCodeEnable = false;
                    var str = await Task.Run(() =>
                    {
                        var s = "";
                        for (var i = 0; i < 500; i++)
                        {
                            //Rfid读卡获取
                            var rfidInfo = _rfidReader.ReadCard();
                            if (!string.IsNullOrEmpty(rfidInfo.MemberNo))
                            {
                                s = rfidInfo.MemberNo;
                                break;
                            }
                            Thread.Sleep(50);
                        }
                        ScanQrCodeEnable = true;
                        return s;
                    });
                    if (!string.IsNullOrEmpty(str))
                    {
                        Info info = null;
                        switch (ScanQrCodeFunc)
                        {
                            case "query":
                                info = await _cardInfoService.GetOne(str, false);//8820180633
                                break;
                            case "consume":
                                info = await _cardInfoService.GetOne(str, true);
                                break;
                        }
                        if (info != null)
                        {
                            LoadData(info);
                        }
                    }
                }));
            }
        }
        RelayCommand _writeCardCommand;
        public RelayCommand WriteCardCommand
        {
            get
            {
                return _writeCardCommand ?? (_writeCardCommand = new RelayCommand(() =>
                {

                    var rfidInfo = _rfidReader.WriteCard(new RfidInfo { MemberNo = CardInfo.CardNum });
                    Message = string.Format("卡号：{0}写卡成功", CardInfo.CardNum);
                }));
            }
        }
        RelayCommand _searchCommand;
        /// <summary>
        /// 搜索命令
        /// </summary>
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(async () =>
                {
                    if (string.IsNullOrWhiteSpace(SearchInfo.Name) && string.IsNullOrWhiteSpace(SearchInfo.Mobile))
                    {
                        Message="搜索条件至少填一项";
                        return;
                    }
                    Info info = await _cardInfoService.GetOne(SearchInfo.Name,SearchInfo.Mobile);
                    Message = info.Message;
                    LoadData(info);
                }));
            }
        }

        RelayCommand _clearFormCommand;
        /// <summary>
        /// 检查服务器状态
        /// </summary>
        public RelayCommand ClearFormCommand
        {
            get
            {
                return _clearFormCommand ?? (_clearFormCommand = new RelayCommand(() =>
                {
                    LoadData(new Info());
                }));
            }
        }
        RelayCommand _checkPurchase;

        public RelayCommand CheckPurchase
        {
            get
            {
                return _checkPurchase ?? (_checkPurchase = new RelayCommand(async () =>
                {
                    if (string.IsNullOrWhiteSpace(CardInfo.Id))
                    {
                        Message = "无会员ID";
                        return;
                    }
                    var info = await _cardInfoService.GetOrderState(CardInfo.Id);
                    Message = info.Message;
            
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
        void LoadData(Info info)
        {
            Message = info?.Message ?? "";
            CardInfo.SeatInfo = info?.SeatsInfo ?? "";
            CardInfo.PhotoSource = string.IsNullOrEmpty(info.Photo) ? new BitmapImage() : new BitmapImage(new Uri(info.Photo));
            CardInfo.Mobile = info?.Member?.Mobile ?? "";
            CardInfo.NoConsumption = info?.Member?.NoConsumption?.ToString() ?? "";
            var step = info?.Member?.Step;
            CardInfo.RealNameState = step == 3 ? "已认证" : step == null ? "" : "未认证";

            CardInfo.Name = info?.Member?.UserName;
            CardInfo.SeatInfo = info?.SeatsInfo;
            CardInfo.Mobile = info?.Member?.Mobile;
            CardInfo.NoConsumption = info?.Member?.NoConsumption?.ToString();
            CardInfo.Id = info?.Member?.Id;
            bool cardIsNull = info.Card == null;
            CardInfo.State = cardIsNull ? "" : info.Card.MyMemberPossessCard.State == 1 ? "正常" : info.Card.MyMemberPossessCard.State == 2 ? "卡失效" : "卡待开启";
            CardInfo.CardNum = cardIsNull ? "" : info.Card.MyMemberPossessCard.CardNum;
            CardInfo.StartDate = cardIsNull ? "" : info.Card.MyMemberPossessCard.BuyTime;
            CardInfo.EndDate = cardIsNull ? "" : info.Card.MyMemberPossessCard.LoseTime;
            CardInfo.CardType = cardIsNull ? "" : info.Card.Name;

            this.RaisePropertyChanged(()=>WriteCardEnable);
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
            //PhotoUrl = new Uri("http://imgsrc.baidu.com/image/c0%3Dshijue1%2C0%2C0%2C294%2C40/sign=23af0bb406f431ada8df4b7a235fc6da/caef76094b36acafbfc578da76d98d1001e99ceb.jpg");
        }
        String _id;
        public String Id
        {
            get { return _id; }
            set { Set(ref _id, value); }
        }
        String _cardNum;
        public String CardNum
        {
            get { return _cardNum; }
            set { Set(ref _cardNum, value); }
        }
        BitmapSource _photoSource;
        public BitmapSource PhotoSource
        {
            get { return _photoSource; }
            set { Set(ref _photoSource, value); }
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
        String _realNameState;
        public String RealNameState
        {
            get { return _realNameState; }
            set { Set(ref _realNameState, value); }
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
        string _seatInfo;
        public String SeatInfo
        {
            get { return _seatInfo; }
            set { Set(ref _seatInfo, value); }
        }
        string _noConsumption;
        public String NoConsumption
        {
            get { return _noConsumption; }
            set { Set(ref _noConsumption, value); }
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
    }
}