using System;
using Xunit;

namespace Tests.Xoroshiro {
    public class RandomXoroshiro64SS {

        [Fact(DisplayName = "RandomXoroshiro64SS: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoroshiro64SS(0);

            Assert.Equal(-1111907010, random.Next());
            Assert.Equal(1289012084, random.Next());
            Assert.Equal(1443993818, random.Next());

            Assert.Equal(8, random.Next(10));
            Assert.Equal(6, random.Next(10));
            Assert.Equal(7, random.Next(10));

            Assert.Equal(5, random.Next(-9, 10));
            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));

            Assert.Equal(0.680903064319863900, random.NextDouble());
            Assert.Equal(0.507100519724190200, random.NextDouble());
            Assert.Equal(0.148549486184492700, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("0D-5F-0A-E3-F5-BE-DF-B9-A7-C5", BitConverter.ToString(buffer1));
            Assert.Equal("62-C7-79-53-87-A5-4D-75-C6-29", BitConverter.ToString(buffer2));
            Assert.Equal("62-72-6C-FA-0C-AE-21-E0-A0-D6", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro64SS: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoroshiro64SS(int.MinValue);

            Assert.Equal(-1225410384, random.Next());
            Assert.Equal(-556015159, random.Next());
            Assert.Equal(-615596447, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(8, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(-3, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));

            Assert.Equal(0.71188667672686280, random.NextDouble());
            Assert.Equal(0.29421674087643623, random.NextDouble());
            Assert.Equal(0.74636549036949870, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("AE-E3-EA-19-C6-44-F6-E8-CD-54", BitConverter.ToString(buffer1));
            Assert.Equal("41-8F-F3-DB-62-35-58-B2-38-9C", BitConverter.ToString(buffer2));
            Assert.Equal("EA-59-1D-29-37-01-00-6F-02-47", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro64SS: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoroshiro64SS(int.MaxValue);

            Assert.Equal(1378121268, random.Next());
            Assert.Equal(-1353754301, random.Next());
            Assert.Equal(-824274258, random.Next());

            Assert.Equal(8, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(5, random.Next(10));

            Assert.Equal(0, random.Next(-9, 10));
            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));

            Assert.Equal(0.153800031868740920, random.NextDouble());
            Assert.Equal(0.645843763602897500, random.NextDouble());
            Assert.Equal(0.844819278689101300, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("70-67-CF-CB-B5-8F-40-80-39-F3", BitConverter.ToString(buffer1));
            Assert.Equal("19-B2-7C-EF-94-2B-BB-7D-CD-13", BitConverter.ToString(buffer2));
            Assert.Equal("C9-F8-E7-96-9A-7D-0D-73-8E-BD", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoroshiro64SS: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoroshiro64SS();
            var random2 = new ScrambledLinear.RandomXoroshiro64SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
