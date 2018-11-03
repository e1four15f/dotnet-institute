using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    abstract class Packer
    {
        private static CodeGenerator cd;
        public static CodeGenerator CD() { return cd; }

        private static string content;
        public static string Content() { return content; }

        public static void Pack(string input, string output)
        {
            cd = new CodeGenerator();
            cd.BuildTree(input);

            byte[] bytes = File.ReadAllBytes(input);

            string[] cd_codes = new string[bytes.Length];
            int size = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                cd_codes[i] = cd.Code()[Convert.ToChar(bytes[i])];
                size += cd_codes[i].Length;
            }

            BitArray bits = new BitArray(size);

            int index = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                for (int j = 0; j < cd_codes[i].Length; j++, index++)
                {
                    bits[index] = (cd_codes[i][j] == '1') ? true : false;
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

        public static void Unpack(string input, string output)
        {
            cd = new CodeGenerator();
            BitArray bits = cd.ReadBinaryHeader(File.ReadAllBytes(input));

            Dictionary<string, char> dict = new Dictionary<string, char>();
            foreach (KeyValuePair<char, string> kvp in cd.Code())
            {
                dict.Add(kvp.Value, kvp.Key);
            }

            string value = "";
            content = "";
            for (int i = 0; i < bits.Length; i++)
            {
                value += bits[i] ? "1" : "0";
                if (dict.ContainsKey(value))
                {
                    content += dict[value];
                    value = "";
                }
            }

            File.WriteAllText(output, content);
        }
    }
}