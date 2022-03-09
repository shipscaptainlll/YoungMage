using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorResource : MonoBehaviour
{
    int id;

    public int ID { get { return id; } set { id = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public event Action<Transform> objectContactedDefractor = delegate { };
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            if (objectContactedDefractor != null) { objectContactedDefractor(transform); }
        }
    }
}
