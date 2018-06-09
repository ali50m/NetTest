﻿using GalaSoft.MvvmLight;
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
        string _rfidMemberNo;
        //Rfid里存的会员号
        public String RfidMemberNo
        {
            get { return _rfidMemberNo; }
            set { Set(ref _rfidMemberNo, value); }
        }

        bool _scanQrCodeEnable = true;
        /// <summary>
        /// 可否扫码
        /// </summary>
        public bool ScanQrCodeEnable
        {
            get { return _scanQrCodeEnable; }
            set { Set(ref _scanQrCodeEnable, value); }
        }

        RelayCommand _openCardCommand;
        /// <summary>
        /// 开卡
        /// </summary>
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
        /// <summary>
        /// 扫码
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
                        Info info = await _cardInfoService.GetOne("88336632114782");
                        LoadData(info);
                    }
                }));
            }
        }
        RelayCommand _readCardCommand;
        public RelayCommand ReadCardCommand
        {
            get
            {
                return _readCardCommand ?? (_readCardCommand = new RelayCommand(() =>
                {
                    var rfidInfo = _rfidReader.ReadCard();
                    MessageBox.Show("卡数据："+ rfidInfo.MemberNo);
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
                    var rfidInfo = _rfidReader.WriteCard(new RfidInfo { MemberNo= RfidMemberNo });
                    MessageBox.Show("写卡成功");
                }));
            }
        }
        RelayCommand _searchCommand;
        /// <summary>
        /// 搜索
        /// </summary>
        public RelayCommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand(async () =>
                {
                    if (string.IsNullOrWhiteSpace(SearchInfo.Name) && string.IsNullOrWhiteSpace(SearchInfo.Mobile))
                    {
                        MessageBox.Show("搜索条件至少填一项");
                        return;
                    }
                    Info info = await _cardInfoService.GetOne(SearchInfo.Name, SearchInfo.Mobile);
                    LoadData(info);
                }));
            }
        }

        RelayCommand _checkServerCommand;
        /// <summary>
        /// 检查服务器状态
        /// </summary>
        public RelayCommand CheckServerCommand
        {
            get
            {
                return _checkServerCommand ?? (_checkServerCommand = new RelayCommand(() =>
                {
                    //throw new NotImplementedException("检查服务器状态未实现");
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

            if (info.Ref == "2")
            {
                Message = info.Message;
                return;
            }
            CardInfo.SeatInfo = info.SeatsInfo;
            CardInfo.Mobile = info.Member.Mobile;
            CardInfo.NoConsumption = info.Member.NoConsumption.ToString();
            bool cardIsNull = info.Card == null;

            CardInfo.State = cardIsNull ? "" : info.Card.MyMemberPossessCard.State == 1 ? "正常" : info.Card.MyMemberPossessCard.State == 2 ? "卡失效" : "";
            CardInfo.CardNum = cardIsNull ? "" : info.Card.MyMemberPossessCard.CardNum;
            CardInfo.Name = cardIsNull ? "" : info.Member.UserName;
            CardInfo.StartDate = cardIsNull ? "" : info.Card.MyMemberPossessCard.BuyTime;
            CardInfo.EndDate = cardIsNull ? "" : info.Card.MyMemberPossessCard.LoseTime;
            CardInfo.CardType = cardIsNull ? "" : info.Card.Name;
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