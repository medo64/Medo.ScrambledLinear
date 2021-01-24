using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoshiro {
    public class Xoshiro128SS {

        [Fact(DisplayName = "xoshiro128**: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoshiro128SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoshiro128SS();
            stateField.SetValue(random, new UInt32[] { 2, 3, 5, 7 });

            UInt32[] values = new UInt32[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt32)random.Next();
            }

            Assert.Equal((UInt32)0x00004380, values[0]);
            Assert.Equal((UInt32)0x00005a00, values[1]);
            Assert.Equal((UInt32)0x00877080, values[2]);
            Assert.Equal((UInt32)0x03848700, values[3]);
            Assert.Equal((UInt32)0x8ee12d12, values[4]);
            Assert.Equal((UInt32)0x72fe5180, values[5]);
            Assert.Equal((UInt32)0x3405045c, values[6]);
            Assert.Equal((UInt32)0xc973e406, values[7]);
            Assert.Equal((UInt32)0xe1049f9f, values[8]);
            Assert.Equal((UInt32)0x0e26e7fc, values[9]);
            Assert.Equal((UInt32)0x66dc59f6, values[10]);
            Assert.Equal((UInt32)0x08d7750b, values[11]);
            Assert.Equal((UInt32)0xb5202b60, values[12]);
            Assert.Equal((UInt32)0x0dcfff1f, values[13]);
            Assert.Equal((UInt32)0xf742df2c, values[14]);
            Assert.Equal((UInt32)0x86b0f158, values[15]);
            Assert.Equal((UInt32)0xb5409aeb, values[16]);
            Assert.Equal((UInt32)0xeecc7092, values[17]);
            Assert.Equal((UInt32)0xfb7aac73, values[18]);
            Assert.Equal((UInt32)0x045a67cf, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt32)0x35b471ff, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128**: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoshiro128SS(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt32)0xCB75F2B4, (UInt32)random.Next());
            Assert.Equal((UInt32)0x49564572, (UInt32)random.Next());
            Assert.Equal((UInt32)0x176FE55D, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoshiro128**: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoshiro128SS(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt32)0x2A7EABCD, (UInt32)random.Next());
            Assert.Equal((UInt32)0xCAA18117, (UInt32)random.Next());
            Assert.Equal((UInt32)0x2FDB45EA, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128**: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoshiro128SS(int.MinValue);

            Assert.Equal((UInt32)0x589236B1, (UInt32)random.Next());
            Assert.Equal((UInt32)0x831E8C88, (UInt32)random.Next());
            Assert.Equal((UInt32)0xDCBDE66B, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoshiro128**: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoshiro128SS(int.MaxValue);

            Assert.Equal((UInt32)0x6D91FDEC, (UInt32)random.Next());
            Assert.Equal((UInt32)0xCED9976E, (UInt32)random.Next());
            Assert.Equal((UInt32)0x5DF652D1, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128**: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoshiro128SS(long.MinValue);

            Assert.Equal((UInt32)0x44A99617, (UInt32)random.Next());
            Assert.Equal((UInt32)0xFF690C6F, (UInt32)random.Next());
            Assert.Equal((UInt32)0x5C598021, (UInt32)random.Next());
        }

        [Fact(DisplayName = "xoshiro128**: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoshiro128SS(long.MaxValue);

            Assert.Equal((UInt32)0x82E8BFC9, (UInt32)random.Next());
            Assert.Equal((UInt32)0xA6E0D824, (UInt32)random.Next());
            Assert.Equal((UInt32)0xAA9F994E, (UInt32)random.Next());
        }


        [Fact(DisplayName = "xoshiro128**: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoshiro128SS();
            var random2 = new ScrambledLinear.Xoshiro128SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
