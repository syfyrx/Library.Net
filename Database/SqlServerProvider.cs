using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace Library.Net.Database
{
	internal class SqlServerProvider : Provider
	{
		private SqlConnection dbConn;
		private SqlCommand dbCmd;
		private SqlDataAdapter dbDataAdapter;
		private SqlTransaction dbTrans;
		/// <summary>
		/// 连接对象
		/// </summary>
		/// <param name="connectionString">连接字符串</param>
		public SqlServerProvider(string connectionString)
		{
			dbConn = new System.Data.SqlClient.SqlConnection(connectionString);
		}
		public override void Open()
		{
			if (dbConn.State != ConnectionState.Open)
			{
				dbConn.Open();
				dbCmd = dbConn.CreateCommand();
			}
		}
		public override void Close()
		{
			if (dbConn.State != ConnectionState.Closed)
			{
				dbConn.Close();
			}
		}
		public override void BeginTrans()
		{
			if (dbConn.State != ConnectionState.Open)
			{
				Open();
			}
			dbTrans = dbConn.BeginTransaction();
			dbCmd.Transaction = dbTrans;
		}
		public override bool InTrans()
		{
			if (dbTrans == null)
				return false;
			else
				return true;
		}
		public override void CommitTrans()
		{
			dbTrans.Commit();
		}
		public override void RollbackTrans()
		{
			dbTrans.Rollback();
		}
		public override int ExecuteNonQuery(string commandText)
		{
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.Text;
			return dbCmd.ExecuteNonQuery();
		}
		public override int ExecuteNonQuery(string commandText, List<DbParameter> ps)
		{
			dbCmd.Parameters.Clear();
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.Text;
			if (ps != null)
				foreach (SqlParameter p in ps)
				{
					dbCmd.Parameters.Add(p);
				}
			return dbCmd.ExecuteNonQuery();
		}
		public override DataSet ExecuteDataSet(string commandText)
		{
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.Text;
			DataSet ds = new DataSet();
			dbDataAdapter = new SqlDataAdapter();
			dbDataAdapter.SelectCommand = dbCmd;
			dbDataAdapter.Fill(ds);
			return ds;
		}
		public override DataSet ExecuteDataSet(string commandText, List<DbParameter> ps)
		{
			dbCmd.Parameters.Clear();
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.Text;
			if (ps != null)
				foreach (SqlParameter p in ps)
				{
					dbCmd.Parameters.Add(p);
				}
			DataSet ds = new DataSet();
			dbDataAdapter = new SqlDataAdapter();
			dbDataAdapter.SelectCommand = dbCmd;
			dbDataAdapter.Fill(ds);
			return ds;
		}
		public override DataTable ExecuteDataTable(string commandText)
		{
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.Text;
			DataTable dt = new DataTable();
			dbDataAdapter = new SqlDataAdapter();
			dbDataAdapter.SelectCommand = dbCmd;
			dbDataAdapter.Fill(dt);
			return dt;
		}
		public override DataTable ExecuteDataTable(string commandText, List<DbParameter> ps)
		{
			dbCmd.Parameters.Clear();
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.Text;
			if (ps != null)
				foreach (SqlParameter p in ps)
				{
					dbCmd.Parameters.Add(p);
				}
			DataTable dt = new DataTable();
			dbDataAdapter = new SqlDataAdapter();
			dbDataAdapter.SelectCommand = dbCmd;
			dbDataAdapter.Fill(dt);
			return dt;
		}
		public override object ExecuteScalar(string commandText)
		{
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.Text;
			return dbCmd.ExecuteScalar();
		}
		public override object ExecuteScalar(string commandText, List<DbParameter> ps)
		{
			dbCmd.Parameters.Clear();
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.Text;
			if (ps != null)
				foreach (SqlParameter p in ps)
				{
					dbCmd.Parameters.Add(p);
				}
			return dbCmd.ExecuteScalar();
		}
		public override int ExecuteProcNonQuery(string commandText)
		{
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.StoredProcedure;
			return dbCmd.ExecuteNonQuery();
		}
		public override int ExecuteProcNonQuery(string commandText, List<DbParameter> ps)
		{
			dbCmd.Parameters.Clear();
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.StoredProcedure;
			if (ps != null)
				foreach (SqlParameter p in ps)
				{
					dbCmd.Parameters.Add(p);
				}
			return dbCmd.ExecuteNonQuery();
		}
		public override DataTable ExecuteProcDataTable(string commandText)
		{
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.StoredProcedure;
			DataTable dt = new DataTable();
			dbDataAdapter = new SqlDataAdapter();
			dbDataAdapter.SelectCommand = dbCmd;
			dbDataAdapter.Fill(dt);
			return dt;
		}
		public override DataTable ExecuteProcDataTable(string commandText, List<DbParameter> ps)
		{
			dbCmd.Parameters.Clear();
			dbCmd.CommandText = commandText;
			dbCmd.CommandType = CommandType.StoredProcedure;
			if (ps != null)
				foreach (SqlParameter p in ps)
				{
					dbCmd.Parameters.Add(p);
				}
			DataTable dt = new DataTable();
			dbDataAdapter = new SqlDataAdapter();
			dbDataAdapter.SelectCommand = dbCmd;
			dbDataAdapter.Fill(dt);
			return dt;
		}
		public override DataTable PagedDataTable(int type, string commandText, string sortText, List<DbParameter> ps, int pageSize, int pageIndex)
		{
			DataTable dt = new DataTable();
			if (type == 0)//使用row_number()
			{
				string sql = "";
				sql = string.Format(@"select * from (select row_number() over(order by {1}) [RowNumber],* from ({0})a) a where [RowNumber] between {2} and {3};", commandText, sortText, (pageIndex - 1) * pageSize + 1, pageIndex * pageSize);
				dt = ExecuteDataTable(sql, ps);
			}
			else if (type == 1)//使用DataReader
			{
				string sql = commandText + " order by " + sortText;
				dbCmd.Parameters.Clear();
				dbCmd.CommandText = sql;
				dbCmd.CommandType = CommandType.Text;
				if (ps != null)
					foreach (SqlParameter p in ps)
					{
						dbCmd.Parameters.Add(p);
					}
				SqlDataReader reader = dbCmd.ExecuteReader(CommandBehavior.SingleResult);
				int count = 0;
				for (int i = 0; i < reader.FieldCount; i++)
				{
					DataColumn col = new DataColumn();
					col.ColumnName = reader.GetName(i);
					col.DataType = reader.GetFieldType(i);
					dt.Columns.Add(col);
				}
				while (reader.Read())
				{
					count++;
					if (count >= (pageIndex - 1) * pageSize + 1 && count <= pageIndex * pageSize)
					{
						DataRow row = dt.NewRow();
						for (int i = 0; i < reader.FieldCount; i++)
						{
							row[i] = reader[i];
						}
						dt.Rows.Add(row);
					}
				}
				reader.Close();
			}
			return dt;
		}
	}
}
