using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests;

[TestClass]
public class SplitMix64_Tests {

    [TestMethod]
    public void Test_InternalStream() {  // checking internal stream is equal to the official implementation
        var random = new ScrambledLinear.SplitMix64(42);

        UInt64[] values = new UInt64[20];
        for (int i = 0; i < values.Length; i++) {
            values[i] = (UInt64)random.Next();
        }

        Assert.AreEqual((UInt64)0xbdd732262feb6e95, values[0]);
        Assert.AreEqual((UInt64)0x28efe333b266f103, values[1]);
        Assert.AreEqual((UInt64)0x47526757130f9f52, values[2]);
        Assert.AreEqual((UInt64)0x581ce1ff0e4ae394, values[3]);
        Assert.AreEqual((UInt64)0x09bc585a244823f2, values[4]);
        Assert.AreEqual((UInt64)0xde4431fa3c80db06, values[5]);
        Assert.AreEqual((UInt64)0x37e9671c45376d5d, values[6]);
        Assert.AreEqual((UInt64)0xccf635ee9e9e2fa4, values[7]);
        Assert.AreEqual((UInt64)0x5705b8770b3d7dd5, values[8]);
        Assert.AreEqual((UInt64)0x9e54d738297f77ae, values[9]);
        Assert.AreEqual((UInt64)0x3474724a775b19bf, values[10]);
        Assert.AreEqual((UInt64)0x7e348a0e451650be, values[11]);
        Assert.AreEqual((UInt64)0x836ded897f3e46e6, values[12]);
        Assert.AreEqual((UInt64)0x851f977347ed6db7, values[13]);
        Assert.AreEqual((UInt64)0xaa47e31c02e78edc, values[14]);
        Assert.AreEqual((UInt64)0x341452c54d7c33f2, values[15]);
        Assert.AreEqual((UInt64)0x1a83d752f35eba75, values[16]);
        Assert.AreEqual((UInt64)0x7ed90003f67f9e1d, values[17]);
        Assert.AreEqual((UInt64)0x17eadff448a86a07, values[18]);
        Assert.AreEqual((UInt64)0xb05eca1a2972b860, values[19]);

        for (int i = 0; i < 1000000; i++) {
            random.Next();
        }
        Assert.AreEqual((UInt64)0xf5bccc21b1a2a703, (UInt64)random.Next());
    }


    [TestMethod]
    public void Test_InternalSeedMin() {
        var random = new ScrambledLinear.SplitMix64(unchecked((long)UInt64.MinValue));

        Assert.AreEqual((UInt64)0xE220A8397B1DCDAF, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0x6E789E6AA1B965F4, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0x06C45D188009454F, (UInt64)random.Next());
    }

    [TestMethod]
    public void Test_InternalSeedMax() {
        var random = new ScrambledLinear.SplitMix64(unchecked((long)UInt64.MaxValue));

        Assert.AreEqual((UInt64)0xE4D971771B652C20, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0xE99FF867DBF682C9, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0x382FF84CB27281E9, (UInt64)random.Next());
    }


    [TestMethod]
    public void Test_SeedInt32Min() {
        var random = new ScrambledLinear.SplitMix64(int.MinValue);

        Assert.AreEqual((UInt64)0xEE1591EA220FB185, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0x4A69C0DB2A03EFBE, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0x4C4DF2838B6AD7DF, (UInt64)random.Next());
    }

    [TestMethod]
    public void Test_SeedInt32Max() {
        var random = new ScrambledLinear.SplitMix64(int.MaxValue);

        Assert.AreEqual((UInt64)0x61FA36A6261A4BE7, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0x097A775B9E76A5C7, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0x6536E03C7465DF5E, (UInt64)random.Next());
    }


    [TestMethod]
    public void Test_SeedInt64Min() {
        var random = new ScrambledLinear.SplitMix64(long.MinValue);

        Assert.AreEqual((UInt64)0x481EC0A212A9F3DB, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0xC46FA638A6309012, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0x61A685FFC80A8140, (UInt64)random.Next());
    }

    [TestMethod]
    public void Test_SeedInt64Max() {
        var random = new ScrambledLinear.SplitMix64(long.MaxValue);

        Assert.AreEqual((UInt64)0x2A67D7552E039EA7, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0xF20C01408082F947, (UInt64)random.Next());
        Assert.AreEqual((UInt64)0xEC159351AF424190, (UInt64)random.Next());
    }


    [TestMethod]
    public void Test_TwoInstances() {  // since we're using 100ns, it should not result in the same random values (let's ignore them being equal by accident)
        var random1 = new ScrambledLinear.SplitMix64();
        var random2 = new ScrambledLinear.SplitMix64();

        Assert.AreNotEqual(random1.Next(), random2.Next());
        Assert.AreNotEqual(random1.Next(), random2.Next());
        Assert.AreNotEqual(random1.Next(), random2.Next());
    }

}
