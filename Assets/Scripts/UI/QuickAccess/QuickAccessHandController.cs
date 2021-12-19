using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessHandController : MonoBehaviour
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] Transform quickAccessPanel;
    [SerializeField] Transform inventoryQuickAccessPanel;
    [SerializeField] Transform hand;
    [SerializeField] ObjectManager objectManager;
    GameObject objectInHand;
    int currentCustomID;
    int? currentSlot;

    public GameObject ObjectInHand
    {
        get
        {
            return objectInHand;
        }
    }

    public int CurrentCustomID
    {
        get
        {
            return currentCustomID;
        }
    }

    public int? CurrentSlot
    {
        get
        {
            return currentSlot;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        subscribeOnSlots();
        currentSlot = 1;
        objectInHand = null;
        clickManager.OneClicked += pickUpFromInventory;
        clickManager.TwoClicked += pickUpFromInventory;
        clickManager.ThreeClicked += pickUpFromInventory;
        clickManager.FourClicked += pickUpFromInventory;
        clickManager.FiveClicked += pickUpFromInventory;
        clickManager.SixClicked += pickUpFromInventory;
        clickManager.SevenClicked += pickUpFromInventory;
        clickManager.EightClicked += pickUpFromInventory;
        clickManager.NineClicked += pickUpFromInventory;
        RefreshCurrentSlot();
    }

    void pickUpFromInventory(int slotNumber)
    {
        if (objectInHand != null)
        {
            if (currentCustomID == 10)
            {
                objectInHand.GetComponent<LassoInvoker>().Unsubscribe();
                Destroy(objectInHand);
            } else { Destroy(objectInHand); }
            
        }
        int targetCustomID = quickAccessPanel.GetChild(slotNumber - 1).Find("Borders").Find("Element").GetComponent<QuickAccessElement>().CustomID;
        if (targetCustomID != 0)
        {
            GameObject objectToTake = objectManager.TakeObject(targetCustomID);
            objectInHand = Instantiate(objectToTake, hand.position, hand.rotation);
            objectInHand.transform.parent = hand;
            if (targetCustomID == 10)
            {
                objectInHand.transform.parent = hand.parent;
            }
        }
        MarkUsedSlot(slotNumber);
        SetCurrentCustomID(slotNumber);
        currentSlot = slotNumber;
        if (targetCustomID == 0)
        {
            objectInHand = null;
        }
    }

    void MarkUsedSlot(int slotNumber)
    {
        if (currentSlot != null)
        {
            UnMarkUsedSlot((int)currentSlot);
        }
        quickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color = new Color(1, 1, 1);
        inventoryQuickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color = new Color(1, 1, 1);
    }

    void UnMarkUsedSlot(int slotNumber)
    {
        quickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color = new Color(0.87f, 0.87f, 0.87f);
        inventoryQuickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color = new Color(0.87f, 0.87f, 0.87f);
    }

    void SetCurrentCustomID(int slotNumber)
    {
        currentCustomID = quickAccessPanel.GetChild(slotNumber - 1).Find("Borders").Find("Element").GetComponent<QuickAccessElement>().CustomID;
    }

    void RefreshCurrentSlot()
    {
        pickUpFromInventory((int)currentSlot);
    }

    void subscribeOnSlots()
    {
        foreach (Transform slot in quickAccessPanel)
        {
            var slotScript = slot.Find("Borders").Find("Element").GetComponent<QuickAccessElement>();
            slotScript.SlotWasUpdated += RefreshCurrentSlot;
        }
    }
}
