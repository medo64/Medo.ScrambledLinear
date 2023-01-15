using System;
using System.Runtime.CompilerServices;

namespace ScrambledLinear;

/// <summary>
/// 64-bit random number generator intended for floating point numbers with 1024-bit state (xoshiro1024*).
/// </summary>
/// <remarks>http://prng.di.unimi.it/xoroshiro1024star.c</remarks>
public class Xoroshiro1024S {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public Xoroshiro1024S()
        : this(DateTime.UtcNow.Ticks) {
    }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="seed">Seed value.</param>
    public Xoroshiro1024S(long seed) {
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
        UInt64 result = unchecked(s0 * 0x9e3779b97f4a7c13);

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
