using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkeleton
{
    public string ObjectType
    {
        get;
    }

    public string Type
    {
        get;
    }

    public int Power
    {
        get;
    }
}
