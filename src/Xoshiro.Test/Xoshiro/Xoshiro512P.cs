using System;
using System.Reflection;
using Xunit;
using Subject = Xoshiro;

namespace Test.Xoshiro {
    public class Xoshiro512P {

        [Fact(DisplayName = "xoshiro512+: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Subject.Xoshiro512P).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Subject.Xoshiro512P).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Subject.Xoshiro512P();
            sField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt64)0x0000000000000007, values[0]);
            Assert.Equal((UInt64)0x000000000000001a, values[1]);
            Assert.Equal((UInt64)0x000000000000182a, values[2]);
            Assert.Equal((UInt64)0x0000000002803821, values[3]);
            Assert.Equal((UInt64)0x000050000400d03c, values[4]);
            Assert.Equal((UInt64)0x0000800006615015, values[5]);
            Assert.Equal((UInt64)0x0000d4140160f01b, values[6]);
            Assert.Equal((UInt64)0x828014200721b034, values[7]);
            Assert.Equal((UInt64)0x840034300af0681a, values[8]);
            Assert.Equal((UInt64)0x86c0be0f0a706031, values[9]);
            Assert.Equal((UInt64)0xc1a0c63c0ad05c2c, values[10]);
            Assert.Equal((UInt64)0xc1817e5004d8b01d, values[11]);
            Assert.Equal((UInt64)0x4680b3428558cc26, values[12]);
            Assert.Equal((UInt64)0x66d15b730a19be25, values[13]);
            Assert.Equal((UInt64)0x6e43511805cc741d, values[14]);
            Assert.Equal((UInt64)0xe1e271a4482c2220, values[15]);
            Assert.Equal((UInt64)0xbb6ab18b89142f38, values[16]);
            Assert.Equal((UInt64)0xc22213a289167a0f, values[17]);
            Assert.Equal((UInt64)0x9e2232eca6672f26, values[18]);
            Assert.Equal((UInt64)0x244704e7c82761ce, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0x72c2c6ccd0bff8c1, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoshiro512+: Seed = 0")]
        public void Init0() {
            var random = new Subject.Xoshiro512P(0);

            Assert.Equal(-81325314, random.Next());
            Assert.Equal(1601013806, random.Next());
            Assert.Equal(-1430995155, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(2, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(-7, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));

            Assert.Equal(0.999761197143335300, random.NextDouble());
            Assert.Equal(0.515100886199905800, random.NextDouble());
            Assert.Equal(0.508433547014529100, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("ED-C4-94-B7-D5-3D-D6-62", BitConverter.ToString(buffer1));
            Assert.Equal("31-52-AA-75-F8-E8-67-AC", BitConverter.ToString(buffer2));
            Assert.Equal("CD-55-1C-F5-8D-E1-F8-D4", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro512+: Seed = Int32.MinValue")]
        public void InitMin() {
            var random = new Subject.Xoshiro512P(int.MinValue);

            Assert.Equal(1305379650, random.Next());
            Assert.Equal(266441212, random.Next());
            Assert.Equal(681674813, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(3, random.Next(10));
            Assert.Equal(4, random.Next(10));

            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));
            Assert.Equal(-2, random.Next(-9, 10));

            Assert.Equal(0.147887634195853050, random.NextDouble());
            Assert.Equal(0.613485294696565700, random.NextDouble());
            Assert.Equal(0.430256385347092430, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("7E-E2-DF-34-71-1D-D7-DF", BitConverter.ToString(buffer1));
            Assert.Equal("2B-74-EB-D7-00-B6-2A-0B", BitConverter.ToString(buffer2));
            Assert.Equal("28-66-1A-DD-3B-EA-ED-C7", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro512+: Seed = Int32.MaxValue")]
        public void InitMax() {
            var random = new Subject.Xoshiro512P(int.MaxValue);

            Assert.Equal(-1702876347, random.Next());
            Assert.Equal(-1552027764, random.Next());
            Assert.Equal(-1696413967, random.Next());

            Assert.Equal(2, random.Next(10));
            Assert.Equal(7, random.Next(10));
            Assert.Equal(6, random.Next(10));

            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));

            Assert.Equal(0.875797007435673100, random.NextDouble());
            Assert.Equal(0.943634923583176900, random.NextDouble());
            Assert.Equal(0.693731369217030600, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("1C-70-2D-82-BE-E2-8B-45", BitConverter.ToString(buffer1));
            Assert.Equal("BC-E9-22-87-93-03-69-F7", BitConverter.ToString(buffer2));
            Assert.Equal("4E-47-98-C0-85-73-28-2A", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoshiro512+: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Subject.Xoshiro512P();
            var random2 = new Subject.Xoshiro512P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
