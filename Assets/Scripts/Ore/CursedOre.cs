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
        
    }
}
