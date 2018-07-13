using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace lab2
{
    class Program
    {
        private const string filename = "input";
        private const string input = "files/" + filename + ".txt";
        private const string output = "files/" + filename + ".pck ";

        static void Main(string[] args)
        {

            FrequencyDictionary fd = new FrequencyDictionary(input);
            Console.WriteLine("Input file size = " + Utils.GetFileSize(input) + " bytes\n" + fd);
            
            CodeGenerator cd = new CodeGenerator();
            cd.BuildTree(fd.Freq());
            Pack(input, output, cd);
            Console.WriteLine("Output file size = " + Utils.GetFileSize(output) + " bytes\n" + cd);
            
            /*
            CodeGenerator cd = new CodeGenerator();
            Unpack(input, output, cd);
            Console.WriteLine("Done!");
            */
            Console.ReadKey();
        }

        private static void Pack(string input, string output, CodeGenerator cd)
        {
            byte[] bytes = File.ReadAllBytes(input);

            int size = 0;
            foreach (byte b in bytes)
            {
                size += cd.Code()[Convert.ToChar(b)].Length;
            }
            
            BitArray bits = new BitArray(size);

            int index = 0;
            foreach (byte b in bytes)
            {
                for (int i = 0; i < cd.Code()[Convert.ToChar(b)].Length; i++, index++)
                {
                    bits[index] = (cd.Code()[Convert.ToChar(b)][i] == '1') ? true : false;
                }
            }

            byte offset = Convert.ToByte(bits.Length % 8 == 0 ? 0 : 8 - bits.Length % 8);

            BitArray codes = cd.GetBinaryHeader(offset);
            bool[] bools = new bool[bits.Count + codes.Count];
            codes.CopyTo(bools, 0);
            bits.CopyTo(bools, codes.Count);
            BitArray all = new BitArray(bools);

            bytes = new byte[all.Length / 8];
            all.CopyTo(bytes, 0);

            File.WriteAllBytes(output, bytes);
        }
        
        private static void Unpack(string input, string output, CodeGenerator cd)
        {
            BitArray bits = cd.ReadBinaryHeader(File.ReadAllBytes(output));

            Dictionary<string, char> dict = new Dictionary<string, char>();
            foreach (KeyValuePair<char, string> kvp in cd.Code())
            {
                dict.Add(kvp.Value, kvp.Key);
            }

            string value = "", str = "";
            for (int i = 0; i < bits.Length; i++)
            {
                value += bits[i] ? "1" : "0";
                if (dict.ContainsKey(value))
                {
                    str += dict[value];
                    // To see output
                    //Console.Write(dict[value]);
                    value = "";
                }
            }

            File.WriteAllText(input, str);
        }
    }
}
