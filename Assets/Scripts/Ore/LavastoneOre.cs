using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavastoneOre : MonoBehaviour, IOre
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
            return "LavastoneOre";
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
            return "LavastoneOreDust";
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
