using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoshiro {
    public class Xoshiro128P {

        [Fact(DisplayName = "xoshiro128+: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoshiro128P).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoshiro128P();
            stateField.SetValue(random, new UInt32[] { 2, 3, 5, 7 });

            UInt32[] values = new UInt32[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt32)random.Next();
            }

            Assert.Equal((UInt32)0x00000009, values[0]);
            Assert.Equal((UInt32)0x00002006, values[1]);
            Assert.Equal((UInt32)0x01004002, values[2]);
            Assert.Equal((UInt32)0x02302e0f, values[3]);
            Assert.Equal((UInt32)0x80307612, values[4]);
            Assert.Equal((UInt32)0xe50cd80a, values[5]);
            Assert.Equal((UInt32)0x667c3d22, values[6]);
            Assert.Equal((UInt32)0x9ba133f6, values[7]);
            Assert.Equal((UInt32)0x806f41d3, values[8]);
            Assert.Equal((UInt32)0xcb7efa4c, values[9]);
            Assert.Equal((UInt32)0x4b884a68, values[10]);
            Assert.Equal((UInt32)0x93ad77c3, values[11]);
            Assert.Equal((UInt32)0xf711883d, values[12]);
            Assert.Equal((UInt32)0x174aec55, values[13]);
            Assert.Equal((UInt32)0xc4ee89fc, values[14]);
            Assert.Equal((UInt32)0x193f0b95, values[15]);
            Assert.Equal((UInt32)0xd10ce413, values[16]);
            Assert.Equal((UInt32)0x8e8a2308, values[17]);
            Assert.Equal((UInt32)0xf3e7edf5, values[18]);
            Assert.Equal((UInt32)0xd8f92310, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt32)0x2469b79f, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128+: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoshiro128P(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt32)0xED6A4F9B, (UInt32)random.Next());
            Assert.Equal((UInt32)0x5808F056, (UInt32)random.Next());
            Assert.Equal((UInt32)0xC6C161E8, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoshiro128+: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoshiro128P(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt32)0xE70EAEF2, (UInt32)random.Next());
            Assert.Equal((UInt32)0x033B04BD, (UInt32)random.Next());
            Assert.Equal((UInt32)0x9197F010, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128+: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoshiro128P(int.MinValue);

            Assert.Equal((UInt32)0xEDD5AF4B, (UInt32)random.Next());
            Assert.Equal((UInt32)0xF45D6B0B, (UInt32)random.Next());
            Assert.Equal((UInt32)0x1AB442B6, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoshiro128+: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoshiro128P(int.MaxValue);

            Assert.Equal((UInt32)0xC73400A5, (UInt32)random.Next());
            Assert.Equal((UInt32)0x92012499, (UInt32)random.Next());
            Assert.Equal((UInt32)0xC5BBCFBF, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128+: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoshiro128P(long.MinValue);

            Assert.Equal((UInt32)0x968D4AD4, (UInt32)random.Next());
            Assert.Equal((UInt32)0xD5B18E5E, (UInt32)random.Next());
            Assert.Equal((UInt32)0xFBBBCDAC, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoshiro128+: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoshiro128P(long.MaxValue);

            Assert.Equal((UInt32)0xC29CFAA2, (UInt32)random.Next());
            Assert.Equal((UInt32)0x172E1CBB, (UInt32)random.Next());
            Assert.Equal((UInt32)0x9D0581B1, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128+: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoshiro128P();
            var random2 = new ScrambledLinear.Xoshiro128P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
