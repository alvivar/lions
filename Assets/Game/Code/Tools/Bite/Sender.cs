using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BiteClient
{
    internal sealed class Sender
    {
        internal Queue<string> messages = new Queue<string>();

        private NetworkStream stream;
        private Thread thread;

        internal Sender(NetworkStream stream)
        {
            this.stream = stream;
            thread = new Thread(Run);
            thread.Start();
        }

        internal void Send(string message)
        {
            lock (messages)
            {
                messages.Enqueue(message);
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
                if (messages.Count < 1)
                    continue;

                lock (messages)
                {
                    var message = messages.Dequeue();

                    // The first 2 bytes should be the length of the message,
                    // according to our protocol.
                    var length = message.Length + 2;
                    var byteLen = new byte[2];
                    byteLen[0] = (byte)((length & 0xFF00) >> 8);
                    byteLen[1] = (byte)((length & 0x00FF));

                    var byteData = Encoding.ASCII.GetBytes(message);

                    // Concat
                    var data = new byte[byteLen.Length + byteData.Length];
                    Array.Copy(byteLen, data, byteLen.Length);
                    Array.Copy(byteData, 0, data, byteLen.Length, byteData.Length);

                    stream.Write(data, 0, data.Length);
                }
            }
        }
    }
}