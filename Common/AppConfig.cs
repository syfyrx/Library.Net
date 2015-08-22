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
        /// 读取配置文件AppSettings节点的某项值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string GetAppSettings(string key)
        {
            string res = null;
            foreach (string strKey in ConfigurationManager.AppSettings)
            {
                if (strKey == key)
                {
                    res = ConfigurationManager.AppSettings[key];
                    break;
                }
            }
            return res;
        }
        /// <summary>
        /// 设置配置文件AppSettings节点的某项值
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public static void SetAppSettings(string key, string value)
        {
            bool isModified = false;
            foreach (string strKey in ConfigurationManager.AppSettings)
            {
                if (strKey == key)
                {
                    isModified = true;
                }
            }
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (isModified)
            {
                config.AppSettings.Settings.Remove(key);
            }
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// 读取配置文件ConnectionStrings节点的某项值
        /// </summary>
        /// <param name="name">name</param>
        /// <returns></returns>
        public static string GetConnectionStrings(string name)
        {
            string res = null;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.ConnectionStrings.ConnectionStrings[name] != null)
            {
                res = config.ConnectionStrings.ConnectionStrings[name].ConnectionString;
            }
            return res;
        }
        ///<summary> 
        ///设置配置文件ConnectionStrings节点的某项值
        ///</summary> 
        ///<param name="name">连接字符串名称</param> 
        ///<param name="connectionString">连接字符串内容</param> 
        ///<param name="providerName">数据提供程序名称</param> 
        private static void SetConnectionStrings(string name, string connectionString, string providerName)
        {
            bool isModified = false;
            if (ConfigurationManager.ConnectionStrings[name] != null)
            {
                isModified = true;
            }
            //新建一个连接字符串实例
            ConnectionStringSettings newSettings = new ConnectionStringSettings(name, connectionString, providerName);
            // 打开可执行的配置文件*.exe.config
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // 如果连接串已存在，首先删除它
            if (isModified)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(name);
            }
            // 将新的连接串添加到配置文件中.  
            config.ConnectionStrings.ConnectionStrings.Add(newSettings);
            // 保存对配置文件所作的更改  
            config.Save(ConfigurationSaveMode.Modified);
            // 强制重新载入配置文件的ConnectionStrings配置节  
            ConfigurationManager.RefreshSection("ConnectionStrings");
        }
    }
}
