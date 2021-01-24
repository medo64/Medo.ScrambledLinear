using System;

namespace ScrambledLinear {

    /// <summary>
    /// 64-bit all-purpose random number generator with 256-bit state (xoshiro256**).
    /// </summary>
    public class RandomXoshiro256SS {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public RandomXoshiro256SS() {
            Algorithm = new Xoshiro256SS();
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="seed">Seed value.</param>
        public RandomXoshiro256SS(int seed) {
            Algorithm = new Xoshiro256SS(seed);
        }

        private readonly Xoshiro256SS Algorithm;


        /// <summary>
        /// Returns random value between 0 and specified number (not inclusive).
        /// </summary>
        /// <param name="upperLimit">One more than the maximum value.</param>
        public int Next(int upperLimit) {
            if (upperLimit < 1) { throw new ArgumentOutOfRangeException(nameof(upperLimit), "Upper limit cannot be less than 1."); }
            return (int)(NextDouble() * upperLimit);
        }

        /// <summary>
        /// Returns random value in specified range.
        /// </summary>
        /// <param name="lowerLimit">Minimum value.</param>
        /// <param name="upperLimit">One more than the maximum value.</param>
        public int Next(int lowerLimit, int upperLimit) {
            if (lowerLimit >= upperLimit) { throw new ArgumentOutOfRangeException(nameof(lowerLimit), "Lower limit cannot be less or equal to upper limit."); }

            long spread = (long)upperLimit - lowerLimit;
            var unadjusted = (long)(NextDouble() * spread);
            return (int)(unadjusted + lowerLimit);
        }

        /// <summary>
        /// Returns random 32-bit integer.
        /// </summary>
        public int Next() {
            return (int)Algorithm.Next();
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
        public virtual void NextBytes(byte[] buffer) {
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
