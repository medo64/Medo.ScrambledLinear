using System;
using System.Runtime.CompilerServices;

namespace ScrambledLinear {

    /// <summary>
    /// 64-bit random number generator intended for floating point numbers with 512-bit state (xoshiro512+).
    /// </summary>
    /// <remarks>http://prng.di.unimi.it/xoshiro512plus.c</remarks>
    public class Xoshiro512P {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public Xoshiro512P()
            : this(DateTime.UtcNow.Ticks) {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="seed">Seed value.</param>
        public Xoshiro512P(long seed) {
            var sm64 = new SplitMix64(seed);
            for (var i = 0; i < 8; i++) {
                s[i] = unchecked((UInt64)sm64.Next());
            }
        }


        private readonly UInt64[] s = new UInt64[8];

        /// <summary>
        /// Returns the next 64-bit pseudo-random number.
        /// </summary>
        public long Next() {
            UInt64 result = unchecked(s[0] + s[2]);

            UInt64 t = s[1] << 11;

            s[2] ^= s[0];
            s[5] ^= s[1];
            s[1] ^= s[2];
            s[7] ^= s[3];
            s[3] ^= s[4];
            s[4] ^= s[5];
            s[0] ^= s[6];
            s[6] ^= s[7];

            s[6] ^= t;

            s[7] = RotateLeft(s[7], 21);

            return (long)result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt64 RotateLeft(UInt64 x, int k) {
            return (x << k) | (x >> (64 - k));
        }

    }
}
