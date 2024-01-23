namespace Utilities.Utilities
{
    public class ArrayHandler
    {
        private readonly int[] array;

        public ArrayHandler(int[] array)
        {
            this.array = array;
        }

        public bool ContainsElement(int element)
        {
            return Array.IndexOf(array, element) != -1;
        }

        public int[] SortArray()
        {
            int[] sortedArray = new int[array.Length];
            Array.Copy(array, sortedArray, array.Length);
            Array.Sort(sortedArray);
            return sortedArray;
        }
    }
}