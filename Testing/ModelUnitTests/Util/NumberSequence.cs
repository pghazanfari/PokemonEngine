using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelUnitTests.Util
{
    public class NumberSequence : Random
    {
        private readonly List<int> intSequence;
        private readonly List<double> doubleSequence;
        private readonly List<byte> byteSequence;

        public NumberSequence(int seed) : base(seed)
        {
            this.intSequence = new List<int>();
            this.doubleSequence = new List<double>();
            this.byteSequence = new List<byte>();
        }

        public NumberSequence() : base()
        {
            this.intSequence = new List<int>();
            this.doubleSequence = new List<double>();
            this.byteSequence = new List<byte>();
        }

        public void Add(int nextInt)
        {
            intSequence.Add(nextInt);
        }

        public void Add(double nextDouble)
        {
            doubleSequence.Add(nextDouble);
        }

        public void Add(byte nextByte)
        {
            byteSequence.Add(nextByte);
        }

        public override void NextBytes(byte[] buffer)
        {
            base.NextBytes(buffer);
            int i = 0;
            for (; i < buffer.Length && i < byteSequence.Count; i++) 
            {
                buffer[i] = byteSequence[i];
            }

            if (i > 0) { byteSequence.RemoveRange(0, i); }
            if (i >= buffer.Length) { return; }

            byte[] newArray = new byte[buffer.Length - i];
            base.NextBytes(newArray);
            for (int j = 0; j < newArray.Length; j++)
            {
                buffer[i + j] = newArray[j];
            }
        }

        public override double NextDouble()
        {
            if (doubleSequence.Count == 0) { return base.NextDouble(); }

            double value = doubleSequence[0];
            doubleSequence.RemoveAt(0);
            return value;
        }

        public override int Next()
        {
            return nextInt(0, int.MaxValue);
        }

        public override int Next(int maxValue)
        {
            return nextInt(0, maxValue);
        }

        public override int Next(int minValue, int maxValue)
        {
            return nextInt(minValue, maxValue);
        }

        private int nextInt(int min, int max)
        {
            if (intSequence.Count == 0 || intSequence[0] < min || intSequence[0] >= max) { return base.Next(min, max); }

            int value = intSequence[0];
            intSequence.RemoveAt(0);
            return value;
        }
    }
}
