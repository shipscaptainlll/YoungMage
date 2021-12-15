using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessElement : MonoBehaviour
{
    [SerializeField] ItemsList itemsList;
    [SerializeField] int customID;
    [SerializeField] SpriteManager spriteManager;
    [SerializeField] ElementTypeEnum elementTypeEnum;
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
            UpdateElement();
        }
    }

    public Transform AttachedCounter
    {
        get
        {
            return attachedCounter;
        }
        set
        {
            if (attachedCounter != null)
            {
                attachedCounter.GetComponent<ICounter>().AmountChanged -= UpdateCounter;
            }
            attachedCounter = value;
            if (attachedCounter != null)
            {
                attachedCounter.GetComponent<ICounter>().AmountChanged += UpdateCounter;
            }
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


    void Start()
    {
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
        if (customID == 0)
        {
            textBox.transform.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if (customID != 0) { textBox.transform.GetComponent<CanvasGroup>().alpha = 1; }
    }
}
