using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingDatabase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] searchedArray = { 90, 50, 2, 34, 534, 3, 4, 5, 6 };
        LinearySearch(searchedArray, 5);
        int[] searchedArray1 = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 14, 15, 20, 22, 38, 39, 40, 52 };
        LinearySearch(searchedArray1, 38);
        BinarySearch(searchedArray1, 38);
        JumpSearch(searchedArray1, 38);
    }

    
    bool LinearySearch(int[] arr, int x)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == x) { Debug.Log("Found");  return true; }
        }
        return false;
    }

    bool BinarySearch(int[] arr, int x)
    {
        int l = 0;
        int r = arr.Length - 1;

        while (l <= r)
        {
            int m = l + (r - l) / 2;

            if (arr[m] == x) { Debug.Log("Found"); return true; }

            if (arr[m] < x)
                l = m + 1;

            if (arr[m] > x)
                l = m - 1;

        }
        return false;
    }

    bool JumpSearch(int[] arr, int x)
    {
        int n = arr.Length;
        int step = (int)Mathf.Sqrt(n);
        int prev = 0;

        while (arr[Mathf.Min(step, n) - 1] < x)
        {
            prev = step;
            step += (int)Mathf.Sqrt(n);
            if (step >= n)
            {
                return false;
            }
        }

        while (arr[prev] < x)
        {
            prev++;
            if (prev == Mathf.Min((step), n))
            {
                return false;
            }
        }

        if (arr[prev] == x)
        {
            Debug.Log("Found");
            return true;
        } else
        {
            return false;
        }
    }

    bool InterpolationSearch(int[] arr, int lo, int hi, int x)
    {
        int pos;

        if (lo <= hi && x >= arr[lo] && x <= arr[hi])
        {
            pos = lo + (((hi - lo) / (arr[hi] - arr[lo])) * (x - arr[lo]));

            if (arr[pos] == x)
                return true;

            if (arr[pos] < x)
            {
                InterpolationSearch(arr, pos + 1, hi, x);
            }

            if (arr[pos] > x)
            {
                InterpolationSearch(arr, lo, pos - 1, x);
            }
        }

        return false;
    }
}
