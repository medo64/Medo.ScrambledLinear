using System;
using System.Runtime.CompilerServices;

namespace ScrambledLinear {

    /// <summary>
    /// 64-bit all-purpose random number generator with 1024-bit state (xoshiro1024++).
    /// </summary>
    /// <remarks>http://prng.di.unimi.it/xoroshiro1024plusplus.c</remarks>
    public class Xoroshiro1024PP {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public Xoroshiro1024PP()
            : this(DateTime.UtcNow.Ticks) {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="seed">Seed value.</param>
        public Xoroshiro1024PP(long seed) {
            var sm64 = new SplitMix64(seed);
            for (var i = 0; i < 16; i++) {
                s[i] = unchecked((UInt64)sm64.Next());
            }
        }


        private int p;
        private readonly UInt64[] s = new UInt64[16];

        /// <summary>
        /// Returns the next 64-bit pseudo-random number.
        /// </summary>
        public long Next() {
            int q = p;
            UInt64 s0 = s[p = (p + 1) & 15];
            UInt64 s15 = s[q];
            UInt64 result = unchecked(RotateLeft(unchecked(s0 + s15), 23) + s15);

            s15 ^= s0;
            s[q] = RotateLeft(s0, 25) ^ s15 ^ (s15 << 27);
            s[p] = RotateLeft(s15, 36);

            return (long)result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt64 RotateLeft(UInt64 x, int k) {
            return (x << k) | (x >> (64 - k));
        }

    }
}
