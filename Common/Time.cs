using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Net.Common
{
    public class Time
    {
        /// <summary> 
        /// 获取Unix时间戳 
        /// </summary> 
        /// <returns></returns> 
        public static int GetTimeStamp()
        {
            return ConvertDateTimeInt(DateTime.UtcNow);
        }
        /// <summary>
        /// Unix时间戳格式转为DateTime时间格式
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}
