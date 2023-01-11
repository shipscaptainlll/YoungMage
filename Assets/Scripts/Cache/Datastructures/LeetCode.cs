using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.WSA;

public class LeetCode : MonoBehaviour
{
    void Start()
    {
        List<int> hello = new List<int>();
        hello.Add(300);
        hello.Add(100);
        hello.Remove(300);
        foreach (var element in hello)
        {
            Debug.Log(element);
        }
    }

    public IList<bool> PrefixesDivBy5(int[] nums)
    {

        if (nums.Length == 0)
        {
            return null;
        }

        List<bool> hello = new List<bool>();

        for (int i = 0; i < nums.Length; i++)
        {
            int cacheInt = 0;
            for (int j = 0; j < i + 1; j++)
            {
                cacheInt += nums[j] * (int)Math.Pow(2, i - j);
            }

            if (cacheInt % 5 == 0)
            {
                Debug.Log(true);
                hello.Add(true);
            }
            else
            {
                Debug.Log(false);
                hello.Add(false);
            }
        }


        return hello;
    }

    public bool DigitCount(string num)
    {

        if (num.Length == 0)
        {
            return false;
        }

        Dictionary<int, int> hello = new Dictionary<int, int>();
        Dictionary<int, int> there = new Dictionary<int, int>();

        for (int i = 0; i < num.Length; i++)
        {
            if (!hello.ContainsKey(num[i] - '0'))
            {
                hello.Add(num[i] - '0', 1);
            }
            else
            {
                hello[num[i] - '0']++;
            }
        }

        for (int i = 0; i < num.Length; i++)
        {
            if (num[i] - '0' != 0)
            {
                there.Add(i, num[i] - '0');
            }
            
        }

        if (there.Count != hello.Count)
        {
            return false;
        }

        foreach (var element in hello)
        {
            int cache = 0;
            there.TryGetValue(element.Key, out cache);

            if (!there.ContainsKey(element.Key)
            || there.ContainsKey(element.Key) && cache != element.Value)
            {
                return false;
            }
        }

        return true;

    }

    public string ReverseStr(string s, int k)
    {


        int counter = 0;
        bool ifthere = true;
        StringBuilder hello = new StringBuilder();

        for (int i = 0; i < s.Length; i++)
        {
            Debug.Log(i);
            Debug.Log(hello.ToString());
            if (ifthere && i + k <= s.Length)
            {

                hello.Append(ReverseIt(s.Substring(i, 2)));
                i += k - 1;
                ifthere = false;
            }
            else
            {
                hello.Append(s[i]);
                counter++;
                if (counter == k)
                {
                    ifthere = true;
                    counter = 0;
                }
            }
        }
        Debug.Log(hello.ToString());
        return hello.ToString();
    }

    string ReverseIt(string hello)
    {
        StringBuilder there = new StringBuilder();

        for (int i = hello.Length - 1; i >= 0; i--)
        {
            there.Append(hello[i]);
        }

        return there.ToString();
    }
    public bool CanWinNim(int n)
    {
        var dp = new bool[n + 1];
        dp[0] = false;
        dp[1] = true;
        dp[2] = true;
        dp[3] = true;

        for (var i = 4; i <= n; i++)
        {
            dp[i] = !(dp[i - 1] && dp[i - 2] && dp[i - 3]);
        }

        //Console.WriteLine(string.Join(",", dp));
        //Debug.Log(dp[n]);
        return dp[n];
    }

    public int CountWords(string[] words1, string[] words2)
    {

        Dictionary<string, int> hello = new Dictionary<string, int>();
        Dictionary<string, int> there = new Dictionary<string, int>();


        foreach (var element in words2)
        {
            if (!there.ContainsKey(element))
            {
                there.Add(element, 1);
            }
            else
            {
                there[element]++;
            }
        }

        foreach (var element in words1)
        {
            if (!hello.ContainsKey(element))
            {
                hello.Add(element, 1);
            }
            else
            {
                hello[element]++;
            }
        }

        HashSet<string> wow = new HashSet<string>();
        HashSet<string> weAre = new HashSet<string>();

        foreach (var element in hello)
        {
            if (element.Value == 1)
            {
                wow.Add(element.Key);
                Debug.Log(element.Key);
            }
        }

        foreach (var element in there)
        {
            if (element.Value == 1)
            {
                weAre.Add(element.Key);
                Debug.Log(element.Key);
            }
        }

        int sum = 0;

        foreach (var element in wow)
        {
            if (weAre.Contains(element))
            {
                sum++;
            }
        }

        return sum;

    }

