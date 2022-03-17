using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessElement : MonoBehaviour
{
    [SerializeField] CounterManager counterManager;
    [SerializeField] int customID;
    [SerializeField] SpriteManager spriteManager;
    [SerializeField] ElementTypeEnum elementTypeEnum;
    [SerializeField] QuickAccessHandController quickAccessHandController;
    int slotNumber;
    Transform attachedCounter;
    Text textBox;

    public int CustomID
    {
        get
        {
            return customID;
        }

        set
        {
            customID = value;
            UpdateAttachedCounter();
            UpdateElement();
            if (SlotWasUpdated != null && quickAccessHandController.CurrentSlot == slotNumber)
            {
                SlotWasUpdated();
            }
        }
    }

    void UpdateAttachedCounter()
    {
        if (attachedCounter != null)
        {
            attachedCounter.GetComponent<ICounter>().AmountChanged -= UpdateCounter;
            attachedCounter.GetComponent<ICounter>().AmmountEnded -= ResetCounter;
        }
        attachedCounter = counterManager.TakeCounter(customID);
        if (attachedCounter != null)
        {
            attachedCounter.GetComponent<ICounter>().AmountChanged += UpdateCounter;
            attachedCounter.GetComponent<ICounter>().AmmountEnded += ResetCounter;
        }
    }

    void ResetCounter(int ID)
    {
        if (CustomID == ID)
        {
            CustomID = 0;
        }
    }

    public Transform AttachedCounter
    {
        get
        {
            return attachedCounter;
        }
    }

    public enum ElementTypeEnum { inventorySlot, quickAccessSlot };

    public string ElementType
    {
        get
        {
            return elementTypeEnum.ToString();
        }
    }

    public event Action SlotWasUpdated = delegate { };
    void Start()
    {
        arrangeSlotNumber();
        attachedCounter = null;
        textBox = transform.parent.Find("AmountCounter").GetComponent<Text>();

        UpdateImage();
    }

    void UpdateElement()
    {
        UpdateImage();
        if (attachedCounter != null)
        {
            UpdateCounter(attachedCounter.GetComponent<ICounter>().Count);
        }
        RegulateCounterVisibility();

    }

    void UpdateImage()
    {
        transform.GetComponent<Image>().sprite = spriteManager.TakeSprite(customID);
    }

    void UpdateCounter(int count)
    {
        textBox.text = count.ToString();
    }

    void RegulateCounterVisibility()
    {
        if (customID == 0 || attachedCounter.GetComponent<ICounter>().Count <= 1)
        {
            textBox.transform.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if (customID != 0 && attachedCounter.GetComponent<ICounter>().Count > 1) { textBox.transform.GetComponent<CanvasGroup>().alpha = 1; }
    }

    void arrangeSlotNumber()
    {
        foreach (Transform slot in transform.parent.parent.parent)
        {
            var foundSlot = slot.Find("Borders").Find("Element");
            if (foundSlot == transform)
            {
                slotNumber = foundSlot.parent.parent.GetSiblingIndex() + 1;
                
                break;
            }
        }
    }
}
