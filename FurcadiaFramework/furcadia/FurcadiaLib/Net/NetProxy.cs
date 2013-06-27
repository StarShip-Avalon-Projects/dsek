/*Log Header
 *Format: (date,Version) AuthorName, Changes.
 * (?,2007) Kylix, Initial Coder and SimpleProxy project manager
 * (Oct 27,2009) Squizzle, Added NetMessage, delegates, and NetProxy wrapper class.
 * (July 26, 2011) Gerolkae, added setting.ini switch for proxy.ini
 */

using System;
using System.Diagnostics;
using System.Security.Permissions;
//using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using Furcadia.IO;
using System.Net.NetworkInformation;

using System.Timers;
/// using System.Timers.Timer;

namespace Furcadia.Net
{
	
	public class NetProxy
	{

		#region Event Handling
		public delegate void ActionDelegate();
		public delegate string DataEventHandler(string data);
		public delegate void DataEventHandler2(string data);

        public delegate void ErrorEventHandler(Exception e, Object o, String n);
        //public delegate void ErrorEventHandler(Exception e);
		/// <summary>
		///This is triggered when the 
		/// </summary>
		public event ActionDelegate Connected;
		/// <summary>
		///This is triggered when the Server Disconnects
		/// </summary>
		public event ActionDelegate ServerDisConnected;
		/// <summary>
		///This is triggered when the Client Disconnects
		/// </summary>
		public event ActionDelegate ClientDisConnected;


		/// <summary>
		/// This is triggered when the Server sends data to the client. Expects a return Value
		/// </summary>
		public event DataEventHandler ServerData;
		/// <summary>
		/// This is triggered when the Server sends data to the client. Doesn't expect a return value.
		/// </summary>
		public event DataEventHandler2 ServerData2;
		/// <summary>
		/// This is triggered when the Client sends data to the server.
		/// </summary>
		public event DataEventHandler ClientData;
		/// <summary>
		/// This is triggered when the Client sends data to the server. Expects a return value.
		/// </summary>
		public event DataEventHandler2 ClientData2;
		/// <summary>
		/// This is triggered when the user has exited/logoff Furcadia and the Furcadia
		/// client is closed.
		/// </summary>
		public event ActionDelegate ClientExited;

		/// <summary>
		/// This is triggered when a handled Exception is thrown.
		/// </summary>
		public event ErrorEventHandler Error;



		#endregion

		#region Private Declarations
		/// <summary>
		/// Max buffer size
		/// </summary>
		private static int BUFFER_CAP = 2048;
		private static int ENCODE_PAGE = 1252;
		private bool CConnected = false;
		private IPEndPoint _endpoint;
		private TcpClient server;
		private TcpClient client;
		private TcpListener listen;
		private static string[] BackupSettings;
		private byte[] clientBuffer = new byte[BUFFER_CAP], serverBuffer = new byte[BUFFER_CAP];
		private string _ServerLeftOvers;
		private string clientBuild, serverBuild;
		public static int _lport = 6700;
		private int _procID;
		private static System.Timers.Timer NewsTimer;
		private static bool _StandAloneMode = false, Clientflag = true;
		private string _proc = "Furcadia.exe", _procpath, _procCMD ="-pick";

	  
		#endregion
		/// <summary>
		/// Use proxy.ini if it exists. otherwise use settings.ini.
		/// </summary>
		private Boolean UseProxyIni;


		#region Constructors

		public NetProxy()
		{
			string SetPath = Paths.GetLocalSettingsPath();
			string SetFile = "/settings.ini";
			string[] sett = FurcIni.LoadFurcadiaSettings(SetPath,SetFile);
			int port = Convert.ToInt32(FurcIni.GetUserSetting("PreferredServerPort", sett));
			try
			{
				_endpoint = new IPEndPoint(Dns.GetHostEntry(Furcadia.Util.Host).AddressList[0], port);
			}
			catch { }
		}

		public NetProxy(int port)
		{
			try
			{
				_endpoint = new IPEndPoint(Dns.GetHostEntry(Furcadia.Util.Host).AddressList[0], port);
			}
			catch { }
		}

		public NetProxy(int port, int lport)
		{
			_lport = lport;
			try
			{
				_endpoint = new IPEndPoint(Dns.GetHostEntry(Furcadia.Util.Host).AddressList[0], port);
			}
			catch { }
		}

		public NetProxy(string host, int port)
		{
			try
			{
				_endpoint = new IPEndPoint(Dns.GetHostEntry(host).AddressList[0], port);
			}
			catch { }
		}

