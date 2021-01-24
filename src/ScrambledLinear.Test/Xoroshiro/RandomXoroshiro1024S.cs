using System;
using Xunit;

namespace Tests.Xoroshiro {
    public class RandomXoroshiro1024S {

        [Fact(DisplayName = "RandomXoroshiro1024S: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoroshiro1024S(0);

            Assert.Equal(1387053340, random.Next());
            Assert.Equal(941123805, random.Next());
            Assert.Equal(752088196, random.Next());

            Assert.Equal(7, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(7, random.Next(10));

            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));

            Assert.Equal(0.535454918970863100, random.NextDouble());
            Assert.Equal(0.134932229618174970, random.NextDouble());
            Assert.Equal(0.519496719151727900, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("7D-52-E3-EE-F0-BF-E2-41-DB-D4", BitConverter.ToString(buffer1));
            Assert.Equal("B1-60-F1-87-61-45-5C-77-A5-86", BitConverter.ToString(buffer2));
            Assert.Equal("D8-66-A2-52-20-27-69-59-5B-3B", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro1024S: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoroshiro1024S(int.MinValue);

            Assert.Equal(-1789471974, random.Next());
            Assert.Equal(552864141, random.Next());
            Assert.Equal(-1680228942, random.Next());

            Assert.Equal(4, random.Next(10));
            Assert.Equal(5, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(1, random.Next(-9, 10));
            Assert.Equal(0, random.Next(-9, 10));
            Assert.Equal(1, random.Next(-9, 10));

            Assert.Equal(0.526731390881853800, random.NextDouble());
            Assert.Equal(0.124815681187239620, random.NextDouble());
            Assert.Equal(0.007669290258956130, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("4D-C6-ED-48-AA-BE-59-E0-90-A4", BitConverter.ToString(buffer1));
            Assert.Equal("50-BD-CB-4C-24-69-92-5C-CE-9C", BitConverter.ToString(buffer2));
            Assert.Equal("1E-68-58-EC-94-2B-15-98-9D-D7", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro1024S: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoroshiro1024S(int.MaxValue);

            Assert.Equal(-543116859, random.Next());
            Assert.Equal(787291130, random.Next());
            Assert.Equal(-379555302, random.Next());

            Assert.Equal(8, random.Next(10));
            Assert.Equal(2, random.Next(10));
            Assert.Equal(9, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));

            Assert.Equal(0.225607599128606400, random.NextDouble());
            Assert.Equal(0.826913124743644500, random.NextDouble());
            Assert.Equal(0.706069752469586000, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("8F-BF-2C-A0-8B-C9-69-9E-35-57", BitConverter.ToString(buffer1));
            Assert.Equal("2E-82-D2-FA-85-54-2D-12-4A-C5", BitConverter.ToString(buffer2));
            Assert.Equal("1F-48-9D-3D-AD-41-8E-C2-F6-3F", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoroshiro1024S: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoroshiro1024S();
            var random2 = new ScrambledLinear.RandomXoroshiro1024S();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
