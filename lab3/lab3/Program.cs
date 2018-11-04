using System;
using System.Diagnostics;
using System.IO;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
            FileSearch fs = new FileSearch("../../files/books");
            Stopwatch timer = new Stopwatch();

            timer.Start();
            fs.BuildIndex();
            timer.Stop();
            Console.WriteLine("Done!\n" + timer.ElapsedMilliseconds + " ms\n");

            string phrase = "";
            while (true)
            {
                Console.Write("Search for or exit: ");
                phrase = Console.ReadLine();

                if (phrase.Equals("exit"))
                {
                    break;
                }

                timer.Restart();
                fs.Find(phrase);
                timer.Stop();
                Console.WriteLine(fs);
                Console.WriteLine(timer.ElapsedMilliseconds + " ms\n");
            }
        }
    }
}