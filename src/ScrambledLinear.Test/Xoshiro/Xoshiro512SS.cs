using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoshiro {
    public class Xoshiro512SS {

        [Fact(DisplayName = "xoshiro512**: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoshiro512SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoshiro512SS();
            stateField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0x0000000000004380, values[0]);
            Assert.Equal((UInt64)0x0000000000005a00, values[1]);
            Assert.Equal((UInt64)0x0000000000016800, values[2]);
            Assert.Equal((UInt64)0x00000000021d9500, values[3]);
            Assert.Equal((UInt64)0x0000003842d15180, values[4]);
            Assert.Equal((UInt64)0x07080021cb412480, values[5]);
            Assert.Equal((UInt64)0x0438002a3caa7600, values[6]);
            Assert.Equal((UInt64)0x032bc20b48709d80, values[7]);
            Assert.Equal((UInt64)0x44390e5cd70872f6, values[8]);
            Assert.Equal((UInt64)0xc05b0e994fbeb99b, values[9]);
            Assert.Equal((UInt64)0x305943d190b5975b, values[10]);
            Assert.Equal((UInt64)0xf110e6d2ff62013b, values[11]);
            Assert.Equal((UInt64)0xda08f526ac1834c0, values[12]);
            Assert.Equal((UInt64)0xd5049107c4a9283a, values[13]);
            Assert.Equal((UInt64)0x371ee3fbd34bb4cf, values[14]);
            Assert.Equal((UInt64)0xb0b1a25de898dc28, values[15]);
            Assert.Equal((UInt64)0x86a2c16393d2fa86, values[16]);
            Assert.Equal((UInt64)0x07de187bce70d3db, values[17]);
            Assert.Equal((UInt64)0x51ef8ed7cc998183, values[18]);
            Assert.Equal((UInt64)0x0703a352cfda1fa8, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0x270ce1bc058010b5, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512**: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoshiro512SS(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0x99EC5F36CB75F2B4, (UInt64)random.Next());
            Assert.Equal((UInt64)0xBF6E1F784956452A, (UInt64)random.Next());
            Assert.Equal((UInt64)0x3832E5E4541959A2, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro512**: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoshiro512SS(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0x8F5520D52A7EAD08, (UInt64)random.Next());
            Assert.Equal((UInt64)0xC476A018CAA1802D, (UInt64)random.Next());
            Assert.Equal((UInt64)0xD914F51AAC453226, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512**: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoshiro512SS(int.MinValue);

            Assert.Equal((UInt64)0x4B7343315892350A, (UInt64)random.Next());
            Assert.Equal((UInt64)0x5CE3308C831E8C64, (UInt64)random.Next());
            Assert.Equal((UInt64)0x3207EEA4666AD48F, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro512**: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoshiro512SS(int.MaxValue);

            Assert.Equal((UInt64)0x437D8D6D6D91FE4F, (UInt64)random.Next());
            Assert.Equal((UInt64)0x8D37886ECED99432, (UInt64)random.Next());
            Assert.Equal((UInt64)0x6E9AAF270A6B44D0, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512**: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoshiro512SS(long.MinValue);

            Assert.Equal((UInt64)0xD01BFA9B44A998C3, (UInt64)random.Next());
            Assert.Equal((UInt64)0x797C6B72FF690D62, (UInt64)random.Next());
            Assert.Equal((UInt64)0x5420CB613692F1D8, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro512**: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoshiro512SS(long.MaxValue);

            Assert.Equal((UInt64)0xE1C2B4B82E8C0C5, (UInt64)random.Next());
            Assert.Equal((UInt64)0x19167A27A6E0D81B, (UInt64)random.Next());
            Assert.Equal((UInt64)0xD15E25597E6D810D, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512**: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoshiro512SS();
            var random2 = new ScrambledLinear.Xoshiro512SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
