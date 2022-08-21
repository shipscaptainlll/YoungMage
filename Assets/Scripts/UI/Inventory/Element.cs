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
    Transform attachedCounter;
    Text textBox;

    Coroutine resizeCoroutine;
    bool coroutineActive;

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
            //Debug.Log("chages in " + transform + " with id " + customID);
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
        UpdateAttachedCounter();
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

    IEnumerator ResizeChangeCount(float duration, float multiplier)
    {
        coroutineActive = true;
        float elapsed = 0;
        RectTransform elementRect = transform.parent.parent.GetComponent<RectTransform>();
        Vector2 elementSize = transform.parent.GetComponent<RectTransform>().sizeDelta;
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
