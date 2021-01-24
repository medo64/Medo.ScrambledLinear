using System;
using Xunit;

namespace Tests.Xoroshiro {
    public class RandomXoroshiro128P {

        [Fact(DisplayName = "RandomXoroshiro128P: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoroshiro128P(0);

            Assert.Equal(483865507, random.Next());
            Assert.Equal(1747211118, random.Next());
            Assert.Equal(-472941597, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(6, random.Next(10));
            Assert.Equal(6, random.Next(10));

            Assert.Equal(2, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));

            Assert.Equal(0.033324319972741990, random.NextDouble());
            Assert.Equal(0.428766221556481600, random.NextDouble());
            Assert.Equal(0.004838425802865087, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("EC-CA-61-4E-EA-03-E7-DB-7B-5B", BitConverter.ToString(buffer1));
            Assert.Equal("CC-AF-0B-B3-9F-25-F2-E2-BF-FC", BitConverter.ToString(buffer2));
            Assert.Equal("19-19-92-57-52-D5-04-47-34-67", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro128P: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoroshiro128P(int.MinValue);

            Assert.Equal(1276354883, random.Next());
            Assert.Equal(1667461579, random.Next());
            Assert.Equal(2019115496, random.Next());

            Assert.Equal(7, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(9, random.Next(10));

            Assert.Equal(-9, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));
            Assert.Equal(2, random.Next(-9, 10));

            Assert.Equal(0.054111314725819870, random.NextDouble());
            Assert.Equal(0.423251973952210500, random.NextDouble());
            Assert.Equal(0.421429903216376630, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("03-50-9E-97-E7-5C-00-B8-9F-38", BitConverter.ToString(buffer1));
            Assert.Equal("26-AB-B6-ED-FA-65-5E-75-09-13", BitConverter.ToString(buffer2));
            Assert.Equal("D7-3F-60-2A-92-62-D9-25-BD-91", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro128P: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoroshiro128P(int.MaxValue);

            Assert.Equal(-997133906, random.Next());
            Assert.Equal(-1053469747, random.Next());
            Assert.Equal(-187667658, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(1, random.Next(10));

            Assert.Equal(1, random.Next(-9, 10));
            Assert.Equal(3, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));

            Assert.Equal(0.082385683951663900, random.NextDouble());
            Assert.Equal(0.658551682406144800, random.NextDouble());
            Assert.Equal(0.375396193788777750, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("65-46-E1-27-D9-26-30-A6-90-44", BitConverter.ToString(buffer1));
            Assert.Equal("13-E2-C9-C4-6A-B7-E8-A9-E6-92", BitConverter.ToString(buffer2));
            Assert.Equal("66-FF-86-1C-29-C8-39-D5-0E-E9", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoroshiro128P: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoroshiro128P();
            var random2 = new ScrambledLinear.RandomXoroshiro128P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
