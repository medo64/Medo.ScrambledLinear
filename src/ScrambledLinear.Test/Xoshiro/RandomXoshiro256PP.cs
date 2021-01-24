using System;
using Xunit;

namespace Tests.Xoshiro {
    public class RandomXoshiro256PP {

        [Fact(DisplayName = "RandomXoshiro256PP: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoshiro256PP(0);

            Assert.Equal(1225466847, random.Next());
            Assert.Equal(-1014967033, random.Next());
            Assert.Equal(-325420036, random.Next());

            Assert.Equal(0, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(-4, random.Next(-9, 10));

            Assert.Equal(0.074233791034438390, random.NextDouble());
            Assert.Equal(0.314522039617037200, random.NextDouble());
            Assert.Equal(0.066070988288324800, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("C1-0C-96-43-43-55-AE-1A-20-E7", BitConverter.ToString(buffer1));
            Assert.Equal("A8-04-FC-D0-D3-20-46-A0-C3-9C", BitConverter.ToString(buffer3));
            Assert.Equal("FA-10-AC-B8-E7-90-D7-10-F7-96", BitConverter.ToString(buffer2));
        }

        [Fact(DisplayName = "RandomXoshiro256PP: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoshiro256PP(int.MinValue);

            Assert.Equal(-939596861, random.Next());
            Assert.Equal(-564861722, random.Next());
            Assert.Equal(413579755, random.Next());

            Assert.Equal(6, random.Next(10));
            Assert.Equal(2, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(2, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));

            Assert.Equal(0.609022381338771000, random.NextDouble());
            Assert.Equal(0.337581021444537170, random.NextDouble());
            Assert.Equal(0.010739716461182214, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("AA-08-27-78-75-59-57-CE-CA-9A", BitConverter.ToString(buffer1));
            Assert.Equal("65-CA-F8-04-8B-79-69-AC-08-DF", BitConverter.ToString(buffer3));
            Assert.Equal("F2-DE-6B-FF-86-28-1E-CE-B3-99", BitConverter.ToString(buffer2));
        }

        [Fact(DisplayName = "RandomXoshiro256PP: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoshiro256PP(int.MaxValue);

            Assert.Equal(2025513595, random.Next());
            Assert.Equal(1598180243, random.Next());
            Assert.Equal(-1715557663, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(1, random.Next(10));
            Assert.Equal(3, random.Next(10));

            Assert.Equal(-8, random.Next(-9, 10));
            Assert.Equal(4, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));

            Assert.Equal(0.131584233612690230, random.NextDouble());
            Assert.Equal(0.158757900655281730, random.NextDouble());
            Assert.Equal(0.274703500283967700, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("53-13-EC-5E-6E-B1-A6-D1-E6-4F", BitConverter.ToString(buffer1));
            Assert.Equal("F9-F4-FD-0F-9A-33-00-79-D7-F4", BitConverter.ToString(buffer3));
            Assert.Equal("BF-7E-82-A9-2E-FE-BB-7C-0B-BE", BitConverter.ToString(buffer2));
        }


        [Fact(DisplayName = "RandomXoshiro256PP: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoshiro256PP();
            var random2 = new ScrambledLinear.RandomXoshiro256PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
