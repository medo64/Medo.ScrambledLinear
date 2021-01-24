using System;
using Xunit;

namespace Tests.Xoroshiro {
    public class RandomXoroshiro128PP {

        [Fact(DisplayName = "RandomXoroshiro128PP: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoroshiro128PP(0);

            Assert.Equal(-496734495, random.Next());
            Assert.Equal(1161860269, random.Next());
            Assert.Equal(1865473592, random.Next());

            Assert.Equal(4, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(3, random.Next(10));

            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(4, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));

            Assert.Equal(0.316018995128142470, random.NextDouble());
            Assert.Equal(0.663537559698846600, random.NextDouble());
            Assert.Equal(0.747768719361041200, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("2B-C9-C1-A3-21-FF-50-D6-01-3B", BitConverter.ToString(buffer1));
            Assert.Equal("41-A5-34-92-7F-19-73-BD-AE-7C", BitConverter.ToString(buffer2));
            Assert.Equal("5C-90-06-DD-D4-B7-92-A7-9E-F7", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro128PP: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoroshiro128PP(int.MinValue);

            Assert.Equal(1687560835, random.Next());
            Assert.Equal(-1423442478, random.Next());
            Assert.Equal(2130192593, random.Next());

            Assert.Equal(6, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));

            Assert.Equal(0.43923267828717670, random.NextDouble());
            Assert.Equal(0.64570995302010180, random.NextDouble());
            Assert.Equal(0.04572897876909199, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("C2-1C-38-7F-06-00-BD-21-6D-E2", BitConverter.ToString(buffer1));
            Assert.Equal("E3-14-59-CE-58-13-A3-8B-F7-31", BitConverter.ToString(buffer2));
            Assert.Equal("3C-67-A4-43-A6-C4-B5-18-3F-5E", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro128PP: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoroshiro128PP(int.MaxValue);

            Assert.Equal(158802640, random.Next());
            Assert.Equal(1569184532, random.Next());
            Assert.Equal(-1163646361, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(7, random.Next(10));

            Assert.Equal(-9, random.Next(-9, 10));
            Assert.Equal(9, random.Next(-9, 10));
            Assert.Equal(1, random.Next(-9, 10));

            Assert.Equal(0.388907956085095650, random.NextDouble());
            Assert.Equal(0.140679433361960320, random.NextDouble());
            Assert.Equal(0.047973743341569010, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("9F-EB-84-49-6C-AA-BC-92-5A-26", BitConverter.ToString(buffer1));
            Assert.Equal("99-FE-43-AD-DC-D7-AF-FC-79-E0", BitConverter.ToString(buffer2));
            Assert.Equal("91-CC-34-49-1B-D3-09-51-EE-FE", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoroshiro128PP: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoroshiro128PP();
            var random2 = new ScrambledLinear.RandomXoroshiro128PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
