using MemberMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MemberMgmt.Services
{
    /// <summary>
    /// Rfid读卡工具
    /// </summary>
    partial class Reader
    {
        //KeyA是0，KeyB是1
        const byte mode1 = 0x00;
        //Idle是0，All是1
        const byte mode2 = 0x00;
        static byte mode = (byte)((mode1 << 1) | mode2);
        //起始块规定为10
        static byte blk_add = Convert.ToByte("10", 16);
        //块数量1-4
        static byte num_blk = Convert.ToByte("01", 16);
        //密钥，规定为"FF FF FF FF FF FF"
        static string readWriteKey = "FF FF FF FF FF FF";


        public RfidInfo WriteCard(RfidInfo info) {

            byte[] snr = convertSNR(readWriteKey, 16);
            byte[] buffer = new byte[16];
            info.Data.CopyTo(buffer,0);

            int nRet = Reader.MF_Write(mode, blk_add, num_blk, snr, buffer);

            info.Message = getStatueText(nRet);
            if (nRet != 0)
            {
                info.Message = getStatueText(buffer[0]);
            }
            else
            {
                info.ID = ConvertData(snr, 0, 4);
                info.Data = buffer.TakeWhile(m => m != 0).ToArray();
                info.DataStr = ConvertData(buffer, 0, 16 * num_blk);
            }
            return info;
        }
        public RfidInfo ReadCard()
        {
            var rfidInfo = new RfidInfo();


            byte[] snr = convertSNR(readWriteKey, 6);

            byte[] buffer = new byte[16 * num_blk];

            int nRet = MF_Read(mode, blk_add, num_blk, snr, buffer);
            rfidInfo.Message = getStatueText(nRet);

            if (nRet != 0)
            {
                rfidInfo.Message = getStatueText(buffer[0]);
            }
            else
            {
                rfidInfo.Success = true;
                rfidInfo.ID = ConvertData(snr, 0, 4);
                rfidInfo.Data = buffer.TakeWhile(m=>m!=0).ToArray();
                rfidInfo.DataStr = ConvertData(buffer, 0, 16 * num_blk);
            }
            return rfidInfo;
        }
        //转换卡号专用
        private byte[] convertSNR(string str, int keyN)
        {
            string regex = "[^a-fA-F0-9]";
            string tmpJudge = Regex.Replace(str, regex, "");

            //长度不对，直接退回错误
            if (tmpJudge.Length != 12) return null;

            string[] tmpResult = Regex.Split(str, regex);
            byte[] result = new byte[keyN];
            int i = 0;
            foreach (string tmp in tmpResult)
            {
                result[i] = Convert.ToByte(tmp, 16);
                i++;
            }
            return result;
        }
        //翻译命令执行结果
        private string getStatueText(int Code)
        {
            string msg = "";
            switch (Code)
            {
                case 0x00:
                    msg = "命令执行成功 .....";
                    break;
                case 0x01:
                    msg = "命令操作失败 .....";
                    break;
                case 0x02:
                    msg = "地址校验错误 .....";
                    break;
                case 0x03:
                    msg = "找不到已选择的端口 .....";
                    break;
                case 0x04:
                    msg = "读写器返回超时 .....";
                    break;
                case 0x05:
                    msg = "数据包流水号不正确 .....";
                    break;
                case 0x07:
                    msg = "接收异常 .....";
                    break;
                case 0x0A:
                    msg = "参数值超出范围 .....";
                    break;
                case 0x80:
                    msg = "参数设置成功 .....";
                    break;
                case 0x81:
                    msg = "参数设置失败 .....";
                    break;
                case 0x82:
                    msg = "通讯超时.....";
                    break;
                case 0x83:
                    msg = "卡不存在.....";
                    break;
                case 0x84:
                    msg = "接收卡数据出错.....";
                    break;
                case 0x85:
                    msg = "未知的错误.....";
                    break;
                case 0x87:
                    msg = "输入参数或者输入命令格式错误.....";
                    break;
                case 0x89:
                    msg = "输入的指令代码不存在.....";
                    break;
                case 0x8A:
                    msg = "在对于卡块初始化命令中出现错误.....";
                    break;
                case 0x8B:
                    msg = "在防冲突过程中得到错误的序列号.....";
                    break;
                case 0x8C:
                    msg = "密码认证没通过.....";
                    break;
                case 0x8F:
                    msg = "读取器接收到未知命令.....";
                    break;
                case 0x90:
                    msg = "卡不支持这个命令.....";
                    break;
                case 0x91:
                    msg = "命令格式有错误.....";
                    break;
                case 0x92:
                    msg = "在命令的FLAG参数中，不支持OPTION 模式.....";
                    break;
                case 0x93:
                    msg = "要操作的BLOCK不存在.....";
                    break;
                case 0x94:
                    msg = "要操作的对象已经别锁定，不能进行修改.....";
                    break;
                case 0x95:
                    msg = "锁定操作不成功.....";
                    break;
                case 0x96:
                    msg = "写操作不成功.....";
                    break;
                default:
                    msg = "未知错误2.....";
                    break;
            }
            return msg;
        }

        //byte数组转字符串
        private string ConvertData(byte[] data, int start, int length)
        {
            var result = new StringBuilder();
            //非负转换
            for (int i = 0; i < length; i++)
            {
                if (data[start + i] < 0)
                {
                    data[start + i] = Convert.ToByte(Convert.ToInt32(data[start + i]) + 256);
                }
            }


            for (int i = 0; i < length; i++)
            {
                result.Append(data[start + i].ToString("X2") + " ");
            }

            return result.ToString();
        }
    }
}
