using System;
using System.Reflection;
using Xunit;
using Subject = Xoroshiro;

namespace Test.Xoroshiro {
    public class Xoroshiro1024PP {

        [Fact(DisplayName = "xoroshiro1024++: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Subject.Xoroshiro1024PP).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Subject.Xoroshiro1024PP).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Subject.Xoroshiro1024PP();
            sField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt64)0x0000000002800002, values[0]);
            Assert.Equal((UInt64)0x0800001002800000, values[1]);
            Assert.Equal((UInt64)0x2800005083800100, values[2]);
            Assert.Equal((UInt64)0x3800107285800508, values[3]);
            Assert.Equal((UInt64)0x5800513386810728, values[4]);
            Assert.Equal((UInt64)0x6810735588851338, values[5]);
            Assert.Equal((UInt64)0x885134968a873558, values[6]);
            Assert.Equal((UInt64)0xa87356b890934968, values[7]);
            Assert.Equal((UInt64)0x093497fa95b56b88, values[8]);
            Assert.Equal((UInt64)0x5b56ba60a2c97fa8, values[9]);
            Assert.Equal((UInt64)0x2c97fa85c7eba608, values[10]);
            Assert.Equal((UInt64)0x7eba62e2ddffa85b, values[11]);
            Assert.Equal((UInt64)0xdffa845801262e2b, values[12]);
            Assert.Equal((UInt64)0x0262e14e1728457d, values[13]);
            Assert.Equal((UInt64)0x62845ab040ae14e0, values[14]);
            Assert.Equal((UInt64)0xfae84f4628c5ab02, values[15]);
            Assert.Equal((UInt64)0xa46bb01fb064f461, values[16]);
            Assert.Equal((UInt64)0x526a4672c87b02f8, values[17]);
            Assert.Equal((UInt64)0xc3d71f6a2ae461a4, values[18]);
            Assert.Equal((UInt64)0x727f6b474392fb52, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0x30629abe64408ac8, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoroshiro1024++: Seed = 0")]
        public void Init0() {
            var random = new Subject.Xoroshiro1024PP(0);

            Assert.Equal(1288051282, random.Next());
            Assert.Equal(198962350, random.Next());
            Assert.Equal(-1055514397, random.Next());

            Assert.Equal(6, random.Next(10));
            Assert.Equal(2, random.Next(10));
            Assert.Equal(2, random.Next(10));

            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));

            Assert.Equal(0.145621536270223120, random.NextDouble());
            Assert.Equal(0.095426973826949310, random.NextDouble());
            Assert.Equal(0.795454481936413500, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("25-E7-E0-61-D8-CC-0E-7B", BitConverter.ToString(buffer1));
            Assert.Equal("E1-3E-2E-DB-D1-DE-B6-57", BitConverter.ToString(buffer2));
            Assert.Equal("DB-36-4B-24-AA-6A-1A-FF", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro1024++: Seed = Int32.MinValue")]
        public void InitMin() {
            var random = new Subject.Xoroshiro1024PP(int.MinValue);

            Assert.Equal(-1783342536, random.Next());
            Assert.Equal(1486589953, random.Next());
            Assert.Equal(1756864868, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(3, random.Next(10));

            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(4, random.Next(-9, 10));

            Assert.Equal(0.009383841642984780, random.NextDouble());
            Assert.Equal(0.502095708697265000, random.NextDouble());
            Assert.Equal(0.450973874517142300, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("22-D8-CE-28-8A-AE-E4-CC", BitConverter.ToString(buffer1));
            Assert.Equal("DA-05-17-E1-F7-AB-00-AB", BitConverter.ToString(buffer2));
            Assert.Equal("E8-64-E6-AC-33-D1-36-81", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro1024++: Seed = Int32.MaxValue")]
        public void InitMax() {
            var random = new Subject.Xoroshiro1024PP(int.MaxValue);

            Assert.Equal(-45087170, random.Next());
            Assert.Equal(620372668, random.Next());
            Assert.Equal(1985137452, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(0, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(-2, random.Next(-9, 10));

            Assert.Equal(0.248351094451162440, random.NextDouble());
            Assert.Equal(0.574090718208660300, random.NextDouble());
            Assert.Equal(0.997789208240043900, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("98-C0-A8-15-8F-31-A3-85", BitConverter.ToString(buffer1));
            Assert.Equal("75-C0-6E-B1-A3-50-A1-3B", BitConverter.ToString(buffer2));
            Assert.Equal("CE-BD-AB-0C-87-55-AD-F7", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoroshiro1024++: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Subject.Xoroshiro1024PP();
            var random2 = new Subject.Xoroshiro1024PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
