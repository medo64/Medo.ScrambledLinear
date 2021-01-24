using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoroshiro {
    public class Xoroshiro1024PP {

        [Fact(DisplayName = "xoroshiro1024++: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoroshiro1024PP).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoroshiro1024PP();
            stateField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0x0000000002800002, values[0]);
            Assert.Equal((UInt64)0x0800001002800000, values[1]);
            Assert.Equal((UInt64)0x2800005083800100, values[2]);
            Assert.Equal((UInt64)0x3800107285800508, values[3]);
            Assert.Equal((UInt64)0x5800513386810728, values[4]);
            Assert.Equal((UInt64)0x6810735588851338, values[5]);
            Assert.Equal((UInt64)0x885134968a873558, values[6]);
            Assert.Equal((UInt64)0xa87356b890934968, values[7]);
            Assert.Equal((UInt64)0x093497fa95b56b88, values[8]);
            Assert.Equal((UInt64)0x5b56ba60a2c97fa8, values[9]);
            Assert.Equal((UInt64)0x2c97fa85c7eba608, values[10]);
            Assert.Equal((UInt64)0x7eba62e2ddffa85b, values[11]);
            Assert.Equal((UInt64)0xdffa845801262e2b, values[12]);
            Assert.Equal((UInt64)0x0262e14e1728457d, values[13]);
            Assert.Equal((UInt64)0x62845ab040ae14e0, values[14]);
            Assert.Equal((UInt64)0xfae84f4628c5ab02, values[15]);
            Assert.Equal((UInt64)0xa46bb01fb064f461, values[16]);
            Assert.Equal((UInt64)0x526a4672c87b02f8, values[17]);
            Assert.Equal((UInt64)0xc3d71f6a2ae461a4, values[18]);
            Assert.Equal((UInt64)0x727f6b474392fb52, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0x30629abe64408ac8, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024++: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoroshiro1024PP(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0x342F13D34CC61A52, (UInt64)random.Next());
            Assert.Equal((UInt64)0x12ED4C0E0BDBECAE, (UInt64)random.Next());
            Assert.Equal((UInt64)0x423FA430C11620E3, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro1024++: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoroshiro1024PP(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0xD4551F4E904C68D4, (UInt64)random.Next());
            Assert.Equal((UInt64)0x77FE5C1A4F09467F, (UInt64)random.Next());
            Assert.Equal((UInt64)0x418F8BCB5DB8CD26, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024++: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoroshiro1024PP(int.MinValue);

            Assert.Equal((UInt64)0x50BB9BBAC3ABF12E, (UInt64)random.Next());
            Assert.Equal((UInt64)0x9FAF7BAFBFAB9CFB, (UInt64)random.Next());
            Assert.Equal((UInt64)0xB346708579DEB937, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro1024++: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoroshiro1024PP(int.MaxValue);

            Assert.Equal((UInt64)0x62DC7F1EFD50063E, (UInt64)random.Next());
            Assert.Equal((UInt64)0xA84D170624FA22BC, (UInt64)random.Next());
            Assert.Equal((UInt64)0xE60C554A7652CB2C, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024++: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoroshiro1024PP(long.MinValue);

            Assert.Equal((UInt64)0xB57B2DE409303B0E, (UInt64)random.Next());
            Assert.Equal((UInt64)0x95DDCD0E3CEC080C, (UInt64)random.Next());
            Assert.Equal((UInt64)0x8CD2062937B114CA, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro1024++: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoroshiro1024PP(long.MaxValue);

            Assert.Equal((UInt64)0x753F1AA12511D893, (UInt64)random.Next());
            Assert.Equal((UInt64)0x97B17DDEFC277762, (UInt64)random.Next());
            Assert.Equal((UInt64)0xBC5C78B91F1EDEE1, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024++: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoroshiro1024PP();
            var random2 = new ScrambledLinear.Xoroshiro1024PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