		public NetProxy(string host, int port, int lport)
		{
			_lport = lport;
			try
			{
				_endpoint = new IPEndPoint(Dns.GetHostEntry(host).AddressList[0], port);
			}
			catch { }
		}

		public NetProxy(IPAddress ip, int port)
		{
			try
			{
				_endpoint = new IPEndPoint(ip, port);
			}
			catch { }
		}
		public NetProxy(IPAddress ip, int port, int lport)
		{
			_lport = lport;
			try
			{
				_endpoint = new IPEndPoint(ip, port);
			}
			catch { }
		}

		public NetProxy(IPEndPoint endpoint, int lport)
		{
			_lport = lport;
			try
			{
				_endpoint = endpoint;
			}
			catch { }
		}

		#endregion

		#region Properties
		
		/// <summary>
		/// Process to start. (default: Furcadia.exe)
		/// </summary>
		public string Process
		{
			get { return _proc;}
			set { _proc=value; }
		}

		/// <summary>
		/// Process ID for closing Furcadia.exe
		/// </summary>
		public int ProcID
		{
			get { return _procID; }
			set { _procID = value; }
		}

		/// <summary>
		/// Process path (default: none)
		/// </summary>
		public string ProcessPath
		{
			get { return _procpath; }
			set { _procpath = value; }
		}
		/// <summary>
		/// Command to pass (default: -pick)
		/// </summary>
		public string ProcessCMD
		{
			get { return _procCMD; }
			set { _procCMD = value; }
		}
		/// <summary>
		/// Proxy is connected, or not.
		/// </summary>
		public bool IsServerConnected
		{
			get {
				if (server != null)
					return server.Connected;
				else
					return false; }
		}

		/// <summary>
		/// Standalone Mode
		/// Keep Connection after Client Closes/Disconnects
		/// </summary>
		public bool StandAloneMode
		{
			get { return _StandAloneMode;  }
			set { _StandAloneMode = value; }
		}

		public bool IsClientConnected
		{
			get {
				if (client != null)
					return client.Connected;
				else
					return false; }
		}
		#endregion
		
		#region Public Static Properties
		public static int BufferCapacity{
			get{
				return BUFFER_CAP;
			}
		}
		
