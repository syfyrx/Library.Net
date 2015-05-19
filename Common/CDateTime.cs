using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Net.CLCommon
{
	public static class CDateTime
	{
		/// <summary>
		/// 时间戳转为DateTime格式
		/// </summary>
		/// <param name="timeStamp">Unix时间戳格式</param>
		/// <returns>DateTime格式</returns>
		public static DateTime ConvertToDateTime(string timeStamp)
		{
			DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			long lTime = long.Parse(timeStamp + "0000000");
			TimeSpan toNow = new TimeSpan(lTime);
			return dtStart.Add(toNow);
		}
		/// <summary>
		/// DateTime时间格式转换为Unix时间戳格式
		/// </summary>
		/// <param name="dateTime"> DateTime时间格式</param>
		/// <returns>Unix时间戳格式</returns>
		public static Int64 ConvertToTimeStamp(System.DateTime dateTime)
		{
			System.DateTime startTime = Convert.ToDateTime("1970-01-01");
			return (dateTime.Ticks - startTime.Ticks) / 10000;
		}
		/// <summary>
		/// 时间格式的字符串转换为Unix时间戳格式
		/// </summary>
		/// <param name="dateTime">时间格式字符串</param>
		/// <returns>Unix时间戳格式</returns>
		public static Int64 ConvertToTimeStamp(string dateTime)
		{
			System.DateTime startTime = Convert.ToDateTime("1970-01-01");
			return (Convert.ToDateTime(dateTime).Ticks - startTime.Ticks) / 10000;
		}
	}
}
