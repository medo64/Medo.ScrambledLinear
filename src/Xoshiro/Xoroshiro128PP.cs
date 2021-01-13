using System;
using System.Runtime.CompilerServices;

namespace Xoshiro {

    /// <summary>
    /// xoroshiro128++
    /// 64-bit all-purpose generator with 128-bit state.
    /// </summary>
    /// <remarks>http://prng.di.unimi.it/xoroshiro128plusplus.c</remarks>
    public class Xoroshiro128PP {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public Xoroshiro128PP()
            : this((int)(DateTime.UtcNow.Ticks % int.MaxValue)) {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="seed">Seed value.</param>
        public Xoroshiro128PP(int seed) {
            UInt64 x = unchecked((uint)seed);
            for (var i = 0; i < 2; i++) {  // splitmix64
                var z = unchecked(x += 0x9E3779B97F4A7C15);
                z = unchecked((z ^ (z >> 30)) * 0xBF58476D1CE4E5B9);
                z = unchecked((z ^ (z >> 27)) * 0x94D049BB133111EB);
                s[i] = unchecked((UInt64)(z ^ (z >> 31)));
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
            int r = 4;
            for (int i = 0; i < buffer.Length; i++) {
                if (r == 4) {  // get next 4 bytes
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

        private readonly UInt64[] s = new UInt64[2];

        private UInt64 NextValue() {
            UInt64 s0 = s[0];
            UInt64 s1 = s[1];
            UInt64 result = unchecked(RotateLeft(unchecked(s0 + s1), 17) + s0);

            s1 ^= s0;
            s[0] = RotateLeft(s0, 49) ^ s1 ^ (s1 << 21);
            s[1] = RotateLeft(s1, 28);

            return result;
        }

        #endregion Implementation

    }
}
