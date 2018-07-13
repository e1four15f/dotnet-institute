using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab2
{
    class CodeGenerator
    {
        private Dictionary<string, int> tree;
        private Dictionary<char, string> code;

        private FrequencyDictionary fd;

        public FrequencyDictionary FD()
        {
            return fd;
        }

        public Dictionary<char, string> Code() { return code; }

        public CodeGenerator()
        {
            tree = new Dictionary<string, int>();
            code = new Dictionary<char, string>();
        }

        public BitArray GetBinaryHeader(byte offset)
        {
            string str = "";

            foreach (KeyValuePair<char, string> kvp in code)
            {
                str += kvp.Key + kvp.Value + "\n";
            }
            str += ",,,";

            BitArray all = new BitArray(0);
            all = Utils.Append(all, new BitArray(new byte[] { offset }));
            all = Utils.Append(all, new BitArray(offset));
            all = Utils.Append(all, new BitArray(Encoding.ASCII.GetBytes(str)));
            
            return all;
        }

        public BitArray ReadBinaryHeader(byte[] all) 
        {
            byte offset = all[0];

            bool[] bools = new bool[all.Length * 8 - 8 - offset];
            BitArray bits = new BitArray(all);
            for (int i = 8 + offset; i < bits.Length; i++)
            {
                bools[i - 8 - offset] = bits[i]; 
            }
            bits = new BitArray(bools);

            byte[] allCrop = new byte[(bits.Length + offset) / 8];
            bits.CopyTo(allCrop, 0);
            
            int index = 0;
            while (!(allCrop[index + 1] == 44 && allCrop[index + 2] == 44 && allCrop[index + 3] == 44))
            {
                index++;
            }
            byte[] codes = new byte[index];
            Array.Copy(allCrop, 0, codes, 0, index);

            string str = System.Text.Encoding.ASCII.GetString(codes) + '\n'; 

            for (int i = 0; i < str.Length; i++)
            {
                string value = "";
                char key = str[i];
                i++;

                while (!str[i].Equals('\n'))
                {
                    value += str[i];
                    i++;
                }
                code.Add(key, value);
            }

            bools = new bool[bits.Length - (index + 4) * 8];
            for (int i = (index + 4) * 8; i < bits.Length; i++)
            {
                bools[i - (index + 4) * 8] = bits[i];
            }
            bits = new BitArray(bools);

            return bits;
        }

        public void BuildTree(string path)
        {
            fd = new FrequencyDictionary(path);

            foreach (KeyValuePair<char, int> kvp in fd.Freq())
            {
                tree.Add(kvp.Key.ToString(), kvp.Value);
                code.Add(kvp.Key, "");
            }

            while (tree.Count > 1)
            {
                string[] key = new String[2];
                int value = 0;

                for (int i = 0; i < 2; i++)
                {
                    key[i] += tree.Last().Key;
                    if (key[i].Length > 0)
                    {
                        foreach (char c in key[i])
                        {
                            code[c] = i.ToString() + code[c];
                        }
                    }
                    value += tree.Last().Value;
                    tree.Remove(tree.Last().Key);
                }

                tree.Add(key[0] + key[1], value);
                tree = Utils.SortDictionary(tree);
            }
        }
        
        public override string ToString()
        {
            string str = String.Format("{0,-3} | {1,-9}\n", "Key", "Code");
            foreach (KeyValuePair<char, string> kvp in code)
            {
                str += String.Format("{0,-3} | {1,-9}\n", kvp.Key, kvp.Value);
            }
            return str;
        }
    }
}
