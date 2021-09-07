using System;
using System.Collections.Generic;
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

        private Queue<Action<string>> actions = new Queue<Action<string>>();

        public Receiver(NetworkStream stream)
        {
            this.stream = stream;
            reader = new StreamReader(this.stream);
            thread = new Thread(Run);
            thread.Start();
        }

        public void React(Action<string> callback)
        {
            lock(actions)
            {
                actions.Enqueue(callback);
            }
        }

        private void Run()
        {
            while (true)
            {
                var response = reader.ReadLine();

                if (DataReceived != null)
                    DataReceived(response);

                if (actions.Count < 1)
                    continue;

                lock(actions)
                {
                    var action = actions.Dequeue();
                    if (action != null)
                        action(response);
                }
            }
        }
    }
}