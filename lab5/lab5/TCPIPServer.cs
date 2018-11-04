using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab5
{
    class TCPIPServer
    {
        private Dictionary<string, string> extensions = new Dictionary<string, string>()
        { 
            {".css", "text/css"},
            {".ico", "image/x-icon"},
            {".html", "text/html"},
            {".jpg", "image/jpeg"},
            {".png", "image/png"}
        };

        private Thread serverThread;
        private string rootDirectory;
        private TcpListener listener;
        private int port;

        public int Port { get { return port; } }

        public TCPIPServer(string path, int port)
        {
            this.rootDirectory = path;
            this.port = port;
        }

        public void Start()
        {
            serverThread = new Thread(this.Listen);
            serverThread.Start();
        }

        private void Listen()
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Process(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void Process(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            using (StreamReader sr = new StreamReader(stream))
            {
                string file = "";
                while (true)
                {
                    string line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    if (line.Contains("GET"))
                    {
                        file = line.Split(' ')[1].Length == 1 ? "/index.html" : line.Split(' ')[1].Split('?')[0];
                        Console.WriteLine("Requested " + file + "...");
                    }
                }

                byte[] responseBytes = ResponseBytes(rootDirectory + file); 

                stream.Write(responseBytes, 0, responseBytes.Length);
                stream.Flush();

                client.Close();
            }
        }

        private byte[] ResponseBytes(string file)
        {
            string extension = Path.GetExtension(file);
            string contentType = extensions.ContainsKey(extension) ? extensions[extension] : "application/octet-stream";

            byte[] input = File.ReadAllBytes(file);
            long contentLength = File.ReadAllBytes(file).Length;

            string header = "HTTP/1.1 200 OK\nContent-Type: " + contentType + 
                "\nContent-Length: " + contentLength + "\n\n";

            byte[] headerBytes = Encoding.ASCII.GetBytes(header);

            byte[] responseBytes = new byte[headerBytes.Length + input.Length];
            Array.Copy(headerBytes, 0, responseBytes, 0, headerBytes.Length);
            Array.Copy(input, 0, responseBytes, headerBytes.Length, input.Length);

            return responseBytes;
        }

        public void Stop()
        {
            listener.Stop();
            serverThread.Abort();
        }
    }
}