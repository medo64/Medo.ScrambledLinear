using System;
using System.Runtime.CompilerServices;

namespace ScrambledLinear {

    /// <summary>
    /// 32-bit all-purpose random number generator with 64-bit state (xoshiro64**).
    /// </summary>
    /// <remarks>http://prng.di.unimi.it/xoroshiro64starstar.c</remarks>
    public class Xoroshiro64SS {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public Xoroshiro64SS()
            : this(DateTime.UtcNow.Ticks) {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="seed">Seed value.</param>
        public Xoroshiro64SS(long seed) {
            var sm64 = new SplitMix64(seed);
            for (var i = 0; i < 2; i++) {
                s[i] = unchecked((UInt32)sm64.Next());
            }
        }


        private readonly UInt32[] s = new UInt32[2];

        /// <summary>
        /// Returns the next 32-bit pseudo-random number.
        /// </summary>
        public int Next() {
            UInt32 s0 = s[0];
            UInt32 s1 = s[1];
            UInt32 result = unchecked(RotateLeft(unchecked(s0 * (UInt32)0x9E3779BB), 5) * 5);

            s1 ^= s0;
            s[0] = RotateLeft(s0, 26) ^ s1 ^ (s1 << 9);
            s[1] = RotateLeft(s1, 13);

            return (int)result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt32 RotateLeft(UInt32 x, int k) {
            return (x << k) | (x >> (32 - k));
        }

    }
}
