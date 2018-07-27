using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

using lab7Lib;

namespace lab7Tests
{
    [TestClass]
    public class RLETests
    {
        [TestMethod]
        [ExpectedException(typeof(System.FormatException))]
        public void Unpack_FormatException()
        {
            byte[] data = Encoding.UTF8.GetBytes("                          ");
            RLE rle = new RLE();
            rle.Unpack(data);
        }

        [TestMethod]
        public void Pack_Unpack_Sample()
        {
            string expected = "12W 1B 12W 3B 24W 1B 14W ";
            byte[] expectedData = Encoding.UTF8.GetBytes("WWWWWWWWWWWWBWWWWWWWWWWWWBBBWWWWWWWWWWWWWWWWWWWWWWWWBWWWWWWWWWWWWWW");

            RLE rle = new RLE();
            byte[] actualData = rle.Pack(expectedData);
            string actual = Encoding.UTF8.GetString(actualData);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Pack_over255chars()
        {
            string expected = "255A ";
            byte[] expectedData = Encoding.UTF8.GetBytes("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

            RLE rle = new RLE();
            byte[] actualData = rle.Pack(expectedData);
            string actual = Encoding.UTF8.GetString(actualData);

            Assert.AreEqual(expected, actual);
        }
    }
}
