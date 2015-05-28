using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Runtime.Remoting;
using System.Reflection;
using System.Collections;

namespace Library.Net.Database
{
	public abstract class Provider
	{
		/// <summary>
		/// 打开数据库连接
		/// </summary>
		public abstract void Open();
		/// <summary>
		/// 关闭数据库连接
		/// </summary>
		public abstract void Close();
        /// <summary>
        /// 释放资源
        /// </summary>
        public abstract void Dispose();
		/// <summary>
		/// 启动事务
		/// </summary>
		public abstract void BeginTrans();
		/// <summary>
		/// 判断是否处理事务处理中
		/// </summary>
		/// <returns></returns>
		public abstract bool InTrans();
		/// <summary>
		/// 提交事务
		/// </summary>
		public abstract void CommitTrans();
		/// <summary>
		/// 回滚事务
		/// </summary>
		public abstract void RollbackTrans();
		/// <summary>
		/// 执行SQL命令，返回影响的行数
		///</summary>
		/// <param name="commandText">SQL命令</param>
		/// <returns>影响的行数</returns>
		public abstract int ExecuteNonQuery(string commandText);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandText">SQL命令</param>
		/// <param name="ps">参数</param>
		/// <returns></returns>
		public abstract int ExecuteNonQuery(string commandText, List<DbParameter> ps);
		/// <summary>
		/// 执行SQL命令，返回DataSet对象
		/// </summary>
		/// <param name="commandText">SQL命令</param>
		/// <returns>DataSet对象</returns>
		public abstract DataSet ExecuteDataSet(string commandText);
		/// <summary>
		/// 执行SQL命令，返回DataSet对象
		/// </summary>
		/// <param name="commandText">SQL命令</param>
		/// <param name="ps">参数</param>
		/// <returns></returns>
		public abstract DataSet ExecuteDataSet(string commandText, List<DbParameter> ps);
		/// <summary>
		/// 执行SQL命令，返回DataTable对象
		/// </summary>
		/// <param name="commandText">SQL命令</param>
		/// <returns></returns>
		public abstract DataTable ExecuteDataTable(string commandText);
		/// <summary>
		/// 执行SQL命令，返回DataTable对象
		/// </summary>
		/// <param name="commandText">SQL命令</param>
		/// <param name="ps">参数</param>
		/// <returns></returns>
		public abstract DataTable ExecuteDataTable(string commandText, List<DbParameter> ps);
		/// <summary>
		/// 执行SQL命令，返回第一行第一列的值
		/// </summary>
		/// <param name="commandText">SQL命令</param>
		/// <returns></returns>
		public abstract object ExecuteScalar(string commandText);
		/// <summary>
		/// 执行SQL命令，返回第一行第一列的值
		/// </summary>
		/// <param name="commandText">SQL命令</param>
		/// <param name="ps">参数</param>
		/// <returns></returns>
		public abstract object ExecuteScalar(string commandText, List<DbParameter> ps);
		/// <summary>
		/// 执行存储过程，返回影响行数
		/// </summary>
		/// <param name="commandText">存储过程名称</param>
		/// <returns></returns>
		public abstract int ExecuteProcNonQuery(string commandText);
		/// <summary>
		/// 执行存储过程，返回影响行数
		/// </summary>
		/// <param name="commandText">存储过程名称</param>
		/// <param name="ps">参数</param>
		/// <returns></returns>
		public abstract int ExecuteProcNonQuery(string commandText, List<DbParameter> ps);
		/// <summary>
		/// 执行存储过程，返回DataTable对象
		/// </summary>
		/// <param name="commandText">存储过程名称</param>
		/// <returns></returns>
		public abstract DataTable ExecuteProcDataTable(string commandText);
		/// <summary>
		/// 执行存储过程，返回DataTable对象
		/// </summary>
		/// <param name="commandText">存储过程名称</param>
		/// <param name="ps">参数</param>
		/// <returns></returns>
		public abstract DataTable ExecuteProcDataTable(string commandText, List<DbParameter> ps);
		/// <summary>
		/// 分页获取数据，返回DataTable对象
		/// </summary>
		/// <param name="type">选择不同的服务器分页方法：0=使用row_number()，1=使用DataReader</param>
		/// <param name="commandText">获取数据集的SQL</param>
		/// <param name="sortText">排序文本</param>
		/// <param name="ps">参数</param>
		/// <param name="pageSize">一页的条数</param>
		/// <param name="pageIndex">页码</param>
		/// <returns></returns>
		public abstract DataTable PagedDataTable(int type, string commandText, string sortText, List<DbParameter> ps, int pageSize, int pageIndex);
	}
}
