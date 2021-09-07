using System;
using System.Net.Sockets;

namespace BiteServer
{
    public sealed class Bite
    {
        private TcpClient client;
        private NetworkStream stream;
        private Receiver receiver;
        private Sender sender;

        public event Action<string> DataReceived;

        public Bite()
        {
            client = new TcpClient("142.93.180.20", 1986);
            stream = client.GetStream();

            receiver = new Receiver(stream);
            sender = new Sender(stream);

            receiver.DataReceived += OnDataReceived;
        }

        public void Send(string data)
        {
            sender.Send(data);
        }

        private void OnDataReceived(string data)
        {
            if (DataReceived != null)
                DataReceived(data); // re-raise event
        }
    }
}