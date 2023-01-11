using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryDataApplier
{
    static InventoryData itemsDataLoaded;

    public static void ApplyInventoryData(Transform mainInventoryHolder, Transform innerQuickaccessHolder, Transform outerQuickaccessHolder, QuickAccessHandController quickAccessHandController, InventoryData inventoryData)
    {
        UpdateData(inventoryData);
        ApplyMainInventory(mainInventoryHolder);
        ApplyQuickaccessInner(innerQuickaccessHolder);
        ApplyQuickaccessOuter(outerQuickaccessHolder);
        ApplyChoosenElement(quickAccessHandController);
        DisconnectData();
    }

    static void UpdateData(InventoryData inventoryData)
    {
        itemsDataLoaded = inventoryData;
    }

    static void DisconnectData()
    {
        itemsDataLoaded = null;
    }

    static void ApplyMainInventory(Transform countersHolder)
    {
        int indexer = 0;
        foreach (Transform row in countersHolder)
        {
            foreach (Transform slot in row)
            {
                //Debug.Log("main inventory id was " + itemsDataLoaded.mainInventoryIDs[indexer]);
                slot.Find("Borders").GetChild(1).GetComponent<Element>().CustomID = itemsDataLoaded.mainInventoryIDs[indexer++];
                
            }
        }
    }

    static void ApplyQuickaccessInner(Transform countersHolder)
    {
        int indexer = 0;
        foreach (Transform row in countersHolder)
        {
            foreach (Transform slot in row)
            {
                //Debug.Log("quickaccess inner id was " + itemsDataLoaded.innerQuickaccessIDs[indexer]);
                slot.Find("Borders").GetChild(2).GetComponent<Element>().CustomID = itemsDataLoaded.innerQuickaccessIDs[indexer++];

            }
        }
    }

    static void ApplyQuickaccessOuter(Transform countersHolder)
    {
        int indexer = 0;
        foreach (Transform row in countersHolder)
        {
            foreach (Transform slot in row)
            {
                //Debug.Log("quickaccess outer id was " + itemsDataLoaded.outerQuickaccessIDs [indexer]);
                slot.Find("Borders").GetChild(2).GetComponent<QuickAccessElement>().CustomID = itemsDataLoaded.outerQuickaccessIDs[indexer++];

            }
        }
    }

    static void ApplyChoosenElement(QuickAccessHandController quickAccessHandController)
    {
        quickAccessHandController.pickUpFromInventory(itemsDataLoaded.choosenQuickElement);
        //Debug.Log(itemsDataLoaded.choosenQuickElement + " here it was");
    }
}
