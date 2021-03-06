using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorResource : MonoBehaviour
{
    [SerializeField] DestroyableObjects destroyableObjects;
    int id;
    GameObject destroyableVersion;

    public DestroyableObjects DestroyableObjects { set { destroyableObjects = value; } }
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
        if (other.GetComponent<RotatingCones>())
        {
            InstantiateDestroyableVersion();
            if (objectContactedDefractor != null) { objectContactedDefractor(transform); }
            
        }
    }

    void InstantiateDestroyableVersion()
    {
        destroyableVersion = Instantiate(destroyableObjects.TakeObject(id), transform.position, transform.rotation);
        destroyableVersion.AddComponent<DestroyableObject>();
    }

    
}
