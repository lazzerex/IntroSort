using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            // Mảng ví dụ
            int[] arr = { 5, 2, 8, 1, 9, 3, 7, 4, 6, 10, 15, 12, 11, 14, 13, 20, 18, 16, 19, 17, 22, 25, 21, 23, 24, 26, 27, 28, 29, 30 };

            // Sao chép mảng gốc
            int[] original = new int[arr.Length];
            Array.Copy(arr, original, arr.Length);

            // Sắp xếp mảng bằng IntroSort
            int maxDepth = (int)Math.Log(arr.Length, 2) * 2;
            IntroSort(arr, 0, arr.Length - 1, maxDepth);

            // In ra mảng gốc, mảng đã sắp xếp và chỉ số
            Console.WriteLine("Mảng gốc\tMảng đã sắp xếp");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"{original[i]}\t{arr[i]}");
            }
        }

        public static void IntroSort(int[] arr, int left, int right, int maxDepth)
        {
            if (arr.Length <= 1)
                return;

            int n = arr.Length;
            if (n <= 16)
            {
                InsertionSort(arr, 0, n - 1);
            }
            else if (maxDepth == 0)
            {
                HeapSort(arr, 0, n - 1);
            }
            else
            {
                int pivotIndex = Partition(arr, 0, n - 1);
                IntroSort(arr, 0, pivotIndex - 1, maxDepth - 1);
                IntroSort(arr, pivotIndex + 1, n - 1, maxDepth - 1);
            }
        }
        private static int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }

            Swap(arr, i + 1, right);
            return i + 1;
        }

        private static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        private static void InsertionSort(int[] arr, int left, int right)
        {
            for (int i = left + 1; i <= right; i++)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= left && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = key;
            }
        }

        private static void HeapSort(int[] arr, int left, int right)
        {
            int n = right - left + 1;
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(arr, n, i, left);

            for (int i = n - 1; i > 0; i--)
            {
                Swap(arr, left, left + i);
                Heapify(arr, i, 0, left);
            }
        }

        private static void Heapify(int[] arr, int n, int i, int left)
        {
            int largest = i;
            int l = 2 * i + 1 + left;
            int r = 2 * i + 2 + left;

            if (l < n + left && arr[l] > arr[largest])
                largest = l;

            if (r < n + left && arr[r] > arr[largest])
                largest = r;

            if (largest != i)
            {
                Swap(arr, i + left, largest);
                Heapify(arr, n, largest - left, left);
            }
        }
    }
}
