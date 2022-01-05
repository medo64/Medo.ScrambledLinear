using System;
using Xunit;

namespace Tests.Xoroshiro {
    public class RandomXoroshiro1024PP {

        [Fact(DisplayName = "RandomXoroshiro1024PP: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoroshiro1024PP(0);

            Assert.Equal(1288051282, random.Next());
            Assert.Equal(198962350, random.Next());
            Assert.Equal(-1055514397, random.Next());

            Assert.Equal(6, random.Next(10));
            Assert.Equal(2, random.Next(10));
            Assert.Equal(2, random.Next(10));

            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));

            Assert.Equal(0.145621536270223230, random.NextDouble());
            Assert.Equal(0.095426973826949310, random.NextDouble());
            Assert.Equal(0.795454481936413500, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("25-E7-E0-61-D8-CC-0E-7B-E1-3E", BitConverter.ToString(buffer1));
            Assert.Equal("DB-36-4B-24-AA-6A-1A-FF-43-39", BitConverter.ToString(buffer2));
            Assert.Equal("97-FA-50-57-5D-40-B7-E2-F8-CC", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro1024PP: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoroshiro1024PP(int.MinValue);

            Assert.Equal(-1012141778, random.Next());
            Assert.Equal(-1079272197, random.Next());
            Assert.Equal(2044639543, random.Next());

            Assert.Equal(8, random.Next(10));
            Assert.Equal(5, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));

            Assert.Equal(0.356616361318965100, random.NextDouble());
            Assert.Equal(0.755064447193925800, random.NextDouble());
            Assert.Equal(0.904188987169948800, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("3E-35-70-A5-F5-BA-73-2D-B9-83", BitConverter.ToString(buffer1));
            Assert.Equal("8A-E6-4E-B1-B2-DC-F6-F2-05-C1", BitConverter.ToString(buffer2));
            Assert.Equal("4A-E1-0F-02-78-83-B7-31-F3-3C", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro1024PP: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoroshiro1024PP(int.MaxValue);

            Assert.Equal(-45087170, random.Next());
            Assert.Equal(620372668, random.Next());
            Assert.Equal(1985137452, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(0, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(-2, random.Next(-9, 10));

            Assert.Equal(0.248351094451162550, random.NextDouble());
            Assert.Equal(0.574090718208660400, random.NextDouble());
            Assert.Equal(0.997789208240043900, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("98-C0-A8-15-8F-31-A3-85-75-C0", BitConverter.ToString(buffer1));
            Assert.Equal("CE-BD-AB-0C-87-55-AD-F7-0F-17", BitConverter.ToString(buffer2));
            Assert.Equal("F1-20-32-64-99-93-84-91-9A-93", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoroshiro1024PP: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoroshiro1024PP();
            var random2 = new ScrambledLinear.RandomXoroshiro1024PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
