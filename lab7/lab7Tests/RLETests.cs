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
            byte[] unpackData = Encoding.UTF8.GetBytes("    ");
            RLE.Unpack(unpackData);
        }

        [TestMethod]
        public void Unpack_Sample()
        {


            byte[] unpackData = Encoding.UTF8.GetBytes("12W 1B 12W 3B 24W 1B 14W ");

            byte[] expectedData = Encoding.UTF8.GetBytes("WWWWWWWWWWWWBWWWWWWWWWWWWBBBWWWWWWWWWWWWWWWWWWWWWWWWBWWWWWWWWWWWWWW");
            byte[] actualData = RLE.Unpack(unpackData);

            for (int i = 0; i < actualData.Length; i++)
            {
                Assert.AreEqual(expectedData[i], actualData[i]);
            }
        }

        [TestMethod]
        public void Pack_Sample()
        {

            byte[] packData = Encoding.UTF8.GetBytes("SSSSSJJJJJDDDUUWBBSSSSSSS>>>>>>>>");

            byte[] expectedData = Encoding.UTF8.GetBytes("5S 5J 3D 2U 1W 2B 7S 8> ");
            byte[] actualData = RLE.Pack(packData);

            for (int i = 0; i < actualData.Length; i++)
            {
                Assert.AreEqual(expectedData[i], actualData[i]);
            }
        }

        [TestMethod]
        public void Pack_Over255chars()
        {

            string data = "";
            for (int i = 0; i < 280; i++)
            {
                data += "A";
            }
            byte[] packData = Encoding.UTF8.GetBytes(data);

            byte[] expectedData = Encoding.UTF8.GetBytes("280A ");
            byte[] actualData = RLE.Pack(packData);

            for (int i = 0; i < actualData.Length; i++)
            {
                Assert.AreEqual(expectedData[i], actualData[i]);
            }
        }

        [TestMethod]
        public void Unpack_Over255chars()
        {

            byte[] unpackData = Encoding.UTF8.GetBytes("1540A ");

            string data = "";
            for (int i = 0; i < 1540; i++)
            {
                data += "A";
            }
            byte[] expectedData = Encoding.UTF8.GetBytes(data);
            byte[] actualData = RLE.Unpack(unpackData);

            for (int i = 0; i < actualData.Length; i++)
            {
                Assert.AreEqual(expectedData[i], actualData[i]);
            }
        }
    }
}
