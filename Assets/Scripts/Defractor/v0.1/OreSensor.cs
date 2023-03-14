using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreSensor : MonoBehaviour
{
    public event Action<int> OreContacted = delegate { };
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
        if (other.GetComponent<DefractorResource>() != null)
        {
            Debug.Log("contacted " + other.transform);
            if (OreContacted != null) { OreContacted(other.GetComponent<DefractorResource>().ID); }
        }
    }
}
