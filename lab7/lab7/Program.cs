using System;
using System.IO;
using lab7Lib;

namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] packBytes = File.ReadAllBytes("../../files/TEST.txt");
            File.WriteAllBytes("../../files/TEST Pack.txt", RLE.Pack(packBytes));

            byte[] unpackBytes = File.ReadAllBytes("../../files/TEST Pack.txt");
            File.WriteAllBytes("../../files/TEST Unpack.txt", RLE.Unpack(unpackBytes));

            Console.WriteLine("Done!");
            Console.Read();
        }
    }
}
