using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Library.Net.Common
{
	public static class AppConfig
	{
		/// <summary>
		/// 设置配置文件AppSettings节点的某项值
		/// </summary>
		/// <param name="key">Key</param>
		/// <param name="value">Value</param>
		public static void SetAppSettings(string key, string value)
		{
			try
			{
				Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				if (config.AppSettings.Settings[key] != null)
					config.AppSettings.Settings[key].Value = value;
				else
					config.AppSettings.Settings.Add(key, value);
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings");
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// 读取配置文件AppSettings节点的某项值
		/// </summary>
		/// <param name="key">key</param>
		/// <returns></returns>
		public static string GetAppSettings(string key)
		{
			string result = "";
			try
			{
				Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				if (config.AppSettings.Settings[key] != null)
				{
					result = config.AppSettings.Settings[key].Value;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
		/// <summary>
		/// 读取配置文件ConnectionStrings节点的某项值
		/// </summary>
		/// <param name="name">name</param>
		/// <returns></returns>
		public static string GetConnectionStrings(string name)
		{
			string result = "";
			try
			{
				Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				if (config.ConnectionStrings.ConnectionStrings[name] != null)
				{
					result = config.ConnectionStrings.ConnectionStrings[name].ConnectionString;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return result;
		}
	}
}
