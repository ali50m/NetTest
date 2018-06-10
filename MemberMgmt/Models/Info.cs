using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberMgmt.Models
{
    class Info
    {
        public string Ref { get; set; }
        public string Message { get; set; }
        public String SeatsInfo { get; set; }
        public Member Member { get; set; }
        public Card Card { get; set; }
        public string Photo { get; set; }
    }

    /*
     
 {
	"ref": "1",
	"seatsInfo": "1行1桌1号,1行1桌3号,1行1桌2号",
	"member": {
		"id": "f9b1a87f112945689f3c8047e252acbb",
		"createDate": "2017-11-21 13:43:51",
		"updateDate": "2018-01-08 16:39:43",
		"username": "唐朝",
		"nickname": "唐朝",
		"headimgurl": "http://wx.qlogo.cn/mmopen/rYHA068nUdniaEfmCO1ib7P2ZwTYU7cURMJgXKiaWbj5qIPsVcybG0Xpb7etlqwzQpOwcTzMMYEFEow5w1CVTD16zn924UD2jvg/0",
		"openId": "oiSj30i5GnAggsDaYoIQexFpluqA",
		"mobile": "",
		"memberRankId": "d52d3fdc59704776a7d8e70fb583058a",
		"memberRankName": "金牌会员",
		"score": 0,
		"balance": 0.00,
		"awardFriend": 0.00,
		"awardProd": 0.00,
		"awardQrCode": "",
		"wsAddress": {
			"id": "66237085190146deb492804b9678e906"
		},
		"noConsumption": 0
	},
	"card": {
		"id": "1",
		"name": "惠民365年卡",
		"img": "/213",
		"isOpen": 1,
		"useDay": 365,
		"detail": "1111",
		"defaultRewardMoney": 40,
		"myMemberPossessCard": {
			"id": "1",
			"userId": "f9b1a87f112945689f3c8047e252acbb",
			"cardId": "1",
			"state": 2,
			"buyTime": 1527164064000,
			"loseTime": 1529135249000,
			"cardNum": "336632114782"
		}
	}
}
     
     */
}
