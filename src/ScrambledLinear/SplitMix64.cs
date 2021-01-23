using System;

namespace ScrambledLinear {
    /// <summary>
    /// splitmix64
    /// Seed initializer.
    /// </summary>
    /// <remarks>http://prng.di.unimi.it/splitmix64.c</remarks>
    internal class SplitMix64 {

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public SplitMix64()
            : this(unchecked((UInt64)DateTime.UtcNow.Ticks)) {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="seed">Seed value.</param>
        public SplitMix64(UInt64 seed) {
            x = seed;
        }

        private UInt64 x;

        /// <summary>
        /// Returns then next 64-bit "random" number.
        /// </summary>
        public UInt64 Next() {
            UInt64 z = unchecked(x += 0x9e3779b97f4a7c15);
            z = unchecked((z ^ (z >> 30)) * 0xbf58476d1ce4e5b9);
            z = unchecked((z ^ (z >> 27)) * 0x94d049bb133111eb);
            return z ^ (z >> 31);
        }

    }
}
