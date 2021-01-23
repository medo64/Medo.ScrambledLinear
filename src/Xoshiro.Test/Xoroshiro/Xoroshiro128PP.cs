using System;
using System.Reflection;
using Xunit;
using Subject = Xoroshiro;

namespace Test.Xoroshiro {
    public class Xoroshiro128PP {

        [Fact(DisplayName = "xoroshiro128++: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Subject.Xoroshiro128PP).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Subject.Xoroshiro128PP).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Subject.Xoroshiro128PP();
            sField.SetValue(random, new UInt64[] { 2, 3 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt64)0x00000000000a0002, values[0]);
            Assert.Equal((UInt64)0x0004204000220009, values[1]);
            Assert.Equal((UInt64)0x081444089082024d, values[2]);
            Assert.Equal((UInt64)0x909c248981326149, values[3]);
            Assert.Equal((UInt64)0xcb8d553498d3056f, values[4]);
            Assert.Equal((UInt64)0xb2ddc177cf5432cf, values[5]);
            Assert.Equal((UInt64)0x2fdcefbbcf1661c1, values[6]);
            Assert.Equal((UInt64)0x7c217e7a4ca98253, values[7]);
            Assert.Equal((UInt64)0x7dd80d50ec380c2e, values[8]);
            Assert.Equal((UInt64)0x8da619d3dac945f6, values[9]);
            Assert.Equal((UInt64)0x587c9282664a84ba, values[10]);
            Assert.Equal((UInt64)0xa6b354d01824ef41, values[11]);
            Assert.Equal((UInt64)0x148c6d3cd65f24fe, values[12]);
            Assert.Equal((UInt64)0xb54f7d3e2084ab12, values[13]);
            Assert.Equal((UInt64)0x97901f4ec0a149a8, values[14]);
            Assert.Equal((UInt64)0xa1616375861db982, values[15]);
            Assert.Equal((UInt64)0xe4103cc653454025, values[16]);
            Assert.Equal((UInt64)0xc102fbdf97e9d3b4, values[17]);
            Assert.Equal((UInt64)0x28b971ee7ed25188, values[18]);
            Assert.Equal((UInt64)0x990b95a3e5677187, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0x7a8fd422209dbe19, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoroshiro128++: Seed = 0")]
        public void Init0() {
            var random = new Subject.Xoroshiro128PP(0);

            Assert.Equal(-496734495, random.Next());
            Assert.Equal(1161860269, random.Next());
            Assert.Equal(1865473592, random.Next());

            Assert.Equal(4, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(3, random.Next(10));

            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(4, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));

            Assert.Equal(0.316018995128142470, random.NextDouble());
            Assert.Equal(0.663537559698846600, random.NextDouble());
            Assert.Equal(0.747768719361041200, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("2B-C9-C1-A3-01-3B-19-C1", BitConverter.ToString(buffer1));
            Assert.Equal("41-A5-34-92-AE-7C-7B-80", BitConverter.ToString(buffer2));
            Assert.Equal("5C-90-06-DD-9E-F7-13-5C", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro128++: Seed = Int32.MinValue")]
        public void InitMin() {
            var random = new Subject.Xoroshiro128PP(int.MinValue);

            Assert.Equal(1135849223, random.Next());
            Assert.Equal(1459908728, random.Next());
            Assert.Equal(-30944965, random.Next());

            Assert.Equal(8, random.Next(10));
            Assert.Equal(7, random.Next(10));
            Assert.Equal(3, random.Next(10));

            Assert.Equal(5, random.Next(-9, 10));
            Assert.Equal(2, random.Next(-9, 10));
            Assert.Equal(-9, random.Next(-9, 10));

            Assert.Equal(0.348167598252529400, random.NextDouble());
            Assert.Equal(0.623373234782368400, random.NextDouble());
            Assert.Equal(0.796830101573609700, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("C2-8A-C4-BA-DA-B0-BE-CF", BitConverter.ToString(buffer1));
            Assert.Equal("9F-C8-A8-5C-94-3E-1C-D8", BitConverter.ToString(buffer2));
            Assert.Equal("91-5C-77-6A-B9-AD-4D-4F", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro128++: Seed = Int32.MaxValue")]
        public void InitMax() {
            var random = new Subject.Xoroshiro128PP(int.MaxValue);

            Assert.Equal(158802640, random.Next());
            Assert.Equal(1569184532, random.Next());
            Assert.Equal(-1163646361, random.Next());

            Assert.Equal(5, random.Next(10));
            Assert.Equal(9, random.Next(10));
            Assert.Equal(7, random.Next(10));

            Assert.Equal(-9, random.Next(-9, 10));
            Assert.Equal(9, random.Next(-9, 10));
            Assert.Equal(1, random.Next(-9, 10));

            Assert.Equal(0.388907956085095650, random.NextDouble());
            Assert.Equal(0.140679433361960320, random.NextDouble());
            Assert.Equal(0.047973743341569010, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("9F-EB-84-49-5A-26-99-41", BitConverter.ToString(buffer1));
            Assert.Equal("99-FE-43-AD-79-E0-96-71", BitConverter.ToString(buffer2));
            Assert.Equal("91-CC-34-49-EE-FE-E0-D3", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoroshiro128++: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Subject.Xoroshiro128PP();
            var random2 = new Subject.Xoroshiro128PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
