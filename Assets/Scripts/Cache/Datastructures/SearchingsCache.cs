using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingsCache : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool LinearSearch(int[] arr, int x)
    {
        int iterations = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            iterations++;
            if (arr[i] == x) {
                Debug.Log("Iterations taken: " + iterations);
                Debug.Log("Linear search: found");
                return true; }
        }
        Debug.Log("Linear search: non-found");
        return false;
    }

    bool BinarySearch(int[] arr, int x)
    {
        int iterations = 0;
        int l = 0;
        int r = arr.Length - 1;

        while (l <= r)
        {
            iterations++;
            int m = l + (r - l) / 2;

            if (arr[m] == x)
            {
                Debug.Log("Iterations taken: " + iterations);
                Debug.Log("Binary search: found");
                return true;
            }

            if (arr[m] < x)
            {
                l = m + 1;
            } else
            {
                r = m - 1;
            }
        }
        Debug.Log("Binary search: non-found");
        return false;
    }

    bool JumpSearch(int[] arr, int x)
    {
        int n = arr.Length;
        int iterations = 0;
        int step = (int)Mathf.Sqrt(n);

        int prev = 0;

        while (arr[Mathf.Min(step, n) - 1] < x)
        {
            iterations++;
            prev = step;
            step += (int)Mathf.Sqrt(n);
            if (prev >= n)
            {
                Debug.Log("Jump search: non-found");
                return false;
            }
        }

        while (arr[prev] < x)
        {
            iterations++;
            prev++;

            if (prev == Mathf.Min(step, n))
            {
                Debug.Log("Jump search: non-found");
                return false;
            }
        }

        if (arr[prev] == x)
        {
            Debug.Log("Iterations taken: " + iterations);
            Debug.Log("Jump search: found");
            return true;
        }

        Debug.Log("Jump search: non-found");
        return false;
    }
}
