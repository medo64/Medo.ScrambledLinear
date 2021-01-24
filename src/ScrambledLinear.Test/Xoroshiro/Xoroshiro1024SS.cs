using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoroshiro {
    public class Xoroshiro1024SS {

        [Fact(DisplayName = "xoroshiro1024**: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoroshiro1024SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoroshiro1024SS();
            stateField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0x0000000000004380, values[0]);
            Assert.Equal((UInt64)0x0000000000007080, values[1]);
            Assert.Equal((UInt64)0x0000000000009d80, values[2]);
            Assert.Equal((UInt64)0x000000000000f780, values[3]);
            Assert.Equal((UInt64)0x0000000000012480, values[4]);
            Assert.Equal((UInt64)0x0000000000017e80, values[5]);
            Assert.Equal((UInt64)0x000000000001ab80, values[6]);
            Assert.Equal((UInt64)0x0000000000020580, values[7]);
            Assert.Equal((UInt64)0x0000000000028c80, values[8]);
            Assert.Equal((UInt64)0x000000000002b980, values[9]);
            Assert.Equal((UInt64)0x0000000000034080, values[10]);
            Assert.Equal((UInt64)0x0000000000039a80, values[11]);
            Assert.Equal((UInt64)0x000000000003c780, values[12]);
            Assert.Equal((UInt64)0x0000000000042180, values[13]);
            Assert.Equal((UInt64)0x000000000004a880, values[14]);
            Assert.Equal((UInt64)0x0000013b00001680, values[15]);
            Assert.Equal((UInt64)0x00016afd000072c0, values[16]);
            Assert.Equal((UInt64)0x0007c0bf00171fc0, values[17]);
            Assert.Equal((UInt64)0x016fc2db007179c0, values[18]);
            Assert.Equal((UInt64)0x07c7fe05171ea6c0, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0x666167f11124a633, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024**: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoroshiro1024SS(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0x99EC5F36CB75F2B4, (UInt64)random.Next());
            Assert.Equal((UInt64)0x422EA740D0977210, (UInt64)random.Next());
            Assert.Equal((UInt64)0x47BACE0BB96B41D5, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro1024**: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoroshiro1024SS(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0x8F5520D52A7EAD08, (UInt64)random.Next());
            Assert.Equal((UInt64)0x3752BDAF106AFAEC, (UInt64)random.Next());
            Assert.Equal((UInt64)0x1C450FE665FF7590, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024**: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoroshiro1024SS(int.MinValue);

            Assert.Equal((UInt64)0x4B7343315892350A, (UInt64)random.Next());
            Assert.Equal((UInt64)0xD9D08FC0E3F91BAE, (UInt64)random.Next());
            Assert.Equal((UInt64)0xEFA39D68E6CDEA18, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro1024**: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoroshiro1024SS(int.MaxValue);

            Assert.Equal((UInt64)0x437D8D6D6D91FE4F, (UInt64)random.Next());
            Assert.Equal((UInt64)0x52B5503AF421C765, (UInt64)random.Next());
            Assert.Equal((UInt64)0xFA4254A8C262B495, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024**: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoroshiro1024SS(long.MinValue);

            Assert.Equal((UInt64)0xD01BFA9B44A998C3, (UInt64)random.Next());
            Assert.Equal((UInt64)0x22C6FB14EC5C2414, (UInt64)random.Next());
            Assert.Equal((UInt64)0x8E628F177B24E5CE, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro1024**: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoroshiro1024SS(long.MaxValue);

            Assert.Equal((UInt64)0xE1C2B4B82E8C0C5, (UInt64)random.Next());
            Assert.Equal((UInt64)0xE572ADE752C32ABE, (UInt64)random.Next());
            Assert.Equal((UInt64)0xD45BE90F7A959250, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024**: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoroshiro1024SS();
            var random2 = new ScrambledLinear.Xoroshiro1024SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
