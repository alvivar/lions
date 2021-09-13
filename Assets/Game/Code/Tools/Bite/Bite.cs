using System;
using System.Net.Sockets;

namespace BiteServer
{
    public sealed class Bite
    {
        internal event Action<string> DataReceived;

        internal TcpClient client;
        internal NetworkStream stream;
        internal Sender sender;
        internal Receiver receiver;

        internal Bite(string host, int port)
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

        internal void OnDataReceived(string data)
        {
            if (DataReceived != null)
                DataReceived(data);
        }

        internal void Stop()
        {
            client.Close();
            stream.Close();
            sender.Stop();
            receiver.Stop();
        }
    }
}