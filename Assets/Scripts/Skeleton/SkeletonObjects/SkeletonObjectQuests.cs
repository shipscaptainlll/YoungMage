using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkeletonObjectQuests : MonoBehaviour
{
    static int firstConnectedCount;
    static int secondConnectedCount;
    static int thirdConnectedCount;
    static int fourthConnectedCount;
    static int fifthConnectedCount;
    static int sixthConnectedCount;

    public static int FirstConnectedCount { get { return firstConnectedCount; } }
    public static int SecondConnectedCount { get { return secondConnectedCount; } }
    public static int ThirdConnectedCount { get { return thirdConnectedCount; } }
    public static int FourthConnectedCount { get { return fourthConnectedCount; } }
    public static int FifthConnectedCount { get { return fifthConnectedCount; } }
    public static int SixthConnectedCount { get { return sixthConnectedCount; } }

    public static event Action<int> FirstObjectConnected = delegate { };
    public static event Action<int> SecondObjectConnected = delegate { };
    public static event Action<int> ThirdObjectConnected = delegate { };
    public static event Action<int> FourthObjectConnected = delegate { };
    public static event Action<int> FifthObjectConnected = delegate { };
    public static event Action<int> SixthObjectConnected = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void CountFirstConnected()
    {
        firstConnectedCount++;
        if (FirstObjectConnected != null) { FirstObjectConnected(firstConnectedCount); }
    }

    public static void CountSecondConnected()
    {
        secondConnectedCount++;
        if (SecondObjectConnected != null) { SecondObjectConnected(secondConnectedCount); }
    }

    public static void CountThirdConnected()
    {
        thirdConnectedCount++;
        if (ThirdObjectConnected != null) { ThirdObjectConnected(thirdConnectedCount); }
    }

    public static void CountFourthConnected()
    {
        fourthConnectedCount++;
        if (FourthObjectConnected != null) { FourthObjectConnected(fourthConnectedCount); }
    }

    public static void CountFifthConnected()
    {
        fifthConnectedCount++;
        if (FifthObjectConnected != null) { FifthObjectConnected(fifthConnectedCount); }
    }

    public static void CountSixthConnected()
    {
        sixthConnectedCount++;
        if (SixthObjectConnected != null) { SixthObjectConnected(sixthConnectedCount); }
    }
}
