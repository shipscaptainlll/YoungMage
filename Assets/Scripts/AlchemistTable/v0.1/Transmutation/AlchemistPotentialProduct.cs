using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemistPotentialProduct : MonoBehaviour, IResource, IShowClickable
{
    [SerializeField] PotentialProductAppearance potentialProductAppearance;
    [SerializeField] int _id;

    public int ID { get { return _id; } }

    public event Action<string> ObjectFound = delegate { };
    public event Action<string> ObjectUnfound = delegate { };
    public void Hide()
    {
        if (potentialProductAppearance.IsCreated || transform.GetComponent<MeshRenderer>().enabled == true)
        {
            ObjectUnfound("TransmutationTable");
            //Debug.Log("hidden " + transform);
        }
        
    }

    public void Show()
    {
        if (transform.GetComponent<MeshRenderer>().enabled == true)
        {
            
            ObjectFound("TransmutationTable");
            //Debug.Log("showed " + transform);
        }
        
    }
}
