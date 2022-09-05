using System;
using System.Net.Sockets;
using System.Text;

namespace BiteClient
{
    public sealed class Bite
    {
        internal bool Connected { get { return client.Connected; } }
        internal long ClientId { get { return clientId; } }
        internal event Action<Frame> OnConnected;
        internal event Action<Frame> OnFrameReceived;

        private TcpClient client;
        private NetworkStream stream;
        private Sender sender;
        private Receiver receiver;

        private long clientId;
        private static int sentId;

        internal Bite(string host, int port)
        {
            client = new TcpClient(host, port);
            stream = client.GetStream();

            try
            {
                sender = new Sender(stream);
                receiver = new Receiver(stream);
                receiver.OnFrameReceived += FrameReceived;
            }
            catch (SocketException e)
            {
                Shutdown();
                Console.WriteLine(e);
            }
        }

        internal void Send(byte[] data, Action<Frame> action = null)
        {
            if (clientId == 0)
                throw new SocketException((int)SocketError.NotConnected);

            if (!Connected)
                throw new SocketException((int)SocketError.NotConnected);

            sentId += 1;
            var frame = new Frame().FromProtocol((int)clientId, sentId, data);
            sender.Send(frame);

            if (action != null)
                receiver.React(action);
        }

        internal void Send(string text, Action<Frame> action = null)
        {
            var bytes = Encoding.ASCII.GetBytes(text);
            Send(bytes, action);
        }

        internal void Shutdown()
        {
            client.Close();
            stream.Close();

            sender.Abort();
            receiver.Abort();
        }

        private void FrameReceived(Frame frame)
        {
            // The first frame is the client id asigned by the server.
            if (clientId < 1)
            {
                var bigEndian = frame.Content;
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(bigEndian);

                clientId = BitConverter.ToInt64(bigEndian, 0);

                if (OnConnected != null)
                    OnConnected(frame);

                return;
            }

            if (OnFrameReceived != null)
                OnFrameReceived(frame);
        }
    }
}