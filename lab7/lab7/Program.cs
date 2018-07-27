using System;
using System.IO;
using lab7Lib;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            RLE rle = new RLE();

            byte[] bytes = File.ReadAllBytes("../../files/TEST.txt");
            File.WriteAllBytes("../../files/TEST Pack.txt", rle.Pack(bytes));

            byte[] bytes = File.ReadAllBytes("../../files/TEST Pack.txt");
            File.WriteAllBytes("../../files/TEST Unpack.txt", rle.Unpack(bytes));

            Console.WriteLine("Done!");
            Console.Read();
        }
    }
}
