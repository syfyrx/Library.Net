using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Net.CLEncryption
{
    /// <summary>
    /// CRC校验相关类
    /// </summary>
    public class CCRC
    {
        /// <summary>
        /// CRC16校验
        /// </summary>
        /// <param name="type">类别（1代表高位在前低位在后，0代表低位在前高位在后）</param>
        /// <param name="data">数据</param>
        /// <returns>添加了CRC校验位的字节数组</returns>
        public static byte[] CRC16_C(int type,byte[] data)
        {
            byte CRC16Lo;   //低位字节
            byte CRC16Hi;   //高位字节 
            byte CL; byte CH;       //多项式码&HA001 
            byte SaveHi; byte SaveLo;
            byte[] tmpData;
            CRC16Lo = 0xFF;
            CRC16Hi = 0xFF;
            CL = 0x01;
            CH = 0xA0;
            tmpData = data;
            byte[] ReturnData = new byte[tmpData.Length+2];
            for (int i = 0; i < tmpData.Length; i++)
            {
                CRC16Lo = (byte)(CRC16Lo ^ tmpData[i]); //每一个数据与CRC寄存器进行异或 
                for (int j= 0; j <= 7; j++)
                {
                    SaveHi = CRC16Hi;
                    SaveLo = CRC16Lo;
                    CRC16Hi = (byte)(CRC16Hi >> 1);      //高位右移一位 
                    CRC16Lo = (byte)(CRC16Lo >> 1);      //低位右移一位 
                    if ((SaveHi & 0x01) == 0x01) //如果高位字节最后一位为1 
                    {
                        CRC16Lo = (byte)(CRC16Lo | 0x80);   //则低位字节右移后前面补1 
                    }             //否则自动补0 
                    if ((SaveLo & 0x01) == 0x01) //如果LSB为1，则与多项式码进行异或 
                    {
                        CRC16Hi = (byte)(CRC16Hi ^ CH);
                        CRC16Lo = (byte)(CRC16Lo ^ CL);
                    }
                }
                ReturnData[i] = tmpData[i];
            }
            //高位在前低位在后
            if (type == 1)
            {
                ReturnData[ReturnData.Length - 2] = CRC16Hi;
                ReturnData[ReturnData.Length - 1] = CRC16Lo;
            }
            //低位在前高位在后
            else if (type == 0)
            {
                ReturnData[ReturnData.Length - 2] = CRC16Lo;
                ReturnData[ReturnData.Length - 1] = CRC16Hi;
            }
            return ReturnData;
        }
    }
}
