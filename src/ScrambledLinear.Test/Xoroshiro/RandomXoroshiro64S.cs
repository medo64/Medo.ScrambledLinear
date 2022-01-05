using System;
using Xunit;

namespace Tests.Xoroshiro {
    public class RandomXoroshiro64S {

        [Fact(DisplayName = "RandomXoroshiro64S: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoroshiro64S(0);

            Assert.Equal(932574677, random.Next());
            Assert.Equal(571770783, random.Next());
            Assert.Equal(-1816336140, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(0, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(9, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));

            Assert.Equal(0.673005643999204000, random.NextDouble());
            Assert.Equal(0.140669378219172360, random.NextDouble());
            Assert.Equal(0.907178434077650300, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("CB-43-6B-49-31-99-5C-8C-D5-0A", BitConverter.ToString(buffer1));
            Assert.Equal("0B-F6-EB-D6-08-49-55-DA-DC-89", BitConverter.ToString(buffer2));
            Assert.Equal("83-AD-90-D1-7C-CF-99-E4-F1-E2", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro64S: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoroshiro64S(int.MinValue);

            Assert.Equal(-2047768281, random.Next());
            Assert.Equal(-1372495921, random.Next());
            Assert.Equal(1794670077, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(6, random.Next(10));
            Assert.Equal(5, random.Next(10));

            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(1, random.Next(-9, 10));

            Assert.Equal(0.473199291620403530, random.NextDouble());
            Assert.Equal(0.501838854514062400, random.NextDouble());
            Assert.Equal(0.898414784111082600, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("D2-77-29-B0-D4-56-0E-73-21-7A", BitConverter.ToString(buffer1));
            Assert.Equal("18-EC-5F-69-BB-26-EA-D5-C6-CD", BitConverter.ToString(buffer2));
            Assert.Equal("C3-FB-74-13-68-66-7E-5D-A4-03", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoroshiro64S: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoroshiro64S(int.MaxValue);

            Assert.Equal(599171261, random.Next());
            Assert.Equal(957906677, random.Next());
            Assert.Equal(-1266798358, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(2, random.Next(10));
            Assert.Equal(2, random.Next(10));

            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(2, random.Next(-9, 10));

            Assert.Equal(0.988461249973625000, random.NextDouble());
            Assert.Equal(0.735286523355171100, random.NextDouble());
            Assert.Equal(0.049030120484530926, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("A5-18-46-81-7F-9A-33-8F-85-79", BitConverter.ToString(buffer1));
            Assert.Equal("50-94-E5-2F-AC-5E-FC-23-B9-B9", BitConverter.ToString(buffer2));
            Assert.Equal("27-73-F1-A8-62-E2-84-95-95-87", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoroshiro64S: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoroshiro64S();
            var random2 = new ScrambledLinear.RandomXoroshiro64S();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
