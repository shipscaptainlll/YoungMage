using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemVisibilityController : MonoBehaviour
{
    [SerializeField] Transform inventoryPanel;
    [SerializeField] GoldCoinsCounter goldCoinsCounter;
    [SerializeField] StoneOreCounter stoneOreCounter;
    [SerializeField] MetalOreCounter metalOreCounter;
    [SerializeField] CursedOreCounter cursedOreCounter;
    [SerializeField] EarthstoneOreCounter earthstoneOreCounter;
    [SerializeField] LavastoneOreCounter lavastoneOreCounter;
    [SerializeField] MagicstoneOreCounter magicstoneOreCounter;
    [SerializeField] WaterstoneOreCounter waterstoneOreCounter;
    [SerializeField] WindstoneOreCounter windstoneOreCounter;

    // Start is called before the first frame update
    void Start()
    {
        goldCoinsCounter.ItemCreated += CreateVisibleItem;
        stoneOreCounter.ItemCreated += CreateVisibleItem;
        metalOreCounter.ItemCreated += CreateVisibleItem;
        cursedOreCounter.ItemCreated += CreateVisibleItem;
        earthstoneOreCounter.ItemCreated += CreateVisibleItem;
        lavastoneOreCounter.ItemCreated += CreateVisibleItem;
        magicstoneOreCounter.ItemCreated += CreateVisibleItem;
        waterstoneOreCounter.ItemCreated += CreateVisibleItem;
        windstoneOreCounter.ItemCreated += CreateVisibleItem;
        SearchEmptySlot();
    }

    void CreateVisibleItem(int customId, Transform attachedCounter)
    {
        Transform foundEmptySlot = SearchEmptySlot();
        if (foundEmptySlot != null)
        {
            Element emptySlotElementScript = foundEmptySlot.Find("Borders").GetChild(1).GetComponent<Element>();
            emptySlotElementScript.AttachedCounter = attachedCounter;
            emptySlotElementScript.CustomID = customId;
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
