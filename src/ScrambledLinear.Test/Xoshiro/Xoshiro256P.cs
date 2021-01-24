using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoshiro {
    public class Xoshiro256P {

        [Fact(DisplayName = "xoshiro256+: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoshiro256P).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoshiro256P();
            stateField.SetValue(random, new UInt64[] { 2, 3, 5, 7 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0x0000000000000009, values[0]);
            Assert.Equal((UInt64)0x0000800000000006, values[1]);
            Assert.Equal((UInt64)0x0001000010000002, values[2]);
            Assert.Equal((UInt64)0xc000a00020060207, values[3]);
            Assert.Equal((UInt64)0xc040f800040e0402, values[4]);
            Assert.Equal((UInt64)0x008158142f058283, values[5]);
            Assert.Equal((UInt64)0xb011081c271283e5, values[6]);
            Assert.Equal((UInt64)0x10b46611140a0766, values[7]);
            Assert.Equal((UInt64)0x1024fa1aaed1a428, values[8]);
            Assert.Equal((UInt64)0x8470b213b341509c, values[9]);
            Assert.Equal((UInt64)0xea54448fab44b08e, values[10]);
            Assert.Equal((UInt64)0xd2f6c3546a188728, values[11]);
            Assert.Equal((UInt64)0xe2dd02e02e79ed5d, values[12]);
            Assert.Equal((UInt64)0xb9e39324498a0d21, values[13]);
            Assert.Equal((UInt64)0xd9f0b839115f6091, values[14]);
            Assert.Equal((UInt64)0x7df3fb90c21787f5, values[15]);
            Assert.Equal((UInt64)0x02802f1c3f4e70e1, values[16]);
            Assert.Equal((UInt64)0xa4bdadfa0bdc3fb2, values[17]);
            Assert.Equal((UInt64)0x1ededdd4f34980b4, values[18]);
            Assert.Equal((UInt64)0xc677c1b3bb034e51, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0x81c7fa64dcc586d9, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256+: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoshiro256P(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0xDAAC60E1ED6A4F9B, (UInt64)random.Next());
            Assert.Equal((UInt64)0x3156A1DA0DC08435, (UInt64)random.Next());
            Assert.Equal((UInt64)0xF9BA3E3285D046AB, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro256+: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoshiro256P(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0x51F724E3E70EAEF2, (UInt64)random.Next());
            Assert.Equal((UInt64)0x405EAB0C549B8E46, (UInt64)random.Next());
            Assert.Equal((UInt64)0xBED2B65D70F3E8A0, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256+: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoshiro256P(int.MinValue);

            Assert.Equal((UInt64)0xDE647CBDEDD5AF4B, (UInt64)random.Next());
            Assert.Equal((UInt64)0x1681D327A90AC035, (UInt64)random.Next());
            Assert.Equal((UInt64)0x710794647C8D02E5, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro256+: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoshiro256P(int.MaxValue);

            Assert.Equal((UInt64)0x412D2887C73400A5, (UInt64)random.Next());
            Assert.Equal((UInt64)0x99E1EAE52A4CA28B, (UInt64)random.Next());
            Assert.Equal((UInt64)0x26FDA907F90C47A8, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256+: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoshiro256P(long.MinValue);

            Assert.Equal((UInt64)0xA14CE725968D4AD4, (UInt64)random.Next());
            Assert.Equal((UInt64)0x4E3CB3C1679199EA, (UInt64)random.Next());
            Assert.Equal((UInt64)0xD11C4375CE185C93, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoshiro256+: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoshiro256P(long.MaxValue);

            Assert.Equal((UInt64)0x4A8808EDC29CFAA2, (UInt64)random.Next());
            Assert.Equal((UInt64)0x6CA381D2C0333E9E, (UInt64)random.Next());
            Assert.Equal((UInt64)0xBD00A091F9A049E3, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoshiro256+: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoshiro256P();
            var random2 = new ScrambledLinear.Xoshiro256P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
