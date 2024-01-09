using System;

class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размер массива:");
            int size;
            while (!int.TryParse(Console.ReadLine(), out size) || size <= 0)
            {
                Console.WriteLine("Please enter a valid positive integer for the size of the array:");
            }

            int[] array = new int[size];
            Random random = new Random();

            Console.WriteLine("Массив:");
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(-10, 11); 
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();

            int positiveCount = CountPositiveElements(array);
            Console.WriteLine($"Количество положительных элементов массива: {positiveCount}");

            int sumAfterLastZero = SumAfterLastZero(array);
            Console.WriteLine($"Сумма элементов массива, расположенных после последнего элемента, равного нулю: {sumAfterLastZero}");

            TransformArray(array);
            Console.WriteLine("Изменённый массив:");
            foreach (var element in array)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();
        }

        static int CountPositiveElements(int[] arr)
        {
            int count = 0;
            foreach (int num in arr)
            {
                if (num > 0)
                {
                    count++;
                }
            }
            return count;
        }

        static int SumAfterLastZero(int[] arr)
        {
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 0)
                {
                    sum = 0;
                }
                else
                {
                    sum += arr[i];
                }
            }
            return sum;
        }

        static void TransformArray(int[] arr)
        {
            Array.Sort(arr, (x, y) =>
            {
                if (x < 0 && y < 0)
                {
                    return 0;
                }
                else if (x < 0)
                {
                    return -1;
                }
                else if (y < 0)
                {
                    return 1;
                }
                else if (Math.Abs(x) <= 1 && Math.Abs(y) <= 1)
                {
                    return 0;
                }
                else if (Math.Abs(x) <= 1)
                {
                    return -1;
                }
                else if (Math.Abs(y) <= 1)
                {
                return 1;
                }
                else
                {
                    return 0;
                }
            });
        }
    }