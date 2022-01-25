using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidasInventoryElement : MonoBehaviour, IBasicElement
{
    [SerializeField] int customID;
    [SerializeField] CounterManager counterManager;
    [SerializeField] DefractorGetData defractorGetData;
    [SerializeField] SpriteManager spriteManager;
    [SerializeField] Transform elementCell;
    [SerializeField] Transform attachedCounter;
    [SerializeField] TypeOfCell typeOfThisCell;
    bool buttonIsActive;

    [SerializeField] Image redBackground;
    bool isEnough;
    int minimalAmmount;

    [SerializeField] Text textBox;
    bool isVisible;
    public enum TypeOfCell { midasCell, midasAutoCell, defractorCell, defractorAutoCell };
    public string TypeOfThisCell { get { return typeOfThisCell.ToString(); } }
    public bool IsVisible
    {
        get
        {
            return isVisible;
        }
    }    

    public bool IsEnough
    {
        get { return isEnough; }
    }

    public int CustomID
    {
        get { return customID; }
        set
        {
            customID = value;
            UpdateMinimalAmmount();
            UpdateAttachedCounter();
            UpdateCounter(attachedCounter.GetComponent<ICounter>().Count);
        }
    }

    public Transform AttachedCounter
    {
        get { return attachedCounter; }
    }

    public event Action VisibilityChanged = delegate { };
    void Start()
    {
        OnStartSettings();
        UpdateMinimalAmmount();
        UpdateImage();
        RegulateCounterVisibility();
        RegulateCellVisibility();
        RegulateAvailability();
        
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

    void UpdateImage()
    {
        transform.Find("Image").GetComponent<Image>().sprite = spriteManager.TakeSprite(customID);
    }

    void OnStartSettings()
    {
        isVisible = true;
        textBox = transform.Find("Image").Find("Counter").GetComponent<Text>();
        attachedCounter.GetComponent<ICounter>().AmountChanged += UpdateCounter;
        minimalAmmount = 10;
    }

    void UpdateCounter(int count)
    {

        textBox.text = count.ToString();
        RegulateCounterVisibility();
        RegulateCellVisibility();
        RegulateAvailability();
    }

    void RegulateCounterVisibility()
    {
        if (attachedCounter.GetComponent<ICounter>().Count <= 1)
        {
            textBox.transform.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if (attachedCounter.GetComponent<ICounter>().Count > 1) 
        { textBox.transform.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    void RegulateCellVisibility()
    {
        if (isVisible && attachedCounter.GetComponent<ICounter>().Count == 0)
        {
            elementCell.GetComponent<CanvasGroup>().alpha = 0;
            isVisible = false;
            VisibilityChanged();
        } else if (!isVisible && attachedCounter.GetComponent<ICounter>().Count > 0) 
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
}
