using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoroshiro {
    public class Xoroshiro128PP {

        [Fact(DisplayName = "xoroshiro128++: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoroshiro128PP).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoroshiro128PP();
            stateField.SetValue(random, new UInt64[] { 2, 3 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0x00000000000a0002, values[0]);
            Assert.Equal((UInt64)0x0004204000220009, values[1]);
            Assert.Equal((UInt64)0x081444089082024d, values[2]);
            Assert.Equal((UInt64)0x909c248981326149, values[3]);
            Assert.Equal((UInt64)0xcb8d553498d3056f, values[4]);
            Assert.Equal((UInt64)0xb2ddc177cf5432cf, values[5]);
            Assert.Equal((UInt64)0x2fdcefbbcf1661c1, values[6]);
            Assert.Equal((UInt64)0x7c217e7a4ca98253, values[7]);
            Assert.Equal((UInt64)0x7dd80d50ec380c2e, values[8]);
            Assert.Equal((UInt64)0x8da619d3dac945f6, values[9]);
            Assert.Equal((UInt64)0x587c9282664a84ba, values[10]);
            Assert.Equal((UInt64)0xa6b354d01824ef41, values[11]);
            Assert.Equal((UInt64)0x148c6d3cd65f24fe, values[12]);
            Assert.Equal((UInt64)0xb54f7d3e2084ab12, values[13]);
            Assert.Equal((UInt64)0x97901f4ec0a149a8, values[14]);
            Assert.Equal((UInt64)0xa1616375861db982, values[15]);
            Assert.Equal((UInt64)0xe4103cc653454025, values[16]);
            Assert.Equal((UInt64)0xc102fbdf97e9d3b4, values[17]);
            Assert.Equal((UInt64)0x28b971ee7ed25188, values[18]);
            Assert.Equal((UInt64)0x990b95a3e5677187, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0x7a8fd422209dbe19, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128++: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoroshiro128PP(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0x6F68E1E7E2646EE1, (UInt64)random.Next());
            Assert.Equal((UInt64)0xBF971B7F454094AD, (UInt64)random.Next());
            Assert.Equal((UInt64)0x48F2DE556F30DE38, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro128++: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoroshiro128PP(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0xB897602E7938C912, (UInt64)random.Next());
            Assert.Equal((UInt64)0x92AC733C00C69E74, (UInt64)random.Next());
            Assert.Equal((UInt64)0x79077F68C57FD4F5, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128++: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoroshiro128PP(int.MinValue);

            Assert.Equal((UInt64)0x93A02A1164962283, (UInt64)random.Next());
            Assert.Equal((UInt64)0x8641DA91AB27FDD2, (UInt64)random.Next());
            Assert.Equal((UInt64)0x9005A4D77EF828D1, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro128++: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoroshiro128PP(int.MinValue);

            Assert.Equal((UInt64)0x93A02A1164962283, (UInt64)random.Next());
            Assert.Equal((UInt64)0x8641DA91AB27FDD2, (UInt64)random.Next());
            Assert.Equal((UInt64)0x9005A4D77EF828D1, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128++: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoroshiro128PP(long.MinValue);

            Assert.Equal((UInt64)0x15D432571A840CF7, (UInt64)random.Next());
            Assert.Equal((UInt64)0xB021311407040E4F, (UInt64)random.Next());
            Assert.Equal((UInt64)0x3DF36AF3B1D593BE, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro128++: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoroshiro128PP(long.MaxValue);

            Assert.Equal((UInt64)0xDB9334625DDFD78E, (UInt64)random.Next());
            Assert.Equal((UInt64)0xFA7A665AEEBC40D7, (UInt64)random.Next());
            Assert.Equal((UInt64)0xC8AA16C88AB0D456, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128++: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoroshiro128PP();
            var random2 = new ScrambledLinear.Xoroshiro128PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
