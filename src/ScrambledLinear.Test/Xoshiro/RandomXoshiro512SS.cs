using System;
using Xunit;

namespace Tests.Xoshiro {
    public class RandomXoshiro512SS {

        [Fact(DisplayName = "RandomXoshiro512SS: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoshiro512SS(0);

            Assert.Equal(-881462604, random.Next());
            Assert.Equal(1230390570, random.Next());
            Assert.Equal(1410947490, random.Next());

            Assert.Equal(1, random.Next(10));
            Assert.Equal(5, random.Next(10));
            Assert.Equal(9, random.Next(10));

            Assert.Equal(1, random.Next(-9, 10));
            Assert.Equal(9, random.Next(-9, 10));
            Assert.Equal(-4, random.Next(-9, 10));

            Assert.Equal(0.219531457878631700, random.NextDouble());
            Assert.Equal(0.157772962862030800, random.NextDouble());
            Assert.Equal(0.287294733517758470, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("01-C7-2A-D7-69-76-DE-AC-73-0A", BitConverter.ToString(buffer1));
            Assert.Equal("02-33-7C-A5-A5-E8-DA-0D-F5-DF", BitConverter.ToString(buffer2));
            Assert.Equal("A9-2B-5B-56-3B-96-46-69-A3-1A", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro512SS: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoshiro512SS(int.MinValue);

            Assert.Equal(1485976842, random.Next());
            Assert.Equal(-2095149980, random.Next());
            Assert.Equal(1718277263, random.Next());

            Assert.Equal(4, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(1, random.Next(-9, 10));
            Assert.Equal(1, random.Next(-9, 10));
            Assert.Equal(5, random.Next(-9, 10));

            Assert.Equal(0.417256542750404800, random.NextDouble());
            Assert.Equal(0.761350906222509100, random.NextDouble());
            Assert.Equal(0.877262475241918400, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("EC-27-F5-F1-E8-47-E7-CC-61-ED", BitConverter.ToString(buffer1));
            Assert.Equal("66-84-B6-A8-BA-12-A0-1C-DB-59", BitConverter.ToString(buffer2));
            Assert.Equal("EC-B7-86-BB-D1-13-66-B5-B0-B7", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro512SS: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoshiro512SS(int.MaxValue);

            Assert.Equal(1838284367, random.Next());
            Assert.Equal(-824601550, random.Next());
            Assert.Equal(174802128, random.Next());

            Assert.Equal(1, random.Next(10));
            Assert.Equal(0, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(5, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));

            Assert.Equal(0.228253829033394750, random.NextDouble());
            Assert.Equal(0.177896103866960860, random.NextDouble());
            Assert.Equal(0.307057091887398050, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("D1-C9-7F-69-18-78-DD-D4-A5-AE", BitConverter.ToString(buffer1));
            Assert.Equal("FA-61-4A-34-AD-55-97-AF-D1-97", BitConverter.ToString(buffer2));
            Assert.Equal("57-47-1B-B3-36-C5-DD-65-44-13", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoshiro512SS: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoshiro512SS();
            var random2 = new ScrambledLinear.RandomXoshiro512SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
