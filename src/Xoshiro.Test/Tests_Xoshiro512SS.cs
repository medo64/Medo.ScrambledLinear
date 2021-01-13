using System;
using System.Reflection;
using Xunit;

namespace Xoshiro.Test {
    public class Tests_Xoshiro512SS {

        [Fact(DisplayName = "xoshiro512**: Reference")]
        public void Test_InternalStream() {  // checking internal stream is equal to the official implementation
            var sField = typeof(Xoshiro512SS).GetField("s", BindingFlags.NonPublic | BindingFlags.Instance);
            var nextValueMethod = typeof(Xoshiro512SS).GetMethod("NextValue", BindingFlags.NonPublic | BindingFlags.Instance);

            var random = new Xoshiro512SS();
            sField.SetValue(random, new UInt64[] { 2, 3, 5, 7, 11, 13, 17, 19 });

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = (UInt64)nextValueMethod.Invoke(random, null);
            }

            Assert.Equal((UInt64)0x0000000000004380, values[0]);
            Assert.Equal((UInt64)0x0000000000005a00, values[1]);
            Assert.Equal((UInt64)0x0000000000016800, values[2]);
            Assert.Equal((UInt64)0x00000000021d9500, values[3]);
            Assert.Equal((UInt64)0x0000003842d15180, values[4]);
            Assert.Equal((UInt64)0x07080021cb412480, values[5]);
            Assert.Equal((UInt64)0x0438002a3caa7600, values[6]);
            Assert.Equal((UInt64)0x032bc20b48709d80, values[7]);
            Assert.Equal((UInt64)0x44390e5cd70872f6, values[8]);
            Assert.Equal((UInt64)0xc05b0e994fbeb99b, values[9]);
            Assert.Equal((UInt64)0x305943d190b5975b, values[10]);
            Assert.Equal((UInt64)0xf110e6d2ff62013b, values[11]);
            Assert.Equal((UInt64)0xda08f526ac1834c0, values[12]);
            Assert.Equal((UInt64)0xd5049107c4a9283a, values[13]);
            Assert.Equal((UInt64)0x371ee3fbd34bb4cf, values[14]);
            Assert.Equal((UInt64)0xb0b1a25de898dc28, values[15]);
            Assert.Equal((UInt64)0x86a2c16393d2fa86, values[16]);
            Assert.Equal((UInt64)0x07de187bce70d3db, values[17]);
            Assert.Equal((UInt64)0x51ef8ed7cc998183, values[18]);
            Assert.Equal((UInt64)0x0703a352cfda1fa8, values[19]);

            for (int i = 0; i < 1000000; i++) {
                nextValueMethod.Invoke(random, null);
            }
            Assert.Equal((UInt64)0x270ce1bc058010b5, (UInt64)nextValueMethod.Invoke(random, null));
        }


        [Fact(DisplayName = "xoshiro512**: Seed = 0")]
        public void Test_Init0() {
            var random = new Xoshiro512SS(0);

            Assert.Equal(-881462604, random.Next());
            Assert.Equal(1230390570, random.Next());
            Assert.Equal(1410947490, random.Next());

            Assert.Equal(1, random.Next(10));
            Assert.Equal(5, random.Next(10));
            Assert.Equal(9, random.Next(10));

            Assert.Equal(1, random.Next(-9, 10));
            Assert.Equal(9, random.Next(-9, 10));
            Assert.Equal(-4, random.Next(-9, 10));

            Assert.Equal(0.219531457878631600, random.NextDouble());
            Assert.Equal(0.157772962862030800, random.NextDouble());
            Assert.Equal(0.287294733517758470, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("01-C7-2A-D7-69-76-DE-AC", BitConverter.ToString(buffer1));
            Assert.Equal("73-0A-A3-DD-2F-2A-58-D8", BitConverter.ToString(buffer2));
            Assert.Equal("02-33-7C-A5-A5-E8-DA-0D", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro512**: Seed = Int32.MinValue")]
        public void Test_InitMin() {
            var random = new Xoshiro512SS(int.MinValue);

            Assert.Equal(1486914389, random.Next());
            Assert.Equal(772687749, random.Next());
            Assert.Equal(-1841154331, random.Next());

            Assert.Equal(7, random.Next(10));
            Assert.Equal(5, random.Next(10));
            Assert.Equal(3, random.Next(10));

            Assert.Equal(5, random.Next(-9, 10));
            Assert.Equal(1, random.Next(-9, 10));
            Assert.Equal(-3, random.Next(-9, 10));

            Assert.Equal(0.649904833661327100, random.NextDouble());
            Assert.Equal(0.183703054525985230, random.NextDouble());
            Assert.Equal(0.294699500811034700, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("2D-A3-05-E0-B2-8B-33-05", BitConverter.ToString(buffer1));
            Assert.Equal("9A-72-EF-2D-43-01-FC-6A", BitConverter.ToString(buffer2));
            Assert.Equal("F1-3C-C0-9A-EA-0E-E3-67", BitConverter.ToString(buffer3));
        }

        [Fact(DisplayName = "xoshiro512**: Seed = Int32.MaxValue")]
        public void Test_InitMax() {
            var random = new Xoshiro512SS(int.MaxValue);

            Assert.Equal(1838284367, random.Next());
            Assert.Equal(-824601550, random.Next());
            Assert.Equal(174802128, random.Next());

            Assert.Equal(1, random.Next(10));
            Assert.Equal(0, random.Next(10));
            Assert.Equal(0, random.Next(10));

            Assert.Equal(-5, random.Next(-9, 10));
            Assert.Equal(5, random.Next(-9, 10));
            Assert.Equal(-8, random.Next(-9, 10));

            Assert.Equal(0.228253829033394640, random.NextDouble());
            Assert.Equal(0.177896103866960860, random.NextDouble());
            Assert.Equal(0.307057091887398050, random.NextDouble());

            var buffer1 = new byte[8]; random.NextBytes(buffer1);
            var buffer2 = new byte[8]; random.NextBytes(buffer2);
            var buffer3 = new byte[8]; random.NextBytes(buffer3);
            Assert.Equal("D1-C9-7F-69-18-78-DD-D4", BitConverter.ToString(buffer1));
            Assert.Equal("A5-AE-2A-32-EA-70-98-ED", BitConverter.ToString(buffer2));
            Assert.Equal("FA-61-4A-34-AD-55-97-AF", BitConverter.ToString(buffer3));
        }


        [Fact(DisplayName = "xoshiro512**: Two instances compared")]
        public void Test_TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Xoshiro512SS();
            var random2 = new Xoshiro512SS();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());

            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
            Assert.NotEqual(random1.NextDouble(), random2.NextDouble());
        }

    }
}
