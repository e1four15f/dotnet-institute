using System;
using System.Collections.Generic;
using System.Text;

namespace lab7Lib
{
    public class RLE
    {
        public byte[] Pack(byte[] data)
        {
            List<char> list = new List<char>();

            int length = 1;
            byte current = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] == current)
                {
                    length++;
                }

                if (data[i] != current || i + 1 == data.Length)
                {
                    foreach (char c in length.ToString())
                    {
                        list.Add(c);
                    }
                    list.Add((char)current);
                    list.Add(' ');

                    length = 1;
                    current = data[i];
                }
            }

            return Encoding.UTF8.GetBytes(list.ToArray());
        }

        public byte[] Unpack(byte[] data)
        {
            string content = Encoding.UTF8.GetString(data);
            string digits = "0123456789";
            
            string result = "";
            string length = "";
            char current = '\n';

            for (int i = 0; i < content.Length; i++)
            {
                if (digits.IndexOf(content[i]) != -1)
                {
                    length += content[i];
                }

                else if (content[i] == ' ')
                {
                    for (int j = 0; j < int.Parse(length); j++)
                    {
                        result += current;
                    }
                    length = "";
                }

                else
                {
                    current = content[i];
                }
            }

            return Encoding.UTF8.GetBytes(result);
        }
    }
}
