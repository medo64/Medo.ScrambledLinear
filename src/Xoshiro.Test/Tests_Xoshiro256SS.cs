using System;
using System.Reflection;
using Xunit;

namespace Xoshiro.Test {
    public class Tests_Xoshiro256SS {

        [Fact(DisplayName = "xoshiro256**: Reference")]
        public void Test_InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Xoshiro256SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Xoshiro256SS).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Xoshiro256SS();
            sField.SetValue(random, new UInt64[] { 2, 3, 5, 7 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt64)0x0000000000004380, values[0]);
            Assert.Equal((UInt64)0x0000000000005a00, values[1]);
            Assert.Equal((UInt64)0x0000000087007080, values[2]);
            Assert.Equal((UInt64)0x0b400000b4008700, values[3]);
            Assert.Equal((UInt64)0x00010f68e1002d00, values[4]);
            Assert.Equal((UInt64)0x0e116800e12da0e0, values[5]);
            Assert.Equal((UInt64)0xcefdc25ae1000360, values[6]);
            Assert.Equal((UInt64)0xdd5d68f882f886e0, values[7]);
            Assert.Equal((UInt64)0xffb0b6069997a2c0, values[8]);
            Assert.Equal((UInt64)0x5b69f105c388cbb0, values[9]);
            Assert.Equal((UInt64)0xa6be22b82be21748, values[10]);
            Assert.Equal((UInt64)0xd7b7e4da0bea4a7a, values[11]);
            Assert.Equal((UInt64)0x55fa51ef1d81ab99, values[12]);
            Assert.Equal((UInt64)0x001b6f78c57ec571, values[13]);
            Assert.Equal((UInt64)0xcc2e78fb73903631, values[14]);
            Assert.Equal((UInt64)0x8a261fdbe7516eae, values[15]);
            Assert.Equal((UInt64)0x0d3ea5f7b8c53bf1, values[16]);
            Assert.Equal((UInt64)0xcd47873f3da30cc1, values[17]);
            Assert.Equal((UInt64)0x7afaa696bd37c0c9, values[18]);
            Assert.Equal((UInt64)0x99bd65623d78cd51, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0x53b12b13cdb8b059, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoshiro256**: Seed = 0")]
        public void Test_Init0() {
            var random = new Xoshiro256SS(0);

            Assert.Equal(-881462604, random.Next());
            Assert.Equal(1230390570, random.Next());
            Assert.Equal(1228138208, random.Next());

            Assert.Equal(4, random.Next(10));
            Assert.Equal(7, random.Next(10));
            Assert.Equal(9, random.Next(10));

            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(1, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));

            Assert.Equal(0.918858012706964800, random.NextDouble());
            Assert.Equal(0.114297464406487580, random.NextDouble());
            Assert.Equal(0.067231891013425300, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("9C-8E-3D-AB-A5-07-4F-1B", BitConverter.ToString(buffer1));
            Assert.Equal("7F-DB-86-69-7F-25-A3-A7", BitConverter.ToString(buffer2));
            Assert.Equal("9C-FC-5D-60-95-AA-FD-7E", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro256**: Seed = Int32.MinValue")]
        public void Test_InitMin() {
            var random = new Xoshiro256SS(int.MinValue);

            Assert.Equal(1486914389, random.Next());
            Assert.Equal(772687749, random.Next());
            Assert.Equal(1154647281, random.Next());

            Assert.Equal(4, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(6, random.Next(10));

            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(3, random.Next(-9, 10));
            Assert.Equal(5, random.Next(-9, 10));

            Assert.Equal(0.984283843867828500, random.NextDouble());
            Assert.Equal(0.973840238110876800, random.NextDouble());
            Assert.Equal(0.068826795380842580, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("D0-4E-EB-E9-FA-13-B2-29", BitConverter.ToString(buffer1));
            Assert.Equal("C3-EE-5E-8F-65-99-8A-21", BitConverter.ToString(buffer2));
            Assert.Equal("77-EA-A9-0D-27-FC-91-0A", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro256**: Seed = Int32.MaxValue")]
        public void Test_InitMax() {
            var random = new Xoshiro256SS(int.MaxValue);

            Assert.Equal(1838284367, random.Next());
            Assert.Equal(-824601550, random.Next());
            Assert.Equal(2062636305, random.Next());

            Assert.Equal(6, random.Next(10));
            Assert.Equal(6, random.Next(10));
            Assert.Equal(1, random.Next(10));

            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(0, random.Next(-9, 10));
            Assert.Equal(9, random.Next(-9, 10));

            Assert.Equal(0.127957773772553680, random.NextDouble());
            Assert.Equal(0.388994105041749360, random.NextDouble());
            Assert.Equal(0.981492799015784200, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("A2-74-02-A6-ED-92-77-86", BitConverter.ToString(buffer1));
            Assert.Equal("DE-FD-24-E2-33-8C-3B-7B", BitConverter.ToString(buffer2));
            Assert.Equal("FA-D9-82-5A-BB-42-28-52", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoshiro256**: Two instances compared")]
        public void Test_TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Xoshiro256SS();
            var random2 = new Xoshiro256SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
