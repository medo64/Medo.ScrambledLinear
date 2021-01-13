using System;
using System.Reflection;
using Xunit;

namespace Xoshiro.Test {
    public class Tests_Xoshiro128PP {

        [Fact(DisplayName = "xoshiro128++: Reference")]
        public void Test_InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Xoshiro128PP).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Xoshiro128PP).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Xoshiro128PP();
            sField.SetValue(random, new UInt32[] { 2, 3, 5, 7 });

            UInt32[] values = new UInt32[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt32)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt32)0x00000482, values[0]);
            Assert.Equal((UInt32)0x00100306, values[1]);
            Assert.Equal((UInt32)0x80202102, values[2]);
            Assert.Equal((UInt32)0x19170d88, values[3]);
            Assert.Equal((UInt32)0x186b0f49, values[4]);
            Assert.Equal((UInt32)0x07a88174, values[5]);
            Assert.Equal((UInt32)0x20aa9338, values[6]);
            Assert.Equal((UInt32)0xc9f24665, values[7]);
            Assert.Equal((UInt32)0xb159878c, values[8]);
            Assert.Equal((UInt32)0x16132738, values[9]);
            Assert.Equal((UInt32)0xed378291, values[10]);
            Assert.Equal((UInt32)0x1ad79e24, values[11]);
            Assert.Equal((UInt32)0x795f9194, values[12]);
            Assert.Equal((UInt32)0xab0f6f38, values[13]);
            Assert.Equal((UInt32)0xb48eae98, values[14]);
            Assert.Equal((UInt32)0x43ccf959, values[15]);
            Assert.Equal((UInt32)0x8cb080e8, values[16]);
            Assert.Equal((UInt32)0x67a48e2a, values[17]);
            Assert.Equal((UInt32)0x78d5e3bc, values[18]);
            Assert.Equal((UInt32)0x5d91a04e, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt32)0x80055dac, (UInt32)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoshiro128++: Seed = 0")]
        public void Test_Init0() {
            var random = new Xoshiro128PP(0);

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

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("C8-06-BD-9F-FB-8B-EA-CD", BitConverter.ToString(buffer1));
            Assert.Equal("3E-17-22-6F-42-A5-27-08", BitConverter.ToString(buffer2));
            Assert.Equal("F7-67-5A-1B-B6-CA-5C-89", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro128++: Seed = Int32.MinValue")]
        public void Test_InitMin() {
            var random = new Xoshiro128PP(int.MinValue);

            Assert.Equal(970596682, random.Next());
            Assert.Equal(-436500853, random.Next());
            Assert.Equal(396903485, random.Next());

            Assert.Equal(7, random.Next(10));
            Assert.Equal(0, random.Next(10));
            Assert.Equal(5, random.Next(10));

            Assert.Equal(4, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));

            Assert.Equal(0.354887533001601700, random.NextDouble());
            Assert.Equal(0.016351415077224374, random.NextDouble());
            Assert.Equal(0.438598690088838340, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("FF-E7-5F-74-E4-0F-B6-BE", BitConverter.ToString(buffer1));
            Assert.Equal("FC-F7-EE-E9-C3-62-41-19", BitConverter.ToString(buffer2));
            Assert.Equal("CA-D6-7D-6B-C1-94-ED-F1", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro128++: Seed = Int32.MaxValue")]
        public void Test_InitMax() {
            var random = new Xoshiro128PP(int.MaxValue);

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

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("59-57-0F-28-14-DF-3E-69", BitConverter.ToString(buffer1));
            Assert.Equal("A6-C0-5F-B7-94-29-E3-ED", BitConverter.ToString(buffer2));
            Assert.Equal("CC-52-FE-85-BC-9F-51-48", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoshiro128++: Two instances compared")]
        public void Test_TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Xoshiro128PP();
            var random2 = new Xoshiro128PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
