using System;
using System.Text;
using System.Collections.Generic;

namespace BiteClient
{
    public class Frame
    {
        public byte[] Data
        {
            get { return data; }
        }
        public byte[] Message
        {
            get
            {
                var message = new byte[data.Length - 2];
                Array.Copy(data, 2, message, 0, message.Length);
                return message;
            }
        }
        public String Text
        {
            get { return Encoding.UTF8.GetString(Message, 0, Message.Length); }
        }

        private int size;
        private byte[] data;

        public Frame(int size, byte[] data)
        {
            this.size = size;
            this.data = data;
        }
    }

    public class Frames
    {
        public bool HasCompleteFrame { get { return frames.Count > 0; } }

        private Queue<Frame> frames = new Queue<Frame>();
        private List<byte> buffer = new List<byte>();

        public void Feed(byte[] data)
        {
            buffer.AddRange(data);
            int size = (buffer[0] << 8) | buffer[1];

            // A complete frame!
            if (size == buffer.Count)
            {
                frames.Enqueue(new Frame(size, buffer.ToArray()));
                buffer.Clear();
            }

            // More than one frame in the buffer, lets split, save the frame,
            // let the rest on the buffer.
            else if (size < buffer.Count)
            {
                var split = buffer.GetRange(0, size);
                frames.Enqueue(new Frame(size, split.ToArray()));
                buffer.RemoveRange(0, size);
            }

            // Not enough data in the buffer, maybe next time.
            else if (size > buffer.Count)
                return;
        }

        public Frame Pop()
        {
            if (!HasCompleteFrame)
                return null;

            return frames.Dequeue();
        }
    }
}