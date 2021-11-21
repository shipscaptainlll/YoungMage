using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, ISkeleton
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
            return "Normal";
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

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
