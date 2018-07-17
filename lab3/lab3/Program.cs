using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSearch fs = new FileSearch("files");

            Stopwatch timer = new Stopwatch();
            timer.Start();
            fs.BuildIndex();
            timer.Stop();
            Console.WriteLine("Done!\n" + timer.ElapsedMilliseconds + " ms\n");

            //string phrase = "Jim did nothing"; 
            string phrase = Console.ReadLine(); 
            Console.WriteLine();
            
            timer.Restart();
            fs.Find(phrase);
            timer.Stop();
            Console.WriteLine("\n" + timer.ElapsedMilliseconds + " ms\n");
            
            Console.Read();
        }
    }
}
