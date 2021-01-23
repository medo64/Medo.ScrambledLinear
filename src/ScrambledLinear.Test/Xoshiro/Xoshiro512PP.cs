using System;
using System.Reflection;
using Xunit;
using Subject = ScrambledLinear;

namespace Tests.Xoshiro {
    public class Xoshiro512PP {

        [Fact(DisplayName = "xoshiro512++: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Subject.Xoshiro512PP).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Subject.Xoshiro512PP).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Subject.Xoshiro512PP();
            sField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt64)0x00000000000e0005, values[0]);
            Assert.Equal((UInt64)0x0000000000340007, values[1]);
            Assert.Equal((UInt64)0x0000000030540014, values[2]);
            Assert.Equal((UInt64)0x0000050070421802, values[3]);
            Assert.Equal((UInt64)0xa0000801a2f8381d, values[4]);
            Assert.Equal((UInt64)0x00005cc2a32aa003, values[5]);
            Assert.Equal((UInt64)0xa82862c1e0961012, values[6]);
            Assert.Equal((UInt64)0x2840225761c9f51b, values[7]);
            Assert.Equal((UInt64)0xeae029f8d4d53802, values[8]);
            Assert.Equal((UInt64)0xff1e48e0c353159b, values[9]);
            Assert.Equal((UInt64)0x0d38d3afbdc9d34e, values[10]);
            Assert.Equal((UInt64)0xbcc0bfd3604bdf14, values[11]);
            Assert.Equal((UInt64)0x27c588bd9d24951a, values[12]);
            Assert.Equal((UInt64)0xfae65f6e00a399b6, values[13]);
            Assert.Equal((UInt64)0x0901669ae9d31a8b, values[14]);
            Assert.Equal((UInt64)0x44e93d6c49edcbe1, values[15]);
            Assert.Equal((UInt64)0x44f87baca59d88f3, values[16]);
            Assert.Equal((UInt64)0x5fad33affae39348, values[17]);
            Assert.Equal((UInt64)0x17ab206ae2e3a053, values[18]);
            Assert.Equal((UInt64)0x67511d1b68e2f7a6, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0xe6388eba147a1c5f, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoshiro512++: Seed = 0")]
        public void Init0() {
            var random = new Subject.Xoshiro512PP(0);

            Assert.Equal(-1509484775, random.Next());
            Assert.Equal(192016366, random.Next());
            Assert.Equal(497484788, random.Next());

            Assert.Equal(7, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(8, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));

            Assert.Equal(0.326677362647187500, random.NextDouble());
            Assert.Equal(0.303055016085369800, random.NextDouble());
            Assert.Equal(0.886694060932961200, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("3D-70-B3-EB-98-1A-C2-F5", BitConverter.ToString(buffer1));
            Assert.Equal("9C-09-C8-D8-5D-24-9A-64", BitConverter.ToString(buffer2));
            Assert.Equal("9A-BB-BC-20-1F-81-33-4E", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro512++: Seed = Int32.MinValue")]
        public void InitMin() {
            var random = new Subject.Xoshiro512PP(int.MinValue);

            Assert.Equal(573382639, random.Next());
            Assert.Equal(1300603088, random.Next());
            Assert.Equal(-531310711, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(9, random.Next(10));

            Assert.Equal(-3, random.Next(-9, 10));
            Assert.Equal(5, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));

            Assert.Equal(0.050102322997752860, random.NextDouble());
            Assert.Equal(0.845558144532805500, random.NextDouble());
            Assert.Equal(0.170082696023451600, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("2C-AD-FD-AA-CF-9D-B2-84", BitConverter.ToString(buffer1));
            Assert.Equal("D3-2E-76-93-45-8D-D8-4B", BitConverter.ToString(buffer2));
            Assert.Equal("AE-D3-3C-54-31-C0-FC-C8", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro512++: Seed = Int32.MaxValue")]
        public void InitMax() {
            var random = new Subject.Xoshiro512PP(int.MaxValue);

            Assert.Equal(-890147392, random.Next());
            Assert.Equal(832093711, random.Next());
            Assert.Equal(-1872391960, random.Next());

            Assert.Equal(0, random.Next(10));
            Assert.Equal(8, random.Next(10));
            Assert.Equal(1, random.Next(10));

            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(3, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));

            Assert.Equal(0.227928046003613940, random.NextDouble());
            Assert.Equal(0.990906342309305900, random.NextDouble());
            Assert.Equal(0.562925762968837300, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("F4-12-0F-D1-53-95-04-F3", BitConverter.ToString(buffer1));
            Assert.Equal("B4-5E-FA-34-83-CF-AA-3C", BitConverter.ToString(buffer2));
            Assert.Equal("88-6A-BE-D2-9B-04-72-DB", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoshiro512++: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Subject.Xoshiro512PP();
            var random2 = new Subject.Xoshiro512PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
