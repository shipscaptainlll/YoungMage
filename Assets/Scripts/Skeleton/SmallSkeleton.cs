using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSkeleton : MonoBehaviour, ISkeleton
{
    public string ObjectType
    {
        get
        {
            return "Skeleton";
        }
    }

    public string Type
    {
        get
        {
            return "SmallSkeleton";
        }
    }

    public int Power
    {
        get
        {
            return 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
