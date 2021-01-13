using System;
using System.Reflection;
using Xunit;

namespace Xoshiro.Test {
    public class Tests_Xoroshiro1024S {

        [Fact(DisplayName = "xoroshiro1024*: Reference")]
        public void Test_InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Xoroshiro1024S).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Xoroshiro1024S).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Xoroshiro1024S();
            sField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
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
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0xce5141cbb7bd463a, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoroshiro1024*: Seed = 0")]
        public void Test_Init0() {
            var random = new Xoroshiro1024S(0);

            Assert.Equal(1387053340, random.Next());
            Assert.Equal(941123805, random.Next());
            Assert.Equal(752088196, random.Next());

            Assert.Equal(7, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(7, random.Next(10));

            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));

            Assert.Equal(0.535454918970863100, random.NextDouble());
            Assert.Equal(0.134932229618174970, random.NextDouble());
            Assert.Equal(0.519496719151727900, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("7D-52-E3-EE-F0-BF-E2-41", BitConverter.ToString(buffer1));
            Assert.Equal("DB-D4-BB-B4-63-E4-4E-AD", BitConverter.ToString(buffer2));
            Assert.Equal("B1-60-F1-87-61-45-5C-77", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro1024*: Seed = Int32.MinValue")]
        public void Test_InitMin() {
            var random = new Xoroshiro1024S(int.MinValue);

            Assert.Equal(-874894911, random.Next());
            Assert.Equal(-1357654814, random.Next());
            Assert.Equal(932381885, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(0, random.Next(10));
            Assert.Equal(9, random.Next(10));

            Assert.Equal(3, random.Next(-9, 10));
            Assert.Equal(0, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));

            Assert.Equal(0.217409618131061200, random.NextDouble());
            Assert.Equal(0.631029949636985300, random.NextDouble());
            Assert.Equal(0.586980573783609600, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("03-0D-5B-C0-A3-64-05-54", BitConverter.ToString(buffer1));
            Assert.Equal("E4-42-03-40-5E-86-A6-43", BitConverter.ToString(buffer2));
            Assert.Equal("75-B5-BF-CE-0C-6A-65-3D", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro1024*: Seed = Int32.MaxValue")]
        public void Test_InitMax() {
            var random = new Xoroshiro1024S(int.MaxValue);

            Assert.Equal(-543116859, random.Next());
            Assert.Equal(787291130, random.Next());
            Assert.Equal(-379555302, random.Next());

            Assert.Equal(8, random.Next(10));
            Assert.Equal(2, random.Next(10));
            Assert.Equal(9, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));

            Assert.Equal(0.225607599128606400, random.NextDouble());
            Assert.Equal(0.826913124743644500, random.NextDouble());
            Assert.Equal(0.706069752469586000, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("8F-BF-2C-A0-8B-C9-69-9E", BitConverter.ToString(buffer1));
            Assert.Equal("35-57-6C-65-88-38-2E-8D", BitConverter.ToString(buffer2));
            Assert.Equal("2E-82-D2-FA-85-54-2D-12", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoroshiro1024*: Two instances compared")]
        public void Test_TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Xoroshiro1024S();
            var random2 = new Xoroshiro1024S();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
