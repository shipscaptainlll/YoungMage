using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopycatCatcher : MonoBehaviour
{
    public event Action CopycatCached = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CopycatManager>() != null)
        {
            
            if (CopycatCached != null)
            {
                CopycatCached();
            }
        }
        
        //Debug.Log(other);
    }
}
