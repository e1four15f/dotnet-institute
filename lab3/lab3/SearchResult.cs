using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab3
{
    class SearchResult
    {
        private Dictionary<string, List<long>> dict;
        private string file;

        public List<Tuple<long, List<Tuple<string, long>>>> scores;

        public SearchResult()
        {
            dict = new Dictionary<string, List<long>>();
        }

        private void AddWord(string word, long position)
        {
            if (!dict.ContainsKey(word))
            {
                dict.Add(word, new List<long>());
            }
            dict[word].Add(position);
        }

        public void BuildIndex(string file)
        {
            this.file = file;
            Console.WriteLine("Indexing " + Path.GetFileName(file));
            using (StreamReader sr = File.OpenText(file))
            {
                string punctuation = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~\t\r";

                string word = "";
                long position = 0;

                while (sr.Peek() >= 0)
                {
                    char c = (char)sr.Read();
                    if (!punctuation.Contains(c))
                    {
                        if (c == ' ' || c == '\n')
                        {
                            if (!word.Equals(""))
                            {
                                AddWord(word, position - word.Length);
                                word = "";
                            }
                        }
                        else
                        {
                            word += c;
                        }
                        position++;
                    }
                }
            }
        }

        public void Find(string phrase)
        {
            string[] words = phrase.Split(' ');
            string filename = Path.GetFileName(file);
            SortedDictionary<long, string> finds = new SortedDictionary<long, string>();

            foreach (string word in words)
            {
                if (dict.ContainsKey(word))
                {
                    for (int j = 0; j < dict[word].Count; j++)
                    {
                        finds.Add(dict[word][j], word);
                    }
                }
            }

            scores = new List<Tuple<long, List<Tuple<string, long>>>>();

            for (int i = 0; i < finds.Count - 1; i++) 
            {
                List<Tuple<string, long>> temp = new List<Tuple<string, long>>();
                int count = Array.IndexOf(words, finds.ElementAt(i).Value);
                long distance = 0;

                temp.Add(new Tuple<string, long>(finds.ElementAt(i).Value, finds.ElementAt(i).Key));
                for (int j = i + 1; j < finds.Count; j++) //  + 1
                {
                    count++;
                    if (count != words.Length && finds.ElementAt(j).Value.Equals(words[count]))
                    {
                        distance += Math.Abs(finds.ElementAt(i).Key - finds.ElementAt(j).Key) - 
                            (finds.ElementAt(i).Value.Length + finds.ElementAt(j).Value.Length);
                        temp.Add(new Tuple<string, long>(finds.ElementAt(j).Value, finds.ElementAt(j).Key));
                    }
                    else
                    {
                        distance /= temp.Count^10;
                        if (distance == 0)
                        {
                            distance = finds.ElementAt(i).Key;
                        }
                        scores.Add(new Tuple<long, List<Tuple<string, long>>>(distance, temp));
                        break;
                    }
                }
            }

            scores.Sort((x, y) => -y.Item1.CompareTo(x.Item1));
        }
        
        public override string ToString()
        {
            int size = scores.Count < 5 ? scores.Count : 5;

            string str = scores.Count > 0 ? "\n" + Path.GetFileName(file) + "\n" : "";
            for (int i = 0; i < size; i++)
            {
                str += String.Format("{0, 10}", scores[i].Item1);
                foreach (Tuple<string, long> tuple in scores[i].Item2)
                {
                    str += " " + tuple.Item1;
                }
                str += "\n";
            }

            return str;
        }
    }
}
