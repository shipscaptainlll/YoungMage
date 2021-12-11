using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemVisibilityController : MonoBehaviour
{
    [SerializeField] Transform inventoryPanel;
    [SerializeField] GoldCoinsCounter goldCoinsCounter;

    // Start is called before the first frame update
    void Start()
    {
        goldCoinsCounter.ItemCreated += CreateVisibleItem;
        SearchEmptySlot();
    }

    void CreateVisibleItem(int customId, Transform attachedCounter)
    {
        Transform foundEmptySlot = SearchEmptySlot();
        if (foundEmptySlot != null)
        {
            Element emptySlotElementScript = foundEmptySlot.Find("Borders").GetChild(1).GetComponent<Element>();
            emptySlotElementScript.CustomID = customId;
            emptySlotElementScript.AttachedCounter = attachedCounter;
        }
    }

    private Transform SearchEmptySlot()
    {
        
        foreach (Transform row in inventoryPanel)
        {
            foreach (Transform slot in row)
            {
                int slotCustomID = slot.Find("Borders").GetChild(1).GetComponent<Element>().CustomID;
                if (slotCustomID == 0)
                {
                    return slot;
                }
            }
        }
        return null;
    }
}
