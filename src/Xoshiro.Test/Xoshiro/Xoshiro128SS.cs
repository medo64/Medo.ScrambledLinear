using System;
using System.Reflection;
using Xunit;
using Subject = Xoshiro;

namespace Test.Xoshiro {
    public class Xoshiro128SS {

        [Fact(DisplayName = "xoshiro128**: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Subject.Xoshiro128SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Subject.Xoshiro128SS).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Subject.Xoshiro128SS();
            sField.SetValue(random, new UInt32[] { 2, 3, 5, 7 });

            UInt32[] values = new UInt32[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt32)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt32)0x00004380, values[0]);
            Assert.Equal((UInt32)0x00005a00, values[1]);
            Assert.Equal((UInt32)0x00877080, values[2]);
            Assert.Equal((UInt32)0x03848700, values[3]);
            Assert.Equal((UInt32)0x8ee12d12, values[4]);
            Assert.Equal((UInt32)0x72fe5180, values[5]);
            Assert.Equal((UInt32)0x3405045c, values[6]);
            Assert.Equal((UInt32)0xc973e406, values[7]);
            Assert.Equal((UInt32)0xe1049f9f, values[8]);
            Assert.Equal((UInt32)0x0e26e7fc, values[9]);
            Assert.Equal((UInt32)0x66dc59f6, values[10]);
            Assert.Equal((UInt32)0x08d7750b, values[11]);
            Assert.Equal((UInt32)0xb5202b60, values[12]);
            Assert.Equal((UInt32)0x0dcfff1f, values[13]);
            Assert.Equal((UInt32)0xf742df2c, values[14]);
            Assert.Equal((UInt32)0x86b0f158, values[15]);
            Assert.Equal((UInt32)0xb5409aeb, values[16]);
            Assert.Equal((UInt32)0xeecc7092, values[17]);
            Assert.Equal((UInt32)0xfb7aac73, values[18]);
            Assert.Equal((UInt32)0x045a67cf, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt32)0x35b471ff, (UInt32)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoshiro128**: Seed = 0")]
        public void Init0() {
            var random = new Subject.Xoshiro128SS(0);

            Assert.Equal(-881462604, random.Next());
            Assert.Equal(1230390642, random.Next());
            Assert.Equal(393209181, random.Next());

            Assert.Equal(6, random.Next(10));
            Assert.Equal(5, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(2, random.Next(-9, 10));
            Assert.Equal(3, random.Next(-9, 10));

            Assert.Equal(0.979232173878699500, random.NextDouble());
            Assert.Equal(0.322598935104906560, random.NextDouble());
            Assert.Equal(0.346315557835623600, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("0B-A2-43-7B-77-46-14-EA", BitConverter.ToString(buffer1));
            Assert.Equal("09-5E-E2-B9-EB-1D-30-05", BitConverter.ToString(buffer2));
            Assert.Equal("8C-CE-4D-42-4C-41-7D-41", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro128**: Seed = Int32.MinValue")]
        public void InitMin() {
            var random = new Subject.Xoshiro128SS(int.MinValue);

            Assert.Equal(1486914308, random.Next());
            Assert.Equal(772687524, random.Next());
            Assert.Equal(994280968, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(3, random.Next(10));

            Assert.Equal(-3, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));
            Assert.Equal(-1, random.Next(-9, 10));

            Assert.Equal(0.032522102817893030, random.NextDouble());
            Assert.Equal(0.562209527939558000, random.NextDouble());
            Assert.Equal(0.645298989489674600, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("99-85-C8-81-0A-F6-87-CC", BitConverter.ToString(buffer1));
            Assert.Equal("8B-29-97-DB-A0-10-3E-4A", BitConverter.ToString(buffer2));
            Assert.Equal("16-CD-8A-3A-28-A2-10-AC", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro128**: Seed = Int32.MaxValue")]
        public void InitMax() {
            var random = new Subject.Xoshiro128SS(int.MaxValue);

            Assert.Equal(1838284268, random.Next());
            Assert.Equal(-824600722, random.Next());
            Assert.Equal(1576424145, random.Next());

            Assert.Equal(2, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(-1, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));

            Assert.Equal(0.486268784618005160, random.NextDouble());
            Assert.Equal(0.529523150529712400, random.NextDouble());
            Assert.Equal(0.190922153880819680, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("8A-ED-FB-28-54-DA-8C-F4", BitConverter.ToString(buffer1));
            Assert.Equal("98-3E-6C-09-80-D3-92-21", BitConverter.ToString(buffer2));
            Assert.Equal("F2-D9-44-BF-60-74-FC-55", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoshiro128**: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Subject.Xoshiro128SS();
            var random2 = new Subject.Xoshiro128SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
