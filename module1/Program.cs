//Створіть клас ArrayManipulator, який має методи для роботи з масивами цілих чисел:
//Метод GenerateRandomArray(int length, int min, int max),
//який створює та повертає новий масив заданої довжини з випадковими числами в діапазоні від min до max.
//Метод FindMax(int[] array), який знаходить та повертає найбільший елемент у масиві.
//Метод SortArray(int[] array), який сортує масив у зростаючому порядку.

//Після створення класу запустіть програму, яка створює масив,
//знаходить найбільший елемент та сортує масив. Виведіть початковий масив,
//знайдений максимум та відсортований масив на консоль.

using System;

namespace module1
{

    class ArrayManipulator
    {

        public int[] GenerateRandomArray(int length, int min, int max)
        {
            System.Random r = new System.Random();
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = (int)r.NextInt64(min, max);
            }
            return array;
        }

        public int FindMax(int[] array)
        {
            if (array.Length == 0) return -1;
            int max = array[0];
            foreach (var item in array)
            {
                if (max < item)
                {
                    max = item;
                }
            }
            return max;
        }

        public void SortArray(int[] array)
        {
            int size = array.Length;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size-1-i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        public void PrintArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item+" ");
            }
            Console.WriteLine();
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            ArrayManipulator arr_man = new ArrayManipulator();
            int[] arr = arr_man.GenerateRandomArray(15, -5, 10);
            Console.WriteLine("Nonsorted array:");

            arr_man.PrintArray(arr);
            Console.WriteLine($"Maximum: {arr_man.FindMax(arr)}");
            arr_man.SortArray(arr);

            Console.WriteLine("Sorted array:");
            arr_man.PrintArray(arr);


        }
    }
}