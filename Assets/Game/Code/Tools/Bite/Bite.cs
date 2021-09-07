using System;
using System.Net.Sockets;

namespace BiteServer
{
    public sealed class Bite
    {
        private TcpClient client;
        private NetworkStream stream;
        private Sender sender;
        private Receiver receiver;

        public event Action<string> DataReceived;

        public Bite(string host, int port)
        {
            client = new TcpClient(host, port);
            stream = client.GetStream();

            sender = new Sender(stream);

            receiver = new Receiver(stream);
            receiver.DataReceived += OnDataReceived;
        }

        public void Send(string data, Action<string> action = null)
        {
            sender.Send(data);

            if (action != null)
                receiver.React(action);
        }

        private void OnDataReceived(string data)
        {
            if (DataReceived != null)
                DataReceived(data); // re-raise event
        }
    }
}