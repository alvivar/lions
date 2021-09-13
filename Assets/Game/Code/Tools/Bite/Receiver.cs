using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace BiteServer
{
    internal sealed class Receiver
    {
        internal event Action<string> DataReceived;
        internal Queue<Action<string>> actions = new Queue<Action<string>>();

        internal NetworkStream stream;
        internal StreamReader reader;
        internal Thread thread;

        internal Receiver(NetworkStream stream)
        {
            this.stream = stream;
            reader = new StreamReader(this.stream);
            thread = new Thread(Run);
            thread.Start();
        }

        internal void React(Action<string> callback)
        {
            lock(actions)
            {
                actions.Enqueue(callback);
            }
        }

        internal void Run()
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

        internal void Stop()
        {
            if (reader != null)
                reader.Close();

            if (thread != null)
                thread.Abort();
        }
    }
}