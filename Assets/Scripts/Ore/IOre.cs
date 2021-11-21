using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOre
{
    public string ObjectType
    {
        get;
    }
    public string Type
    {
        get;
    }
    public int Health
    {
        get;
    }
    public int Hardness
    {
        get;
    }
    public string ProductionType
    {
        get;
    }
    public int ProductionPerCycle
    {
        get;
    }
}
