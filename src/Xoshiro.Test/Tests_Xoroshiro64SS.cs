using System;
using System.Reflection;
using Xunit;

namespace Xoshiro.Test {
    public class Tests_Xoroshiro64SS {

        [Fact(DisplayName = "xoroshiro64**: Reference")]
        public void Test_InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Xoroshiro64SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Xoroshiro64SS).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Xoroshiro64SS();
            sField.SetValue(random, new UInt32[] { 2, 3 });

            UInt32[] values = new UInt32[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt32)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt32)0xc55829e3, values[0]);
            Assert.Equal((UInt32)0x3ad5d56c, values[1]);
            Assert.Equal((UInt32)0x2228bc62, values[2]);
            Assert.Equal((UInt32)0xbce5cbe5, values[3]);
            Assert.Equal((UInt32)0x9b6feedc, values[4]);
            Assert.Equal((UInt32)0x3170179d, values[5]);
            Assert.Equal((UInt32)0x290a849d, values[6]);
            Assert.Equal((UInt32)0x32e9b2bd, values[7]);
            Assert.Equal((UInt32)0x89835db6, values[8]);
            Assert.Equal((UInt32)0xc1baeecf, values[9]);
            Assert.Equal((UInt32)0x4a59225a, values[10]);
            Assert.Equal((UInt32)0x6889b4f8, values[11]);
            Assert.Equal((UInt32)0xef9b8c64, values[12]);
            Assert.Equal((UInt32)0x3c816368, values[13]);
            Assert.Equal((UInt32)0x61293207, values[14]);
            Assert.Equal((UInt32)0xf44d7827, values[15]);
            Assert.Equal((UInt32)0xa8a2f5b4, values[16]);
            Assert.Equal((UInt32)0x4551efc3, values[17]);
            Assert.Equal((UInt32)0x0181292b, values[18]);
            Assert.Equal((UInt32)0xfc1b22f1, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt32)0x8ceb61ad, (UInt32)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoroshiro64**: Seed = 0")]
        public void Test_Init0() {
            var random = new Xoroshiro64SS(0);

            Assert.Equal(-1111907010, random.Next());
            Assert.Equal(1289012084, random.Next());
            Assert.Equal(1443993818, random.Next());

            Assert.Equal(8, random.Next(10));
            Assert.Equal(6, random.Next(10));
            Assert.Equal(7, random.Next(10));

            Assert.Equal(5, random.Next(-9, 10));
            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));

            Assert.Equal(0.680903064319863900, random.NextDouble());
            Assert.Equal(0.507100519724190200, random.NextDouble());
            Assert.Equal(0.148549486184492700, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("0D-5F-0A-E3-F5-BE-DF-B9", BitConverter.ToString(buffer1));
            Assert.Equal("A7-C5-A6-FA-62-C7-79-53", BitConverter.ToString(buffer2));
            Assert.Equal("87-A5-4D-75-C6-29-76-20", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro64**: Seed = Int32.MinValue")]
        public void Test_InitMin() {
            var random = new Xoroshiro64SS(int.MinValue);

            Assert.Equal(-1302761731, random.Next());
            Assert.Equal(1177142087, random.Next());
            Assert.Equal(1379804917, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(1, random.Next(10));
            Assert.Equal(4, random.Next(10));

            Assert.Equal(3, random.Next(-9, 10));
            Assert.Equal(-4, random.Next(-9, 10));
            Assert.Equal(2, random.Next(-9, 10));

            Assert.Equal(0.775412092916667500, random.NextDouble());
            Assert.Equal(0.717774518299847800, random.NextDouble());
            Assert.Equal(0.624506967375055000, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("09-7E-48-D9-DA-C5-25-56", BitConverter.ToString(buffer1));
            Assert.Equal("08-B8-61-3D-F4-02-B0-C0", BitConverter.ToString(buffer2));
            Assert.Equal("4B-D7-8D-E6-5F-A6-DA-4E", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro64**: Seed = Int32.MaxValue")]
        public void Test_InitMax() {
            var random = new Xoroshiro64SS(int.MaxValue);

            Assert.Equal(1378121268, random.Next());
            Assert.Equal(-1353754301, random.Next());
            Assert.Equal(-824274258, random.Next());

            Assert.Equal(8, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(5, random.Next(10));

            Assert.Equal(0, random.Next(-9, 10));
            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));

            Assert.Equal(0.153800031868740920, random.NextDouble());
            Assert.Equal(0.645843763602897500, random.NextDouble());
            Assert.Equal(0.844819278689101300, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("70-67-CF-CB-B5-8F-40-80", BitConverter.ToString(buffer1));
            Assert.Equal("39-F3-EB-47-19-B2-7C-EF", BitConverter.ToString(buffer2));
            Assert.Equal("94-2B-BB-7D-CD-13-34-C1", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoroshiro64**: Two instances compared")]
        public void Test_TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Xoroshiro64SS();
            var random2 = new Xoroshiro64SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
