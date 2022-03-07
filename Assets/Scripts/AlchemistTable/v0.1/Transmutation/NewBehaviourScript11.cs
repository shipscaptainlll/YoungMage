using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewBehaviourScript11 : MonoBehaviour
{
    public int[] shortest;
    Dictionary<string, int> gekki = new Dictionary<string, int>();
    int windowID = 0;
    List<string> Windows = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        NewBehaviourScript11 wm = new NewBehaviourScript11();
        
        wm.Open("Calculator");
        wm.Open("Browser");
        Debug.Log(wm.gekki.First().Key);
        wm.Open("Player");
        wm.Close("Browser");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(string windowName)
    {
        windowID++;
        gekki.Add(windowName, windowID);
        
    }

    public void Close(string windowName)
    {
        gekki.Remove(windowName);
        windowID--;
    }

    public string GetTopWindow()
    {
        return Windows.Last();
    }

}
