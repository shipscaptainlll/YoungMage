using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellElementMidasCauldron : MonoBehaviour, IBasicElement
{
    [SerializeField] CounterManager counterManager;
    [SerializeField] DefractorGetData defractorGetData;
    [SerializeField] int customID;
    [SerializeField] SpriteManager spriteManager;
    [SerializeField] Transform earningsCounter;
    [SerializeField] Text textBox;
    [SerializeField] TypeOfCell typeOfThisCell;
    Transform attachedCounter;

    [SerializeField] Image redBackground;
    bool isEnough;
    int minimalAmmount;
    public enum TypeOfCell { midasCell, midasAutoCell, defractorCell, defractorAutoCell };
    public event Action DefractorCellUpdated = delegate { };
    public event Action DefractorCellAmmountUpdated = delegate { };
    public string TypeOfThisCell { get { return typeOfThisCell.ToString(); } }
    public int CustomID
    {
        get
        {
            return customID;
        }

        set
        {
            customID = value;
            UpdateMinimalAmmount();
            UpdateAttachedCounter();
            UpdateElement();
            if (typeOfThisCell == TypeOfCell.defractorCell)
            {
                Debug.Log("was sent");
                DefractorCellUpdated();
            }
        }
    }

    public bool IsEnough
    {
        get { return isEnough; }
    }

    void UpdateMinimalAmmount()
    {
        if (typeOfThisCell == TypeOfCell.midasCell)
        {
            minimalAmmount = 10;
        }
        else if (typeOfThisCell == TypeOfCell.defractorCell || typeOfThisCell == TypeOfCell.defractorAutoCell)
        {
            minimalAmmount = defractorGetData.GetResourceMinimalAmmount(CustomID);
        }
    }


    void UpdateAttachedCounter()
    {
        if (attachedCounter != null)
        {
            attachedCounter.GetComponent<ICounter>().AmountChanged -= UpdateCounter;
        }
        attachedCounter = counterManager.TakeCounter(customID);
        if (attachedCounter != null)
        {
            attachedCounter.GetComponent<ICounter>().AmountChanged += UpdateCounter;
        }
    }

    public Transform AttachedCounter
    {
        get
        {
            return attachedCounter;
        }
    }

    void Start()
    {
        FirstSettings();
        UpdateImage();
    }

    void FirstSettings()
    {
        attachedCounter = null;
        isEnough = false;
        customID = 0;
        minimalAmmount = 10;
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
        transform.Find("Image").GetComponent<Image>().sprite = spriteManager.TakeSprite(customID);
    }

    void UpdateCounter(int count)
    {
        if (DefractorCellAmmountUpdated != null)
        {
            DefractorCellAmmountUpdated();
        }
        textBox.text = count.ToString();
        RegulateCounterVisibility();
        RegulateAvailability();
        UpdateFinalEarnings();
    }

    void RegulateCounterVisibility()
    {
        if (customID == 0 || attachedCounter.GetComponent<ICounter>().Count <= 1)
        {
            textBox.transform.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if (customID != 0 && attachedCounter.GetComponent<ICounter>().Count > 1) {
            textBox.transform.GetComponent<CanvasGroup>().alpha = 1; }
    }

    void UpdateFinalEarnings()
    {
        earningsCounter.GetComponent<Text>().text = GetPrice().ToString();
    }

    void RegulateAvailability()
    {
        CheckIsEnough();
        RegulateBackgroundColor();
        RegulateCounterColor();
        RegulateImageTransparency();
    }

    void CheckIsEnough()
    {
        if (attachedCounter.GetComponent<ICounter>().Count >= minimalAmmount)
        {
            isEnough = true;
        }
        else { isEnough = false; }
    }

    void RegulateBackgroundColor()
    {
        if (attachedCounter.GetComponent<ICounter>().Count >= minimalAmmount)
        {
            redBackground.GetComponent<CanvasGroup>().alpha = 0;
        }
        else { redBackground.GetComponent<CanvasGroup>().alpha = 1; }
    }

    void RegulateCounterColor()
    {
        if (attachedCounter.GetComponent<ICounter>().Count >= minimalAmmount)
        {
            textBox.color = Color.black;
        }
        else { textBox.color = Color.red; }
    }

    void RegulateImageTransparency()
    {
        if (attachedCounter.GetComponent<ICounter>().Count >= minimalAmmount)
        {
            transform.Find("Image").GetComponent<CanvasGroup>().alpha = 1;
        }
        else { transform.Find("Image").GetComponent<CanvasGroup>().alpha = 0.75f; }
    }

    public int GetPrice()
    {
        switch (customID)
        {
            case 2:
                return 1;
            case 3:
                return 2;
            case 4:
                return 5;
            case 5:
                return 10;
            case 6:
                return 11;
                case 7:
                return 10;
            case 8:
                return 12;
            case 9:
                return 11;
        }
        return 0;
    }
    public int GetPrice(int outerCustomID)
    {
        switch (outerCustomID)
        {
            case 2:
                return 1;
            case 3:
                return 2;
            case 4:
                return 5;
            case 5:
                return 10;
            case 6:
                return 11;
            case 7:
                return 10;
            case 8:
                return 12;
            case 9:
                return 11;
        }
        return 0;
    }

}
