using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Library.Net.Common
{
    public static class WebConfig
    {
        /// <summary>
        /// 读取配置文件AppSettings里某项的值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 读取配置文件ConnectionStrings里某项的值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetConnectionStrings(string key)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
