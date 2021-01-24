using System;
using System.Reflection;
using Xunit;

namespace Tests.Xoroshiro {
    public class Xoroshiro1024S {

        [Fact(DisplayName = "xoroshiro1024*: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var stateField = typeof(ScrambledLinear.Xoroshiro1024S).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new ScrambledLinear.Xoroshiro1024S();
            stateField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)random.Next();
            }

            Assert.Equal((UInt64)0xdaa66d2c7ddf7439, values[0]);
            Assert.Equal((UInt64)0x1715609f7c746c5f, values[1]);
            Assert.Equal((UInt64)0x538454127b096485, values[2]);
            Assert.Equal((UInt64)0xcc623af8783354d1, values[3]);
            Assert.Equal((UInt64)0x08d12e6b76c84cf7, values[4]);
            Assert.Equal((UInt64)0x81af155173f23d43, values[5]);
            Assert.Equal((UInt64)0xbe1e08c472873569, values[6]);
            Assert.Equal((UInt64)0x36fbefaa6fb125b5, values[7]);
            Assert.Equal((UInt64)0xec48ca036b700e27, values[8]);
            Assert.Equal((UInt64)0x28b7bd766a05064d, values[9]);
            Assert.Equal((UInt64)0xde0497cf65c3eebf, values[10]);
            Assert.Equal((UInt64)0x56e27eb562eddf0b, values[11]);
            Assert.Equal((UInt64)0x935172286182d731, values[12]);
            Assert.Equal((UInt64)0x0c2f590e5eacc77d, values[13]);
            Assert.Equal((UInt64)0xc17c33675a6bafef, values[14]);
            Assert.Equal((UInt64)0xc32d8c82894a7c13, values[15]);
            Assert.Equal((UInt64)0x2ea5064a02746c5f, values[16]);
            Assert.Equal((UInt64)0xed726a45c7857785, values[17]);
            Assert.Equal((UInt64)0x4fe762cab69fb3d1, values[18]);
            Assert.Equal((UInt64)0x5bc65d89723fd1f7, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0xce5141cbb7bd463a, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024*: Seed = UInt64.MinValue")]
        public void InternalSeedMin() {
            var random = new ScrambledLinear.Xoroshiro1024S(unchecked((long)UInt64.MinValue));

            Assert.Equal((UInt64)0x505422BF52ACC11C, (UInt64)random.Next());
            Assert.Equal((UInt64)0x3CB2CFFD381868DD, (UInt64)random.Next());
            Assert.Equal((UInt64)0x659BB9FB2CD3F484, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro1024*: Seed = UInt64.MaxValue")]
        public void InternalSeedMax() {
            var random = new ScrambledLinear.Xoroshiro1024S(unchecked((long)UInt64.MaxValue));

            Assert.Equal((UInt64)0xC77B22A53FBF10EB, (UInt64)random.Next());
            Assert.Equal((UInt64)0x05BCFA0DD9C6804B, (UInt64)random.Next());
            Assert.Equal((UInt64)0x560BC66137A66D96, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024*: Seed = Int32.MinValue")]
        public void SeedInt32Min() {
            var random = new ScrambledLinear.Xoroshiro1024S(int.MinValue);

            Assert.Equal((UInt64)0x37709FFE9556D31A, (UInt64)random.Next());
            Assert.Equal((UInt64)0xA986C6C20F4098D, (UInt64)random.Next());
            Assert.Equal((UInt64)0x937B1EA29BD9BDB2, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro1024*: Seed = Int32.MaxValue")]
        public void SeedInt32Max() {
            var random = new ScrambledLinear.Xoroshiro1024S(int.MaxValue);

            Assert.Equal((UInt64)0x01D9C2E6DFA0B1C5, (UInt64)random.Next());
            Assert.Equal((UInt64)0x2C4BF50E2EED1BFA, (UInt64)random.Next());
            Assert.Equal((UInt64)0xC2E8FB2DE960721A, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024*: Seed = Int64.MinValue")]
        public void SeedInt64Min() {
            var random = new ScrambledLinear.Xoroshiro1024S(long.MinValue);

            Assert.Equal((UInt64)0xECAD87386E976956, (UInt64)random.Next());
            Assert.Equal((UInt64)0x53DF87490BE297C0, (UInt64)random.Next());
            Assert.Equal((UInt64)0x7A6B027292FA107B, (UInt64)random.Next());
        }

        [Fact(DisplayName = "xoroshiro1024*: Seed = Int64.MaxValue")]
        public void SeedInt64Max() {
            var random = new ScrambledLinear.Xoroshiro1024S(long.MaxValue);

            Assert.Equal((UInt64)0x4A9F28DE41FCE445, (UInt64)random.Next());
            Assert.Equal((UInt64)0x025278B17D4C9DB0, (UInt64)random.Next());
            Assert.Equal((UInt64)0x8E76BAA66B7D67A1, (UInt64)random.Next());
        }


        [Fact(DisplayName = "xoroshiro1024*: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.Xoroshiro1024S();
            var random2 = new ScrambledLinear.Xoroshiro1024S();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
