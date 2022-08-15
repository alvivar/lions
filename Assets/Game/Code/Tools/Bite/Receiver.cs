using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace BiteClient
{
    internal sealed class Receiver
    {
        internal event Action<byte[]> DataReceived;

        private Queue<Action<byte[]>> actions = new Queue<Action<byte[]>>();
        private NetworkStream stream;
        private Thread thread;

        internal Receiver(NetworkStream stream)
        {
            this.stream = stream;
            thread = new Thread(Run);
            thread.Start();
        }

        internal void React(Action<byte[]> callback)
        {
            lock (actions)
            {
                actions.Enqueue(callback);
            }
        }

        internal void Abort()
        {
            if (thread != null)
                thread.Abort();
        }

        private void Run()
        {
            while (true)
            {
                if (!stream.CanRead)
                    return;

                // Incoming message may be larger than the buffer size.
                byte[] buffer = new byte[4096];
                int numberOfBytesRead = 0;

                using (MemoryStream ms = new MemoryStream())
                {
                    do
                    {
                        numberOfBytesRead = stream.Read(buffer, 0, buffer.Length);
                        ms.Write(buffer, 0, numberOfBytesRead);
                    }
                    while (stream.DataAvailable);

                    if (numberOfBytesRead <= 0)
                        throw new SocketException((int)SocketError.NetworkUnreachable);

                    buffer = ms.ToArray();
                }

                if (DataReceived != null)
                    DataReceived(buffer);

                if (actions.Count < 1)
                    continue;

                lock (actions)
                {
                    var action = actions.Dequeue();
                    if (action != null)
                        action(buffer);
                }
            }
        }
    }
}