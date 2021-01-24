using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoroshiro {
    public class Xoroshiro64SS {

        [Fact(DisplayName = "xoroshiro64**: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoroshiro64SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoroshiro64SS();
            stateField.SetValue(random, new UInt32[] { 2, 3 });

            UInt32[] values = new UInt32[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt32)random.Next();
            }

            Assert.Equal((UInt32)0xc55829e3, values[0]);
            Assert.Equal((UInt32)0x3ad5d56c, values[1]);
            Assert.Equal((UInt32)0x2228bc62, values[2]);
            Assert.Equal((UInt32)0xbce5cbe5, values[3]);
            Assert.Equal((UInt32)0x9b6feedc, values[4]);
            Assert.Equal((UInt32)0x3170179d, values[5]);
            Assert.Equal((UInt32)0x290a849d, values[6]);
            Assert.Equal((UInt32)0x32e9b2bd, values[7]);
            Assert.Equal((UInt32)0x89835db6, values[8]);
            Assert.Equal((UInt32)0xc1baeecf, values[9]);
            Assert.Equal((UInt32)0x4a59225a, values[10]);
            Assert.Equal((UInt32)0x6889b4f8, values[11]);
            Assert.Equal((UInt32)0xef9b8c64, values[12]);
            Assert.Equal((UInt32)0x3c816368, values[13]);
            Assert.Equal((UInt32)0x61293207, values[14]);
            Assert.Equal((UInt32)0xf44d7827, values[15]);
            Assert.Equal((UInt32)0xa8a2f5b4, values[16]);
            Assert.Equal((UInt32)0x4551efc3, values[17]);
            Assert.Equal((UInt32)0x0181292b, values[18]);
            Assert.Equal((UInt32)0xfc1b22f1, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt32)0x8ceb61ad, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoroshiro64**: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoroshiro64SS(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt32)0xBDB9A53E, (UInt32)random.Next());
            Assert.Equal((UInt32)0x4CD4C374, (UInt32)random.Next());
            Assert.Equal((UInt32)0x561198DA, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoroshiro64**: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoroshiro64SS(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt32)0xC5791C05, (UInt32)random.Next());
            Assert.Equal((UInt32)0x2B5E41E0, (UInt32)random.Next());
            Assert.Equal((UInt32)0x45AEF501, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoroshiro64**: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoroshiro64SS(int.MinValue);

            Assert.Equal((UInt32)0xB6F5B8B0, (UInt32)random.Next());
            Assert.Equal((UInt32)0xDEDBE1C9, (UInt32)random.Next());
            Assert.Equal((UInt32)0xDB4EBE61, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoroshiro64**: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoroshiro64SS(int.MaxValue);

            Assert.Equal((UInt32)0x52247634, (UInt32)random.Next());
            Assert.Equal((UInt32)0xAF4F5943, (UInt32)random.Next());
            Assert.Equal((UInt32)0xCEDE92AE, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoroshiro64**: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoroshiro64SS(long.MinValue);

            Assert.Equal((UInt32)0xC8E67BF0, (UInt32)random.Next());
            Assert.Equal((UInt32)0x808BBDBD, (UInt32)random.Next());
            Assert.Equal((UInt32)0x1D8057BA, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoroshiro64**: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoroshiro64SS(long.MaxValue);

            Assert.Equal((UInt32)0x91C3DE6B, (UInt32)random.Next());
            Assert.Equal((UInt32)0x048C6F42, (UInt32)random.Next());
            Assert.Equal((UInt32)0xF13EFEB2, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoroshiro64**: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoroshiro64SS();
            var random2 = new ScrambledLinear.Xoroshiro64SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
