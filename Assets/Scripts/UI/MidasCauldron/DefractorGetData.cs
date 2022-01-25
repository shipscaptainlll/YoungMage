using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorGetData : MonoBehaviour
{

    void Start()
    {
        
    }

    public int GetProductID(int targetCustomID)
    {
        switch (targetCustomID)
        {
            case 2:
                return 18;
            case 3:
                return 19;
            case 4:
                return 20;
            case 5:
                return 21;
            case 6:
                return 22;
            case 7:
                return 23;
            case 8:
                return 24;
            case 9:
                return 25;
        }
        return 0;
    }

    public int GetResourceMinimalAmmount(int targetCustomID)
    {
        switch (targetCustomID)
        {
            case 2:
                return 10;
            case 3:
                return 10;
            case 4:
                return 10;
            case 5:
                return 1;
            case 6:
                return 100;
            case 7:
                return 100;
            case 8:
                return 100;
            case 9:
                return 100;
        }
        return 0;
    }

    public int GetProductValue(int targetCustomID)
    {
        switch (targetCustomID)
        {
            case 18:
                return 3;
            case 19:
                return 2;
            case 20:
                return 1;
            case 21:
                return 1;
            case 22:
                return 1;
            case 23:
                return 1;
            case 24:
                return 1;
            case 25:
                return 1;
        }
        return 0;
    }
}
