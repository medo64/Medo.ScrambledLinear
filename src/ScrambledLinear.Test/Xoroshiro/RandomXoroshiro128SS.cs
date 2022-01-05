using System;
using Xunit;

namespace Tests.Xoroshiro {
    public class RandomXoroshiro128SS {

        [Fact(DisplayName = "RandomXoroshiro128SS: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoroshiro128SS(0);

            Assert.Equal(513008477, random.Next());
            Assert.Equal(411655688, random.Next());
            Assert.Equal(-328227702, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(4, random.Next(10));

            Assert.Equal(-4, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(9, random.Next(-9, 10));

            Assert.Equal(0.708651653075394700, random.NextDouble());
            Assert.Equal(0.446242351024120900, random.NextDouble());
            Assert.Equal(0.335942288854950800, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("C4-9A-93-5E-D9-DC-1F-65-C5-8E", BitConverter.ToString(buffer1));
            Assert.Equal("C2-65-8F-C1-9B-33-D2-A5-0B-2D", BitConverter.ToString(buffer2));
            Assert.Equal("53-5A-CD-D5-21-2F-1D-BA-F4-D0", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro128SS: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoroshiro128SS(int.MinValue);

            Assert.Equal(1629107051, random.Next());
            Assert.Equal(-1725533250, random.Next());
            Assert.Equal(-37720879, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(4, random.Next(10));

            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));
            Assert.Equal(8, random.Next(-9, 10));

            Assert.Equal(0.197384636126208270, random.NextDouble());
            Assert.Equal(0.098027479767138880, random.NextDouble());
            Assert.Equal(0.592142293683830600, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("2D-90-A4-10-30-FC-74-13-74-6B", BitConverter.ToString(buffer1));
            Assert.Equal("1C-05-63-28-91-38-3B-E8-12-3E", BitConverter.ToString(buffer2));
            Assert.Equal("7A-22-8C-03-20-09-C2-60-7C-44", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro128SS: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoroshiro128SS(int.MaxValue);

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
            Assert.Equal(0.270536255452349050, random.NextDouble());
            Assert.Equal(0.730583427926400000, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("57-58-B5-EE-55-78-B9-84-76-81", BitConverter.ToString(buffer1));
            Assert.Equal("4F-FE-E7-D5-F2-5B-7E-90-B1-90", BitConverter.ToString(buffer2));
            Assert.Equal("E8-82-1D-6F-FA-A2-10-C3-F5-7B", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoroshiro128SS: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoroshiro128SS();
            var random2 = new ScrambledLinear.RandomXoroshiro128SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
