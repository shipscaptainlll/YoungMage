using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalOre : MonoBehaviour, IOre
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
            return "MetalOre";
        }
    }

    public int Health
    {
        get
        {
            return 200;
        }
    }

    public int Hardness
    {
        get
        {
            return 2;
        }
    }

    public string ProductionType
    {
        get
        {
            return "MetalOrePieces";
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
