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

        private Queue<string> messages = new Queue<string>();

        public Sender(NetworkStream stream)
        {
            this.stream = stream;
            writer = new StreamWriter(this.stream);
            thread = new Thread(Run);
            thread.Start();
        }

        public void Send(string data)
        {
            lock(messages)
            {
                messages.Enqueue(data);
            }
        }

        private void Run()
        {
            while (true)
            {
                if (messages.Count < 1)
                    continue;

                lock(messages)
                {
                    writer.WriteLine(messages.Dequeue());
                    writer.Flush();
                }
            }
        }
    }
}