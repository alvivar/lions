using System.Net.Sockets;
using System.Threading;

namespace Bite2
{
    public sealed class Sender
    {
        private NetworkStream _stream;
        private Thread _thread;

        public Sender(NetworkStream stream)
        {
            _stream = stream;
            _thread = new Thread(Run);
            _thread.Start();
        }

        public void SendData(byte[] data)
        {
            // transition the data to the thread and send it...
        }

        private void Run()
        {
            // main thread loop for sending data...

            while (true)
            {

            }
        }
    }
}