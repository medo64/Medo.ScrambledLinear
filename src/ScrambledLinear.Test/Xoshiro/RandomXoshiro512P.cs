using System;
using Xunit;

namespace Tests.Xoshiro {
    public class RandomXoshiro512P {

        [Fact(DisplayName = "RandomXoshiro512P: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoshiro512P(0);

            Assert.Equal(-81325314, random.Next());
            Assert.Equal(1601013806, random.Next());
            Assert.Equal(-1430995155, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(2, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));

            Assert.Equal(0.999761197143335400, random.NextDouble());
            Assert.Equal(0.515100886199905800, random.NextDouble());
            Assert.Equal(0.508433547014529100, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("ED-C4-94-B7-D5-3D-D6-62-31-52", BitConverter.ToString(buffer1));
            Assert.Equal("CD-55-1C-F5-8D-E1-F8-D4-EC-FE", BitConverter.ToString(buffer2));
            Assert.Equal("BD-2E-0E-A1-E8-33-1C-F1-95-E5", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro512P: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoshiro512P(int.MinValue);

            Assert.Equal(-1384478364, random.Next());
            Assert.Equal(-1366788341, random.Next());
            Assert.Equal(-1967436673, random.Next());

            Assert.Equal(7, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(1, random.Next(10));

            Assert.Equal(2, random.Next(-9, 10));
            Assert.Equal(3, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));

            Assert.Equal(0.594491490338892700, random.NextDouble());
            Assert.Equal(0.069746134675015560, random.NextDouble());
            Assert.Equal(0.302482037083182330, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("C6-D2-52-8F-AA-3B-BE-5B-08-D5", BitConverter.ToString(buffer1));
            Assert.Equal("FA-E2-7C-5C-E5-AE-27-4B-A4-09", BitConverter.ToString(buffer2));
            Assert.Equal("97-93-54-3D-54-44-2B-08-5D-2E", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro512P: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoshiro512P(int.MaxValue);

            Assert.Equal(-1702876347, random.Next());
            Assert.Equal(-1552027764, random.Next());
            Assert.Equal(-1696413967, random.Next());

            Assert.Equal(2, random.Next(10));
            Assert.Equal(7, random.Next(10));
            Assert.Equal(6, random.Next(10));

            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));

            Assert.Equal(0.875797007435673100, random.NextDouble());
            Assert.Equal(0.943634923583176900, random.NextDouble());
            Assert.Equal(0.693731369217030600, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("1C-70-2D-82-BE-E2-8B-45-BC-E9", BitConverter.ToString(buffer1));
            Assert.Equal("4E-47-98-C0-85-73-28-2A-FB-99", BitConverter.ToString(buffer2));
            Assert.Equal("62-A1-D6-5E-D4-36-9F-02-0A-56", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoshiro512P: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoshiro512P();
            var random2 = new ScrambledLinear.RandomXoshiro512P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
