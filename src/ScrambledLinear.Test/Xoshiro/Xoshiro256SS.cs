using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoshiro {
    public class Xoshiro256SS {

        [Fact(DisplayName = "xoshiro256**: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoshiro256SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoshiro256SS();
            stateField.SetValue(random, new UInt64[] { 2, 3, 5, 7 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0x0000000000004380, values[0]);
            Assert.Equal((UInt64)0x0000000000005a00, values[1]);
            Assert.Equal((UInt64)0x0000000087007080, values[2]);
            Assert.Equal((UInt64)0x0b400000b4008700, values[3]);
            Assert.Equal((UInt64)0x00010f68e1002d00, values[4]);
            Assert.Equal((UInt64)0x0e116800e12da0e0, values[5]);
            Assert.Equal((UInt64)0xcefdc25ae1000360, values[6]);
            Assert.Equal((UInt64)0xdd5d68f882f886e0, values[7]);
            Assert.Equal((UInt64)0xffb0b6069997a2c0, values[8]);
            Assert.Equal((UInt64)0x5b69f105c388cbb0, values[9]);
            Assert.Equal((UInt64)0xa6be22b82be21748, values[10]);
            Assert.Equal((UInt64)0xd7b7e4da0bea4a7a, values[11]);
            Assert.Equal((UInt64)0x55fa51ef1d81ab99, values[12]);
            Assert.Equal((UInt64)0x001b6f78c57ec571, values[13]);
            Assert.Equal((UInt64)0xcc2e78fb73903631, values[14]);
            Assert.Equal((UInt64)0x8a261fdbe7516eae, values[15]);
            Assert.Equal((UInt64)0x0d3ea5f7b8c53bf1, values[16]);
            Assert.Equal((UInt64)0xcd47873f3da30cc1, values[17]);
            Assert.Equal((UInt64)0x7afaa696bd37c0c9, values[18]);
            Assert.Equal((UInt64)0x99bd65623d78cd51, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0x53b12b13cdb8b059, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256**: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoshiro256SS(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0x99EC5F36CB75F2B4, (UInt64)random.Next());
            Assert.Equal((UInt64)0xBF6E1F784956452A, (UInt64)random.Next());
            Assert.Equal((UInt64)0x1A5F849D4933E6E0, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro256**: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoshiro256SS(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0x8F5520D52A7EAD08, (UInt64)random.Next());
            Assert.Equal((UInt64)0xC476A018CAA1802D, (UInt64)random.Next());
            Assert.Equal((UInt64)0x81DE31C0D260469E, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256**: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoshiro256SS(int.MinValue);

            Assert.Equal((UInt64)0x4B7343315892350A, (UInt64)random.Next());
            Assert.Equal((UInt64)0x5CE3308C831E8C64, (UInt64)random.Next());
            Assert.Equal((UInt64)0x58A6F7CF05B3E407, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro256**: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoshiro256SS(int.MaxValue);

            Assert.Equal((UInt64)0x437D8D6D6D91FE4F, (UInt64)random.Next());
            Assert.Equal((UInt64)0x8D37886ECED99432, (UInt64)random.Next());
            Assert.Equal((UInt64)0x3F9E82FA7AF15511, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256**: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoshiro256SS(long.MinValue);

            Assert.Equal((UInt64)0xD01BFA9B44A998C3, (UInt64)random.Next());
            Assert.Equal((UInt64)0x797C6B72FF690D62, (UInt64)random.Next());
            Assert.Equal((UInt64)0x4576AF98398380B1, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro256**: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoshiro256SS(long.MaxValue);

            Assert.Equal((UInt64)0x0E1C2B4B82E8C0C5, (UInt64)random.Next());
            Assert.Equal((UInt64)0x19167A27A6E0D81B, (UInt64)random.Next());
            Assert.Equal((UInt64)0x7B5F1A55D35896BD, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256**: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoshiro256SS();
            var random2 = new ScrambledLinear.Xoshiro256SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
