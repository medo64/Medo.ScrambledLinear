using System;
using System.Reflection;
using Xunit;

namespace Xoshiro.Test {
    public class Tests_Xoshiro128P {

        [Fact(DisplayName = "xoshiro128+: Reference")]
        public void Test_InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Xoshiro128P).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Xoshiro128P).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Xoshiro128P();
            sField.SetValue(random, new UInt32[] { 2, 3, 5, 7 });

            UInt32[] values = new UInt32[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt32)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt32)0x00000009, values[0]);
            Assert.Equal((UInt32)0x00002006, values[1]);
            Assert.Equal((UInt32)0x01004002, values[2]);
            Assert.Equal((UInt32)0x02302e0f, values[3]);
            Assert.Equal((UInt32)0x80307612, values[4]);
            Assert.Equal((UInt32)0xe50cd80a, values[5]);
            Assert.Equal((UInt32)0x667c3d22, values[6]);
            Assert.Equal((UInt32)0x9ba133f6, values[7]);
            Assert.Equal((UInt32)0x806f41d3, values[8]);
            Assert.Equal((UInt32)0xcb7efa4c, values[9]);
            Assert.Equal((UInt32)0x4b884a68, values[10]);
            Assert.Equal((UInt32)0x93ad77c3, values[11]);
            Assert.Equal((UInt32)0xf711883d, values[12]);
            Assert.Equal((UInt32)0x174aec55, values[13]);
            Assert.Equal((UInt32)0xc4ee89fc, values[14]);
            Assert.Equal((UInt32)0x193f0b95, values[15]);
            Assert.Equal((UInt32)0xd10ce413, values[16]);
            Assert.Equal((UInt32)0x8e8a2308, values[17]);
            Assert.Equal((UInt32)0xf3e7edf5, values[18]);
            Assert.Equal((UInt32)0xd8f92310, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt32)0x2469b79f, (UInt32)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoshiro128+: Seed = 0")]
        public void Test_Init0() {
            var random = new Xoshiro128P(0);

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

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("CC-C9-40-E8-CE-C4-AE-F6", BitConverter.ToString(buffer1));
            Assert.Equal("72-33-53-C9-97-FC-CE-A8", BitConverter.ToString(buffer2));
            Assert.Equal("22-94-0F-DC-1E-9A-7E-C9", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro128+: Seed = Int32.MinValue")]
        public void Test_InitMin() {
            var random = new Xoshiro128P(int.MinValue);

            Assert.Equal(-1139840677, random.Next());
            Assert.Equal(1722071269, random.Next());
            Assert.Equal(330089930, random.Next());

            Assert.Equal(6, random.Next(10));
            Assert.Equal(2, random.Next(10));
            Assert.Equal(6, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));

            Assert.Equal(0.178740676492452620, random.NextDouble());
            Assert.Equal(0.359783500432968140, random.NextDouble());
            Assert.Equal(0.679580256342887900, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("57-5F-B5-F6-03-9D-F8-7F", BitConverter.ToString(buffer1));
            Assert.Equal("3F-0B-9C-12-A1-EA-18-B7", BitConverter.ToString(buffer2));
            Assert.Equal("FA-6C-09-41-0C-8B-E7-F8", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro128+: Seed = Int32.MaxValue")]
        public void Test_InitMax() {
            var random = new Xoshiro128P(int.MaxValue);

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

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("CB-DB-45-39-A3-F7-27-E9", BitConverter.ToString(buffer1));
            Assert.Equal("ED-79-C7-E1-CA-69-1A-C5", BitConverter.ToString(buffer2));
            Assert.Equal("4D-89-7B-87-61-98-9B-E0", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoshiro128+: Two instances compared")]
        public void Test_TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Xoshiro128P();
            var random2 = new Xoshiro128P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
