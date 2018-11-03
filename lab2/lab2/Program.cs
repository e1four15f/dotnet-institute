using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace lab2
{
    class Program
    {
        private const string path = "../../files/";
        private static string input, output;

        static void Main(string[] args)
        {   
            while (true)
            {
                try
                {
                    string[] str = Console.ReadLine().Split(' ');
                    if (str.Length == 1 && str[0].Equals("exit")) { break; }

                    if (str.Length == 3)
                    {
                        input = path + str[1];
                        output = path + str[2];

                        if (str[0].Equals("-pack"))
                        {
                            Console.WriteLine("\nPacking...");

                            Stopwatch timer = new Stopwatch();
                            timer.Start();
                            Packer.Pack(input, output);
                            timer.Stop();

                            Console.WriteLine("Pack done with " + timer.Elapsed +
                                "\nPack quality " + (double)Utils.GetFileSize(output) / (double)Utils.GetFileSize(input)
                                + "\n");
                        }

                        else if (str[0].Equals("-unpack"))
                        {
                            Console.WriteLine("\nUnpacking...");

                            Stopwatch timer = new Stopwatch();
                            timer.Start();
                            Packer.Unpack(input, output);
                            timer.Stop();

                            Console.WriteLine("Unpack done with " + timer.Elapsed + "\n");
                        }

                        else if (str[0].Equals("-test"))
                        {
                            Console.WriteLine("\nCreating text file...");

                            Stopwatch timer = new Stopwatch();
                            timer.Start();
                            Utils.CreateTextFile(path + str[1], int.Parse(str[2]));
                            timer.Stop();

                            Console.WriteLine("Creation done with " + timer.Elapsed + "\n");
                        }

                        else { throw new FormatException("Invalid string format\n"); }
                    }
                    
                    else { throw new FormatException("Invalid string format\n"); }
                }
                
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
