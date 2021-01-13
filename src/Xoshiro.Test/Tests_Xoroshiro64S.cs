using System;
using System.Reflection;
using Xunit;

namespace Xoshiro.Test {
    public class Tests_Xoroshiro64S {

        [Fact(DisplayName = "xoroshiro64*: Reference")]
        public void Test_InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Xoroshiro64S).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Xoroshiro64S).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Xoroshiro64S();
            sField.SetValue(random, new UInt32[] { 2, 3 });

            UInt32[] values = new UInt32[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt32)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt32)0x3c6ef376, values[0]);
            Assert.Equal((UInt32)0xe52aefbb, values[1]);
            Assert.Equal((UInt32)0xd036a793, values[2]);
            Assert.Equal((UInt32)0x0ac7d613, values[3]);
            Assert.Equal((UInt32)0x62924cb1, values[4]);
            Assert.Equal((UInt32)0xceb58025, values[5]);
            Assert.Equal((UInt32)0xc841aa6d, values[6]);
            Assert.Equal((UInt32)0xc85175ea, values[7]);
            Assert.Equal((UInt32)0xf40f3895, values[8]);
            Assert.Equal((UInt32)0x1acf917e, values[9]);
            Assert.Equal((UInt32)0x9543c1d0, values[10]);
            Assert.Equal((UInt32)0xc240dc54, values[11]);
            Assert.Equal((UInt32)0xa318f8e0, values[12]);
            Assert.Equal((UInt32)0x452d9bd2, values[13]);
            Assert.Equal((UInt32)0xd89b751c, values[14]);
            Assert.Equal((UInt32)0xd986e259, values[15]);
            Assert.Equal((UInt32)0x210dd189, values[16]);
            Assert.Equal((UInt32)0x3a088319, values[17]);
            Assert.Equal((UInt32)0x799c01db, values[18]);
            Assert.Equal((UInt32)0xeb2cf837, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt32)0x48e1789c, (UInt32)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoroshiro64*: Seed = 0")]
        public void Test_Init0() {
            var random = new Xoroshiro64S(0);

            Assert.Equal(932574677, random.Next());
            Assert.Equal(571770783, random.Next());
            Assert.Equal(-1816336140, random.Next());

            Assert.Equal(9, random.Next(10));
            Assert.Equal(0, random.Next(10));
            Assert.Equal(8, random.Next(10));

            Assert.Equal(9, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));

            Assert.Equal(0.673005640506744400, random.NextDouble());
            Assert.Equal(0.140669375658035280, random.NextDouble());
            Assert.Equal(0.907178431749343900, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("CB-43-6B-49-31-99-5C-8C", BitConverter.ToString(buffer1));
            Assert.Equal("D5-0A-91-D9-0B-F6-EB-D6", BitConverter.ToString(buffer2));
            Assert.Equal("08-49-55-DA-DC-89-CD-71", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro64*: Seed = Int32.MinValue")]
        public void Test_InitMin() {
            var random = new Xoroshiro64S(int.MinValue);

            Assert.Equal(-920822812, random.Next());
            Assert.Equal(-583200866, random.Next());
            Assert.Equal(-1924111503, random.Next());

            Assert.Equal(7, random.Next(10));
            Assert.Equal(0, random.Next(10));
            Assert.Equal(6, random.Next(10));

            Assert.Equal(-3, random.Next(-9, 10));
            Assert.Equal(3, random.Next(-9, 10));
            Assert.Equal(0, random.Next(-9, 10));

            Assert.Equal(0.879846319556236300, random.NextDouble());
            Assert.Equal(0.835736081004142800, random.NextDouble());
            Assert.Equal(0.685153156518936200, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("C9-40-F5-AA-3C-09-BD-93", BitConverter.ToString(buffer1));
            Assert.Equal("F3-35-62-40-6B-E6-CD-22", BitConverter.ToString(buffer2));
            Assert.Equal("58-49-D7-7F-70-F7-4A-9D", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoroshiro64*: Seed = Int32.MaxValue")]
        public void Test_InitMax() {
            var random = new Xoroshiro64S(int.MaxValue);

            Assert.Equal(599171261, random.Next());
            Assert.Equal(957906677, random.Next());
            Assert.Equal(-1266798358, random.Next());

            Assert.Equal(3, random.Next(10));
            Assert.Equal(2, random.Next(10));
            Assert.Equal(2, random.Next(10));

            Assert.Equal(-2, random.Next(-9, 10));
            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(2, random.Next(-9, 10));

            Assert.Equal(0.988461241126060500, random.NextDouble());
            Assert.Equal(0.735286518931388900, random.NextDouble());
            Assert.Equal(0.049030110239982605, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("A5-18-46-81-7F-9A-33-8F", BitConverter.ToString(buffer1));
            Assert.Equal("85-79-D9-2E-50-94-E5-2F", BitConverter.ToString(buffer2));
            Assert.Equal("AC-5E-FC-23-B9-B9-CE-4A", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoroshiro64*: Two instances compared")]
        public void Test_TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Xoroshiro64S();
            var random2 = new Xoroshiro64S();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
