using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefractorProductElement : MonoBehaviour
{
    [SerializeField] DefractorGetData defractorGetData;
    [SerializeField] SellElementMidasCauldron attachedResourceCell;
    [SerializeField] CounterManager counterManager;
    [SerializeField] int customID;
    [SerializeField] SpriteManager spriteManager;
    [SerializeField] Transform elementCell;
    Transform attachedCounter;
    bool buttonIsActive;

    [SerializeField] Image redBackground;
    bool isEnough;
    int minimalAmmount;

    Text textBox;
    bool isVisible;

    public bool IsVisible
    {
        get
        {
            return isVisible;
        }
    }

    public int CustomID
    {
        get { return customID; }
        set { customID = value;
            UpdateImage();
            UpdateAttachedCounter();
            UpdateCounter();
        }
    }

    void UpdateAttachedCounter()
    {
        attachedCounter = counterManager.TakeCounter(customID);
    }

    void UpdateCustomID()
    {
        CustomID = defractorGetData.GetProductID(attachedResourceCell.CustomID);
    }

    public Transform AttachedCounter
    {
        get { return attachedCounter; }
    }

    public event Action VisibilityChanged = delegate { };
    void Start()
    {
        OnStartSettings();
        UpdateImage();
        RegulateCounterVisibility();
        RegulateCellVisibility();
        RegulateAvailability();
    }

    void UpdateImage()
    {
        transform.Find("Image").GetComponent<Image>().sprite = spriteManager.TakeSprite(customID);
    }

    void OnStartSettings()
    {
        isVisible = true;
        textBox = transform.Find("Counter").GetComponent<Text>();
        attachedResourceCell.DefractorCellUpdated += UpdateCustomID;
        attachedResourceCell.DefractorCellAmmountUpdated += UpdateCounter;
        minimalAmmount = 10;
    }

    void UpdateCounter()
    {
        if (attachedCounter != null)
        {
            minimalAmmount = defractorGetData.GetResourceMinimalAmmount(attachedResourceCell.CustomID);
            int constantValue = 1;
            textBox.text = constantValue.ToString();
            RegulateCounterVisibility();
            RegulateCellVisibility();
            RegulateAvailability();
        }
    }

    void RegulateCounterVisibility()
    {
        textBox.transform.GetComponent<CanvasGroup>().alpha = 0;
    }

    void RegulateCellVisibility()
    {
        if (isVisible && attachedResourceCell.GetComponent<IBasicElement>().AttachedCounter.GetComponent<ICounter>().Count == 0)
        {
            elementCell.GetComponent<CanvasGroup>().alpha = 0;
            isVisible = false;
            VisibilityChanged();
        } else if (!isVisible && attachedResourceCell.GetComponent<IBasicElement>().AttachedCounter.GetComponent<ICounter>().Count > 0) 
        {
            elementCell.GetComponent<CanvasGroup>().alpha = 1;
            isVisible = true;
            VisibilityChanged();
        }
    }

    void RegulateAvailability()
    {
        if (CustomID != 0)
        {
            CheckIsEnough();
            RegulateBackgroundColor();
            RegulateCounterColor();
            RegulateImageTransparency();
        }
        else
        {
            SetColorsToDefault();
        }
    }

    void SetColorsToDefault()
    {
        redBackground.GetComponent<CanvasGroup>().alpha = 0;
        textBox.color = Color.black;
        transform.Find("Image").GetComponent<CanvasGroup>().alpha = 1;
    }

    void CheckIsEnough()
    {
        if (attachedResourceCell.GetComponent<IBasicElement>().AttachedCounter.GetComponent<ICounter>().Count >= minimalAmmount)
        {
            isEnough = true;
        }
        else { isEnough = false; }
    }

    void RegulateBackgroundColor()
    {
        if (attachedResourceCell.GetComponent<IBasicElement>().AttachedCounter.GetComponent<ICounter>().Count >= minimalAmmount)
        {
            redBackground.GetComponent<CanvasGroup>().alpha = 0;
        }
        else { redBackground.GetComponent<CanvasGroup>().alpha = 1; }
    }

    void RegulateCounterColor()
    {
        if (attachedResourceCell.GetComponent<IBasicElement>().AttachedCounter.GetComponent<ICounter>().Count >= minimalAmmount)
        {
            textBox.color = Color.black;
        }
        else { textBox.color = Color.red; }
    }

    void RegulateImageTransparency()
    {
        if (attachedResourceCell.GetComponent<IBasicElement>().AttachedCounter.GetComponent<ICounter>().Count >= minimalAmmount)
        {
            transform.Find("Image").GetComponent<CanvasGroup>().alpha = 1;
        }
        else { transform.Find("Image").GetComponent<CanvasGroup>().alpha = 0.75f; }
    }
}
