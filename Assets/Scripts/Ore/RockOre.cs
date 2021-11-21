using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockOre : MonoBehaviour, IOre
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
            return "RockOre";
        }
    }

    public int Health
    {
        get
        {
            return 100;
        }
    }

    public int Hardness
    {
        get
        {
            return 1;
        }
    }

    public string ProductionType
    {
        get
        {
            return "RockOrePieces";
        }
    }

    public int ProductionPerCycle
    {
        get
        {
            return 5;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
