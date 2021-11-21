using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthstoneOre : MonoBehaviour, IOre
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
            return "EarthstoneOre";
        }
    }

    public int Health
    {
        get
        {
            return 500;
        }
    }

    public int Hardness
    {
        get
        {
            return 4;
        }
    }

    public string ProductionType
    {
        get
        {
            return "EarthstoneOreDust";
        }
    }

    public int ProductionPerCycle
    {
        get
        {
            return 10;
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