		public static int EncoderPage {
			get{
				return ENCODE_PAGE;
			}
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// Sets the startup Process to the associated file. (default: %FurcadiaInstallPath%/Furcadia.exe)
		/// </summary>
		/// <param name="file">Process full path and file name.</param>
		public void SetProcess(string file)
		{
			if (!File.Exists(file)) throw new FileNotFoundException("File not found.",file);
			ProcessPath = Path.GetDirectoryName(file);
			Process = Path.GetFileName(file);
		}


		/// <summary>
		/// Connects to the Furcadia Server and starts the mini proxy.
		/// </summary>
		public void Connect()
		{
			try
			{
				string proxyIni = "localhost " + _lport.ToString();

				/// UAC Perms Needed to Create proxy.ini
				/// Win 7 Change your UAC Level or add file create Permissions to [%program files%/furcadia]
				/// Maybe Do this at install
				string fIni = Paths.GetInstallPath() + "proxy.ini" ;
				/// Check proxy.ini if it exoists then use it
				/// 
				/// otherwise use settings.ini to avoid UAC issues on %Program Files%
				if (File.Exists(proxyIni))
					File.Delete(proxyIni);
				UseProxyIni = false;
				BackupSettings = Settings.InitializeFurcadiaSettings();

				
				if (listen != null)
				{
					listen.Start();
					listen.BeginAcceptTcpClient(new AsyncCallback(AsyncListener), listen);
				}
				else
				{
					bool isAvailable = true;
					IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
					TcpConnectionInformation[] tcpInfoArray = ipGlobalProperties.GetActiveTcpConnections();

					foreach (TcpConnectionInformation tcpi in tcpInfoArray)
					{
						if (tcpi.LocalEndPoint.Port == this._endpoint.Port)
						{
							isAvailable = false;
							break;
						}
					}
					if (isAvailable)
					{
					   
						listen = new TcpListener(IPAddress.Any, _lport);
						listen.Start();
						listen.BeginAcceptTcpClient(new AsyncCallback(AsyncListener), listen);
					}
					else throw new Exception("Port " + _lport.ToString() + " is being used.");

				}
				
				//Run         
				if (string.IsNullOrEmpty(ProcessPath)) ProcessPath = Paths.GetInstallPath();
				//check ProcessPath is not a directory
				if (!Directory.Exists(ProcessPath)) throw new DirectoryNotFoundException("Process path not found.");
				Directory.SetCurrentDirectory(ProcessPath);
				if (!File.Exists(Path.Combine(ProcessPath,Process))) throw new Exception("Client executable '"+Process+"' not found.");
				System.Diagnostics.Process proc = System.Diagnostics.Process.Start(Process,ProcessCMD );
				proc.EnableRaisingEvents = true;
				proc.Exited += delegate
				{
					if (this.ClientExited != null)
					{
						CConnected = false;
						this.ClientExited();
					}
				 };
				ProcID = proc.Id;
				CConnected = true;
			}
			catch (Exception e) { if (Error != null) Error(e, this, "Connect()"); } //else throw e;
		}


		public void SendClient (INetMessage message)
		{
			SendClient(message.GetString());
		}
		
		public void SendClient (string message)
		{
			try
			{
				if (client.Client != null && client.GetStream().CanWrite == true && client.Connected == true)
					client.GetStream().Write(System.Text.Encoding.GetEncoding(EncoderPage).GetBytes(message), 0, System.Text.Encoding.GetEncoding(EncoderPage).GetBytes(message).Length);
			}
			catch (Exception e) { if (Error != null) Error(e, this, "SendClient()"); }

		}

		public void SendServer (INetMessage message)
		{
			SendServer(message.GetString());
		}

		public void SendServer (string message)
		{
			try
			{
				if (server.GetStream ().CanWrite)
					server.GetStream ().Write (System.Text.Encoding.GetEncoding (EncoderPage).GetBytes (message), 0, System.Text.Encoding.GetEncoding(EncoderPage).GetBytes(message).Length);
			}
			catch (Exception e) { if (Error != null) Error(e, this, "SendServer"); }
		}

		/// <summary>
		/// Closes the Client Connection
		/// </summary>
		public void CloseClient()
		{
			try
			{
				if (listen != null) listen.Stop();
				if (client != null && client.Connected == true)
				{
					NetworkStream clientStream = client.GetStream();
					if (clientStream != null)
					{
						clientStream.Flush();
						clientStream.Dispose();
						clientStream.Close();
					}

					client.Close();
				}
			}
			catch (Exception e) { if (Error != null) Error(e,this, "CloseClient()"); }

		}

		/// <summary>
		/// Terminates the connection.
		/// </summary>
		public void Kill()
		{
			try
			{
			   if (listen != null) listen.Stop();

			   if (client != null && client.Connected == true) 
				{
					NetworkStream clientStream = client.GetStream();
					if (clientStream != null)
					{
						clientStream.Flush();
						clientStream.Dispose();
						clientStream.Close();
					}
                   
					client.Close();
				}

			   if (server != null && server.Connected == true)
			   { 
					NetworkStream serverStream = server.GetStream();
					if (serverStream != null ) 
					{
						serverStream.Flush();
						serverStream.Dispose();
						serverStream.Close();
					}
				   server.Close();
				}
				
				
			}
			catch (Exception e) { if (Error != null) Error(e,this, "Kill()"); }
		}

		//Implement IDisposable.
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Free other state (managed objects).
			}
			// Free your own state (unmanaged objects).
			// Set large fields to null.
		}


		#endregion

		#region Private Methods
		private void AsyncListener(IAsyncResult ar)
		{
			try
			{
				
				try
				{
					client = listen.EndAcceptTcpClient(ar);
				}
				catch (SocketException se) { if (se.ErrorCode > 0) throw se; }
				listen.Stop();
				// Connects to the server
				server = new TcpClient(Util.Host,_endpoint.Port);
				
				if (!server.Connected) throw new Exception("There was a problem connecting to the server.");
				
				client.GetStream().BeginRead(clientBuffer, 0, clientBuffer.Length, new AsyncCallback(GetClientData), client);
				server.GetStream().BeginRead(serverBuffer, 0, serverBuffer.Length, new AsyncCallback(GetServerData), server);

				if (Connected != null)
				{
					Connected();
					Clientflag = true;
					/// Delete proxy.ini or restore settings.ini
					if (UseProxyIni)
					{
						File.Delete(Paths.GetInstallPath() + "proxy.ini");
					}
					else
					{
						/// reset settings.ini
						/// 10second delay timer
						NewsTimer = new System.Timers.Timer();
						NewsTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
						NewsTimer.Enabled = true;
						NewsTimer.Interval = 10000;
						NewsTimer.AutoReset = false;
						///GC.KeepAlive(NewsTimer);
					}
					
				}
			}
			catch (Exception e) { if (Error != null) Error(e,this, "AsyncListener()");}
		}

