using System;
using Xunit;

namespace Tests.Xoshiro {
    public class RandomXoshiro128SS {

        [Fact(DisplayName = "RandomXoshiro128SS: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoshiro128SS(0);

            Assert.Equal(-881462604, random.Next());
            Assert.Equal(1230390642, random.Next());
            Assert.Equal(393209181, random.Next());

            Assert.Equal(6, random.Next(10));
            Assert.Equal(5, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(2, random.Next(-9, 10));
            Assert.Equal(3, random.Next(-9, 10));

            Assert.Equal(0.979232173878699500, random.NextDouble());
            Assert.Equal(0.322598935104906560, random.NextDouble());
            Assert.Equal(0.346315557835623600, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("0B-A2-43-7B-77-46-14-EA-09-5E", BitConverter.ToString(buffer1));
            Assert.Equal("EB-1D-30-05-8C-CE-4D-42-4C-41", BitConverter.ToString(buffer2));
            Assert.Equal("49-EA-82-06-87-63-35-C9-40-20", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro128SS: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoshiro128SS(int.MinValue);

            Assert.Equal(1485977265, random.Next());
            Assert.Equal(-2095149944, random.Next());
            Assert.Equal(-591534485, random.Next());

            Assert.Equal(4, random.Next(10));
            Assert.Equal(8, random.Next(10));
            Assert.Equal(6, random.Next(10));

            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(9, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));

            Assert.Equal(0.7168454837519675, random.NextDouble());
            Assert.Equal(0.8563127571251243, random.NextDouble());
            Assert.Equal(0.8006251561455429, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("B7-F2-D1-91-32-5A-D5-B6-F4-5E", BitConverter.ToString(buffer1));
            Assert.Equal("BD-F7-D2-42-23-65-2F-A8-69-F3", BitConverter.ToString(buffer2));
            Assert.Equal("F3-52-85-4E-66-26-3B-E2-8A-E7", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro128SS: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoshiro128SS(int.MaxValue);

            Assert.Equal(1838284268, random.Next());
            Assert.Equal(-824600722, random.Next());
            Assert.Equal(1576424145, random.Next());

            Assert.Equal(2, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));

            Assert.Equal(0.486268784618005160, random.NextDouble());
            Assert.Equal(0.529523150529712400, random.NextDouble());
            Assert.Equal(0.190922153880819680, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("8A-ED-FB-28-54-DA-8C-F4-98-3E", BitConverter.ToString(buffer1));
            Assert.Equal("80-D3-92-21-F2-D9-44-BF-60-74", BitConverter.ToString(buffer2));
            Assert.Equal("E9-47-A2-C7-5D-54-B3-57-B7-D4", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoshiro128SS: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoshiro128SS();
            var random2 = new ScrambledLinear.RandomXoshiro128SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
