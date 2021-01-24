using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoroshiro {
    public class Xoroshiro128SS {

        [Fact(DisplayName = "xoroshiro128**: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoroshiro128SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoroshiro128SS();
            stateField.SetValue(random, new UInt64[] { 2, 3 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0x0000000000002d00, values[0]);
            Assert.Equal((UInt64)0x0000002d16801680, values[1]);
            Assert.Equal((UInt64)0xfd4666c380001680, values[2]);
            Assert.Equal((UInt64)0x0000170719d7311d, values[3]);
            Assert.Equal((UInt64)0xad6460cd1ca9ff7f, values[4]);
            Assert.Equal((UInt64)0x9d27db403189b227, values[5]);
            Assert.Equal((UInt64)0xf7bbe64255a77278, values[6]);
            Assert.Equal((UInt64)0xfc23c2b014f20029, values[7]);
            Assert.Equal((UInt64)0x3d20fe602c2a2204, values[8]);
            Assert.Equal((UInt64)0x276cb6523c1daa93, values[9]);
            Assert.Equal((UInt64)0x9dcf22f402a52d98, values[10]);
            Assert.Equal((UInt64)0xc894b1d2230f129c, values[11]);
            Assert.Equal((UInt64)0xb6f196642d3cf7d4, values[12]);
            Assert.Equal((UInt64)0xf13677b6cbbb17a0, values[13]);
            Assert.Equal((UInt64)0xc832d137e8c1a1fa, values[14]);
            Assert.Equal((UInt64)0x3463981f81504377, values[15]);
            Assert.Equal((UInt64)0x8a3eb1f9ef6e208a, values[16]);
            Assert.Equal((UInt64)0xf3f2793cbeb28a51, values[17]);
            Assert.Equal((UInt64)0x5f7fccfdec767727, values[18]);
            Assert.Equal((UInt64)0x5efc1c51004b1b9f, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0xb2d58a35d893d9f9, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128**: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoroshiro128SS(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0xDEC90D521E93E35D, (UInt64)random.Next());
            Assert.Equal((UInt64)0x6D33AC6F18895E08, (UInt64)random.Next());
            Assert.Equal((UInt64)0xAB21904EEC6FA48A, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro128**: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoroshiro128SS(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0x1C78F7E86460D21C, (UInt64)random.Next());
            Assert.Equal((UInt64)0x486D6868ED82DF60, (UInt64)random.Next());
            Assert.Equal((UInt64)0x581A82CCC82B2157, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128**: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoroshiro128SS(int.MinValue);

            Assert.Equal((UInt64)0xE55313FE611A336B, (UInt64)random.Next());
            Assert.Equal((UInt64)0x491B609E992673BE, (UInt64)random.Next());
            Assert.Equal((UInt64)0xB355BDB0FDC06CD1, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro128**: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoroshiro128SS(int.MaxValue);

            Assert.Equal((UInt64)0x7DCD9A594FABD194, (UInt64)random.Next());
            Assert.Equal((UInt64)0x9386B8927643F296, (UInt64)random.Next());
            Assert.Equal((UInt64)0x9F1BC4B7015DCF37, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128**: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoroshiro128SS(long.MinValue);

            Assert.Equal((UInt64)0xB3EE3EA3EFEEC154, (UInt64)random.Next());
            Assert.Equal((UInt64)0xED64991026FD4C66, (UInt64)random.Next());
            Assert.Equal((UInt64)0xA967F7439B28AD96, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro128**: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoroshiro128SS(long.MaxValue);

            Assert.Equal((UInt64)0x206CFC8B5171B13A, (UInt64)random.Next());
            Assert.Equal((UInt64)0x92D06AB19784D904, (UInt64)random.Next());
            Assert.Equal((UInt64)0x52E45D4370EAE307, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128**: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoroshiro128SS();
            var random2 = new ScrambledLinear.Xoroshiro128SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
