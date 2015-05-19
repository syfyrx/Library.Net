using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace Library.Net.Sockets
{
	/// <summary>
	/// 同步TCPSocket服务器端
	/// 使用方法：
	///     1. 定义变量
	///         private Library.Net.Socket.SyncTCPSocketServer m_TcpServerSocket = null;
	///     2. 实例化对象
	///         m_TcpServerSocket = new Library.Net.Socket.SyncTCPSocketServer(null, port, TCPServerReceiveData, TCPClientOnline);
	///     3. 定义函数
	///         private void TCPServerReceiveData(byte[] dataBuffer,IPEndPoint ipe)
	///         {
	///         
	///         }
	///         private void TCPClientOnline(IPEndPoint ipe, bool isOnline)
	///         {
	///         
	///         }
	/// </summary>
	public class SyncTCPSocketServer
	{
		//服务端接收数据回调事件
		public delegate void TCPServerReceiveData(byte[] data, IPEndPoint ipe);
		public static TCPServerReceiveData m_tcpServerReceiveDataDelegate = null;

		//客户端通讯回调事件
		public delegate void TCPClientCommunicate(IPEndPoint ipe, bool isOnline);
		public static TCPClientCommunicate m_tcpClientCommunicateDelegate = null;

		private string m_ipAddress = "";//IP地址
		private int m_port = -1;//端口号
		private Socket m_serverSocket = null;//服务器端Socket
		private Thread m_serverThread = null;//服务器端线程
		private int m_iListenCount = 1024;//监听个数
		private List<TCPAcceptClient> m_listTCPAcceptClient = new List<TCPAcceptClient>();//客户端列表

		#region 构造函数和析构函数
		public SyncTCPSocketServer(string ipAddress, int port, TCPServerReceiveData tcpServerReceiveDataDelegate, TCPClientCommunicate tcpClientCommunicateDelegate)
		{
			this.m_ipAddress = ipAddress;
			this.m_port = port;
			m_tcpServerReceiveDataDelegate = tcpServerReceiveDataDelegate;
			m_tcpClientCommunicateDelegate = tcpClientCommunicateDelegate;
			m_serverThread = new Thread(new ThreadStart(ServerStart));
			m_serverThread.Priority = ThreadPriority.BelowNormal;
			m_serverThread.IsBackground = true;
			m_serverThread.Start();
		}
		~SyncTCPSocketServer()
		{
			Dispose();
		}
		public void Dispose()
		{
			//先杀死服务器线程
			if (m_serverThread != null)
			{
				IPAddress ip = IPAddress.Parse("127.0.0.1");
				Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				clientSocket.Connect(new IPEndPoint(ip, m_port)); //配置服务器IP与端口
				m_serverThread.Join();
				m_serverThread.Abort();
				m_serverThread = null;
				for (int i = 0; i < m_listTCPAcceptClient.Count; i++)
				{
					m_listTCPAcceptClient[i].Dispose();
					m_listTCPAcceptClient.RemoveAt(i);
					i--;
				}
				if (m_serverSocket != null)
				{
					m_serverSocket.Close();
					m_serverSocket.Dispose();
					m_serverSocket = null;
				}
			}
		}
		#endregion

		/// <summary>
		/// TCP的Listen线程
		/// </summary>
		private void ServerStart()
		{
			try
			{
				Socket socket = null;
				if (m_serverSocket != null)
				{
					if (m_serverSocket.IsBound)
					{
						m_serverSocket.Close(100);
						m_serverSocket = null;
					}
				}
				m_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				//IPAddress ipAddress = null;
				//if (IPAddress.TryParse(m_ipAddress, out ipAddress))
				//{
				//    m_serverSocket.Bind(new IPEndPoint(ipAddress, m_port));// listen a specified IP
				//}
				m_serverSocket.Bind(new IPEndPoint(IPAddress.Any, m_port));// listen localhost any IP
				m_serverSocket.Listen(m_iListenCount);
				while (true)
				{
					socket = m_serverSocket.Accept();
					if ((socket.LocalEndPoint as IPEndPoint).Address.ToString() != "127.0.0.1")
					{
						if (socket.Connected)
						{
							TCPAcceptClient tcpAcceptClient = new TCPAcceptClient(socket, TCPAcceptClientOffline);
							m_listTCPAcceptClient.Add(tcpAcceptClient);
						}
					}
					else
					{
						break;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		/// <summary>
		/// 发送消息给所有的客户端
		/// </summary>
		/// <param name="bysMsg"></param>
		public void ServerSendMsg(byte[] bysMsg)
		{
			foreach (TCPAcceptClient t in m_listTCPAcceptClient)
			{
				if (t.m_isOnline)
				{
					t.SendMsg(bysMsg);
				}
			}
		}
		/// <summary>
		/// 发送消息给指定的客户端
		/// </summary>
		/// <param name="bysMsg">发送的内容</param>
		/// <param name="ipe">IP和端口号</param>
		public void ServerSendMsg(byte[] bysMsg, IPEndPoint ipe)
		{
			foreach (TCPAcceptClient t in m_listTCPAcceptClient)
			{
				if (t.m_isOnline && t.m_clientIPEndPoint.Equals(ipe))
				{
					t.SendMsg(bysMsg);
					break;
				}
			}
		}
		private void TCPAcceptClientOffline(IPEndPoint ipe)
		{
			for (int i = 0; i < m_listTCPAcceptClient.Count; i++)
			{
				if (m_listTCPAcceptClient[i].m_clientIPEndPoint.Equals(ipe))
				{
					TCPAcceptClient t = m_listTCPAcceptClient[i];
					m_listTCPAcceptClient.RemoveAt(i);
					if (m_tcpClientCommunicateDelegate != null && t.m_clientIPEndPoint != null)
					{
						m_tcpClientCommunicateDelegate(t.m_clientIPEndPoint, false);
					}
					t.Dispose();
					break;
				}
			}
		}
		#region Accept客户端类
		internal class TCPAcceptClient
		{
			public delegate void TCPAcceptClientOffline(IPEndPoint ipe);
			private TCPAcceptClientOffline m_tcpAcceptClientOfflineDelegate = null;
			public IPEndPoint m_clientIPEndPoint = null;//客户端的IP地址和端口
			public bool m_isOnline = true;//客户端连接状态
			private Socket m_clientSocket = null;//客户端Socket
			private Thread m_clientThread = null;//客户端线程
			private NetworkStream m_nsRead = null;//客户端Socket读
			private NetworkStream m_nsWrite = null;//客户端Socket写
			private byte[] m_bysReceive = new byte[64 * 1024];//数据缓冲区
			private int m_iMaxSendLen = 63 * 1024;

			#region 构造函数和析构函数
			public TCPAcceptClient(Socket socket)
			{
				m_clientSocket = socket;
				m_clientIPEndPoint = m_clientSocket.RemoteEndPoint as IPEndPoint;
				m_nsRead = new NetworkStream(m_clientSocket);
				m_nsWrite = new NetworkStream(m_clientSocket);
				m_clientThread = new Thread(new ThreadStart(AcceptMsg));
				m_clientThread.Priority = ThreadPriority.AboveNormal;
				m_clientThread.IsBackground = true;
				m_clientThread.Start();
			}
			public TCPAcceptClient(Socket socket, TCPAcceptClientOffline tcpAcceptClientOnfflineDelegate)
			{
				m_tcpAcceptClientOfflineDelegate = tcpAcceptClientOnfflineDelegate;
				m_clientSocket = socket;
				m_clientIPEndPoint = m_clientSocket.RemoteEndPoint as IPEndPoint;
				m_nsRead = new NetworkStream(m_clientSocket);
				m_nsWrite = new NetworkStream(m_clientSocket);
				m_clientThread = new Thread(new ThreadStart(AcceptMsg));
				m_clientThread.Priority = ThreadPriority.AboveNormal;
				m_clientThread.IsBackground = true;
				m_clientThread.Start();
			}
			~TCPAcceptClient()
			{
				Dispose();
			}
			public void Dispose()
			{
				if (m_nsRead != null)
				{
					m_nsRead.Close();
					m_nsRead.Dispose();
					m_nsRead = null;
				}
				if (m_nsWrite != null)
				{
					m_nsWrite.Close();
					m_nsWrite.Dispose();
					m_nsWrite = null;
				}
				if (m_clientThread != null)
				{
					m_clientThread.Abort();
					m_clientThread = null;
					if (m_clientSocket != null)
					{
						m_clientSocket.Close();
						m_clientSocket.Dispose();
						m_clientSocket = null;
					}
				}
			}
			#endregion

			/// <summary>
			/// 发送
			/// </summary>
			/// <param name="bysMsg">字节数组</param>
			public void SendMsg(byte[] bysMsg)
			{
				int iSendLen = bysMsg.Length;
				int k = -1;
				try
				{
					do
					{
						k++;
						m_nsWrite.Write(bysMsg, k * m_iMaxSendLen, iSendLen > m_iMaxSendLen ? m_iMaxSendLen : iSendLen);
						if (iSendLen > m_iMaxSendLen)
						{
							Thread.Sleep(0);
						}
						iSendLen -= m_iMaxSendLen;
					}
					while (iSendLen > 0);
				}
				catch
				{
					if (m_tcpAcceptClientOfflineDelegate != null && m_clientIPEndPoint != null)
					{
						m_tcpAcceptClientOfflineDelegate(m_clientIPEndPoint);
					}
				}
			}
			private void AcceptMsg()
			{
				//回调函数，实时通知客户端掉线事件
				try
				{
					if (m_tcpClientCommunicateDelegate != null && m_clientIPEndPoint != null)
					{
						try
						{
							m_tcpClientCommunicateDelegate(m_clientIPEndPoint, true);
						}
						catch
						{
						}
					}
					while (true)
					{
						if (m_nsRead.CanRead)
						{
							do
							{
								int iLen = 0;
								iLen = m_nsRead.Read(m_bysReceive, 0, m_bysReceive.Length);
								if (iLen > 0)
								{
									if (m_tcpServerReceiveDataDelegate != null)
									{
										byte[] byTemp = new byte[iLen];
										Array.Copy(m_bysReceive, byTemp, iLen);
										try
										{
											m_tcpServerReceiveDataDelegate(byTemp, m_clientIPEndPoint);
										}
										catch
										{
										}
									}
								}
								else
								{
									if (m_tcpAcceptClientOfflineDelegate != null && m_clientIPEndPoint != null)
									{
										m_tcpAcceptClientOfflineDelegate(m_clientIPEndPoint);
									}
								}
							} while (m_nsRead.DataAvailable);
						}
					}
				}
				catch
				{
					m_isOnline = false;
					if (m_tcpAcceptClientOfflineDelegate != null && m_clientIPEndPoint != null)
					{
						m_tcpAcceptClientOfflineDelegate(m_clientIPEndPoint);
					}
				}
			}
		}
		#endregion
	}
}
