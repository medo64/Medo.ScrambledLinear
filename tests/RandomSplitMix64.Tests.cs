using System;
using Xunit;

namespace Tests {
    public class RandomSplitMix64 {

        [Fact(DisplayName = "RandomSplitMix64: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomSplitMix64(0);

            Assert.Equal(2065550767, random.Next());
            Assert.Equal(-1581685260, random.Next());
            Assert.Equal(-2146876081, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(1, random.Next(10));
            Assert.Equal(3, random.Next(10));

            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(5, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));

            Assert.Equal(0.952030691367826500, random.NextDouble());
            Assert.Equal(0.396467975628813530, random.NextDouble());
            Assert.Equal(0.761034421627626900, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("7B-DB-BB-E0-3F-A0-21-86-2F-A9", BitConverter.ToString(buffer1));
            Assert.Equal("19-4D-CC-00-16-0F-4E-B5-AB-80", BitConverter.ToString(buffer2));
            Assert.Equal("55-12-52-75-5C-82-29-7D-86-7F", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomSplitMix64: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomSplitMix64(int.MinValue);

            Assert.Equal(571453829, random.Next());
            Assert.Equal(704901054, random.Next());
            Assert.Equal(-1955932193, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(0, random.Next(10));
            Assert.Equal(6, random.Next(10));

            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));
            Assert.Equal(1, random.Next(-9, 10));

            Assert.Equal(0.206939391850385230, random.NextDouble());
            Assert.Equal(0.233547948811709480, random.NextDouble());
            Assert.Equal(0.850294317496663800, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("D5-40-07-E4-31-A7-EF-A4-1F-40", BitConverter.ToString(buffer1));
            Assert.Equal("30-3B-2D-AF-AE-6D-3B-28-70-57", BitConverter.ToString(buffer2));
            Assert.Equal("86-B1-B0-D8-F9-54-36-4D-3D-E0", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomSplitMix64: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomSplitMix64(int.MaxValue);

            Assert.Equal(639257575, random.Next());
            Assert.Equal(-1636391481, random.Next());
            Assert.Equal(1952833374, random.Next());

            Assert.Equal(8, random.Next(10));
            Assert.Equal(7, random.Next(10));
            Assert.Equal(3, random.Next(10));

            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));

            Assert.Equal(0.901738151029807500, random.NextDouble());
            Assert.Equal(0.239443331204800000, random.NextDouble());
            Assert.Equal(0.925917537765641700, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("13-97-07-FD-8A-30-DF-FF-15-66", BitConverter.ToString(buffer1));
            Assert.Equal("97-38-63-0E-75-AD-A5-A7-DA-FE", BitConverter.ToString(buffer2));
            Assert.Equal("C6-4F-3A-5C-4D-D6-44-1E-3F-B6", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomSplitMix64: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomSplitMix64();
            var random2 = new ScrambledLinear.RandomSplitMix64();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
