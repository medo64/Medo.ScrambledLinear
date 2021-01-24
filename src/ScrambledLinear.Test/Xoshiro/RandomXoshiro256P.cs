using System;
using Xunit;

namespace Tests.Xoshiro {
    public class RandomXoshiro256P {

        [Fact(DisplayName = "RandomXoshiro256P: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoshiro256P(0);

            Assert.Equal(-311799909, random.Next());
            Assert.Equal(230720565, random.Next());
            Assert.Equal(-2049947989, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(2, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(4, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));
            Assert.Equal(3, random.Next(-9, 10));

            Assert.Equal(0.856445371069032300, random.NextDouble());
            Assert.Equal(0.356342777329195840, random.NextDouble());
            Assert.Equal(0.327356837970121700, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("E9-BF-55-2A-DE-43-AA-95-49-C6", BitConverter.ToString(buffer1));
            Assert.Equal("82-67-8C-77-94-BE-59-A1-BE-F9", BitConverter.ToString(buffer2));
            Assert.Equal("CA-2E-45-0C-67-16-51-FD-DB-BE", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro256P: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoshiro256P(int.MinValue);

            Assert.Equal(-304763061, random.Next());
            Assert.Equal(-1458913227, random.Next());
            Assert.Equal(2089616101, random.Next());

            Assert.Equal(1, random.Next(10));
            Assert.Equal(6, random.Next(10));
            Assert.Equal(6, random.Next(10));

            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(2, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));

            Assert.Equal(0.35437749236863914, random.NextDouble());
            Assert.Equal(0.27679730659694490, random.NextDouble());
            Assert.Equal(0.48074350084782160, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("16-48-C5-FF-D4-F1-7E-57-26-6D", BitConverter.ToString(buffer1));
            Assert.Equal("A5-03-E5-11-B8-04-49-B4-F5-88", BitConverter.ToString(buffer2));
            Assert.Equal("19-99-09-FF-C2-7D-C0-D0-A2-7B", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro256P: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoshiro256P(int.MaxValue);

            Assert.Equal(-952893275, random.Next());
            Assert.Equal(709665419, random.Next());
            Assert.Equal(-116635736, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(3, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));

            Assert.Equal(0.680819455151640200, random.NextDouble());
            Assert.Equal(0.402258381698285000, random.NextDouble());
            Assert.Equal(0.447930474607467500, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("99-53-65-D5-16-1A-0D-1B-FF-5A", BitConverter.ToString(buffer1));
            Assert.Equal("B5-B7-95-91-F0-08-98-CB-6A-CE", BitConverter.ToString(buffer2));
            Assert.Equal("8E-4D-F3-91-A5-78-14-B9-50-F8", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoshiro256P: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoshiro256P();
            var random2 = new ScrambledLinear.RandomXoshiro256P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
