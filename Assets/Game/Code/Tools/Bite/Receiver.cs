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
        private Frames frames = new Frames();

        internal Receiver(NetworkStream stream)
        {
            this.stream = stream;
            thread = new Thread(Run);
            thread.Start();
        }

        internal void React(Action<byte[]> callback)
        {
            lock (actions)
                actions.Enqueue(callback);
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
                int bytesRead = 0;

                using (var ms = new MemoryStream())
                {
                    do
                    {
                        bytesRead = stream.Read(buffer, 0, buffer.Length);
                        ms.Write(buffer, 0, bytesRead);
                    }
                    while (stream.DataAvailable);

                    if (bytesRead <= 0)
                        throw new SocketException((int)SocketError.NetworkUnreachable);

                    frames.Feed(ms.ToArray());
                }

                if (frames.HasCompleteFrame)
                {
                    if (DataReceived != null)
                        DataReceived(frames.Pop().Data);

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
}