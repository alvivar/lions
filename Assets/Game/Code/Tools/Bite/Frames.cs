using System;
using System.Text;
using System.Collections.Generic;

namespace BiteClient
{
    public class Frame
    {
        public byte[] Data { get { return data; } }
        public int Size { get { return (data[0] << 8) | data[1]; } }
        public byte[] Content
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
            get
            {
                var content = Content;
                return Encoding.UTF8.GetString(content, 0, content.Length);
            }
        }

        private byte[] data = new byte[0];

        public Frame Feed(byte[] data)
        {
            var newData = new byte[this.data.Length + data.Length];
            Array.Copy(this.data, newData, this.data.Length);
            Array.Copy(data, 0, newData, this.data.Length, data.Length);
            this.data = newData;

            return this;
        }

        /// Remove and returns the remainder data that overflows the size.
        public Byte[] SplitRemainder()
        {
            var remainder = new byte[data.Length - Size];
            Array.Copy(data, data.Length, remainder, 0, remainder.Length);
            Array.Resize(ref data, Size);

            return remainder;
        }
    }

    public class Frames
    {
        public bool HasCompleteFrame { get { return frames.Count > 0; } }

        private Queue<Frame> frames = new Queue<Frame>();
        private Frame frame = new Frame();

        public void Feed(byte[] data)
        {
            frame.Feed(data);

            // A complete frame!
            if (frame.Size == frame.Data.Length)
            {
                frames.Enqueue(frame);
                frame = new Frame();
            }

            // More than one frame in the buffer, lets split, save the frame,
            // buffer the rest on a new frame.
            else if (frame.Size < frame.Data.Length)
            {
                var newFrame = new Frame().Feed(frame.SplitRemainder());
                frames.Enqueue(frame);
            }

            // Not enough data in the buffer for a complete frame, maybe on the
            // next feed.
            else if (frame.Size > frame.Data.Length)
                return;
        }

        public Frame Dequeue()
        {
            return frames.Dequeue();
        }
    }
}