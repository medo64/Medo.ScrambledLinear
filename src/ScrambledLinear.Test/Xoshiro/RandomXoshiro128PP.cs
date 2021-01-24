using System;
using Xunit;

namespace Tests.Xoshiro {
    public class RandomXoshiro128PP {

        [Fact(DisplayName = "RandomXoshiro128PP: Seed = 0")]
        public void Seed0() {
            var random = new ScrambledLinear.RandomXoshiro128PP(0);

            Assert.Equal(809868197, random.Next());
            Assert.Equal(-1386195741, random.Next());
            Assert.Equal(-1105856865, random.Next());

            Assert.Equal(0, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(3, random.Next(10));

            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(2, random.Next(-9, 10));
            Assert.Equal(0, random.Next(-9, 10));

            Assert.Equal(0.179341408424079420, random.NextDouble());
            Assert.Equal(0.014623910188674927, random.NextDouble());
            Assert.Equal(0.233002224471420050, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("C8-06-BD-9F-FB-8B-EA-CD-3E-17", BitConverter.ToString(buffer1));
            Assert.Equal("42-A5-27-08-F7-67-5A-1B-B6-CA", BitConverter.ToString(buffer2));
            Assert.Equal("1A-AB-E3-E0-1C-B4-B2-D4-29-89", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro128PP: Seed = Int32.MinValue")]
        public void SeedMin() {
            var random = new ScrambledLinear.RandomXoshiro128PP(int.MinValue);

            Assert.Equal(216487803, random.Next());
            Assert.Equal(-226547209, random.Next());
            Assert.Equal(-899790812, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(5, random.Next(10));

            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(0, random.Next(-9, 10));

            Assert.Equal(0.897117348853498700, random.NextDouble());
            Assert.Equal(0.249194541247561570, random.NextDouble());
            Assert.Equal(0.178257775027304900, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("3E-1A-F3-99-E9-A5-F4-C5-97-E3", BitConverter.ToString(buffer1));
            Assert.Equal("9B-E4-BB-B3-4C-99-5F-AC-0C-A1", BitConverter.ToString(buffer2));
            Assert.Equal("15-A5-B1-7F-A6-30-C6-3F-1A-05", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "RandomXoshiro128PP: Seed = Int32.MaxValue")]
        public void SeedMax() {
            var random = new ScrambledLinear.RandomXoshiro128PP(int.MaxValue);

            Assert.Equal(-1071997238, random.Next());
            Assert.Equal(436709223, random.Next());
            Assert.Equal(-1948286467, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(-9, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));
            Assert.Equal(8, random.Next(-9, 10));

            Assert.Equal(0.933169814525172100, random.NextDouble());
            Assert.Equal(0.409962782869115470, random.NextDouble());
            Assert.Equal(0.217653268482536080, random.NextDouble());

            var buffer1 = new byte[10]; random.NextBytes(buffer1);
            var buffer2 = new byte[10]; random.NextBytes(buffer2);
            var buffer3 = new byte[10]; random.NextBytes(buffer3);
            Assert.Equal("59-57-0F-28-14-DF-3E-69-A6-C0", BitConverter.ToString(buffer1));
            Assert.Equal("94-29-E3-ED-CC-52-FE-85-BC-9F", BitConverter.ToString(buffer2));
            Assert.Equal("A7-2F-F7-0D-FB-49-12-C9-F0-23", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "RandomXoshiro128PP: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new ScrambledLinear.RandomXoshiro128PP();
            var random2 = new ScrambledLinear.RandomXoshiro128PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
