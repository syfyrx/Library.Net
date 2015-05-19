﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Net.CLCommon
{
	public class CHashFile
	{
		/// <summary>
		/// 计算文件的 MD5 值
		/// </summary>
		/// <param name="fullFileName">要计算 MD5 值的路径和文件名</param>
		/// <returns>MD5 值16进制字符串</returns>
		public static string MD5File(string fullFileName)
		{
			return HashFile(fullFileName, "md5");
		}
		/// <summary>
		/// 计算文件的 SHA1 值
		/// </summary>
		/// <param name="fullFileName">要计算 SHA1 值的路径和文件名</param>
		/// <returns>SHA1 值16进制字符串</returns>
		public static string SHA1File(string fullFileName)
		{
			return HashFile(fullFileName, "sha1");
		}
		/// <summary>
		/// 计算文件的哈希值
		/// </summary>
		/// <param name="fullFileName">要计算哈希值的路径和文件名</param>
		/// <param name="algName">算法:sha1,md5</param>
		/// <returns>哈希值16进制字符串</returns>
		private static string HashFile(string fullFileName, string algName)
		{
			if (!System.IO.File.Exists(fullFileName))
				return string.Empty;
			System.IO.FileStream fs = new System.IO.FileStream(fullFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			byte[] hashBytes = HashData(fs, algName);
			fs.Close();
			return ByteArrayToHexString(hashBytes);
		}
		/// <summary>
		/// 计算哈希值
		/// </summary>
		/// <param name="stream">要计算哈希值的 Stream</param>
		/// <param name="algName">算法:sha1,md5</param>
		/// <returns>哈希值字节数组</returns>
		private static byte[] HashData(System.IO.Stream stream, string algName)
		{
			System.Security.Cryptography.HashAlgorithm algorithm;
			if (algName == null)
			{
				throw new ArgumentNullException("algName 不能为 null");
			}
			if (string.Compare(algName, "sha1", true) == 0)
			{
				algorithm = System.Security.Cryptography.SHA1.Create();
			}
			else
			{
				if (string.Compare(algName, "md5", true) != 0)
				{
					throw new Exception("algName 只能使用 sha1 或 md5");
				}
				algorithm = System.Security.Cryptography.MD5.Create();
			}
			return algorithm.ComputeHash(stream);
		}
		/// <summary>
		/// 字节数组转换为16进制表示的字符串
		/// </summary>
		private static string ByteArrayToHexString(byte[] buf)
		{
			return BitConverter.ToString(buf).Replace("-", "");
		}
	}
}
