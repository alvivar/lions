using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Bite2
{
    public sealed class Sender
    {
        private NetworkStream _stream;
        private StreamWriter _writer;
        private Thread _thread;

        private Queue<string> _queue = new Queue<string>();

        public Sender(NetworkStream stream)
        {
            _stream = stream;
            _writer = new StreamWriter(_stream);
            _thread = new Thread(Run);
            _thread.Start();
        }

        public void SendData(byte[] data)
        {
            var utf8 = Encoding.UTF8.GetString(data);

            lock(_queue)
            {
                _queue.Enqueue(utf8);
            }
        }

        private void Run()
        {
            while (true)
            {
                if (_queue.Count < 1)
                    continue;

                lock(_queue)
                {
                    var data = _queue.Dequeue();
                    _writer.WriteLine(data);
                    _writer.Flush();
                }
            }
        }
    }
}