using System;
using System.Net.Sockets;

namespace BiteClient
{
    public sealed class Bite
    {
        internal bool Connected { get { return client.Connected; } }
        internal event Action<Frame> FrameReceived;

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
                receiver.FrameReceived += OnFrameReceived;
            }
            catch (SocketException e)
            {
                Shutdown();
                Console.WriteLine(e);
            }
        }

        internal void Send(string data, Action<Frame> action = null)
        {
            if (!Connected)
                throw new SocketException((int)SocketError.NotConnected);

            sender.Send(data);

            if (action != null)
                receiver.React(action);
        }

        internal void Shutdown()
        {
            client.Close();
            stream.Close();

            sender.Abort();
            receiver.Abort();
        }

        private void OnFrameReceived(Frame frame)
        {
            if (FrameReceived != null)
                FrameReceived(frame);
        }
    }
}