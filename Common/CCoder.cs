using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Net.CLCommon
{
	public class CCoder
	{
		/// <summary>
		/// 编码方式
		/// </summary>
		private CodingMethod m_codingMethod;
		public CCoder(CodingMethod codingMethod)
		{
			m_codingMethod = codingMethod;
		}
		public enum CodingMethod
		{
			Default = 0,
			ASCII = 1,
			Unicode = 2,
			UTF8 = 3,
			/// <summary>
			/// 十六进制
			/// </summary>
			Hex = 4,
			/// <summary>
			/// 十进制
			/// </summary>
			Dec = 5,
			/// <summary>
			/// 八进制
			/// </summary>
			Oct = 6,
			/// <summary>
			/// 二进制
			/// </summary>
			Bin = 7
		}
		/// <summary>
		/// 数据解码
		/// </summary>
		/// <param name="bytes">要解码的字节数组</param>
		/// <param name="index">第一个要解码的字节的索引</param>
		/// <param name="count">要解码的字节数</param>
		/// <returns>解码后的数据</returns>
		public string GetDecodeString(byte[] bytes, int index, int count)
		{
			string result = "";
			switch (m_codingMethod)
			{
				case CodingMethod.Default:
					{
						result = Encoding.Default.GetString(bytes, index, count);
						break;
					}
				case CodingMethod.ASCII:
					{
						result = Encoding.ASCII.GetString(bytes, index, count);
						break;
					}
				case CodingMethod.Unicode:
					{
						result = Encoding.Unicode.GetString(bytes, index, count);
						break;
					}
				case CodingMethod.UTF8:
					{
						result = Encoding.UTF8.GetString(bytes, index, count);
						break;
					}
				case CodingMethod.Hex://十六进制
					{
						for (int i = 0; i < bytes.Length; i++)
						{
							result += bytes[i].ToString("X2") + " ";
						}
						break;
					}
				case CodingMethod.Dec://十进制
					{
						for (int i = 0; i < bytes.Length; i++)
						{
							result += Convert.ToString(bytes[i], 10) + " ";
						}
						break;
					}
				case CodingMethod.Oct://八进制
					{
						for (int i = 0; i < bytes.Length; i++)
						{
							result += Convert.ToString(bytes[i], 8) + " ";
						}
						break;
					}
				case CodingMethod.Bin://二进制
					{
						for (int i = 0; i < bytes.Length; i++)
						{
							result += Convert.ToString(bytes[i], 2) + " ";
						}
						break;
					}
				default:
					break;
			}
			return result;
		}
		/// <summary>
		/// 数据编码
		/// </summary>
		/// <param name="s">要编码的字符串</param>
		/// <returns>编码后的数据</returns>
		public virtual byte[] GetEncodeBytes(string s)
		{
			byte[] result = null;
			switch (m_codingMethod)
			{
				case CodingMethod.Default:
					{
						result = Encoding.Default.GetBytes(s);
						break;
					}
				case CodingMethod.ASCII:
					{
						result = Encoding.ASCII.GetBytes(s);
						break;
					}
				case CodingMethod.Unicode:
					{
						result = Encoding.Unicode.GetBytes(s);
						break;
					}
				case CodingMethod.UTF8:
					{
						result = Encoding.UTF8.GetBytes(s);
						break;
					}
				case CodingMethod.Hex:
					{
						s = s.Replace(" ", "");
						if ((s.Length % 2) != 0)
							s += "F";
						result = new byte[s.Length / 2];
						for (int i = 0; i < result.Length; i++)
							result[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
						break;
					}
				case CodingMethod.Dec://十进制
					{
						s = s.Replace(" ", "");
						result = new byte[s.Length];
						for (int i = 0; i < result.Length; i++)
							result[i] = Convert.ToByte(s.Substring(i, 1), 10);
						break;
					}
				case CodingMethod.Oct://八进制
					{
						s = s.Replace(" ", "");
						result = new byte[s.Length];
						for (int i = 0; i < result.Length; i++)
							result[i] = Convert.ToByte(s.Substring(i, 1).ToString(), 8);
						break;
					}
				case CodingMethod.Bin://二进制
					{
						s = s.Replace(" ", "");
						result = new byte[s.Length];
						for (int i = 0; i < result.Length; i++)
							result[i] = Convert.ToByte(s.Substring(i, 1).ToString(), 2);
						break;
					}
				default:
					break;
			}
			return result;
		}
	}
}
