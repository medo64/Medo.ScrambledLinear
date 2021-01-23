using System;
using System.Reflection;
using Xunit;
using Subject = ScrambledLinear;

namespace Tests.Xoroshiro {
    public class Xoroshiro128P {

        [Fact(DisplayName = "xoroshiro128+: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Subject.Xoroshiro128P).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Subject.Xoroshiro128P).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Subject.Xoroshiro128P();
            sField.SetValue(random, new UInt64[] { 2, 3 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt64)0x0000000000000005, values[0]);
            Assert.Equal((UInt64)0x0000002002010001, values[1]);
            Assert.Equal((UInt64)0x4042034103000401, values[2]);
            Assert.Equal((UInt64)0xc200802906418622, values[3]);
            Assert.Equal((UInt64)0x4b584b8e82114b42, values[4]);
            Assert.Equal((UInt64)0x8873a0a2c478ce45, values[5]);
            Assert.Equal((UInt64)0xec125ba517df4805, values[6]);
            Assert.Equal((UInt64)0xf126e89f888ac936, values[7]);
            Assert.Equal((UInt64)0x7d538b2d6d5c0a31, values[8]);
            Assert.Equal((UInt64)0xc5ca6b5f76568c7e, values[9]);
            Assert.Equal((UInt64)0x3ca69040defb83d5, values[10]);
            Assert.Equal((UInt64)0xf77cc38777498158, values[11]);
            Assert.Equal((UInt64)0xd9ff9ed43deeb20a, values[12]);
            Assert.Equal((UInt64)0xb151cefed8143275, values[13]);
            Assert.Equal((UInt64)0x4470569c543a87b9, values[14]);
            Assert.Equal((UInt64)0x45ed15f66d0d7e9e, values[15]);
            Assert.Equal((UInt64)0xcf5a2b124e4e02fb, values[16]);
            Assert.Equal((UInt64)0xc5b3758d060def69, values[17]);
            Assert.Equal((UInt64)0x4dc6535276d460b7, values[18]);
            Assert.Equal((UInt64)0xdcfc4af9ba43013c, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0x190110b514026bac, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoroshiro128+: Seed = 0")]
        public void Init0() {
            var random = new Subject.Xoroshiro128P(0);

            Assert.Equal(483865507, random.Next());
            Assert.Equal(1747211118, random.Next());
            Assert.Equal(-472941597, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(6, random.Next(10));
            Assert.Equal(6, random.Next(10));

            Assert.Equal(2, random.Next(-9, 10));
            Assert.Equal(7, random.Next(-9, 10));
            Assert.Equal(6, random.Next(-9, 10));

            Assert.Equal(0.033324319972741990, random.NextDouble());
            Assert.Equal(0.428766221556481600, random.NextDouble());
            Assert.Equal(0.004838425802865087, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("EC-CA-61-4E-7B-5B-1C-C5", BitConverter.ToString(buffer1));
            Assert.Equal("CC-AF-0B-B3-BF-FC-53-F0", BitConverter.ToString(buffer2));
            Assert.Equal("19-19-92-57-34-67-CD-EA", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro128+: Seed = Int32.MinValue")]
        public void InitMin() {
            var random = new Subject.Xoroshiro128P(int.MinValue);

            Assert.Equal(1517881543, random.Next());
            Assert.Equal(1242668366, random.Next());
            Assert.Equal(-1372545643, random.Next());

            Assert.Equal(1, random.Next(10));
            Assert.Equal(2, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));

            Assert.Equal(0.3657960461513492000, random.NextDouble());
            Assert.Equal(0.7647975629751376000, random.NextDouble());
            Assert.Equal(0.0017562971023676877, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("C6-C7-77-9E-7A-BC-E5-4C", BitConverter.ToString(buffer1));
            Assert.Equal("32-71-3B-99-46-54-43-A8", BitConverter.ToString(buffer2));
            Assert.Equal("C6-9F-5D-56-B8-F7-71-87", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro128+: Seed = Int32.MaxValue")]
        public void InitMax() {
            var random = new Subject.Xoroshiro128P(int.MaxValue);

            Assert.Equal(-997133906, random.Next());
            Assert.Equal(-1053469747, random.Next());
            Assert.Equal(-187667658, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(4, random.Next(10));
            Assert.Equal(1, random.Next(10));

            Assert.Equal(1, random.Next(-9, 10));
            Assert.Equal(3, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));

            Assert.Equal(0.082385683951663900, random.NextDouble());
            Assert.Equal(0.658551682406144800, random.NextDouble());
            Assert.Equal(0.375396193788777750, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("65-46-E1-27-90-44-26-62", BitConverter.ToString(buffer1));
            Assert.Equal("13-E2-C9-C4-E6-92-31-BE", BitConverter.ToString(buffer2));
            Assert.Equal("66-FF-86-1C-0E-E9-27-77", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoroshiro128+: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Subject.Xoroshiro128P();
            var random2 = new Subject.Xoroshiro128P();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
