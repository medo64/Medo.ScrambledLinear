using System;
using System.Runtime.CompilerServices;

namespace Xoroshiro {

    /// <summary>
    /// xoshiro1024++
    /// 64-bit all-purpose generator with 1024-bit state.
    /// </summary>
    /// <remarks>http://prng.di.unimi.it/xoroshiro1024plusplus.c</remarks>
    public class Xoroshiro1024PP {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public Xoroshiro1024PP()
            : this((int)(DateTime.UtcNow.Ticks % int.MaxValue)) {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="seed">Seed value.</param>
        public Xoroshiro1024PP(int seed) {
            UInt64 x = unchecked((uint)seed);  // convert seed to unsigned equivalent
            for (var i = 0; i < 16; i++) {  // splitmix64
                var z = unchecked(x += 0x9E3779B97F4A7C15);
                z = unchecked((z ^ (z >> 30)) * 0xBF58476D1CE4E5B9);
                z = unchecked((z ^ (z >> 27)) * 0x94D049BB133111EB);
                s[i] = z ^ (z >> 31);
            }
        }


        /// <summary>
        /// Returns random 32-bit integer.
        /// </summary>
        public int Next() {
            var buffer = BitConverter.GetBytes(NextValue());
            return BitConverter.ToInt32(buffer, 0);
        }

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
        /// Returns random number between 0 and 1 (not inclusive).
        /// </summary>
        public double NextDouble() {
            var value = NextValue();
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
                    bufferRnd = BitConverter.GetBytes(NextValue());
                    r = 0;
                }
                buffer[i] = bufferRnd[r];
                r++;
            }
        }


        #region Implementation

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt64 RotateLeft(UInt64 x, int k) {
            return (x << k) | (x >> (64 - k));
        }

        private int p;
        private readonly UInt64[] s = new UInt64[16];

        private UInt64 NextValue() {
            int q = p;
            UInt64 s0 = s[p = (p + 1) & 15];
            UInt64 s15 = s[q];
            UInt64 result = unchecked(RotateLeft(unchecked(s0 + s15), 23) + s15);

            s15 ^= s0;
            s[q] = RotateLeft(s0, 25) ^ s15 ^ (s15 << 27);
            s[p] = RotateLeft(s15, 36);

            return result;
        }

        #endregion Implementation

    }
}
