using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace BiteServer
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

        private void Run()
        {
            while (true)
            {
                if (messages.Count < 1)
                    continue;

                lock (messages)
                {
                    var message = messages.Dequeue();
                    message = $"{message.Trim()}\n"; // Ending in newline.

                    // The first 2 bytes should be the length of the message,
                    // according to our protocol.
                    var length = message.Length + 2;
                    var byteLength = new byte[2];
                    byteLength[0] = (byte)((length & 0xFF00) >> 8);
                    byteLength[1] = (byte)((length & 0x00FF));

                    var byteText = Encoding.ASCII.GetBytes(message);

                    // Concat
                    var data = new byte[byteLength.Length + byteText.Length];
                    Array.Copy(byteLength, data, byteLength.Length);
                    Array.Copy(byteText, 0, data, byteLength.Length, byteText.Length);

                    stream.Write(data, 0, data.Length);
                }
            }
        }

        internal void Close()
        {
            if (thread != null)
                thread.Abort();
        }
    }
}