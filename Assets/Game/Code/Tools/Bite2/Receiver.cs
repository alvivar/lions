using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace Bite2
{
    public sealed class Receiver
    {
        public event EventHandler<DataReceivedEventArgs> DataReceived;

        private NetworkStream _stream;
        private Thread _thread;

        public Receiver(NetworkStream stream)
        {
            _stream = stream;
            _thread = new Thread(Run);
            _thread.Start();
        }

        private void Run()
        {
            // main thread loop for receiving data...

            while (true)
            {

            }
        }
    }
}