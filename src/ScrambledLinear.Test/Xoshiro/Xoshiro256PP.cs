using System;
using System.Reflection;
using Xunit;
using Subject = ScrambledLinear;

namespace Tests.Xoshiro {
    public class Xoshiro256PP {

        [Fact(DisplayName = "xoshiro256++: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Subject.Xoshiro256PP).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Subject.Xoshiro256PP).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Subject.Xoshiro256PP();
            sField.SetValue(random, new UInt64[] { 2, 3, 5, 7 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt64)0x0000000004800002, values[0]);
            Assert.Equal((UInt64)0x0000000003000046, values[1]);
            Assert.Equal((UInt64)0x0008800001000082, values[2]);
            Assert.Equal((UInt64)0x0010030113e60057, values[3]);
            Assert.Equal((UInt64)0xc0022702016e227d, values[4]);
            Assert.Equal((UInt64)0xca587acd558440ae, values[5]);
            Assert.Equal((UInt64)0x4e13c15601e78908, values[6]);
            Assert.Equal((UInt64)0xf8d2350bc60ede18, values[7]);
            Assert.Equal((UInt64)0x1d5c06e32d169564, values[8]);
            Assert.Equal((UInt64)0x99f5c2b8ea03589c, values[9]);
            Assert.Equal((UInt64)0xcc22445de2b73aa0, values[10]);
            Assert.Equal((UInt64)0xdcb4f8d194b08f29, values[11]);
            Assert.Equal((UInt64)0x4684e3549410df33, values[12]);
            Assert.Equal((UInt64)0x726fb165f126ae40, values[13]);
            Assert.Equal((UInt64)0xb0791972099884d1, values[14]);
            Assert.Equal((UInt64)0xeb54513457404e6d, values[15]);
            Assert.Equal((UInt64)0x94b4d1c7e1cfcb3f, values[16]);
            Assert.Equal((UInt64)0xea097ea70ded95ce, values[17]);
            Assert.Equal((UInt64)0x7b6e92d65d86c925, values[18]);
            Assert.Equal((UInt64)0x7747c1a709a94dbb, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0x537a0323a1edb13b, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoshiro256++: Seed = 0")]
        public void Init0() {
            var random = new Subject.Xoshiro256PP(0);

            Assert.Equal(1225466847, random.Next());
            Assert.Equal(-1014967033, random.Next());
            Assert.Equal(-325420036, random.Next());

            Assert.Equal(0, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(-4, random.Next(-9, 10));

            Assert.Equal(0.074233791034438390, random.NextDouble());
            Assert.Equal(0.314522039617037200, random.NextDouble());
            Assert.Equal(0.066070988288324800, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("C1-0C-96-43-43-55-AE-1A", BitConverter.ToString(buffer1));
            Assert.Equal("20-E7-FA-10-9F-13-04-18", BitConverter.ToString(buffer2));
            Assert.Equal("FA-10-AC-B8-E7-90-D7-10", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro256++: Seed = Int32.MinValue")]
        public void InitMin() {
            var random = new Subject.Xoshiro256PP(int.MinValue);

            Assert.Equal(-538983557, random.Next());
            Assert.Equal(-1685164826, random.Next());
            Assert.Equal(881290506, random.Next());

            Assert.Equal(2, random.Next(10));
            Assert.Equal(5, random.Next(10));
            Assert.Equal(2, random.Next(10));

            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(-6, random.Next(-9, 10));
            Assert.Equal(4, random.Next(-9, 10));

            Assert.Equal(0.410693841952493900, random.NextDouble());
            Assert.Equal(0.462764240211700500, random.NextDouble());
            Assert.Equal(0.353243913753216200, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("95-17-B2-7A-BB-5C-0D-8A", BitConverter.ToString(buffer1));
            Assert.Equal("9C-CD-C2-89-54-51-5F-72", BitConverter.ToString(buffer2));
            Assert.Equal("F5-38-08-CA-E0-F6-A9-90", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro256++: Seed = Int32.MaxValue")]
        public void InitMax() {
            var random = new Subject.Xoshiro256PP(int.MaxValue);

            Assert.Equal(2025513595, random.Next());
            Assert.Equal(1598180243, random.Next());
            Assert.Equal(-1715557663, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(1, random.Next(10));
            Assert.Equal(3, random.Next(10));

            Assert.Equal(-8, random.Next(-9, 10));
            Assert.Equal(4, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));

            Assert.Equal(0.131584233612690230, random.NextDouble());
            Assert.Equal(0.158757900655281730, random.NextDouble());
            Assert.Equal(0.274703500283967700, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("53-13-EC-5E-6E-B1-A6-D1", BitConverter.ToString(buffer1));
            Assert.Equal("E6-4F-DF-68-C8-6F-7F-E0", BitConverter.ToString(buffer2));
            Assert.Equal("BF-7E-82-A9-2E-FE-BB-7C", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoshiro256++: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Subject.Xoshiro256PP();
            var random2 = new Subject.Xoshiro256PP();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
