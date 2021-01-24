using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoshiro {
    public class Xoshiro128PP {

        [Fact(DisplayName = "xoshiro128++: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoshiro128PP).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoshiro128PP();
            stateField.SetValue(random, new UInt32[] { 2, 3, 5, 7 });

            UInt32[] values = new UInt32[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt32)random.Next();
            }

            Assert.Equal((UInt32)0x00000482, values[0]);
            Assert.Equal((UInt32)0x00100306, values[1]);
            Assert.Equal((UInt32)0x80202102, values[2]);
            Assert.Equal((UInt32)0x19170d88, values[3]);
            Assert.Equal((UInt32)0x186b0f49, values[4]);
            Assert.Equal((UInt32)0x07a88174, values[5]);
            Assert.Equal((UInt32)0x20aa9338, values[6]);
            Assert.Equal((UInt32)0xc9f24665, values[7]);
            Assert.Equal((UInt32)0xb159878c, values[8]);
            Assert.Equal((UInt32)0x16132738, values[9]);
            Assert.Equal((UInt32)0xed378291, values[10]);
            Assert.Equal((UInt32)0x1ad79e24, values[11]);
            Assert.Equal((UInt32)0x795f9194, values[12]);
            Assert.Equal((UInt32)0xab0f6f38, values[13]);
            Assert.Equal((UInt32)0xb48eae98, values[14]);
            Assert.Equal((UInt32)0x43ccf959, values[15]);
            Assert.Equal((UInt32)0x8cb080e8, values[16]);
            Assert.Equal((UInt32)0x67a48e2a, values[17]);
            Assert.Equal((UInt32)0x78d5e3bc, values[18]);
            Assert.Equal((UInt32)0x5d91a04e, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt32)0x80055dac, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128++: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoshiro128PP(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt32)0x30459BA5, (UInt32)random.Next());
            Assert.Equal((UInt32)0xAD6054E3, (UInt32)random.Next());
            Assert.Equal((UInt32)0xBE15F69F, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoshiro128++: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoshiro128PP(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt32)0xA2BCA593, (UInt32)random.Next());
            Assert.Equal((UInt32)0xA8BC8ABC, (UInt32)random.Next());
            Assert.Equal((UInt32)0x4DD3E401, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128++: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoshiro128PP(int.MinValue);

            Assert.Equal((UInt32)0x0CE7577B, (UInt32)random.Next());
            Assert.Equal((UInt32)0xF27F29F7, (UInt32)random.Next());
            Assert.Equal((UInt32)0xCA5E4824, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoshiro128++: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoshiro128PP(int.MinValue);

            Assert.Equal((UInt32)0x0CE7577B, (UInt32)random.Next());
            Assert.Equal((UInt32)0xF27F29F7, (UInt32)random.Next());
            Assert.Equal((UInt32)0xCA5E4824, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128++: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoshiro128PP(long.MinValue);

            Assert.Equal((UInt32)0x594F5E26, (UInt32)random.Next());
            Assert.Equal((UInt32)0x1041649A, (UInt32)random.Next());
            Assert.Equal((UInt32)0xB3C56514, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoshiro128++: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoshiro128PP(long.MaxValue);

            Assert.Equal((UInt32)0x7C80F008, (UInt32)random.Next());
            Assert.Equal((UInt32)0xD12699A6, (UInt32)random.Next());
            Assert.Equal((UInt32)0x698FD399, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128++: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoshiro128PP();
            var random2 = new ScrambledLinear.Xoshiro128PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
