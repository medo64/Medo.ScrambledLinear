using System;
using System.Runtime.CompilerServices;

namespace ScrambledLinear;

/// <summary>
/// 64-bit all-purpose random number generator with 128-bit state (xoroshiro128**).
/// </summary>
/// <remarks>http://prng.di.unimi.it/xoroshiro128starstar.c</remarks>
public class Xoroshiro128SS {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public Xoroshiro128SS()
        : this(DateTime.UtcNow.Ticks) {
    }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="seed">Seed value.</param>
    public Xoroshiro128SS(long seed) {
        var sm64 = new SplitMix64(seed);
        for (var i = 0; i < 2; i++) {
            s[i] = unchecked((UInt64)sm64.Next());
        }
    }


    private readonly UInt64[] s = new UInt64[2];

    /// <summary>
    /// Returns the next 64-bit pseudo-random number.
    /// </summary>
    public long Next() {
        UInt64 s0 = s[0];
        UInt64 s1 = s[1];
        UInt64 result = unchecked(RotateLeft(unchecked(s0 * 5), 7) * 9);

        s1 ^= s0;
        s[0] = RotateLeft(s0, 24) ^ s1 ^ (s1 << 16);
        s[1] = RotateLeft(s1, 37);

        return (long)result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static UInt64 RotateLeft(UInt64 x, int k) {
        return (x << k) | (x >> (64 - k));
    }

}
