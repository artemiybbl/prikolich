using System;

namespace MatrixOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер квадратной матрицы:");
            int size;
            while (!int.TryParse(Console.ReadLine(), out size) || size <= 0)
            {
                Console.WriteLine("Please enter a valid positive integer for the size of the matrix:");
            }

            double[,] matrix = new double[size, size];
            Random random = new Random();

            Console.WriteLine("Матрица:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = (random.NextDouble() * 20) - 10; 
                    Console.Write($"{matrix[i, j]:F2}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Rearrange(matrix);
            Console.WriteLine("Изменённая матрица:");
            PrintMatrix(matrix);

            int rowNumber = FindFirst(matrix);
            if (rowNumber != -1)
            {
                Console.WriteLine($"Первая строка без положительных элементов: {rowNumber}");
            }
            else
            {
                Console.WriteLine("Строки без положительных элементов отсутствуют.");
            }

            
        }

        static void Rearrange(double[,] matrix)
        {
            int size = matrix.GetLength(0);
            var positions = new Tuple<int, int>[size]; // массив для хранения позиций (строка, столбец)

            var flattened = matrix.Cast<double>() // Преобразование в одномерный массив
                        .Select((value, index) => new { Value = value, Index = index })
                        .OrderByDescending(item => item.Value)
                        .Take(size)
                        .ToList();

            // Получение позиций первых N наибольших элементов
            for (int i = 0; i < size; i++)
            {
                int row = flattened[i].Index / matrix.GetLength(1); // строка
                int col = flattened[i].Index % matrix.GetLength(1); // столбец
                positions[i] = new Tuple<int, int>(row, col);
            }

            for (int i = 0; i < size; i++)
            {
                int row = positions[i].Item1;
                int col = positions[i].Item2;

                double temp = matrix[i, i];
                matrix[i, i] = matrix[row, col];
                matrix[row, col] = temp;
            }
        }

        static int FindFirst(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                bool hasPositive = false;
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        hasPositive = true;
                        break;
                    }
                }
                if (!hasPositive)
                {
                    return i + 1; 
                }
            }
            return -1; 
        }

        static void PrintMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i, j]:F2}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
