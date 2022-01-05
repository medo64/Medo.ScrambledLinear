using System;
using Xunit;

namespace Tests.Xoroshiro {
    public class RandomXoroshiro1024SS {

        [Fact(DisplayName = "RandomXoroshiro1024SS: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoroshiro1024SS(0);

            Assert.Equal(-881462604, random.Next());
            Assert.Equal(-795381232, random.Next());
            Assert.Equal(-1184153131, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(4, random.Next(10));

            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(4, random.Next(-9, 10));

            Assert.Equal(0.655539621966331100, random.NextDouble());
            Assert.Equal(0.558268575131565900, random.NextDouble());
            Assert.Equal(0.955407932519656500, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("FB-A4-DE-27-E1-05-D0-C3-ED-B4", BitConverter.ToString(buffer1));
            Assert.Equal("23-0A-CF-17-C8-D2-16-75-8B-91", BitConverter.ToString(buffer2));
            Assert.Equal("87-94-55-6C-23-21-A3-CA-9C-74", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro1024SS: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoroshiro1024SS(int.MinValue);

            Assert.Equal(1485976842, random.Next());
            Assert.Equal(-470213714, random.Next());
            Assert.Equal(-422712808, random.Next());

            Assert.Equal(6, random.Next(10));
            Assert.Equal(7, random.Next(10));
            Assert.Equal(2, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(9, random.Next(-9, 10));

            Assert.Equal(0.236185155447127930, random.NextDouble());
            Assert.Equal(0.695268780784026000, random.NextDouble());
            Assert.Equal(0.063261183592838390, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("2B-BA-A2-93-E9-BA-B1-71-84-BB", BitConverter.ToString(buffer1));
            Assert.Equal("4A-5C-2F-22-AD-0B-C7-E7-12-D9", BitConverter.ToString(buffer2));
            Assert.Equal("7E-31-DD-DD-E5-78-E8-9F-A5-13", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro1024SS: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoroshiro1024SS(int.MaxValue);

            Assert.Equal(1838284367, random.Next());
            Assert.Equal(-199112859, random.Next());
            Assert.Equal(-1033718635, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(1, random.Next(10));
            Assert.Equal(2, random.Next(10));

            Assert.Equal(6, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));

            Assert.Equal(0.193587739648113040, random.NextDouble());
            Assert.Equal(0.285017530096625450, random.NextDouble());
            Assert.Equal(0.116275265153288230, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("CA-DC-78-98-C8-66-8F-5B-BB-46", BitConverter.ToString(buffer1));
            Assert.Equal("C0-92-BA-16-33-C9-08-6D-60-48", BitConverter.ToString(buffer3));
            Assert.Equal("A2-29-66-B1-C6-F1-BE-90-6E-1F", BitConverter.ToString(buffer2));
        }


        [Fact(DisplayName = "RandomXoroshiro1024SS: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoroshiro1024SS();
            var random2 = new ScrambledLinear.RandomXoroshiro1024SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
