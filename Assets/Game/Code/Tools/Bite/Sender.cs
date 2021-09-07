using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace BiteServer
{
    public sealed class Sender
    {
        private NetworkStream stream;
        private StreamWriter writer;
        private Thread thread;

        private Queue<string> queue = new Queue<string>();

        public Sender(NetworkStream stream)
        {
            this.stream = stream;
            writer = new StreamWriter(this.stream);
            thread = new Thread(Run);
            thread.Start();
        }

        public void Send(string data)
        {
            lock(queue)
            {
                queue.Enqueue(data);
            }
        }

        private void Run()
        {
            while (true)
            {
                if (queue.Count < 1)
                    continue;

                lock(queue)
                {
                    writer.WriteLine(queue.Dequeue());
                    writer.Flush();
                }
            }
        }
    }
}