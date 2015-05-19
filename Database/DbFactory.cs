using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Net.Database
{
	public class DbFactory
	{
		private DbFactory()
		{
		}
		/// <summary>
		/// 获取数据库访问对象
		/// </summary>
		/// <param name="pProvider">数据库访问类型</param>
		/// <param name="pConnString">数据库连接字串</param>
		/// <returns>数据访问对象</returns>
		public static Provider GetInstance(DbProvider mDbProvider, string mConnectionString)
		{
			Provider provider = null;
			switch (mDbProvider)
			{
				case DbProvider.SqlServer:
					provider = new SqlServerProvider(mConnectionString);
					break;
				case DbProvider.SQLite:
					break;
				case DbProvider.MySQL:
					break;
				default:
					break;
			}
			return provider;
		}
	}
	/// <summary>
	/// 数据库类型列表
	/// </summary>
	public enum DbProvider
	{
		SqlServer,
		SQLite,
		MySQL
	}
}
