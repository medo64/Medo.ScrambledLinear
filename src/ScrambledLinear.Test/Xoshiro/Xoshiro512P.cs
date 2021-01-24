using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoshiro {
    public class Xoshiro512P {

        [Fact(DisplayName = "xoshiro512+: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoshiro512P).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoshiro512P();
            stateField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0x0000000000000007, values[0]);
            Assert.Equal((UInt64)0x000000000000001a, values[1]);
            Assert.Equal((UInt64)0x000000000000182a, values[2]);
            Assert.Equal((UInt64)0x0000000002803821, values[3]);
            Assert.Equal((UInt64)0x000050000400d03c, values[4]);
            Assert.Equal((UInt64)0x0000800006615015, values[5]);
            Assert.Equal((UInt64)0x0000d4140160f01b, values[6]);
            Assert.Equal((UInt64)0x828014200721b034, values[7]);
            Assert.Equal((UInt64)0x840034300af0681a, values[8]);
            Assert.Equal((UInt64)0x86c0be0f0a706031, values[9]);
            Assert.Equal((UInt64)0xc1a0c63c0ad05c2c, values[10]);
            Assert.Equal((UInt64)0xc1817e5004d8b01d, values[11]);
            Assert.Equal((UInt64)0x4680b3428558cc26, values[12]);
            Assert.Equal((UInt64)0x66d15b730a19be25, values[13]);
            Assert.Equal((UInt64)0x6e43511805cc741d, values[14]);
            Assert.Equal((UInt64)0xe1e271a4482c2220, values[15]);
            Assert.Equal((UInt64)0xbb6ab18b89142f38, values[16]);
            Assert.Equal((UInt64)0xc22213a289167a0f, values[17]);
            Assert.Equal((UInt64)0x9e2232eca6672f26, values[18]);
            Assert.Equal((UInt64)0x244704e7c82761ce, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0x72c2c6ccd0bff8c1, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512+: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoshiro512P(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0xE8E50551FB2712FE, (UInt64)random.Next());
            Assert.Equal((UInt64)0xB38727A95F6D882E, (UInt64)random.Next());
            Assert.Equal((UInt64)0x46231E4CAAB4BF2D, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro512+: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoshiro512P(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0x1D0969C3CDD7AE09, (UInt64)random.Next());
            Assert.Equal((UInt64)0xF28CE6CAC284C14E, (UInt64)random.Next());
            Assert.Equal((UInt64)0xFFBB2DB177BF24E2, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512+: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoshiro512P(int.MinValue);

            Assert.Equal((UInt64)0x3A63846DAD7A8964, (UInt64)random.Next());
            Assert.Equal((UInt64)0x656392FBAE88770B, (UInt64)random.Next());
            Assert.Equal((UInt64)0x6309B4D08ABB4C7F, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro512+: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoshiro512P(int.MaxValue);

            Assert.Equal((UInt64)0xC73116E29A802B45, (UInt64)random.Next());
            Assert.Equal((UInt64)0x94AB19D3A37DEF8C, (UInt64)random.Next());
            Assert.Equal((UInt64)0xE13F10099AE2C6F1, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512+: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoshiro512P(long.MinValue);

            Assert.Equal((UInt64)0xA9C546A1DAB4751B, (UInt64)random.Next());
            Assert.Equal((UInt64)0xF73BA35A3E322B53, (UInt64)random.Next());
            Assert.Equal((UInt64)0xF28C83ECE24298C2, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro512+: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoshiro512P(long.MaxValue);

            Assert.Equal((UInt64)0x167D6AA6DD45E037, (UInt64)random.Next());
            Assert.Equal((UInt64)0xCDBE27520A6A40B7, (UInt64)random.Next());
            Assert.Equal((UInt64)0xA77C7052CEE15741, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro512+: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoshiro512P();
            var random2 = new ScrambledLinear.Xoshiro512P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
