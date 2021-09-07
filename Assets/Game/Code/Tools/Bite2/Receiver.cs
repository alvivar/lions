using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Bite2
{
    public sealed class Receiver
    {
        public event Action<string> DataReceived;

        private NetworkStream _stream;
        private StreamReader _reader;
        private Thread _thread;

        public Receiver(NetworkStream stream)
        {
            _stream = stream;
            _reader = new StreamReader(_stream);
            _thread = new Thread(Run);
            _thread.Start();
        }

        private void Run()
        {
            while (true)
            {
                var response = _reader.ReadLine();

                if (DataReceived != null)
                    DataReceived(response);
            }
        }
    }
}