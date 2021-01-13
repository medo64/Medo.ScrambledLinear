using System;
using System.Reflection;
using Xunit;

namespace Xoshiro.Test {
    public class Tests_Xoroshiro1024SS {

        [Fact(DisplayName = "xoroshiro1024**: Reference")]
        public void Test_InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Xoroshiro1024SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Xoroshiro1024SS).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Xoroshiro1024SS();
            sField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt64)0x0000000000004380, values[0]);
            Assert.Equal((UInt64)0x0000000000007080, values[1]);
            Assert.Equal((UInt64)0x0000000000009d80, values[2]);
            Assert.Equal((UInt64)0x000000000000f780, values[3]);
            Assert.Equal((UInt64)0x0000000000012480, values[4]);
            Assert.Equal((UInt64)0x0000000000017e80, values[5]);
            Assert.Equal((UInt64)0x000000000001ab80, values[6]);
            Assert.Equal((UInt64)0x0000000000020580, values[7]);
            Assert.Equal((UInt64)0x0000000000028c80, values[8]);
            Assert.Equal((UInt64)0x000000000002b980, values[9]);
            Assert.Equal((UInt64)0x0000000000034080, values[10]);
            Assert.Equal((UInt64)0x0000000000039a80, values[11]);
            Assert.Equal((UInt64)0x000000000003c780, values[12]);
            Assert.Equal((UInt64)0x0000000000042180, values[13]);
            Assert.Equal((UInt64)0x000000000004a880, values[14]);
            Assert.Equal((UInt64)0x0000013b00001680, values[15]);
            Assert.Equal((UInt64)0x00016afd000072c0, values[16]);
            Assert.Equal((UInt64)0x0007c0bf00171fc0, values[17]);
            Assert.Equal((UInt64)0x016fc2db007179c0, values[18]);
            Assert.Equal((UInt64)0x07c7fe05171ea6c0, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0x666167f11124a633, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoroshiro1024**: Seed = 0")]
        public void Test_Init0() {
            var random = new Xoroshiro1024SS(0);

            Assert.Equal(-881462604, random.Next());
            Assert.Equal(-795381232, random.Next());
            Assert.Equal(-1184153131, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(4, random.Next(10));

            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(4, random.Next(-9, 10));

            Assert.Equal(0.655539621966331000, random.NextDouble());
            Assert.Equal(0.558268575131565900, random.NextDouble());
            Assert.Equal(0.955407932519656500, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("FB-A4-DE-27-E1-05-D0-C3", BitConverter.ToString(buffer1));
            Assert.Equal("ED-B4-C6-F4-11-6F-53-5C", BitConverter.ToString(buffer2));
            Assert.Equal("23-0A-CF-17-C8-D2-16-75", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro1024**: Seed = Int32.MinValue")]
        public void Test_InitMin() {
            var random = new Xoroshiro1024SS(int.MinValue);

            Assert.Equal(1486914389, random.Next());
            Assert.Equal(1541689574, random.Next());
            Assert.Equal(270369782, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(1, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(4, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));

            Assert.Equal(0.890965365816991200, random.NextDouble());
            Assert.Equal(0.954959475701542700, random.NextDouble());
            Assert.Equal(0.787838221812683600, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("B3-A1-41-D6-8D-45-B6-91", BitConverter.ToString(buffer1));
            Assert.Equal("F4-10-08-12-8F-AA-F8-4A", BitConverter.ToString(buffer2));
            Assert.Equal("CA-29-62-6B-9D-80-15-24", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro1024**: Seed = Int32.MaxValue")]
        public void Test_InitMax() {
            var random = new Xoroshiro1024SS(int.MaxValue);

            Assert.Equal(1838284367, random.Next());
            Assert.Equal(-199112859, random.Next());
            Assert.Equal(-1033718635, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(1, random.Next(10));
            Assert.Equal(2, random.Next(10));

            Assert.Equal(6, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));

            Assert.Equal(0.193587739648112930, random.NextDouble());
            Assert.Equal(0.285017530096625340, random.NextDouble());
            Assert.Equal(0.116275265153288120, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("CA-DC-78-98-C8-66-8F-5B", BitConverter.ToString(buffer1));
            Assert.Equal("BB-46-79-B8-C3-C9-BE-0F", BitConverter.ToString(buffer2));
            Assert.Equal("A2-29-66-B1-C6-F1-BE-90", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoroshiro1024**: Two instances compared")]
        public void Test_TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Xoroshiro1024SS();
            var random2 = new Xoroshiro1024SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
