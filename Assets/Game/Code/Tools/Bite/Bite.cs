using System;
using System.Net.Sockets;

namespace BiteClient
{
    public sealed class Bite
    {
        internal bool Connected { get { return client.Connected; } }
        internal event Action<byte[]> DataReceived;

        private TcpClient client;
        private NetworkStream stream;
        private Sender sender;
        private Receiver receiver;

        internal Bite(string host, int port)
        {
            client = new TcpClient(host, port);
            stream = client.GetStream();

            try
            {
                sender = new Sender(stream);
                receiver = new Receiver(stream);
                receiver.DataReceived += OnDataReceived;
            }
            catch (SocketException e)
            {
                Close();
                Console.WriteLine(e);
            }
        }

        internal void Send(string data, Action<byte[]> action = null)
        {
            if (!client.Connected)
                throw new SocketException((int)SocketError.NotConnected);

            sender.Send(data);

            if (action != null)
                receiver.React(action);
        }

        internal void Close()
        {
            client.Close();
            stream.Close();

            sender.Abort();
            receiver.Abort();
        }

        private void OnDataReceived(byte[] data)
        {
            if (DataReceived != null)
                DataReceived(data);
        }
    }
}