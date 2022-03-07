using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationProductStore : MonoBehaviour
{
    [SerializeField] Transform countersHolder;
    [SerializeField] PotentialProductAppearance potentialProductAppearance;
    

    // Start is called before the first frame update
    void Start()
    {
        potentialProductAppearance.ObjectProduced += TransferToCounter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TransferToCounter(int ID)
    {
        foreach (Transform counter in countersHolder)
        {
            if (counter.GetComponent<ICounter>().ID == ID)
            {
                counter.GetComponent<ICounter>().AddResource(1);
            }
        }
    }
}
