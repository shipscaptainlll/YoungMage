using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopycatCatcher : MonoBehaviour
{
    public event Action<Transform> CopycatCached = delegate { };
    public event Action<Transform> SkeletonTeleported = delegate { };
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
                Debug.Log("Catched " + transform);
                CopycatCached(other.transform.GetComponent<Copycat>().ConnectedInstance);
                SkeletonTeleported(other.transform);
            }

        }
        
        //Debug.Log(other);
    }
}
