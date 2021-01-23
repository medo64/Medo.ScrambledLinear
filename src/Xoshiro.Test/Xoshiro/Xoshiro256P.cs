using System;
using System.Reflection;
using Xunit;
using Subject = Xoshiro;

namespace Test.Xoshiro {
    public class Xoshiro256P {

        [Fact(DisplayName = "xoshiro256+: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Subject.Xoshiro256P).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Subject.Xoshiro256P).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Subject.Xoshiro256P();
            sField.SetValue(random, new UInt64[] { 2, 3, 5, 7 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt64)0x0000000000000009, values[0]);
            Assert.Equal((UInt64)0x0000800000000006, values[1]);
            Assert.Equal((UInt64)0x0001000010000002, values[2]);
            Assert.Equal((UInt64)0xc000a00020060207, values[3]);
            Assert.Equal((UInt64)0xc040f800040e0402, values[4]);
            Assert.Equal((UInt64)0x008158142f058283, values[5]);
            Assert.Equal((UInt64)0xb011081c271283e5, values[6]);
            Assert.Equal((UInt64)0x10b46611140a0766, values[7]);
            Assert.Equal((UInt64)0x1024fa1aaed1a428, values[8]);
            Assert.Equal((UInt64)0x8470b213b341509c, values[9]);
            Assert.Equal((UInt64)0xea54448fab44b08e, values[10]);
            Assert.Equal((UInt64)0xd2f6c3546a188728, values[11]);
            Assert.Equal((UInt64)0xe2dd02e02e79ed5d, values[12]);
            Assert.Equal((UInt64)0xb9e39324498a0d21, values[13]);
            Assert.Equal((UInt64)0xd9f0b839115f6091, values[14]);
            Assert.Equal((UInt64)0x7df3fb90c21787f5, values[15]);
            Assert.Equal((UInt64)0x02802f1c3f4e70e1, values[16]);
            Assert.Equal((UInt64)0xa4bdadfa0bdc3fb2, values[17]);
            Assert.Equal((UInt64)0x1ededdd4f34980b4, values[18]);
            Assert.Equal((UInt64)0xc677c1b3bb034e51, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0x81c7fa64dcc586d9, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoshiro256+: Seed = 0")]
        public void Init0() {
            var random = new Subject.Xoshiro256P(0);

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

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("E9-BF-55-2A-DE-43-AA-95", BitConverter.ToString(buffer1));
            Assert.Equal("49-C6-90-C8-7C-59-6A-2A", BitConverter.ToString(buffer2));
            Assert.Equal("82-67-8C-77-94-BE-59-A1", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro256+: Seed = Int32.MinValue")]
        public void InitMin() {
            var random = new Subject.Xoshiro256P(int.MinValue);

            Assert.Equal(-1139840677, random.Next());
            Assert.Equal(-352892913, random.Next());
            Assert.Equal(-624707362, random.Next());

            Assert.Equal(8, random.Next(10));
            Assert.Equal(8, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(-4, random.Next(-9, 10));

            Assert.Equal(0.073029424077849740, random.NextDouble());
            Assert.Equal(0.835876291881205300, random.NextDouble());
            Assert.Equal(0.057153288233847244, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("7E-F7-66-5A-6C-29-4E-BD", BitConverter.ToString(buffer1));
            Assert.Equal("48-0D-B1-CF-10-30-18-A0", BitConverter.ToString(buffer2));
            Assert.Equal("B9-8F-78-F5-83-8F-94-B4", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro256+: Seed = Int32.MaxValue")]
        public void InitMax() {
            var random = new Subject.Xoshiro256P(int.MaxValue);

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

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("99-53-65-D5-16-1A-0D-1B", BitConverter.ToString(buffer1));
            Assert.Equal("FF-5A-96-C4-6F-92-88-29", BitConverter.ToString(buffer2));
            Assert.Equal("B5-B7-95-91-F0-08-98-CB", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoshiro256+: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Subject.Xoshiro256P();
            var random2 = new Subject.Xoshiro256P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
