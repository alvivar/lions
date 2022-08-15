using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace BiteClient
{
    internal sealed class Receiver
    {
        internal event Action<Frame> FrameReceived;

        private Queue<Action<Frame>> actions = new Queue<Action<Frame>>();
        private NetworkStream stream;
        private Thread thread;
        private Frames frames = new Frames();

        internal Receiver(NetworkStream stream)
        {
            this.stream = stream;
            thread = new Thread(Run);
            thread.Start();
        }

        internal void React(Action<Frame> callback)
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

                    // If seems that the connection was closed.
                    if (bytesRead <= 0)
                        throw new SocketException((int)SocketError.NetworkUnreachable);

                    frames.Feed(ms.ToArray());
                }

                if (frames.HasCompleteFrame)
                {
                    var frame = frames.Dequeue();

                    if (FrameReceived != null)
                        FrameReceived(frame);

                    if (actions.Count < 1)
                        continue;

                    lock (actions)
                    {
                        var action = actions.Dequeue();
                        if (action != null)
                            action(frame);
                    }
                }
            }
        }
    }
}