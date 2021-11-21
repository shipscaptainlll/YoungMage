using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedOre : MonoBehaviour, IOre
{
    public string ObjectType
    {
        get
        {
            return "Ore";
        }
    }

    public string Type
    {
        get
        {
            return "CursedOre";
        }
    }

    public int Health
    {
        get
        {
            return 250;
        }
    }

    public int Hardness
    {
        get
        {
            return 3;
        }
    }

    public string ProductionType
    {
        get
        {
            return "CursedOrePieces";
        }
    }

    public int ProductionPerCycle
    {
        get
        {
            return 3;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowParameters();
    }

    void ShowParameters()
    {
        Debug.Log("Object type is: " + ObjectType);
        Debug.Log("Type is: " + Type);
        Debug.Log("Max health is: " + Health);
        Debug.Log("Hardness is: " + Hardness);
        Debug.Log("Production type is: " + ProductionType);
        Debug.Log("Production per cycle is: " + ProductionPerCycle);
    }
}
