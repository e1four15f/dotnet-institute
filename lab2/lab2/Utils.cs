﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

            List<char> content = new List<char>();
            Parallel.For(0, size * 4, i =>
            {
                char c = chars[rng.Next(chars.Length)];
                for (int j = 0; j < 256; j++)
                {
                    lock (content)
                    {
                        content.Add(c);
                    }
                }
            });

            File.WriteAllText(filename, string.Join("", content.ToArray()));
        }
    }
}
