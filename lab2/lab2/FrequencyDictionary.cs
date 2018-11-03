using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace lab2
{
    class FrequencyDictionary
    {
        private Dictionary<char, int> freq;
        public Dictionary<char, int> Freq() { return freq; }

        public FrequencyDictionary(string path)
        {
            freq = new Dictionary<char, int>();
            Console.WriteLine("Creating frequency table...");
            ReadFile(path);
        }
        
        private void ReadFile(string path)
        {
            string text = File.ReadAllText(path);
            for (int i = 0; i < text.Length; i++)
            {
                AddKey(text[i]);
            }
        }

        private void AddKey(char key)
        {
            if (!freq.ContainsKey(key))
            {
                freq.Add(key, 1);
            }
            else
            {
                freq[key]++;
            }
        }
        
        public override string ToString()
        {
            string str = String.Format("Key | Frequency\n");
            foreach (KeyValuePair<char, int> kvp in freq)
            {
                str += String.Format("{0,-3} | {1,-9}\n", kvp.Key, kvp.Value);
            }
            return str;
        }
    }
}
