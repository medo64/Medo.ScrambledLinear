using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoshiro {
    public class Xoshiro256PP {

        [Fact(DisplayName = "xoshiro256++: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoshiro256PP).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoshiro256PP();
            stateField.SetValue(random, new UInt64[] { 2, 3, 5, 7 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0x0000000004800002, values[0]);
            Assert.Equal((UInt64)0x0000000003000046, values[1]);
            Assert.Equal((UInt64)0x0008800001000082, values[2]);
            Assert.Equal((UInt64)0x0010030113e60057, values[3]);
            Assert.Equal((UInt64)0xc0022702016e227d, values[4]);
            Assert.Equal((UInt64)0xca587acd558440ae, values[5]);
            Assert.Equal((UInt64)0x4e13c15601e78908, values[6]);
            Assert.Equal((UInt64)0xf8d2350bc60ede18, values[7]);
            Assert.Equal((UInt64)0x1d5c06e32d169564, values[8]);
            Assert.Equal((UInt64)0x99f5c2b8ea03589c, values[9]);
            Assert.Equal((UInt64)0xcc22445de2b73aa0, values[10]);
            Assert.Equal((UInt64)0xdcb4f8d194b08f29, values[11]);
            Assert.Equal((UInt64)0x4684e3549410df33, values[12]);
            Assert.Equal((UInt64)0x726fb165f126ae40, values[13]);
            Assert.Equal((UInt64)0xb0791972099884d1, values[14]);
            Assert.Equal((UInt64)0xeb54513457404e6d, values[15]);
            Assert.Equal((UInt64)0x94b4d1c7e1cfcb3f, values[16]);
            Assert.Equal((UInt64)0xea097ea70ded95ce, values[17]);
            Assert.Equal((UInt64)0x7b6e92d65d86c925, values[18]);
            Assert.Equal((UInt64)0x7747c1a709a94dbb, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0x537a0323a1edb13b, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256++: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoshiro256PP(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0x53175D61490B23DF, (UInt64)random.Next());
            Assert.Equal((UInt64)0x61DA6F3DC380D507, (UInt64)random.Next());
            Assert.Equal((UInt64)0x5C0FDF91EC9A7BFC, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro256++: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoshiro256PP(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0x56CCF8CE948E27B2, (UInt64)random.Next());
            Assert.Equal((UInt64)0xE68588432E5A5B90, (UInt64)random.Next());
            Assert.Equal((UInt64)0xE3E9B5A48119CA8B, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256++: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoshiro256PP(int.MinValue);

            Assert.Equal((UInt64)0x4D0C7CC1C7FEE3C3, (UInt64)random.Next());
            Assert.Equal((UInt64)0xE8074142DE54E4E6, (UInt64)random.Next());
            Assert.Equal((UInt64)0xB08A559618A6B9EB, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro256++: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoshiro256PP(int.MinValue);

            Assert.Equal((UInt64)0x4D0C7CC1C7FEE3C3, (UInt64)random.Next());
            Assert.Equal((UInt64)0xE8074142DE54E4E6, (UInt64)random.Next());
            Assert.Equal((UInt64)0xB08A559618A6B9EB, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256++: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoshiro256PP(long.MinValue);

            Assert.Equal((UInt64)0xDAEA07477CFA9A4E, (UInt64)random.Next());
            Assert.Equal((UInt64)0xB61308E62CA15389, (UInt64)random.Next());
            Assert.Equal((UInt64)0xFB3CDD02C5E74124, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro256++: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoshiro256PP(long.MaxValue);

            Assert.Equal((UInt64)0xA14925D27F28E2AB, (UInt64)random.Next());
            Assert.Equal((UInt64)0xE1AC012C894E8DDB, (UInt64)random.Next());
            Assert.Equal((UInt64)0x015F08B1AF9E9938, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256++: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoshiro256PP();
            var random2 = new ScrambledLinear.Xoshiro256PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
