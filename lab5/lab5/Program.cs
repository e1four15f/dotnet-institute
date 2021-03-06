﻿using System;
using System.Threading;

namespace lab5
{
    class Program
    {
        public static TCPIPServer server;

        static void Main(string[] args)
        {
            server = new TCPIPServer("../../files", 322);
            server.Start();
            Console.WriteLine("Server is running on this port: " + server.Port);
            
            Thread exitThread = new Thread(Exit);
            exitThread.Start();
        }

        public static void Exit()
        {
            while (true)
            {
                if (Console.ReadLine() == "exit")
                {
                    server.Stop();
                    Console.WriteLine("Server was stopped!");
                    Thread.Sleep(1500);
                    Environment.Exit(0);
                }
            }
        }
    }
}