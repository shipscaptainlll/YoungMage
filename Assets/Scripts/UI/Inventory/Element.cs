using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    [SerializeField] CounterManager counterManager;
    [SerializeField] int customID;
    [SerializeField] SpriteManager spriteManager;
    [SerializeField] ElementTypeEnum elementTypeEnum;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] private int m_transmutationSlotID;
    [SerializeField] private TransmutationElementsManager m_transmutationElementsManager;
    Transform attachedCounter;
    Text textBox;
    Vector2 elementSize;

    Coroutine showElementCoroutine;
    Coroutine resizeCoroutine;
    bool coroutineActive;
    
    public event Action<int, int, int> TransmutationSlotElementFilled = delegate { };
    public int CustomID
    {
        get
        {
            return customID;
        }

        set
        {
            if (elementTypeEnum == ElementTypeEnum.transmutationSlotSlot)
            {
                m_transmutationElementsManager.RemoveActivatedObjectID(customID);
                m_transmutationElementsManager.AddActivatedObjectID(value);
                Debug.Log("Added value " + value);
            }

            int prevCustomID = customID;
            
            
            customID = value;
            StopAllCoroutines();
            if (elementTypeEnum != ElementTypeEnum.transmutationSlotSlot)
            {
                UpdateAttachedCounter();
            }
            else
            {
                attachedCounter = counterManager.TakeCounter(customID);
            }
            
            UpdateElement();
            if (elementTypeEnum == ElementTypeEnum.transmutationSlotSlot)
            {
                TransmutationSlotElementFilled?.Invoke(prevCustomID, value, m_transmutationSlotID);
            }
            
            if (!coroutineActive)
            {
                showElementCoroutine = StartCoroutine(ShowElement(0.65f));
            }
            else
            {
                StopCoroutine(resizeCoroutine);
                showElementCoroutine = StartCoroutine(ShowElement(0.65f));
            }

            
            //Debug.Log("chages in " + transform + " with id " + customID);
        }
    }
    
    public int TransmutationSlotID { get {return m_transmutationSlotID;} }
    
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
        Debug.Log("counter was reseted");
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

    public enum ElementTypeEnum { inventorySlot, quickAccessSlot, transmutationSlotSlot, transmutationInventorySlot };

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
        UpdateAttachedCounter();
        UpdateImage();
    }

    

    void UpdateElement()
    {
        UpdateImage();
        if (attachedCounter != null && elementTypeEnum != ElementTypeEnum.transmutationSlotSlot)
        {
            UpdateCounter(attachedCounter.GetComponent<ICounter>().Count);
            RegulateCounterVisibility();
        } else if (elementTypeEnum == ElementTypeEnum.transmutationInventorySlot)
        {
            if (attachedCounter != null)
            {
                UpdateCounter(attachedCounter.GetComponent<ICounter>().Count);
            }
            if (customID == 0)
            {
                textBox.transform.GetComponent<CanvasGroup>().alpha = 0;
            }
            else if (customID != 0) {
                textBox.transform.GetComponent<CanvasGroup>().alpha = 1; }
        }
        
        
        
    }

    IEnumerator ShowElement(float duration)
    {
        float elapsed = 0;
        RectTransform elementRect = transform.GetComponent<RectTransform>();
        float finalSize = elementSize.x;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        Vector2 startElementSize = transform.GetComponent<RectTransform>().sizeDelta;
        float currentSize = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentSize = Mathf.Lerp(startElementSize.x, finalSize, animationCurve.Evaluate(elapsed / duration));
            elementRect.sizeDelta = new Vector2(currentSize, currentSize);
            yield return null;
        }
        //Debug.Log(finalSize);
        elementRect.sizeDelta = new Vector2(finalSize, finalSize);

        showElementCoroutine = null;
        yield return null;
    }

    IEnumerator ResizeChangeCount(float duration, float multiplier)
    {
        coroutineActive = true;
        float elapsed = 0;
        RectTransform elementRect = transform.GetComponent<RectTransform>();
        //Vector2 elementSize = elementSize;
        float currentSize = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentSize = Mathf.Lerp(elementSize.x, elementSize.x * multiplier, elapsed / duration);
            elementRect.sizeDelta = new Vector2(currentSize, currentSize);
            yield return null;
        }
        elementRect.sizeDelta = new Vector2(currentSize * multiplier, currentSize * multiplier);

        elapsed = 0;

        while (elapsed < duration/1.5f)
        {
            elapsed += Time.deltaTime;
            currentSize = Mathf.Lerp(elementSize.x * multiplier, elementSize.x, elapsed / duration);
            elementRect.sizeDelta = new Vector2(currentSize, currentSize);
            yield return null;
        }

        elementRect.sizeDelta = new Vector2(currentSize, currentSize);
        coroutineActive = false;
        yield return null;
    }

    void UpdateImage()
    {
        transform.GetComponent<Image>().sprite = spriteManager.TakeSprite(customID);
        RectTransform elementRect = transform.GetComponent<RectTransform>();
        //Debug.Log("true1");
        if (elementTypeEnum == ElementTypeEnum.transmutationSlotSlot)
        {
            elementSize = new Vector2(25, 25);
        }
        else if (elementTypeEnum == ElementTypeEnum.transmutationInventorySlot)
        {
            elementSize = new Vector2(36, 36);
        }
        else
        {
            elementSize = new Vector2(38, 38);
        }
        
    }

    void UpdateCounter(int count)
    {
        textBox.text = count.ToString();
        RegulateCounterVisibility();

        if (!coroutineActive)
        {
            resizeCoroutine = StartCoroutine(ResizeChangeCount(0.25f, 1.05f));
        }
        else
        {
            StopCoroutine(resizeCoroutine);
            resizeCoroutine = StartCoroutine(ResizeChangeCount(0.25f, 1.05f));
        }
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
}
