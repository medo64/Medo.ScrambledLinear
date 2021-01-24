using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoroshiro {
    public class Xoroshiro128P {

        [Fact(DisplayName = "xoroshiro128+: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoroshiro128P).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoroshiro128P();
            stateField.SetValue(random, new UInt64[] { 2, 3 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0x0000000000000005, values[0]);
            Assert.Equal((UInt64)0x0000002002010001, values[1]);
            Assert.Equal((UInt64)0x4042034103000401, values[2]);
            Assert.Equal((UInt64)0xc200802906418622, values[3]);
            Assert.Equal((UInt64)0x4b584b8e82114b42, values[4]);
            Assert.Equal((UInt64)0x8873a0a2c478ce45, values[5]);
            Assert.Equal((UInt64)0xec125ba517df4805, values[6]);
            Assert.Equal((UInt64)0xf126e89f888ac936, values[7]);
            Assert.Equal((UInt64)0x7d538b2d6d5c0a31, values[8]);
            Assert.Equal((UInt64)0xc5ca6b5f76568c7e, values[9]);
            Assert.Equal((UInt64)0x3ca69040defb83d5, values[10]);
            Assert.Equal((UInt64)0xf77cc38777498158, values[11]);
            Assert.Equal((UInt64)0xd9ff9ed43deeb20a, values[12]);
            Assert.Equal((UInt64)0xb151cefed8143275, values[13]);
            Assert.Equal((UInt64)0x4470569c543a87b9, values[14]);
            Assert.Equal((UInt64)0x45ed15f66d0d7e9e, values[15]);
            Assert.Equal((UInt64)0xcf5a2b124e4e02fb, values[16]);
            Assert.Equal((UInt64)0xc5b3758d060def69, values[17]);
            Assert.Equal((UInt64)0x4dc6535276d460b7, values[18]);
            Assert.Equal((UInt64)0xdcfc4af9ba43013c, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0x190110b514026bac, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128+: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoroshiro128P(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0x509946A41CD733A3, (UInt64)random.Next());
            Assert.Equal((UInt64)0xD805FCAC6824536E, (UInt64)random.Next());
            Assert.Equal((UInt64)0xDADC02F3E3CF7BE3, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro128+: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoroshiro128P(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0xCE7969DEF75BAEE9, (UInt64)random.Next());
            Assert.Equal((UInt64)0x05C309D0F76F99B0, (UInt64)random.Next());
            Assert.Equal((UInt64)0x89E339D2525A4A88, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128+: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoroshiro128P(int.MinValue);

            Assert.Equal((UInt64)0x387F52C54C13A143, (UInt64)random.Next());
            Assert.Equal((UInt64)0x20FB1E01636371CB, (UInt64)random.Next());
            Assert.Equal((UInt64)0x8E3CC644785941E8, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro128+: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoroshiro128P(int.MaxValue);

            Assert.Equal((UInt64)0x6B74AE01C490F1AE, (UInt64)random.Next());
            Assert.Equal((UInt64)0x9CF9A7E7C13553CD, (UInt64)random.Next());
            Assert.Equal((UInt64)0xA4462016F4D06B36, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128+: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoroshiro128P(long.MinValue);

            Assert.Equal((UInt64)0x0C8E66DAB8DA83ED, (UInt64)random.Next());
            Assert.Equal((UInt64)0xDC25F5219A45505F, (UInt64)random.Next());
            Assert.Equal((UInt64)0x6FAE64839FC3AF54, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro128+: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoroshiro128P(long.MaxValue);

            Assert.Equal((UInt64)0x1C73D895AE8697EE, (UInt64)random.Next());
            Assert.Equal((UInt64)0x2B7D77257BC5C2EC, (UInt64)random.Next());
            Assert.Equal((UInt64)0x6C3BFF7206797525, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro128+: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoroshiro128P();
            var random2 = new ScrambledLinear.Xoroshiro128P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
