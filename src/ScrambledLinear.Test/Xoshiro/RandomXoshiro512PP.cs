using System;
using Xunit;

namespace Tests.Xoshiro {
    public class RandomXoshiro512PP {

        [Fact(DisplayName = "RandomXoshiro512PP: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoshiro512PP(0);

            Assert.Equal(-1509484775, random.Next());
            Assert.Equal(192016366, random.Next());
            Assert.Equal(497484788, random.Next());

            Assert.Equal(7, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));

            Assert.Equal(0.326677362647187500, random.NextDouble());
            Assert.Equal(0.303055016085369800, random.NextDouble());
            Assert.Equal(0.886694060932961200, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("3D-70-B3-EB-98-1A-C2-F5-9C-09", BitConverter.ToString(buffer1));
            Assert.Equal("9A-BB-BC-20-1F-81-33-4E-6F-CB", BitConverter.ToString(buffer2));
            Assert.Equal("0A-DF-0A-89-E8-DE-DD-9C-D0-6A", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro512PP: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoshiro512PP(int.MinValue);

            Assert.Equal(-1640805210, random.Next());
            Assert.Equal(-1753468639, random.Next());
            Assert.Equal(1162165502, random.Next());

            Assert.Equal(4, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(9, random.Next(10));

            Assert.Equal(5, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));

            Assert.Equal(0.243250448744908750, random.NextDouble());
            Assert.Equal(0.295764847945367600, random.NextDouble());
            Assert.Equal(0.979685462900648400, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("F4-2B-20-A7-19-64-A3-A9-DD-8F", BitConverter.ToString(buffer1));
            Assert.Equal("33-17-6C-B5-F5-22-5B-6A-03-75", BitConverter.ToString(buffer2));
            Assert.Equal("96-D4-63-DF-D9-C1-AE-AD-3A-1B", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro512PP: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoshiro512PP(int.MaxValue);

            Assert.Equal(-890147392, random.Next());
            Assert.Equal(832093711, random.Next());
            Assert.Equal(-1872391960, random.Next());

            Assert.Equal(0, random.Next(10));
            Assert.Equal(8, random.Next(10));
            Assert.Equal(1, random.Next(10));

            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(3, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));

            Assert.Equal(0.227928046003613940, random.NextDouble());
            Assert.Equal(0.990906342309305900, random.NextDouble());
            Assert.Equal(0.562925762968837300, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("F4-12-0F-D1-53-95-04-F3-B4-5E", BitConverter.ToString(buffer1));
            Assert.Equal("88-6A-BE-D2-9B-04-72-DB-5D-19", BitConverter.ToString(buffer2));
            Assert.Equal("21-5B-DC-67-3E-1D-80-64-23-E9", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoshiro512PP: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoshiro512PP();
            var random2 = new ScrambledLinear.RandomXoshiro512PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
