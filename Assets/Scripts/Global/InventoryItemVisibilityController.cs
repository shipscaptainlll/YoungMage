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
    [SerializeField] MagicWandCounter magicWandCounter;
    [SerializeField] StoneHandsCounter stoneHandsCounter;
    [SerializeField] LeggingsCounter leggingsCounter;
    [SerializeField] PlateArmorCounter plateArmorCounter;
    [SerializeField] ShoesCounter shoesCounter;
    [SerializeField] HelmCounter helmCounter;
    [SerializeField] MagicGlovesCounterUpdated magicGlovesCounter;
    [SerializeField] BracersCounter bracersCounter;
    [SerializeField] StoneBrickCounter stoneBrickCounter;
    [SerializeField] MetalIngotCounter metalIngotCounter;
    [SerializeField] CursedIngotCounter cursedIngotCounter;
    [SerializeField] EarthstoneDustCounter earthstoneDustCounter;
    [SerializeField] LavastoneDustCounter lavastoneDustCounter;
    [SerializeField] MagicstoneDustCounter magicstoneDustCounter;
    [SerializeField] WaterstoneDustCounter waterstoneDustCounter;
    [SerializeField] WindstoneDustCounter windstoneDustCounter;



    // Start is called before the first frame update
    void Start()
    {
        goldCoinsCounter.ItemCreated += CreateVisibleItem;
        stoneOreCounter.ItemCreated += CreateVisibleItem;
        stoneOreCounter.AmmountEnded += DeleteVisibleItem;
        metalOreCounter.ItemCreated += CreateVisibleItem;
        cursedOreCounter.ItemCreated += CreateVisibleItem;
        earthstoneOreCounter.ItemCreated += CreateVisibleItem;
        lavastoneOreCounter.ItemCreated += CreateVisibleItem;
        magicstoneOreCounter.ItemCreated += CreateVisibleItem;
        waterstoneOreCounter.ItemCreated += CreateVisibleItem;
        windstoneOreCounter.ItemCreated += CreateVisibleItem;
        magicWandCounter.ItemCreated += CreateVisibleItem;
        stoneHandsCounter.ItemCreated += CreateVisibleItem;
        leggingsCounter.ItemCreated += CreateVisibleItem;
        plateArmorCounter.ItemCreated += CreateVisibleItem;
        shoesCounter.ItemCreated += CreateVisibleItem;
        helmCounter.ItemCreated += CreateVisibleItem;
        bracersCounter.ItemCreated += CreateVisibleItem;
        magicGlovesCounter.ItemCreated += CreateVisibleItem;
        stoneBrickCounter.ItemCreated += CreateVisibleItem;
        metalIngotCounter.ItemCreated += CreateVisibleItem;
        cursedIngotCounter.ItemCreated += CreateVisibleItem;
        earthstoneDustCounter.ItemCreated += CreateVisibleItem;
        lavastoneDustCounter.ItemCreated += CreateVisibleItem;
        magicstoneDustCounter.ItemCreated += CreateVisibleItem;
        waterstoneDustCounter.ItemCreated += CreateVisibleItem;
        windstoneDustCounter.ItemCreated += CreateVisibleItem;
        SearchEmptySlot();
        StartCoroutine(Hello());
        //goldCoinsCounter.AddResource(1);
    }


    void CreateVisibleItem(int customId, Transform attachedCounter)
    {
        if (customId == 1) { 
            return;
        }
        Transform foundEmptySlot = SearchEmptySlot();
        if (foundEmptySlot != null)
        {
            Element emptySlotElementScript = foundEmptySlot.Find("Borders").GetChild(1).GetComponent<Element>();
            emptySlotElementScript.CustomID = customId;
        }
    }

    void DeleteVisibleItem(int customId)
    {
        //Debug.Log("Starting deleting process");
        Transform foundSlot = SearchForSlot(customId);
        Element slotElementScript = foundSlot.Find("Borders").GetChild(1).GetComponent<Element>();
        slotElementScript.CustomID = 0;
    }

    Transform SearchForSlot(int customId)
    {
        foreach (Transform row in inventoryPanel)
        {
            foreach (Transform slot in row)
            {
                if (customId == slot.Find("Borders").GetChild(1).GetComponent<Element>().CustomID)
                {
                    Debug.Log("found in " + slot);
                    return slot;
                }
            }
        }
        //Debug.Log("Didnt find anything");
        return null;
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

    
    IEnumerator Hello()
    {
        yield return new WaitForSeconds(1);
        magicWandCounter.AddResource(1);
    }
}
