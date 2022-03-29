using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesPopup : MonoBehaviour
{
    [SerializeField] Transform oreCountersHolder;
    [SerializeField] Transform otherCountersHolder;
    [SerializeField] Transform itemsCountersHolder;
    [SerializeField] Transform materialsCountersHolder;
    [SerializeField] ResourcesPopupInstantiator resourcesPopupInstantiator;
    [SerializeField] ResourcesPopupUpdater resourcesPopupUpdater;
    [SerializeField] ResourcesPopupDatabase resourcesPopupDatabase;

    // Start is called before the first frame update
    void Start()
    {
        SubscribeOnCounters(oreCountersHolder);
        SubscribeOnCounters(otherCountersHolder);
        SubscribeOnCounters(itemsCountersHolder);
        SubscribeOnCounters(materialsCountersHolder);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SubscribeOnCounters(Transform counter)
    {
        foreach (Transform element in counter)
        {
            element.GetComponent<ICounter>().AddedAmmount += NotifyPlayerUI;
        }
    }

    void NotifyPlayerUI(int ID, int ammountAdded)
    {
        if (!resourcesPopupDatabase.CheckIfInside(ID))
        {
            //Debug.Log("1");
            resourcesPopupInstantiator.InstantiatePopupBlock(ID, ammountAdded);
        } else { 
            resourcesPopupUpdater.UpdatePopupBlock(ID, ammountAdded); }
        
        
    }
}
