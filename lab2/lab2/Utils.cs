using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab2
{
    abstract class Utils
    {
        public static int GetFileSize(string path)
        {
            return File.ReadAllBytes(path).Length;
        }

        public static Dictionary<char, int> SortDictionary(Dictionary<char, int> dict)
        {
            Dictionary<char, int> temp = new Dictionary<char, int>();
            foreach (KeyValuePair<char, int> kvp in dict.OrderBy(x => -x.Value))
            {
                temp.Add(kvp.Key, kvp.Value);
            }
            return temp;
        }

        public static Dictionary<string, int> SortDictionary(Dictionary<string, int> dict)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> kvp in dict.OrderBy(x => -x.Value))
            {
                temp.Add(kvp.Key.ToString(), kvp.Value);
            }
            return temp;
        }

        public static BitArray Append(BitArray A, BitArray B)
        {
            bool[] bools = new bool[A.Count + B.Count];
            A.CopyTo(bools, 0);
            B.CopyTo(bools, A.Count);
            return new BitArray(bools);
        }

        public static void CreateTextFile(string filename, int size)
        {
            string chars = "123467890ABCDEFGHJKLMNPQRTUVWXYZabcdefghjkmnpqrtuvwxyz!@#$%^&*()-=_+|{}][';~";
            Random rng = new Random();

            string content = "";
            for (int i = 0; i < size * 1024; i++)
            {
                content += chars.Substring(rng.Next(chars.Length), 1);
            }

            File.WriteAllText(filename, content);
        }
    }
}
