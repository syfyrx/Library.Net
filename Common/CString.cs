using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Library.Net.CLCommon
{
    /// <summary>
    /// 字符串操作类
    /// </summary>
    public class CString
    {
        /// <summary>
        /// 将字符串按指定间隔换行
        /// </summary>
        /// <param name="strOld">待处理的字符串</param>
        /// <param name="interval">间隔</param>
        /// <returns></returns>
        public static string LinefeedByInterval(string strOld, int interval)
        {
            try
            {
                StringBuilder strNew = new StringBuilder();
                int count = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(strOld.Length) / interval));
                for (int i = 0; i < count; i++)
                {
                    if ((i + 1) * interval > strOld.Length)
                        strNew.Append(strOld.Substring(i * interval));
                    else
                        strNew.Append(strOld.Substring(i * interval, interval) + Environment.NewLine);
                }
                return strNew.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 将字符串按指定间隔分隔
        /// </summary>
        /// <param name="strOld">要进行处理的字符串</param>
        /// <param name="interval">间隔</param>
        /// <returns></returns>
        public static string[] SplitByInterval(string strOld, int interval)
        {
            try
            {
                string strNew = "", str = "";
                int count = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(strOld.Length) / interval));
                str = strOld;
                for (int i = 0; i < count; i++)
                {
                    if (str.Length > interval)
                    {
                        strNew += str.Substring(0, interval) + ";";
                        str = str.Substring(interval, str.Length - interval);
                    }
                    else
                        strNew += str;
                }
                return strNew.Split(';');
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
