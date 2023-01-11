using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SortsCache : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        int[] arr = { 900, 32, 2, 33, 34, 23, 35, 4, 200 };

        float[] array = { 0.3f, 0.24f, 0.02f, 0.005f, 0.1f, 0.002f, 0.25f };

        
        ResetOriginalArray(ref arr);
        BubbleSort(arr, arr.Length);
        ShowArray("Bubble Sort", arr);

        ResetOriginalArray(ref arr);
        InsertionSort(arr, arr.Length);
        ShowArray("Insertion Sort", arr);

        ResetOriginalArray(ref arr);
        SelectionSort(arr, arr.Length);
        ShowArray("Selection Sort", arr);

        ResetOriginalArray(ref arr);
        MergeSort(arr, 0, arr.Length - 1);
        ShowArray("Merge Sort", arr);

        ResetOriginalArray(ref arr);
        QuickSort(arr, 0, arr.Length - 1);
        ShowArray("Quick Sort", arr);

        ResetOriginalArray(ref arr);
        HeapSort(arr, arr.Length);
        ShowArray("Heap Sort", arr);

        ResetOriginalArray(ref arr);
        BucketSort(array, array.Length);
        ShowArray("Bucket Sort", array);

        int[] arr1 = { 900, 32, 2, 33, 34, 23, 35, 4, 200 };
        ResetOriginalArray(ref arr);
        RadixSort(arr1, arr1.Length);
        ShowArray("Radix Sort", arr1);

        

        ResetOriginalArray(ref arr);
        GnomeSort(arr, arr.Length);
        ShowArray("Gnome Sort", arr);
        */
    }

    void GnomeSort(int[] arr, int n)
    {
        int index = 0;

        while (index < n)
        {
            if (index == 0)
                index++;
            if (arr[index] >= arr[index - 1])
                index++;
            else
            {
                int temp = 0;
                temp = arr[index];
                arr[index] = arr[index - 1];
                arr[index - 1] = temp;
                index--;
            }
        }
        return;
    }

    void ShellSort(int[] arr, int n)
    {
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            
            for (int i = gap; i < n; i++)
            {
                int j;
                int temp = arr[i];

                for (j = i; j >= gap && arr[j - gap] > arr[j]; j -= gap)
                {
                    arr[j] = arr[j - gap];
                }
                arr[j] = temp;
            }
        }
    }

    void TimSort(int[] arr, int n)
    {
        int RUN = 32;

        for (int i = 0; i < n; i += RUN)
        {
            TimInsertion(arr, i, Mathf.Min((i + 32 - 1), (n - 1)));
        }

        for (int size = RUN; size < n; size *= 2)
        {
            for (int i = 0; i < n; i += size * 2)
            {
                int m = i + size - 1;
                int right = Mathf.Min((i + 2 * size - 1), (n - 1));
                if (m < right)
                {
                    TimMerge(arr, i, m, right);
                }
                
            }
        }
    }

    void TimInsertion(int[] arr, int left, int right)
    {
        for (int i = left + 1; i <= right; i++)
        {
            int temp = arr[i];
            int j = i - 1;
            while (j >= left && arr[j] > temp)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = temp;
        }
    }

    void TimMerge(int[] arr, int left, int middle, int right)
    {
        int n1 = middle - left + 1;
        int n2 = right - middle;

        int[] L = new int[n1];
        int[] R = new int[n2];

        int i, j, key;

        for (i = 0; i < n1; i++)
        {
            L[i] = arr[left + i];
        }

        for (j = 0; j < n2; j++)
        {
            R[j] = arr[middle + 1 + i];
        }

        i = 0;
        j = 0;
        key = left;

        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                arr[key] = L[i];
                i++;
                key++;
            }
            else
            {
                arr[key] = R[j];
                j++;
                key++;
            }
        }

        while (i < n1)
        {
            arr[key] = L[i];
            i++;
            key++;
        }

        while (j < n2)
        {
            arr[key] = R[j];
            j++;
            key++;
        }
    }

    void CountingSort(char[] arr, int n)
    {
        int[] count = new int[256];
        char[] output = new char[n];

        for (int i = 0; i < 256; i++)
        {
            count[i] = 0;
        }

        for (int i = 0; i < n; i++)
        {
            count[arr[i]]++;
        }

        for (int i = 1; i < 256; i++)
        {
            count[i] += count[i - 1];
        }

        for (int i = n - 1; i >= 0; i--)
        {
            output[count[arr[i]] - 1] = arr[i];
            count[arr[i]]--;
        }

        for (int i = 0; i < n; i++)
        {
            arr[i] = output[i];
        }
    }

    void RadixSort(int[] arr, int n)
    {
        int max = GetMax(arr, n);

        for (int exp = 1; max / exp > 0; exp *= 10)
        {
            CountSortRadix(arr, n, exp);
        }
    }

    void CountSortRadix(int[] arr, int n, int exp)
    {
        int[] count = new int[10];
        int i;
        int[] output = new int[n];

        for (i = 0; i < 10; i++)
        {
            count[i] = 0;
        }

        for (i = 0; i < n; i++)
        {
            count[(arr[i] / exp) % 10]++;
        }

        for (i = 1; i < 10; i++)
        {
            count[i] += count[i - 1];

        }

        for (i = n - 1; i >= 0; i--)
        {
            output[count[(arr[i] / exp) % 10] - 1] = arr[i];
            count[(arr[i] / exp) % 10]--;
        }

        for (i = 0; i < n; i++)
        {
            arr[i] = output[i];
        }
    }

    int GetMax(int[] arr, int n)
    {
        int max = arr[0];

        for (int i = 1; i < n; i++)
        {
            if (arr[i] > max)
            {
                max = arr[i];
            }
        }
        return max;
    }

    void BucketSort(float[] arr, int n)
    {
        List<float>[] buckets = new List<float>[n];

        for (int i = 0; i < n; i++)
        {
            buckets[i] = new List<float>();
        }

        for (int i = 0; i < n; i++)
        {
            float idx = arr[i] * n;
            buckets[(int)idx].Add(arr[i]);
        }

        for (int i = 0; i < n; i++)
        {
            buckets[i].Sort();
        }

        int index = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < buckets[i].Count; j++)
            {
                arr[index++] = buckets[i][j];
            }
        }
    }

    void HeapSort(int[] arr, int n)
    {
        for (int i = (n / 2) - 1; i >= 0; i--)
        {
            Heapify(arr, n, i);
        }

        for (int i = n - 1; i > 0; i--)
        {
            Swap(arr, 0, i);

            Heapify(arr, i, 0);
            
        }
    }

    void Heapify(int[] arr, int N, int i)
    {
        int maximum = i;
        int l = 2 * i + 1;
        int r = 2 * i + 2;

        if (l < N && arr[l] > arr[maximum])
        {
            maximum = l;
        }

        if (r < N && arr[r] > arr[maximum])
        {
            maximum = r;
        }
        
        if (maximum != i)
        {
            Swap(arr, i, maximum);
            Heapify(arr, N, maximum);
        }
    }



    void QuickSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int partition = Partition(arr, left, right);

            if (partition > 1)
            {
                QuickSort(arr, left, partition - 1);
            }

            if (partition < right)
            {
                QuickSort(arr, partition + 1, right);
            }
        }
    }

    int Partition(int[] arr, int left, int right)
    {
        int partition = arr[left];

        while (true)
        {
            while (arr[left] < partition)
            {
                left++;
            }

            while (arr[right] > partition)
            {
                right--;
            }

            if (left < right)
            {
                if (arr[left] == arr[right]) { return right; }
                Swap(arr, left, right);
            }
            else { return right; }
        }
        
    }

    void MergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int middle = left + (right - left) / 2;

            MergeSort(arr, left, middle);
            MergeSort(arr, middle + 1, right);

            Merge(arr, left, middle, right);
        }
    }

    void Merge(int[] arr, int left, int middle, int right)
    {
        int n1 = middle - left + 1;
        int n2 = right - middle;

        int[] L = new int[n1];
        int[] R = new int[n2];

        int i = 0;
        int j = 0;
        for (i = 0; i < n1; i++)
            L[i] = arr[left + i];
        for (j = 0; j < n2; j++)
            R[j] = arr[middle + j + 1];

        i = 0;
        j = 0;
        int key = left;

        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                arr[key] = L[i];
                i++;
                key++;
            } else
            {
                arr[key] = R[j];
                j++;
                key++;
            }
        }

        while (i < n1)
        {
            arr[key] = L[i];
            i++;
            key++;
        }

        while (j < n2)
        {
            arr[key] = R[j];
            j++;
            key++;
        }
    }

    void SelectionSort(int[] arr, int n)
    {
        for (int i = 0; i < n; i++)
        {
            int min = i;
            for (int j = i + 1; j < n; j++)
            {
                if (arr[j] < arr[min])
                {
                    min = j;
                }
            }
            if (min != i)
            {
                Swap(arr, i, min);
            }
        }
    }

    void InsertionSort(int[] arr, int n) 
    {
        for (int i = 1; i < n; i++)
        {
            int max = arr[i];
            int j = i - 1;
            while (j >= 0 && arr[j] > max)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = max;
        }
    }

    void BubbleSort(int[] arr, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n - 1; j++)
            {
                if (arr[j + 1] < arr[j])
                {
                    Swap(arr, j, j + 1);
                }
            }
        }
    }



    void Swap(int[] arr, int i, int j)
    {
        int cache = arr[i];
        arr[i] = arr[j];
        arr[j] = cache;
    }

    void ResetOriginalArray(ref int[] arr)
    {
        
        arr = new int[] { 900, 32, 2, 33, 34, 23, 35, 4, 200 };
        ShowArray("Original array", arr);
    }

    void ShowArray(string name, int[] arr)
    {
        StringBuilder newString = new StringBuilder();
        newString.Append(name);
        newString.Append(": ");
        for (int i = 0; i < arr.Length; i++)
        {
            newString.Append(arr[i] + " ");
        }
        Debug.Log(newString);
    }

    void ShowArray(string name, float[] arr)
    {
        StringBuilder newString = new StringBuilder();
        newString.Append(name);
        newString.Append(": ");
        for (int i = 0; i < arr.Length; i++)
        {
            newString.Append(arr[i] + " ");
        }
        Debug.Log(newString);
    }
}
