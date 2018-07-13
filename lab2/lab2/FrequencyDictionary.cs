using System;
using System.Collections.Generic;
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
            ReadFile(path);
        }
        
        public void ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    AddKey((char)sr.Read());
                }
            }

            freq = Utils.SortDictionary(freq);
        }

        private void AddKey(char key)
        {
            if (!freq.ContainsKey(key))
            {
                freq.Add(key, 1);
            }
            else
            {
                freq[key] += 1;
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
