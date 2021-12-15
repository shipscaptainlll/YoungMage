using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    List<RaycastResult> hitObjects = new List<RaycastResult>();
    public event Action QuitAccessChanged = delegate { };
    [SerializeField] Transform quickAccessPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform inventoryPanel = transform as RectTransform;

        if (RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, Input.mousePosition))
        {
            if (GetObjectUnderMouse() != eventData.pointerDrag.transform)
            {
                if (CheckElementType() == "inventorySlot")
                {
                    SwapProperties(eventData);
                } else if (CheckElementType() == "quickAccessSlot")
                {
                    TransferProperties(eventData);
                }
                
            }
        }
    }

    void SwapProperties(PointerEventData eventData)
    {
        SwapCounter(eventData);
        SwapCustomID(eventData);
    }

    void TransferProperties(PointerEventData eventData)
    {
        if (CheckForRepeating(eventData) != null)
        {
            Transform copyToErase = CheckForRepeating(eventData);
            EraseCopy(copyToErase);
        }
        CopyCounter(eventData);
        CopyCustomID(eventData);
    }

    void SwapCustomID(PointerEventData eventData)
    {
        GameObject targetObject = GetObjectUnderMouse();

        int cacheCustomId = targetObject.GetComponent<Element>().CustomID;
        targetObject.GetComponent<Element>().CustomID = eventData.pointerDrag.transform.GetComponent<Element>().CustomID;
        eventData.pointerDrag.transform.GetComponent<Element>().CustomID = cacheCustomId;
    }

    void SwapCounter(PointerEventData eventData)
    {
        GameObject targetObject = GetObjectUnderMouse();
        targetObject.GetComponent<Element>().AttachedCounter = eventData.pointerDrag.transform.GetComponent<Element>().AttachedCounter;
    }

    void CopyCustomID(PointerEventData eventData)
    {
        GameObject targetObject = GetObjectUnderMouse();
        targetObject.GetComponent<Element>().CustomID = eventData.pointerDrag.transform.GetComponent<Element>().CustomID;
        int slotNumber = targetObject.transform.parent.parent.GetSiblingIndex();

        CopyIDToQuickAccess(eventData, slotNumber);
    }

    void CopyIDToQuickAccess(PointerEventData eventData, int slotNumber)
    {
        quickAccessPanel.GetChild(slotNumber).Find("Borders").Find("Element").GetComponent<QuickAccessElement>().CustomID = eventData.pointerDrag.transform.GetComponent<Element>().CustomID;
    }

    void CopyCounter(PointerEventData eventData)
    {
        GameObject targetObject = GetObjectUnderMouse();
        targetObject.GetComponent<Element>().AttachedCounter = eventData.pointerDrag.transform.GetComponent<Element>().AttachedCounter;
        int slotNumber = targetObject.transform.parent.parent.GetSiblingIndex();
        CopyCounterToQuickAccess(eventData, slotNumber);
    }

    void CopyCounterToQuickAccess(PointerEventData eventData, int slotNumber)
    {
        quickAccessPanel.GetChild(slotNumber).Find("Borders").Find("Element").GetComponent<QuickAccessElement>().AttachedCounter = eventData.pointerDrag.transform.GetComponent<Element>().AttachedCounter;
    }

    Transform CheckForRepeating(PointerEventData eventData)
    {
        Transform quickAccessPanel = transform.Find("QuickAccess");
        foreach (Transform slot in quickAccessPanel.GetChild(0))
        {
            if (slot.GetChild(0).Find("Element").GetComponent<Element>().CustomID == eventData.pointerDrag.transform.GetComponent<Element>().CustomID)
            {
                return slot;
                
            } 
        }
        return null;
    }

    void EraseCopy(Transform slotToErase)
    {
        slotToErase.GetChild(0).Find("Element").GetComponent<Element>().CustomID = 0;
        slotToErase.GetChild(0).Find("Element").GetComponent<Element>().AttachedCounter = null;

        int numberOfSlot = slotToErase.GetSiblingIndex();
        
        EraseQuickAccessCopy(numberOfSlot);
        
    }

    void EraseQuickAccessCopy(int numberOfSlot)
    {
        quickAccessPanel.GetChild(numberOfSlot).Find("Borders").Find("Element").GetComponent<QuickAccessElement>().CustomID = 0;
        quickAccessPanel.GetChild(numberOfSlot).Find("Borders").Find("Element").GetComponent<QuickAccessElement>().AttachedCounter = null;
    }

    GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects[2].gameObject;
    }

    string CheckElementType()
    {
        GameObject objectUnderMouse = GetObjectUnderMouse();

        string objectType = objectUnderMouse.GetComponent<Element>().ElementType;

        return objectType;
    }
}