    public bool WordPattern(string pattern, string s)
    {

        Dictionary<char, string> hello = new Dictionary<char, string>();
        Dictionary<string, string> there = new Dictionary<string, string>();

        List<string> oneTwoThree = new List<string>();

        StringBuilder cache = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {

            if (s[i] == ' ' && cache != null)
            {
                oneTwoThree.Add(cache.ToString());
                cache.Clear();
            }
            else
            {
                cache.Append(s[i]);
            }
        }

        if (cache != null)
        {
            oneTwoThree.Add(cache.ToString());
            cache.Clear();
        }

        if (oneTwoThree.Count != pattern.Length)
        {
            return false;
        }

        for (int i = 0; i < pattern.Length; i++)
        {
            if (!hello.ContainsKey(pattern[i]))
            {
                hello.Add(pattern[i], i.ToString());
            }
            else
            {
                hello[pattern[i]] += i.ToString();
            }

            if (!there.ContainsKey(oneTwoThree[i]))
            {
                there.Add(oneTwoThree[i], i.ToString());
            }
            else
            {
                there[oneTwoThree[i]] += i.ToString();
            }
        }

        StringBuilder throughIt = new StringBuilder();
        StringBuilder throughIs = new StringBuilder();

        foreach (var element in hello)
        {
            throughIt.Append(element.Value);
        }

        foreach (var element in there)
        {
            throughIs.Append(element.Value);
        }

        Debug.Log(throughIt);
        Debug.Log(throughIs);

        if (throughIt.ToString() == throughIs.ToString())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<int> GetRow(int rowIndex)
    {
        List<int> hello = new List<int>();
        if (rowIndex == 0)
        {
            hello.Add(1);
            return hello;
        }
        else if (rowIndex == 1)
        {
            hello.Add(1);
            hello.Add(1);
            return hello;
        }

        List<int> newThere = GetRow(rowIndex - 1);

        for (int i = 0; i < rowIndex + 1; i++)
        {
            if (i == 0 || i == rowIndex)
            {
                hello.Add(1);
            }
            else
            {
                hello.Add(newThere[i - 1] + newThere[i]);
            }
        }

        return hello;
    }

    public void DigitSum(string s, int k)
    {
        if (s.Length == 3)
        {
            //Debug.Log(s);
        }

        Dictionary<int, string> first = new Dictionary<int, string>();
        HashSet<string> second = new HashSet<string>();

        while (true)
        {
            int number = 0;
            for (int i = 0; i < s.Length; i += k)
            {
                StringBuilder helloThere = new StringBuilder();

                for (int j = i; j - i < k; j++)
                {
                    if (j < s.Length)
                    {
                        helloThere.Append(s[j].ToString());
                    }

                }

                first.Add(number, helloThere.ToString());
                number++;
            }


            StringBuilder consec = new StringBuilder();
            int iterationn = 0;
            foreach (var element in first)
            {
                int cache = 0;
                iterationn++;
                for (int i = 0; i < element.Value.Length; i++)
                {
                    cache += element.Value[i] - '0';

                }
                consec.Append(cache);
            }

            if (consec.ToString().Length <= k)
            {
                //return consec.ToString();
                Debug.Log(consec.ToString());
            }
            else
            {
                s = consec.ToString();
                Debug.Log(consec.ToString());
                first = new Dictionary<int, string>();
            }
        }




    }

    public IList<IList<string>> GroupAnagrams(string[] strs)
    {


        List<IList<string>> results = new List<IList<string>>();
        if (strs.Length == 0) { return results; }
        if (strs.Length == 1)
        {
            List<string> subResult = new List<string>();
            subResult.Add(strs[0]);
            results.Add(subResult);
            return results;
        }
        for (int i = 0; i < strs.Length; i++)
        {
            if (strs[i] == "-1")
            {
                continue;
            }
            List<string> subResult = new List<string>();
            for (int j = i + 1; j < strs.Length; j++)
            {
                if (strs[j] == "-1")
                {
                    continue;
                }
                else if (isAnagram(strs[i], strs[j]))
                {
                    subResult.Add(strs[j]);
                    strs[j] = "-1";
                }
            }
            subResult.Add(strs[i]);
            results.Add(subResult);

        }

        return results;
    }

    public bool isAnagram(string origin, string compared)
    {
        if (origin.Length != compared.Length)
        {
            return false;
        }

        string hello = origin;
        string there = compared;
        for (int i = 0; i < hello.Length; i++)
        {


            for (int j = 0; j < there.Length; j++)
            {
                if (hello[i] == there[j])
                {
                    //if (there.Length == 1) { return true; }
                    Debug.Log(hello[i] + " is equal to " + there[j]);
                    Debug.Log(there);
                    there = there.Remove(j, 1);
                    j--;
                    Debug.Log(there);
                    
                    break;
                }
                if (j == there.Length - 1) { return false; }
            }
        }
        Debug.Log(origin + " is equal to " + compared);
        return true;
    }

    public int Compress(char[] chars)
    {
        char cache = chars[0];
        int enumeration = 1;
        Dictionary<char, int> result = new Dictionary<char, int>();

        

        for (int i = 1; i < chars.Length; i++)
        {
            if (cache == chars[i])
            {
                enumeration++;
            }
            else if (cache != chars[i])
            {
                result.Add(cache, enumeration);
                cache = chars[i];
                enumeration = 1;
            }
        }

        if (cache != ' ' && enumeration > 0)
        {
            Debug.Log(cache + " " + enumeration);
            result.Add(cache, enumeration);
        }

        StringBuilder helloThere = new StringBuilder();

        foreach (var element in result)
        {
            helloThere.Append(element.Key);
            string valuabl = element.Value.ToString();
            for (int i = 0; i < valuabl.Length; i++)
            {
                helloThere.Append(valuabl[i]);
            }
        }

        Debug.Log(helloThere);

        return helloThere.ToString().Length;
    }



    public void IsPalindrome(ListNode head)
    {
        int iterations = 1;
        ListNode transp = head;

        while (transp.next != null)
        {
            iterations++;
            transp = transp.next;
        }

        int middle = (int)(iterations / 2);
        ListNode middleNode = head;
        ListNode prev = middleNode;
        ListNode next = middleNode;
        for (int i = 0; i < iterations - 1; i++)
        {
            prev = middleNode;
            
            middleNode = next;
            next = next.next;
            middleNode.next = prev;
        }
        next.next = middleNode;
        head.next = null;
        for (int i = 0; i < 7; i++)
        {
            Debug.Log(next.val);
            next = next.next;
        }
    }

    int GetValueFrom(ListNode there, int here)
    {

        ListNode hellooo = there;
        int resultValue = hellooo.val;
        for (int i = 0; i < here; i++)
        {
            hellooo = hellooo.next;
            resultValue = hellooo.val;
        }
        return resultValue;
    }

}
 public class ListNode
{
      public int val;
      public ListNode next;
      public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
             }
  }
