using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterstoneOre : MonoBehaviour, IOre
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
            return "WaterstoneOre";
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
            return "WaterstoneOreDust";
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
        
    }
}
