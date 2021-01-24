using System;
using Xunit;

namespace Tests.Xoshiro {
    public class RandomXoshiro128P {

        [Fact(DisplayName = "RandomXoshiro128P: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoshiro128P(0);

            Assert.Equal(-311799909, random.Next());
            Assert.Equal(1476980822, random.Next());
            Assert.Equal(-960405016, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(5, random.Next(10));
            Assert.Equal(6, random.Next(10));

            Assert.Equal(-8, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));

            Assert.Equal(0.020846147090196610, random.NextDouble());
            Assert.Equal(0.149681575596332550, random.NextDouble());
            Assert.Equal(0.796243760734796500, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("CC-C9-40-E8-CE-C4-AE-F6-72-33", BitConverter.ToString(buffer1));
            Assert.Equal("97-FC-CE-A8-22-94-0F-DC-1E-9A", BitConverter.ToString(buffer2));
            Assert.Equal("4E-5F-78-82-F8-E5-E2-C0-2E-88", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro128P: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoshiro128P(int.MinValue);

            Assert.Equal(-304763061, random.Next());
            Assert.Equal(-195204341, random.Next());
            Assert.Equal(448021174, random.Next());

            Assert.Equal(2, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(7, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(1, random.Next(-9, 10));
            Assert.Equal(2, random.Next(-9, 10));

            Assert.Equal(0.380231220275163650, random.NextDouble());
            Assert.Equal(0.928233403712511100, random.NextDouble());
            Assert.Equal(0.748391102999448800, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("60-29-18-B4-98-A3-26-98-01-F0", BitConverter.ToString(buffer1));
            Assert.Equal("62-1D-BB-74-B8-F5-D7-8C-8D-B8", BitConverter.ToString(buffer2));
            Assert.Equal("27-13-81-51-92-D9-89-86-67-E2", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro128P: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoshiro128P(int.MaxValue);

            Assert.Equal(-952893275, random.Next());
            Assert.Equal(-1845418855, random.Next());
            Assert.Equal(-977547329, random.Next());

            Assert.Equal(2, random.Next(10));
            Assert.Equal(6, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(5, random.Next(-9, 10));
            Assert.Equal(3, random.Next(-9, 10));

            Assert.Equal(0.510156519711017600, random.NextDouble());
            Assert.Equal(0.318624529987573600, random.NextDouble());
            Assert.Equal(0.425376910716295240, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("CB-DB-45-39-A3-F7-27-E9-ED-79", BitConverter.ToString(buffer1));
            Assert.Equal("CA-69-1A-C5-4D-89-7B-87-61-98", BitConverter.ToString(buffer2));
            Assert.Equal("9D-59-3C-68-BD-2A-5C-CF-B8-48", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoshiro128+: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoshiro128P();
            var random2 = new ScrambledLinear.RandomXoshiro128P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
