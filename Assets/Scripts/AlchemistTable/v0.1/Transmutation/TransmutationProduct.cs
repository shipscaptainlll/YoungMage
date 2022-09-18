using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationProduct : MonoBehaviour, IResource
{
    [SerializeField] int _id;

    public int ID { get { return _id; } }
    public event Action EnteredPortal = delegate { };
    bool isTeleported = false;
    private void OnTriggerEnter(Collider other)
    {
        
        if (!isTeleported && other.gameObject.layer == 6)
        {
            isTeleported = true;
            EnteredPortal();
        }
    }

}
