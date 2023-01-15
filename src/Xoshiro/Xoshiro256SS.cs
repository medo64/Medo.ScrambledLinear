using System;
using System.Runtime.CompilerServices;

namespace ScrambledLinear {

    /// <summary>
    /// 64-bit all-purpose random number generator with 256-bit state (xoshiro256**).
    /// </summary>
    /// <remarks>http://prng.di.unimi.it/xoshiro256starstar.c</remarks>
    public class Xoshiro256SS {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public Xoshiro256SS()
            : this(DateTime.UtcNow.Ticks) {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="seed">Seed value.</param>
        public Xoshiro256SS(long seed) {
            var sm64 = new SplitMix64(seed);
            for (var i = 0; i < 4; i++) {
                s[i] = unchecked((UInt64)sm64.Next());
            }
        }


        private readonly UInt64[] s = new UInt64[4];

        /// <summary>
        /// Returns the next 64-bit pseudo-random number.
        /// </summary>
        public long Next() {
            UInt64 result = unchecked(RotateLeft(unchecked(s[1] * 5), 7) * 9);

            UInt64 t = s[1] << 17;

            s[2] ^= s[0];
            s[3] ^= s[1];
            s[1] ^= s[2];
            s[0] ^= s[3];

            s[2] ^= t;

            s[3] = RotateLeft(s[3], 45);

            return (long)result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt64 RotateLeft(UInt64 x, int k) {
            return (x << k) | (x >> (64 - k));
        }

    }
}