		private static void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			/// reset settings.ini
			Settings.RestoreFurcadiaSettings(BackupSettings);
		}



		private void GetClientData (IAsyncResult ar)
		{
			try
			{
				if (client.Connected == false)
				{
					throw new SocketException((int)SocketError.NotConnected);
				}
				List<string> lines = new List<string> ();
				//read = number of bytes read
				int read = client.GetStream ().EndRead (ar);
				//ClientBuild is a string containing data read off the clientBuffer
				clientBuild = System.Text.Encoding.GetEncoding (EncoderPage).GetString (clientBuffer, 0, read);
				while (client.GetStream ().DataAvailable) {
					//clientBuffer.Length = NetProxy.BUFFER_CAP

					if (clientBuffer.Length <= 0)
						ClientDisConnected();
					int pos = client.GetStream ().Read (clientBuffer, 0, clientBuffer.Length );
					clientBuild += System.Text.Encoding.GetEncoding(EncoderPage).GetString(clientBuffer, 0, pos);			
				}
				//Every line should end with '\n'
				//Split function removes the last character
					 
				lines.AddRange(clientBuild.Split('\n'));
				for (int i = 0; i < lines.Count  ; i++)
				{
					ClientData2(lines[i]);
					// we want ServerConnected and Check for null data
					// Application may intentionally return ClientData = null
					// to Avoid "Throat Tired" Syndrome. 
					// Let Application handle packet routing.
					if (IsServerConnected == true && ClientData != null )
					{
						INetMessage msg = new NetMessage();
						//Send the line to the ClientData event and write the return value to a new NetMessage
						msg.Write(ClientData(lines[i]));
						//If the NetMessage doesn't have '\n' at the end add it
						//The '\n' separates the server/client protocols
						if (msg.GetString().EndsWith("\n") == false) msg.Write("\n");
						//Send it on it's way...
						SendServer(msg);
					}
			}
			}
			catch (Exception e) 
			{
				if (client.Connected == true) ClientDisConnected();
				if (Error != null) Error(e,this, "GetClientData()"); 
			} // else throw e;
            if (IsClientConnected && clientBuild.Length < 1 || IsClientConnected == false)
                if (ClientDisConnected != null) ClientDisConnected();
			if (client.Connected == true  && clientBuild.Length >= 1)
			{
				client.GetStream().BeginRead(clientBuffer, 0, clientBuffer.Length, new AsyncCallback(GetClientData), client);
			}
			
		}
		
		private void GetServerData (IAsyncResult ar)
		{
			try
			{

				if (IsServerConnected == false)
				{
				   throw new SocketException((int)SocketError.NotConnected);
				}
				else
				{
					List<string> lines = new List<string>();
					int read = server.GetStream().EndRead(ar);
					if (read < 1)
					{
						ServerDisConnected();
                        return;
					}
					//If we have left over data add it to this server build
					if (!string.IsNullOrEmpty(_ServerLeftOvers) && _ServerLeftOvers.Length > 0)
						serverBuild += _ServerLeftOvers;
					serverBuild = System.Text.Encoding.GetEncoding(EncoderPage).GetString(serverBuffer, 0, read);
					while (server.GetStream().DataAvailable == true)
					{
						int pos = server.GetStream().Read(serverBuffer, 0, serverBuffer.Length);
						serverBuild += System.Text.Encoding.GetEncoding(EncoderPage).GetString(serverBuffer, 0, pos);

					}
					lines.AddRange(serverBuild.Split('\n'));
					if (!serverBuild.EndsWith("\n"))
					{
					  _ServerLeftOvers += lines[lines.Count -1] ;
					  lines.RemoveAt(lines.Count - 1);
					}
					 
					for (int i = 0; i < lines.Count; i++)
					{
						ServerData2(lines[i]);
						if (IsClientConnected == true && ServerData != null && 
							ServerData(lines[i]) != "" && ServerData(lines[i]) != null)
						{
							//if (!lines[i].Contains("\n"))
							//{
							//    _ServerLeftOvers += lines[i] + "\n";
							//}
							if (i < lines.Count)
							{
								NetMessage msg = new NetMessage();
								msg.Write( ServerData(lines[i])+ '\n' );
								SendClient(msg);
							}
						}
					}
				}
			}
			catch (Exception e) 
			{
			   // if (IsServerConnected == true) ServerDisConnected();
				if (Error != null) Error(e, this, "GetServerData()"); 
			} //else throw e;
			// Detect if client disconnected
            if (IsServerConnected && serverBuild.Length < 1 || IsServerConnected == false)
                if (ServerDisConnected != null) ServerDisConnected();
			if (IsServerConnected && serverBuild.Length > 0)
				server.GetStream().BeginRead(serverBuffer, 0, serverBuffer.Length, new AsyncCallback(GetServerData), server);
							 
		}
		#endregion
	}
}