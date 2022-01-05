using System;
using Xunit;

namespace Tests.Xoshiro {
    public class RandomXoshiro256SS {

        [Fact(DisplayName = "RandomXoshiro256SS: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoshiro256SS(0);

            Assert.Equal(-881462604, random.Next());
            Assert.Equal(1230390570, random.Next());
            Assert.Equal(1228138208, random.Next());

            Assert.Equal(4, random.Next(10));
            Assert.Equal(7, random.Next(10));
            Assert.Equal(9, random.Next(10));

            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(1, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));

            Assert.Equal(0.918858012706964900, random.NextDouble());
            Assert.Equal(0.114297464406487690, random.NextDouble());
            Assert.Equal(0.067231891013425420, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("9C-8E-3D-AB-A5-07-4F-1B-7F-DB", BitConverter.ToString(buffer1));
            Assert.Equal("9C-FC-5D-60-95-AA-FD-7E-B8-AA", BitConverter.ToString(buffer2));
            Assert.Equal("6C-66-18-35-C4-EA-55-B4-90-06", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro256SS: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoshiro256SS(int.MinValue);

            Assert.Equal(1485976842, random.Next());
            Assert.Equal(-2095149980, random.Next());
            Assert.Equal(95675399, random.Next());

            Assert.Equal(6, random.Next(10));
            Assert.Equal(2, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));

            Assert.Equal(0.915896528104570300, random.NextDouble());
            Assert.Equal(0.602903289674118100, random.NextDouble());
            Assert.Equal(0.472856548561592760, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("53-F0-FA-4C-20-52-85-C6-48-97", BitConverter.ToString(buffer1));
            Assert.Equal("37-51-28-DD-6C-71-C0-AC-83-D0", BitConverter.ToString(buffer2));
            Assert.Equal("4B-92-61-C0-A0-73-B6-37-94-32", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro256SS: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoshiro256SS(int.MaxValue);

            Assert.Equal(1838284367, random.Next());
            Assert.Equal(-824601550, random.Next());
            Assert.Equal(2062636305, random.Next());

            Assert.Equal(6, random.Next(10));
            Assert.Equal(6, random.Next(10));
            Assert.Equal(1, random.Next(10));

            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(0, random.Next(-9, 10));
            Assert.Equal(9, random.Next(-9, 10));

            Assert.Equal(0.127957773772553680, random.NextDouble());
            Assert.Equal(0.388994105041749470, random.NextDouble());
            Assert.Equal(0.981492799015784200, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("A2-74-02-A6-ED-92-77-86-DE-FD", BitConverter.ToString(buffer1));
            Assert.Equal("FA-D9-82-5A-BB-42-28-52-D6-6A", BitConverter.ToString(buffer2));
            Assert.Equal("E4-B8-84-01-E3-66-55-54-12-CD", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoshiro256SS: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoshiro256SS();
            var random2 = new ScrambledLinear.RandomXoshiro256SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
