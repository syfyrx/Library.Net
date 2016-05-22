using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Library.Net.Common
{
	/// <summary>
	/// 用户输入验证类
	/// </summary>
	public class Validate
	{
		/// <summary>
		/// 匹配由字母、数字、下划线、汉字组成的字符串
		/// </summary>
		/// <param name="str">进行匹配的字符串</param>
		/// <returns>true:匹配成功;false:匹配失败</returns>
		public static bool RegexCommonString(string str)
		{
			Regex exStr = new Regex(@"^[\da-zA-Z_\u4e00-\u9fa5]+$");
			if (exStr.Match(str).Success)
				return true;
			else
				return false;
		}
		/// <summary>
		/// 匹配数字字符串
		/// </summary>
		/// <param name="str">进行匹配的字符串</param>
		/// <returns>true:匹配成功;false:匹配失败</returns>
		public static bool RegexNumberString(string str)
		{
			Regex exStr = new Regex(@"^-?[0-9]*\.?[0-9]+$");
			if (exStr.Match(str).Success)
				return true;
			else
				return false;
		}
		/// <summary>
		/// 匹配数字、字母组成的字符串
		/// </summary>
		/// <param name="str">进行匹配的字符串</param>
		/// <returns>true:匹配成功;false:匹配失败</returns>
		public static bool RegexNumberLetterString(string str)
		{
			Regex exStr = new Regex(@"^[\da-zA-z]+$");
			if (exStr.Match(str).Success)
				return true;
			else
				return false;
		}
		/// <summary>
		/// 匹配字母组成的字符串
		/// </summary>
		/// <param name="str">进行匹配的字符串</param>
		/// <returns>true:匹配成功;false:匹配失败</returns>
		public static bool RegexLetterString(string str)
		{
			Regex exStr = new Regex(@"^[a-zA-Z]+$");
			if (exStr.Match(str).Success)
				return true;
			else
				return false;
		}
		/// <summary>
		/// 匹配大写字母组成的字符串
		/// </summary>
		/// <param name="str">进行匹配的字符串</param>
		/// <returns>true:匹配成功;false:匹配失败</returns>
		public static bool RegexUpperLetterString(string str)
		{
			Regex exStr = new Regex(@"^[A-Z]+$");
			if (exStr.Match(str).Success)
				return true;
			else
				return false;
		}
		/// <summary>
		/// 匹配小写字母组成的字符串
		/// </summary>
		/// <param name="str">进行匹配的字符串</param>
		/// <returns>true:匹配成功;false:匹配失败</returns>
		public static bool RegexLowerLetterString(string str)
		{
			Regex exStr = new Regex(@"^[a-z]+$");
			if (exStr.Match(str).Success)
				return true;
			else
				return false;
		}
		/// <summary>
		/// 匹配整数的字符串
		/// </summary>
		/// <param name="str">进行匹配的字符串</param>
		/// <returns>true:匹配成功;false:匹配失败</returns>
		public static bool RegexIntegerString(string str)
		{
			Regex exStr = new Regex(@"^-?[1-9]\d*$");
			if (exStr.Match(str).Success)
				return true;
			else
				return false;
		}
		/// <summary>
		/// 匹配正整数的字符串
		/// </summary>
		/// <param name="str">进行匹配的字符串</param>
		/// <returns>true:匹配成功;false:匹配失败</returns>
		public static bool RegexPlusNumberString(string str)
		{
			Regex exStr = new Regex(@"^\d+$");
			if (exStr.Match(str).Success)
				return true;
			else
				return false;
		}
	}
}
