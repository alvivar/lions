using System;
using System.Diagnostics;
using System.Net.Sockets;

namespace Bite2
{
    public sealed class Bite2
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private Receiver _receiver;
        private Sender _sender;

        // Consumers register to receive data.
        public event EventHandler<DataReceivedEventArgs> DataReceived;

        public Bite2()
        {
            _client = new TcpClient("142.93.180.20", 1986);
            _stream = _client.GetStream();

            _receiver = new Receiver(_stream);
            _sender = new Sender(_stream);

            _receiver.DataReceived += OnDataReceived;
        }

        // Called by producers to send data over the socket.
        public void SendData(byte[] data)
        {
            _sender.SendData(data);
        }

        private void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            var handler = DataReceived;
            if (handler != null) DataReceived(this, e); // re-raise event
        }
    }
}