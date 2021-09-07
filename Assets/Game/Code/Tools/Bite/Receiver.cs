using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace BiteServer
{
    public sealed class Receiver
    {
        public event Action<string> DataReceived;

        private NetworkStream stream;
        private StreamReader reader;
        private Thread thread;

        public Receiver(NetworkStream stream)
        {
            this.stream = stream;
            reader = new StreamReader(this.stream);
            thread = new Thread(Run);
            thread.Start();
        }

        private void Run()
        {
            while (true)
            {
                var response = reader.ReadLine();

                if (DataReceived != null)
                    DataReceived(response);
            }
        }
    }
}