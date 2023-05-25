using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public int[] mainInventoryIDs;
    public int[] innerQuickaccessIDs;
    public int[] outerQuickaccessIDs;
    public int choosenQuickElement;

    public InventoryData(Transform mainInventoryHolder, Transform innerQuickaccessHolder, Transform outerQuickaccessHolder, QuickAccessHandController quickAccessHandController)
    {
        GetMainInventory(mainInventoryHolder);
        GetQuickaccessInner(innerQuickaccessHolder);
        GetQuickaccessOuter(outerQuickaccessHolder);
        GetChoosenElement(quickAccessHandController);
    }

    void GetMainInventory(Transform countersHolder)
    {
        List<int> cache = new List<int>();
        foreach (Transform row in countersHolder)
        {
            foreach (Transform slot in row)
            {
                int slotCustomID = slot.Find("Borders").GetChild(1).GetComponent<Element>().CustomID;
                cache.Add(slotCustomID);
                //Debug.Log("main inventory id " + slotCustomID);
            }
        }
        mainInventoryIDs = new int[cache.Count];
        mainInventoryIDs = cache.ToArray();
    }

    void GetQuickaccessInner(Transform countersHolder)
    {
        List<int> cache = new List<int>();
        foreach (Transform row in countersHolder)
        {
            foreach (Transform slot in row)
            {
                int slotCustomID = slot.Find("Borders").GetChild(2).GetComponent<Element>().CustomID;
                cache.Add(slotCustomID);
                //Debug.Log("inner quickaccess id " + slotCustomID);
            }
        }
        innerQuickaccessIDs = new int[cache.Count];
        innerQuickaccessIDs = cache.ToArray();
    }

    void GetQuickaccessOuter(Transform countersHolder)
    {
        List<int> cache = new List<int>();
        foreach (Transform row in countersHolder)
        {
            foreach (Transform slot in row)
            {
                int slotCustomID = slot.Find("Borders").Find("Element").GetComponent<QuickAccessElement>().CustomID;
                cache.Add(slotCustomID);
                //Debug.Log("outer qucikaccess id " + slotCustomID);
            }
        }
        outerQuickaccessIDs = new int[cache.Count];
        outerQuickaccessIDs = cache.ToArray();
    }

    void GetChoosenElement (QuickAccessHandController quickAccessHandController)
    {
        choosenQuickElement = (int) quickAccessHandController.CurrentSlot;
        //Debug.Log("current " + choosenQuickElement);
    }
}
