using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoObjectsCatcher : MonoBehaviour
{
    [SerializeField] SacketResourceCatcher sacketResourceCatcher;

    [SerializeField] Transform countersHolder;
    [SerializeField] SacketMagnetism sacketMagnetism;
    [SerializeField] Transform catchParticles;
    [SerializeField] ItemsCounterQuests itemsCounterQuests;
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
        //Debug.Log("Catched some " + other.transform);
        if (other.GetComponent<GlobalResource>() != null)
        {
            if (!other.GetComponent<GlobalResource>().WasCollected)
            {
                Debug.Log("Catched new");
                itemsCounterQuests.countQuestItem(other.GetComponent<GlobalResource>().ID);
            }
            CatchResource(other.transform);
        }
    }


    void CatchResource(Transform resource)
    {
        AddToCounter(resource.GetComponent<GlobalResource>().ID);
        //InstantiateCatchParticles(resource);
        Destroy(resource.gameObject);
    }

    void AddToCounter(int customerID)
    {
        foreach (Transform holder in countersHolder)
        {
            foreach (Transform counter in holder)
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
