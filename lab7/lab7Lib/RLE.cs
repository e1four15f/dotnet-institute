using System;
using System.Collections.Generic;
using System.Text;

namespace lab7Lib
{
    public abstract class RLE
    {
        public static byte[] Pack(byte[] data)
        {
            string input = Encoding.UTF8.GetString(data) + " ";
            string packData = "";

            int count = 1;
            char current = input[0];

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == current)
                {
                    count++;
                }

                if (input[i] != current || i + 1 == input.Length)
                {
                    packData += count.ToString() + current + " ";
                    count = 1;
                    current = input[i];
                }
            }

            return Encoding.UTF8.GetBytes(packData);
        }

        public static byte[] Unpack(byte[] data)
        {
            string input = Encoding.UTF8.GetString(data);
            string digits = "0123456789";
            
            string unpackData = "", count = "";
            char current = '\n';

            for (int i = 0; i < input.Length; i++)
            {
                if (digits.IndexOf(input[i]) != -1)
                {
                    count += input[i];
                }

                else if (input[i] == ' ')
                {
                    for (int j = 0; j < int.Parse(count); j++)
                    {
                        unpackData += current;
                    }
                    count = "";
                }

                else
                {
                    current = input[i];
                }
            }

            return Encoding.UTF8.GetBytes(unpackData);
        }
    }
}
