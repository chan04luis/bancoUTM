using System;
using System.Collections.Generic;

public class Quicksorter<T> where T : IComparable<T>
{
    public void Sort(List<T> list, bool ascending = true)
    {
        if (list == null || list.Count == 0)
            return;

        Quicksort(list, 0, list.Count - 1, ascending);
    }

    private void Quicksort(List<T> list, int left, int right, bool ascending)
    {
        if (left < right)
        {
            int pivotIndex = Partition(list, left, right, ascending);
            Quicksort(list, left, pivotIndex - 1, ascending);
            Quicksort(list, pivotIndex + 1, right, ascending);
        }
    }

    private int Partition(List<T> list, int left, int right, bool ascending)
    {
        T pivot = list[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            int compareResult = ascending ? list[j].CompareTo(pivot) : pivot.CompareTo(list[j]);
            if (compareResult >= 0)
            {
                i++;
                Swap(list, i, j);
            }
        }

        Swap(list, i + 1, right);
        return i + 1;
    }

    private void Swap(List<T> list, int i, int j)
    {
        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}
