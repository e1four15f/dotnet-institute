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
        private SearchResult[] results;

        public FileSearch(string path)
        {
            files = Directory.GetFiles(path);
            results = new SearchResult[files.Length];
        }

        public void BuildIndex()
        {
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = new SearchResult();
                results[i].BuildIndex(files[i]);
            }
        }

        public void Find(string phrase)
        {
            for (int i = 0; i < results.Length; i++)
            {
                results[i].Find(phrase);
                Console.Write(results[i]);
            }
        }
    }
}
