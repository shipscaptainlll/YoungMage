using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessHandController : MonoBehaviour
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] Transform sacketModel;
    [SerializeField] Transform magicwandModel;
    [SerializeField] Animator sacketEnter;
    [SerializeField] Transform quickAccessPanel;
    [SerializeField] Transform inventoryQuickAccessPanel;
    [SerializeField] Transform hand;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] Transform countersHolder;
    Transform currentCounter;
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

    public Transform CurrentCounter { get { return currentCounter; } }
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
        if (targetCustomID != 0 && targetCustomID != 10)
        {
            magicwandModel.gameObject.SetActive(false);
            sacketModel.gameObject.SetActive(true);
            GameObject objectToTake = objectManager.TakeObject(targetCustomID);
            objectInHand = Instantiate(objectToTake, hand.position, hand.rotation);
            SetObjectMovement(objectInHand);
            sacketEnter.Play("GetSacketAnimation");
            objectInHand.transform.parent = hand;
            if (targetCustomID == 10)
            {
                objectInHand.transform.parent = hand.parent;
            }
        }
        else if (targetCustomID == 10)
        {
            magicwandModel.gameObject.SetActive(true);
            sacketModel.gameObject.SetActive(false);
        }
        MarkUsedSlot(slotNumber);
        SetCurrentCustomID(slotNumber);
        currentSlot = slotNumber;
        if (targetCustomID == 0)
        {
            objectInHand = null;
        }
        FindCurrentCounter();
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
        Color quickAccessColor = quickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color;
        quickAccessColor = new Color(0.6792453f, 0.4657796f, 0.2210751f);
        quickAccessColor.a = 0.7294118f;
        quickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color = quickAccessColor;
        inventoryQuickAccessPanel.GetChild(slotNumber - 1).GetComponent<Image>().color = quickAccessColor;
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

    void SetObjectMovement(GameObject targetObject)
    {
        targetObject.AddComponent<Rotate>();
        Rotate rotator = targetObject.GetComponent<Rotate>(); rotator.RotationSpeed = 150; rotator.XAxis = 1; rotator.YAxis = 1; rotator.ZAxis = -1;
        rotator.StartRotation();
    }

    public void MakeObjectInvisible()
    {
        if (objectInHand != null)
        {
            objectInHand.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void MakeObjectVisible()
    {
        if (objectInHand != null)
        {
            objectInHand.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void FindCurrentCounter()
    {
        foreach (Transform element in countersHolder)
        {
            if (element.GetComponent<ICounter>().ID == currentCustomID)
            {
                currentCounter = element;
            }
        }
    }
}
