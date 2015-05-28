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
		/// �����ݿ�����
		/// </summary>
		public abstract void Open();
		/// <summary>
		/// �ر����ݿ�����
		/// </summary>
		public abstract void Close();
        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public abstract void Dispose();
		/// <summary>
		/// ��������
		/// </summary>
		public abstract void BeginTrans();
		/// <summary>
		/// �ж��Ƿ�����������
		/// </summary>
		/// <returns></returns>
		public abstract bool InTrans();
		/// <summary>
		/// �ύ����
		/// </summary>
		public abstract void CommitTrans();
		/// <summary>
		/// �ع�����
		/// </summary>
		public abstract void RollbackTrans();
		/// <summary>
		/// ִ��SQL�������Ӱ�������
		///</summary>
		/// <param name="commandText">SQL����</param>
		/// <returns>Ӱ�������</returns>
		public abstract int ExecuteNonQuery(string commandText);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandText">SQL����</param>
		/// <param name="ps">����</param>
		/// <returns></returns>
		public abstract int ExecuteNonQuery(string commandText, List<DbParameter> ps);
		/// <summary>
		/// ִ��SQL�������DataSet����
		/// </summary>
		/// <param name="commandText">SQL����</param>
		/// <returns>DataSet����</returns>
		public abstract DataSet ExecuteDataSet(string commandText);
		/// <summary>
		/// ִ��SQL�������DataSet����
		/// </summary>
		/// <param name="commandText">SQL����</param>
		/// <param name="ps">����</param>
		/// <returns></returns>
		public abstract DataSet ExecuteDataSet(string commandText, List<DbParameter> ps);
		/// <summary>
		/// ִ��SQL�������DataTable����
		/// </summary>
		/// <param name="commandText">SQL����</param>
		/// <returns></returns>
		public abstract DataTable ExecuteDataTable(string commandText);
		/// <summary>
		/// ִ��SQL�������DataTable����
		/// </summary>
		/// <param name="commandText">SQL����</param>
		/// <param name="ps">����</param>
		/// <returns></returns>
		public abstract DataTable ExecuteDataTable(string commandText, List<DbParameter> ps);
		/// <summary>
		/// ִ��SQL������ص�һ�е�һ�е�ֵ
		/// </summary>
		/// <param name="commandText">SQL����</param>
		/// <returns></returns>
		public abstract object ExecuteScalar(string commandText);
		/// <summary>
		/// ִ��SQL������ص�һ�е�һ�е�ֵ
		/// </summary>
		/// <param name="commandText">SQL����</param>
		/// <param name="ps">����</param>
		/// <returns></returns>
		public abstract object ExecuteScalar(string commandText, List<DbParameter> ps);
		/// <summary>
		/// ִ�д洢���̣�����Ӱ������
		/// </summary>
		/// <param name="commandText">�洢��������</param>
		/// <returns></returns>
		public abstract int ExecuteProcNonQuery(string commandText);
		/// <summary>
		/// ִ�д洢���̣�����Ӱ������
		/// </summary>
		/// <param name="commandText">�洢��������</param>
		/// <param name="ps">����</param>
		/// <returns></returns>
		public abstract int ExecuteProcNonQuery(string commandText, List<DbParameter> ps);
		/// <summary>
		/// ִ�д洢���̣�����DataTable����
		/// </summary>
		/// <param name="commandText">�洢��������</param>
		/// <returns></returns>
		public abstract DataTable ExecuteProcDataTable(string commandText);
		/// <summary>
		/// ִ�д洢���̣�����DataTable����
		/// </summary>
		/// <param name="commandText">�洢��������</param>
		/// <param name="ps">����</param>
		/// <returns></returns>
		public abstract DataTable ExecuteProcDataTable(string commandText, List<DbParameter> ps);
		/// <summary>
		/// ��ҳ��ȡ���ݣ�����DataTable����
		/// </summary>
		/// <param name="type">ѡ��ͬ�ķ�������ҳ������0=ʹ��row_number()��1=ʹ��DataReader</param>
		/// <param name="commandText">��ȡ���ݼ���SQL</param>
		/// <param name="sortText">�����ı�</param>
		/// <param name="ps">����</param>
		/// <param name="pageSize">һҳ������</param>
		/// <param name="pageIndex">ҳ��</param>
		/// <returns></returns>
		public abstract DataTable PagedDataTable(int type, string commandText, string sortText, List<DbParameter> ps, int pageSize, int pageIndex);
	}
}
