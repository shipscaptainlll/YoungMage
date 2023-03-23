using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidasResourcesCosts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCost(int customID)
    {
        switch (customID)
        {
            case 0:
                return 0;
            case 1:
                return 1;
            case 2:
                return 1;
            case 3:
                return 2;
            case 4:
                return 3;
            case 5:
                return 4;
            case 6:
                return 4;
            case 7:
                return 4;
            case 8:
                return 4;
            case 9:
                return 4;
            case 10:
                return 100000;
            case 11:
                return 24;
            case 12:
                return 64;
            case 13:
                return 80;
            case 14:
                return 48;
            case 15:
                return 32;
            case 16:
                return 168;
            case 17:
                return 48;
            case 18:
                return 2;
            case 19:
                return 4;
            case 20:
                return 6;
            case 21:
                return 8;
            case 22:
                return 8;
            case 23:
                return 8;
            case 24:
                return 8;
            case 25:
                return 8;
            case 27:
                return 1;
        }
        return 0;
    }
}
