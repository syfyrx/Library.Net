using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Library.Net.CLCommon
{
    /// <summary>
    /// 硬件相关类
    /// </summary>
    public class CHardware
    {
        #region 获取CPU序列号代码
        /// <summary>
        /// 获取CPU序列号代码
        /// </summary>
        /// <returns>CPU信息</returns>
        public static List<string> GetCPUInfo()
        {
            try
            {
                List<string> cpuInfo = new List<string>();
                ManagementClass cimobject = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = cimobject.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo.Add(mo.Properties["ProcessorId"].Value.ToString());
                }
                return cpuInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取网上硬件地址
        public static List<string> GetNetworkCardInfo()
        {
            try
            {
                List<string> networkCardInfo = new List<string>();
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                        networkCardInfo.Add(mo["MacAddress"].ToString());
                    mo.Dispose();
                }
                return networkCardInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取硬盘ID
        public static List<string> GetHarddiskInfo()
        {
            try
            {
                List<string> harddiskInfo = new List<string>();
                ManagementClass cimobject = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = cimobject.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    harddiskInfo.Add(mo.Properties["Model"].Value.ToString());
                }
                return harddiskInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
