using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coding.Tests
{
    /// <summary>
    /// Tests to sorted algorithm.
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/1/2016 12:48:10 PM</Date>
    [TestClass]
    public class SortedAlgorithmTests
    {
        /// <summary>
        /// Constant used to array generation.
        /// </summary>
        private const int ArraySize = 10;

        /// <summary>
        /// Perfoms the array generation.
        /// </summary>
        /// <param name="arraySize">the array size to be generated.</param>
        /// <returns>array generated.</returns>
        private int[] GenerateRandomArray(int arraySize)
        {
            Random random = new Random();
            var array = new int[arraySize];

            Parallel.For(0, arraySize, (index) =>
            {
                array[index] = random.Next(10);
            });

            return array;
        }

        /// <summary>
        /// Sorted algorithm - bubble sort.
        /// </summary>
        [TestMethod]
        public void BubbleSortTest()
        {
            int minValue = 0;
            var array = GenerateRandomArray(ArraySize);

            System.Diagnostics.Debug.WriteLine("Unsorted array: " + string.Join(", ", array));

            for (int x = 0; x < array.Length; x++)
            {
                for (int y = x + 1; y < array.Length; y++)
                {
                    if (array[x] > array[y])
                    {
                        minValue = array[x];
                        array[x] = array[y];
                        array[y] = minValue;
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine("Sorted array: " + string.Join(", ", array));
        }
    }
}
