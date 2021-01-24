using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoroshiro {
    public class Xoroshiro64S {

        [Fact(DisplayName = "xoroshiro64*: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoroshiro64S).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoroshiro64S();
            stateField.SetValue(random, new UInt32[] { 2, 3 });

            UInt32[] values = new UInt32[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt32)random.Next();
            }

            Assert.Equal((UInt32)0x3c6ef376, values[0]);
            Assert.Equal((UInt32)0xe52aefbb, values[1]);
            Assert.Equal((UInt32)0xd036a793, values[2]);
            Assert.Equal((UInt32)0x0ac7d613, values[3]);
            Assert.Equal((UInt32)0x62924cb1, values[4]);
            Assert.Equal((UInt32)0xceb58025, values[5]);
            Assert.Equal((UInt32)0xc841aa6d, values[6]);
            Assert.Equal((UInt32)0xc85175ea, values[7]);
            Assert.Equal((UInt32)0xf40f3895, values[8]);
            Assert.Equal((UInt32)0x1acf917e, values[9]);
            Assert.Equal((UInt32)0x9543c1d0, values[10]);
            Assert.Equal((UInt32)0xc240dc54, values[11]);
            Assert.Equal((UInt32)0xa318f8e0, values[12]);
            Assert.Equal((UInt32)0x452d9bd2, values[13]);
            Assert.Equal((UInt32)0xd89b751c, values[14]);
            Assert.Equal((UInt32)0xd986e259, values[15]);
            Assert.Equal((UInt32)0x210dd189, values[16]);
            Assert.Equal((UInt32)0x3a088319, values[17]);
            Assert.Equal((UInt32)0x799c01db, values[18]);
            Assert.Equal((UInt32)0xeb2cf837, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt32)0x48e1789c, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoroshiro64*: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoroshiro64S(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt32)0x3795F5D5, (UInt32)random.Next());
            Assert.Equal((UInt32)0x2214879F, (UInt32)random.Next());
            Assert.Equal((UInt32)0x93BCE8F4, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoroshiro64*: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoroshiro64S(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt32)0x0FA25B60, (UInt32)random.Next());
            Assert.Equal((UInt32)0x06ABCA03, (UInt32)random.Next());
            Assert.Equal((UInt32)0x6A0917EE, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoroshiro64*: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoroshiro64S(int.MinValue);

            Assert.Equal((UInt32)0x85F18927, (UInt32)random.Next());
            Assert.Equal((UInt32)0xAE315FCF, (UInt32)random.Next());
            Assert.Equal((UInt32)0x6AF87DFD, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoroshiro64*: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoroshiro64S(int.MaxValue);

            Assert.Equal((UInt32)0x23B6A0BD, (UInt32)random.Next());
            Assert.Equal((UInt32)0x39187EF5, (UInt32)random.Next());
            Assert.Equal((UInt32)0xB47E30EA, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoroshiro64*: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoroshiro64S(long.MinValue);

            Assert.Equal((UInt32)0x8474A3F9, (UInt32)random.Next());
            Assert.Equal((UInt32)0xC8CDAC62, (UInt32)random.Next());
            Assert.Equal((UInt32)0x902F33BF, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoroshiro64*: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoroshiro64S(long.MaxValue);

            Assert.Equal((UInt32)0x7A82D2FD, (UInt32)random.Next());
            Assert.Equal((UInt32)0xD66DAD7E, (UInt32)random.Next());
            Assert.Equal((UInt32)0x5181FE64, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoroshiro64*: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoroshiro64S();
            var random2 = new ScrambledLinear.Xoroshiro64S();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
