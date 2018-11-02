using System;
using System.Threading;

namespace lab1
{
    class Program
    {
        private static Random rand = new Random();

        static void Main(string[] args)
        {
            Console.ReadKey();
            int N = 0;
            while (true)
            {
                while (N == 0)
                {
                    try
                    {
                        Console.Write("N = ");
                        N = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                try { 
                    decimal[,] A = CreateRandomMatrix(N);
                    decimal[,] B = CreateRandomMatrix(N);

                    decimal[,] C = MatrixMultiplication(A, B);

                    Console.WriteLine("Matrix Sum = " + MatrixSum(C));

                    Thread.Sleep(10);
                    if (Decimal.ToDouble(C[0, 0]) < 0.5)
                    {
                        break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    N = 0;
                }
            }

            Console.WriteLine("Done!");
            Console.Read();
        }

        private static decimal[,] CreateRandomMatrix(int N)
        {
            decimal[,] matrix = new decimal[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = rand.Next(0, 2);
                }
            }
            return matrix;
        }

        private static decimal[,] MatrixMultiplication(decimal[,] A, decimal[,] B)
        {
            decimal[,] C = new decimal[A.GetLength(0), A.GetLength(0)];

            for (int i = 0; i < C.GetLength(0); i++)
            {
                for (int j = 0; j < C.GetLength(0); j++)
                {
                    C[i, j] = 0;
                    for (int r = 0; r < C.GetLength(0); r++)
                    {
                        C[i, j] += A[i, r] * B[r, j];
                    }
                }
            }

            return C;
        }

        private static decimal MatrixSum(decimal[,] matrix)
        {
            decimal matrixSum = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    matrixSum += matrix[i, j];
                }
            }

            return matrixSum;
        }
    }
}
