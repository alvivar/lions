using System;
using System.Net.Sockets;

namespace Bite2
{
    public sealed class Bite2
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private Receiver _receiver;
        private Sender _sender;

        public event Action<string> DataReceived;

        public Bite2()
        {
            _client = new TcpClient("142.93.180.20", 1986);
            _stream = _client.GetStream();

            _receiver = new Receiver(_stream);
            _sender = new Sender(_stream);

            _receiver.DataReceived += OnDataReceived;
        }

        public void SendData(byte[] data)
        {
            _sender.SendData(data);
        }

        private void OnDataReceived(string data)
        {
            if (DataReceived != null)
                DataReceived(data); // re-raise event
        }
    }
}