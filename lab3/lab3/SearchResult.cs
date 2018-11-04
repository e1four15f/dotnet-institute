using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab3
{
    class SearchResult
    {
        private Dictionary<int, string> text;
        private string file, fullText;
        private int phraseLength;

        public SearchResult(string file)
        {
            text = new Dictionary<int, string>();
            this.file = file;
        }

        public void BuildIndex()
        {
            Console.WriteLine("Indexing " + Path.GetFileName(file));
            fullText = File.ReadAllText(file);

            string punctuation = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~\t\r";

            string word = "";

            for (int i = 0; i < fullText.Length; i++)
            {
                char c = char.ToLower(fullText[i]); 
                if (!punctuation.Contains(c))
                {
                    if (c == ' ' || c == '\n')
                    {
                        if (!word.Equals(""))
                        {
                            text.Add(i - word.Length, word);
                            word = "";
                        }
                    }
                    else
                    {
                        word += c;
                    }
                }
            }
            
        }

        public List<string> Find(string phrase)
        {
            string[] words = phrase.ToLower().Split(' ');
            phraseLength = phrase.Length - words.Length + 1;

            List<string> phrases = new List<string>();
            
            int count = 0, position = 0;
            foreach (KeyValuePair<int, string> kvp in text)
            {
                position = count == 0 ? kvp.Key : position;
                if (kvp.Value.Equals(words[count]))
                {
                    if (count == words.Length - 1)
                    {
                        phrases.Add(GetPhrase(position));
                    }
                    count = count + 1 == words.Length ? 0 : count + 1;
                }
                else
                {
                    count = 0;
                }
            }

            return phrases;
        }

        private string GetPhrase(int line)
        {
            string phrase = "...";
            for (int i = 0; i < phraseLength + 40; i++)
            {
                if (line + i - 20 >= 0)
                { 
                    char c = fullText[line + i - 20];
                    if (c.Equals('\n') || c.Equals('\r'))
                    {
                        c = ' ';
                    }
                    phrase += c;
                }
            }
            return phrase + "...";
        }

        public override string ToString()
        {
            string result = "";
            foreach (KeyValuePair<int, string> kvp in text)
            {
                result += kvp.Key + "\t" + kvp.Value + "\n";
            }
            return result;
        }
    }
}