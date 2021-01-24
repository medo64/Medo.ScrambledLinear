using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoshiro {
    public class Xoshiro512PP {

        [Fact(DisplayName = "xoshiro512++: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoshiro512PP).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoshiro512PP();
            stateField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0x00000000000e0005, values[0]);
            Assert.Equal((UInt64)0x0000000000340007, values[1]);
            Assert.Equal((UInt64)0x0000000030540014, values[2]);
            Assert.Equal((UInt64)0x0000050070421802, values[3]);
            Assert.Equal((UInt64)0xa0000801a2f8381d, values[4]);
            Assert.Equal((UInt64)0x00005cc2a32aa003, values[5]);
            Assert.Equal((UInt64)0xa82862c1e0961012, values[6]);
            Assert.Equal((UInt64)0x2840225761c9f51b, values[7]);
            Assert.Equal((UInt64)0xeae029f8d4d53802, values[8]);
            Assert.Equal((UInt64)0xff1e48e0c353159b, values[9]);
            Assert.Equal((UInt64)0x0d38d3afbdc9d34e, values[10]);
            Assert.Equal((UInt64)0xbcc0bfd3604bdf14, values[11]);
            Assert.Equal((UInt64)0x27c588bd9d24951a, values[12]);
            Assert.Equal((UInt64)0xfae65f6e00a399b6, values[13]);
            Assert.Equal((UInt64)0x0901669ae9d31a8b, values[14]);
            Assert.Equal((UInt64)0x44e93d6c49edcbe1, values[15]);
            Assert.Equal((UInt64)0x44f87baca59d88f3, values[16]);
            Assert.Equal((UInt64)0x5fad33affae39348, values[17]);
            Assert.Equal((UInt64)0x17ab206ae2e3a053, values[18]);
            Assert.Equal((UInt64)0x67511d1b68e2f7a6, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0xe6388eba147a1c5f, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512++: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoshiro512PP(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0x11685366A6071719, (UInt64)random.Next());
            Assert.Equal((UInt64)0x3437B3FD0B71EFEE, (UInt64)random.Next());
            Assert.Equal((UInt64)0x66E01D101DA703F4, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro512++: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoshiro512PP(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0xBB793FC0E84BBFB, (UInt64)random.Next());
            Assert.Equal((UInt64)0xAA8C0E452BB592E2, (UInt64)random.Next());
            Assert.Equal((UInt64)0x24C3C432FA40BDC2, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512++: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoshiro512PP(int.MinValue);

            Assert.Equal((UInt64)0x55294D789E334CA6, (UInt64)random.Next());
            Assert.Equal((UInt64)0xC84FC07A977C3121, (UInt64)random.Next());
            Assert.Equal((UInt64)0xCAF4627245453CFE, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro512++: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoshiro512PP(int.MinValue);

            Assert.Equal((UInt64)0x55294D789E334CA6, (UInt64)random.Next());
            Assert.Equal((UInt64)0xC84FC07A977C3121, (UInt64)random.Next());
            Assert.Equal((UInt64)0xCAF4627245453CFE, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512++: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoshiro512PP(long.MinValue);

            Assert.Equal((UInt64)0xEEEA3B68B241D4CA,(UInt64)random.Next());
            Assert.Equal((UInt64)0x706CC1C2314B6112, (UInt64)random.Next());
            Assert.Equal((UInt64)0xEC14DD26EAB3AF3C, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro512++: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoshiro512PP(long.MaxValue);

            Assert.Equal((UInt64)0xC1634DDD6FB06E8A, (UInt64)random.Next());
            Assert.Equal((UInt64)0x151658D902B17AB3, (UInt64)random.Next());
            Assert.Equal((UInt64)0xA1DF450BB6ED0DAF, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512++: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoshiro512PP();
            var random2 = new ScrambledLinear.Xoshiro512PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
