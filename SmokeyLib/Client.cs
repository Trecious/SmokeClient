using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using SomkeClient.BackEnd;

namespace SmokeyLib
{
    class Client
    {
        private ServerProvider _serverProvider;
        private IPEndPoint _cmServer;

        private Socket _socket;
        private NetworkStream _socketStream;
        private StreamWriter _socketWriter;

        public Client()
        {
            _serverProvider = new ServerProvider();
            _cmServer = _serverProvider.GetNextCmServer();
        }

        public void Connect()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = 5000,
                SendTimeout = 5000
            };
        }

        public bool Send(object data)
        {
            if (_socket == null || _socketStream == null || _socketWriter == null)
                return false;

            try
            {
                _socketWriter.Write(data.);
            }
            catch (IOException)
            {
                return false;
            }

        }
    }
}
