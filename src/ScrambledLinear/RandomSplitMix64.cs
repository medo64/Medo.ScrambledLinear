using System;

namespace ScrambledLinear {

    /// <summary>
    /// Seed initializer PRNG (splitmix64).
    /// </summary>
    public class RandomSplitMix64 {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public RandomSplitMix64() {
            Algorithm = new SplitMix64();
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="seed">Seed value.</param>
        public RandomSplitMix64(int seed) {
            Algorithm = new SplitMix64(seed);
        }

        private readonly SplitMix64 Algorithm;


        /// <summary>
        /// Returns random value between 0 and specified number (not inclusive).
        /// </summary>
        /// <param name="maxValue">One more than the maximum value.</param>
        public int Next(int maxValue) {
            if (maxValue < 1) { throw new ArgumentOutOfRangeException(nameof(maxValue), "Upper limit cannot be less than 1."); }
            return (int)(NextDouble() * maxValue);
        }

        /// <summary>
        /// Returns random value in specified range.
        /// </summary>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">One more than the maximum value.</param>
        public int Next(int minValue, int maxValue) {
            if (minValue >= maxValue) { throw new ArgumentOutOfRangeException(nameof(minValue), "Lower limit cannot be less or equal to upper limit."); }

            long spread = (long)maxValue - minValue;
            var unadjusted = (long)(NextDouble() * spread);
            return (int)(unadjusted + minValue);
        }


        /// <summary>
        /// Returns random 32-bit integer.
        /// </summary>
        public int Next() {
            return unchecked((int)Algorithm.Next());
        }

        /// <summary>
        /// Returns random number between 0 and 1 (not inclusive).
        /// </summary>
        public double NextDouble() {
            var value = (UInt64)Algorithm.Next();
            var buffer = BitConverter.GetBytes(((UInt64)0x3FF << 52) | (value >> 12));
            return BitConverter.ToDouble(buffer) - 1.0;
        }

        /// <summary>
        /// Fills buffer with random numbers.
        /// </summary>
        /// <param name="buffer">Buffer to fill.</param>
        public void NextBytes(byte[] buffer) {
            if (buffer == null) { throw new ArgumentNullException(nameof(buffer), "Buffer cannot be null."); }

            byte[] bufferRnd = Array.Empty<byte>();
            int r = 8;
            for (int i = 0; i < buffer.Length; i++) {
                if (r == 8) {  // get next 8 bytes
                    bufferRnd = BitConverter.GetBytes(Algorithm.Next());
                    r = 0;
                }
                buffer[i] = bufferRnd[r];
                r++;
            }
        }

    }
}
