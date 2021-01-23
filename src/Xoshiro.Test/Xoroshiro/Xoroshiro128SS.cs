using System;
using System.Reflection;
using Xunit;
using Subject = Xoroshiro;

namespace Test.Xoroshiro {
    public class Xoroshiro128SS {

        [Fact(DisplayName = "xoroshiro128**: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Subject.Xoroshiro128SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Subject.Xoroshiro128SS).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Subject.Xoroshiro128SS();
            sField.SetValue(random, new UInt64[] { 2, 3 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
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
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0xb2d58a35d893d9f9, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoroshiro128**: Seed = 0")]
        public void Init0() {
            var random = new Subject.Xoroshiro128SS(0);

            Assert.Equal(513008477, random.Next());
            Assert.Equal(411655688, random.Next());
            Assert.Equal(-328227702, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(4, random.Next(10));

            Assert.Equal(-4, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(9, random.Next(-9, 10));

            Assert.Equal(0.708651653075394600, random.NextDouble());
            Assert.Equal(0.446242351024120900, random.NextDouble());
            Assert.Equal(0.335942288854950800, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("C4-9A-93-5E-C5-8E-5E-45", BitConverter.ToString(buffer1));
            Assert.Equal("C2-65-8F-C1-0B-2D-5B-22", BitConverter.ToString(buffer2));
            Assert.Equal("53-5A-CD-D5-F4-D0-E4-CF", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro128**: Seed = Int32.MinValue")]
        public void InitMin() {
            var random = new Subject.Xoroshiro128SS(int.MinValue);

            Assert.Equal(1252327749, random.Next());
            Assert.Equal(-647857809, random.Next());
            Assert.Equal(-1209046716, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(8, random.Next(10));
            Assert.Equal(2, random.Next(10));

            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));

            Assert.Equal(0.858084861520274300, random.NextDouble());
            Assert.Equal(0.188885411841849930, random.NextDouble());
            Assert.Equal(0.585610121708333800, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("A2-26-93-43-AA-A4-0C-E5", BitConverter.ToString(buffer1));
            Assert.Equal("85-9F-16-BC-29-93-4A-76", BitConverter.ToString(buffer2));
            Assert.Equal("9F-1D-A2-B1-97-52-09-BB", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro128**: Seed = Int32.MaxValue")]
        public void InitMax() {
            var random = new Subject.Xoroshiro128SS(int.MaxValue);

            Assert.Equal(1336660372, random.Next());
            Assert.Equal(1984164502, random.Next());
            Assert.Equal(22925111, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(4, random.Next(10));

            Assert.Equal(9, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));

            Assert.Equal(0.692134186987936300, random.NextDouble());
            Assert.Equal(0.270536255452348940, random.NextDouble());
            Assert.Equal(0.730583427926399900, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("57-58-B5-EE-76-81-0B-CC", BitConverter.ToString(buffer1));
            Assert.Equal("4F-FE-E7-D5-B1-90-01-F5", BitConverter.ToString(buffer2));
            Assert.Equal("E8-82-1D-6F-F5-7B-76-AD", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoroshiro128**: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Subject.Xoroshiro128SS();
            var random2 = new Subject.Xoroshiro128SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
