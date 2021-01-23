using System;
using System.Reflection;
using Xunit;
using Subject = ScrambledLinear;

namespace Tests {
    public class SplitMix64 {

        [Fact(DisplayName = "splitmix64: Reference")]
        public void InternalStream() {  // checking internal stream is equal to the official implementation
            var random = new Subject.SplitMix64(42);

            UInt64[] values = new UInt64[20];
            for (int i = 0; i < values.Length; i++) {
                values[i] = random.Next();
            }

            Assert.Equal((UInt64)0xbdd732262feb6e95, values[0]);
            Assert.Equal((UInt64)0x28efe333b266f103, values[1]);
            Assert.Equal((UInt64)0x47526757130f9f52, values[2]);
            Assert.Equal((UInt64)0x581ce1ff0e4ae394, values[3]);
            Assert.Equal((UInt64)0x09bc585a244823f2, values[4]);
            Assert.Equal((UInt64)0xde4431fa3c80db06, values[5]);
            Assert.Equal((UInt64)0x37e9671c45376d5d, values[6]);
            Assert.Equal((UInt64)0xccf635ee9e9e2fa4, values[7]);
            Assert.Equal((UInt64)0x5705b8770b3d7dd5, values[8]);
            Assert.Equal((UInt64)0x9e54d738297f77ae, values[9]);
            Assert.Equal((UInt64)0x3474724a775b19bf, values[10]);
            Assert.Equal((UInt64)0x7e348a0e451650be, values[11]);
            Assert.Equal((UInt64)0x836ded897f3e46e6, values[12]);
            Assert.Equal((UInt64)0x851f977347ed6db7, values[13]);
            Assert.Equal((UInt64)0xaa47e31c02e78edc, values[14]);
            Assert.Equal((UInt64)0x341452c54d7c33f2, values[15]);
            Assert.Equal((UInt64)0x1a83d752f35eba75, values[16]);
            Assert.Equal((UInt64)0x7ed90003f67f9e1d, values[17]);
            Assert.Equal((UInt64)0x17eadff448a86a07, values[18]);
            Assert.Equal((UInt64)0xb05eca1a2972b860, values[19]);

            for (int i = 0; i < 1000000; i++) {
                random.Next();
            }
            Assert.Equal((UInt64)0xf5bccc21b1a2a703, random.Next());
        }


        [Fact(DisplayName = "splitmix64: Seed = 0")]
        public void Init0() {
            var random = new Subject.SplitMix64(0);

            Assert.Equal((UInt64)0xE220A8397B1DCDAF, random.Next());
            Assert.Equal((UInt64)0x6E789E6AA1B965F4, random.Next());
            Assert.Equal((UInt64)0x06C45D188009454F, random.Next());
        }

        [Fact(DisplayName = "splitmix64: Seed = Int32.MinValue")]
        public void InitMinInt() {
            var random = new Subject.SplitMix64(unchecked((uint)int.MinValue));

            Assert.Equal((UInt64)0x25493CC63225736C, random.Next());
            Assert.Equal((UInt64)0xF8845BB42853955B, random.Next());
            Assert.Equal((UInt64)0xE2C35CDF1BA90FD6, random.Next());
        }

        [Fact(DisplayName = "splitmix64: Seed = Int32.MaxValue")]
        public void InitMaxInt() {
            var random = new Subject.SplitMix64(int.MaxValue);

            Assert.Equal((UInt64)0x61FA36A6261A4BE7, random.Next());
            Assert.Equal((UInt64)0x97A775B9E76A5C7, random.Next());
            Assert.Equal((UInt64)0x6536E03C7465DF5E, random.Next());
        }

        [Fact(DisplayName = "splitmix64: Seed = UInt64.MaxValue")]
        public void InitMax() {
            var random = new Subject.SplitMix64(UInt64.MaxValue);

            Assert.Equal((UInt64)0xE4D971771B652C20, random.Next());
            Assert.Equal((UInt64)0xE99FF867DBF682C9, random.Next());
            Assert.Equal((UInt64)0x382FF84CB27281E9, random.Next());
        }


        [Fact(DisplayName = "splitmix64: Two instances compared")]
        public void TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
            var random1 = new Subject.SplitMix64();
            var random2 = new Subject.SplitMix64();

            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
            Assert.NotEqual(random1.Next(), random2.Next());
        }

    }
}
