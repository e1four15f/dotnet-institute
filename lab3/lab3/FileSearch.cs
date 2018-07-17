using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab3
{
    class FileSearch
    {
        private string[] files;
        private SearchResult[] searchResults;

        private string RusKey = "Ё!\"№;%:?*()_+ЙЦУКЕНГШЩЗХЪ/ФЫВАПРОЛДЖЭЯЧСМИТЬБЮ,ё1234567890-=йцукенгшщзхъ\\фывапролджэячсмитьбю. ";
        private string EngKey = "~!@#$%^&*()_+QWERTYUIOP{}|ASDFGHJKL:\"ZXCVBNM<>?`1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./ ";

        private Dictionary<string, List<string>> results;

        public FileSearch(string path)
        {
            files = Directory.GetFiles(path);
            searchResults = new SearchResult[files.Length];
        }

        public void BuildIndex()
        {
            for (int i = 0; i < searchResults.Length; i++)
            {
                searchResults[i] = new SearchResult(files[i]);
                searchResults[i].BuildIndex();
            }
        }

        public void Find(string phrase)
        {
            results = new Dictionary<string, List<string>>();
            for (int i = 0; i < searchResults.Length; i++)
            {
                List<string> list = new List<string>();

                foreach (string s in searchResults[i].Find(phrase))
                {
                    list.Add(s);
                }

                string phraseRus = "";
                foreach (char c in phrase)
                {
                    int index = Array.IndexOf(RusKey.ToCharArray(), c);
                    if (index == -1)
                    {
                        break;
                    }
                    phraseRus += EngKey[index];
                }

                foreach (string s in searchResults[i].Find(phraseRus))
                {
                    list.Add(s);
                }

                results.Add(Path.GetFileName(files[i]), list);
            }
        }

        public override string ToString()
        {
            string str = "";
            foreach (KeyValuePair<string, List<string>> kvp in results)
            {
                if (kvp.Value.Count != 0)
                {
                    str += "\n\t" + kvp.Key + "\n";
                    foreach (string s in kvp.Value)
                    {
                        str += s + "\n";
                    }
                }
            }
            if (str.Equals(""))
            {
                str = "None";
            }
            return str;
        }
    }
}