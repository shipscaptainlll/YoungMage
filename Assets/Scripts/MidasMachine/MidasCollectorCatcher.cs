using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MidasCollectorCatcher : MonoBehaviour
{
    [SerializeField] Material dematerializeMaterial;

    public event Action<int> ResourceEnteredCollector = delegate { };
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
        if (other.GetComponent<MidasResource>() != null)
        {
            int resourceID = other.GetComponent<GlobalResource>().ID;
            if (ResourceEnteredCollector != null) { ResourceEnteredCollector(resourceID); }
            Destroy(other.gameObject);
        }
    }
}
