using System;
using System.Runtime.CompilerServices;

namespace ScrambledLinear;

/// <summary>
/// 32-bit generator intended for floating point numbers with 128-bit state (xoshiro128+).
/// </summary>
/// <remarks>http://prng.di.unimi.it/xoshiro128plus.c</remarks>
public class Xoshiro128P {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public Xoshiro128P()
        : this(DateTime.UtcNow.Ticks) {
    }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="seed">Seed value.</param>
    public Xoshiro128P(long seed) {
        var sm64 = new SplitMix64(seed);
        for (var i = 0; i < 4; i++) {
            s[i] = unchecked((UInt32)sm64.Next());
        }
    }


    private readonly UInt32[] s = new UInt32[4];

    /// <summary>
    /// Returns the next 32-bit pseudo-random number.
    /// </summary>
    public int Next() {
        UInt32 result = unchecked(s[0] + s[3]);

        UInt32 t = s[1] << 9;

        s[2] ^= s[0];
        s[3] ^= s[1];
        s[1] ^= s[2];
        s[0] ^= s[3];

        s[2] ^= t;

        s[3] = RotateLeft(s[3], 11);

        return (int)result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static UInt32 RotateLeft(UInt32 x, int k) {
        return (x << k) | (x >> (32 - k));
    }

}
