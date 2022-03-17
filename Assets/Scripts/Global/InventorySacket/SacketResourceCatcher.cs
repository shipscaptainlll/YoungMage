using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacketResourceCatcher : MonoBehaviour
{
    [SerializeField] Transform countersHolder;
    [SerializeField] SacketMagnetism sacketMagnetism;
    [SerializeField] Transform catchParticles;

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
        if (sacketMagnetism.FieldActivated && other.gameObject.GetComponent<GlobalResource>() != null)
        {
            CatchResource(other.transform);
        }
    }

    void CatchResource(Transform resource)
    {
        AddToCounter(resource.GetComponent<GlobalResource>().ID);
        InstantiateCatchParticles(resource);
        Destroy(resource.gameObject);
    }

    void AddToCounter(int customerID)
    {
        foreach(Transform holder in countersHolder)
        {
            foreach(Transform counter in holder)
            {
                if (counter.GetComponent<ICounter>().ID == customerID)
                {
                    counter.GetComponent<ICounter>().AddResource(1);
                    return;
                }
            }
        }
    }
    void InstantiateCatchParticles(Transform resource)
    {
        Instantiate(catchParticles, transform.position, transform.rotation);
    }
}
