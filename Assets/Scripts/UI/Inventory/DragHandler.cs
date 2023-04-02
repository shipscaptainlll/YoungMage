using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] Transform quickaccessElementsHolder;
    [SerializeField] Transform quickAccessPanel;
    int typeSiblingIndex;
    int rowSiblingIndex;
    int slotSiblingIndex;

    Coroutine clickDelayCoroutine;
    bool doubleClicked;

    List<RaycastResult> hitObjects = new List<RaycastResult>();
    
    public event Action QuickAccessElementFilled = delegate { };
    public void OnBeginDrag(PointerEventData eventData)
    {
        typeSiblingIndex = transform.parent.parent.parent.parent.GetSiblingIndex();
        rowSiblingIndex = transform.parent.parent.parent.GetSiblingIndex();
        slotSiblingIndex = transform.parent.parent.GetSiblingIndex();
    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.parent.position = Input.mousePosition;
        //transform.parent.Find("AmountCounter").position = Input.mousePosition;
        transform.parent.parent.SetAsLastSibling();
        transform.parent.parent.parent.SetAsLastSibling();
        transform.parent.parent.parent.parent.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.parent.localPosition = Vector3.zero;
        //transform.parent.Find("AmountCounter").position = Vector3.zero;
        transform.parent.parent.SetSiblingIndex(slotSiblingIndex);
        transform.parent.parent.parent.SetSiblingIndex(rowSiblingIndex);
        transform.parent.parent.parent.parent.SetSiblingIndex(typeSiblingIndex);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log(" is free");
        ManageDoubleClicks();
        if (Input.GetKey(KeyCode.LeftControl) || doubleClicked)
        {
            if (GetObjectUnderMouse().GetComponent<Element>().ElementType == Element.ElementTypeEnum.inventorySlot.ToString() && GetObjectUnderMouse().GetComponent<Element>().CustomID != 0)
            {
                foreach (Transform element in quickaccessElementsHolder)
                {
                    if (element.GetChild(0).Find("Element").GetComponent<Element>().CustomID == GetObjectUnderMouse().GetComponent<Element>().CustomID)
                    {
                        return;
                    }
                }

                foreach (Transform element in quickaccessElementsHolder)
                {
                    //Debug.Log(" is free2");
                    if (element.GetChild(0).Find("Element").GetComponent<Element>().CustomID == 0)
                    {
                        CopyCustomID(eventData, element.GetChild(0).Find("Element"));
                        
                        return;
                    }
                }
            }
            
        }
    }
    void CopyCustomID(PointerEventData eventData, Transform freeQuickElement)
    {
        GameObject targetObject = GetObjectUnderMouse();
        if (QuickAccessElementFilled != null && targetObject.GetComponent<Element>().CustomID == 2)
        {
            QuickAccessElementFilled();
        }
        freeQuickElement.GetComponent<Element>().CustomID = targetObject.GetComponent<Element>().CustomID;
        int slotNumber = freeQuickElement.transform.parent.parent.GetSiblingIndex();

        CopyIDToQuickAccess(eventData, slotNumber, freeQuickElement);
    }

    void CopyIDToQuickAccess(PointerEventData eventData, int slotNumber, Transform freeQuickElement)
    {
        quickAccessPanel.GetChild(slotNumber).Find("Borders").Find("Element").GetComponent<QuickAccessElement>().CustomID = freeQuickElement.GetComponent<Element>().CustomID;
    }

    GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects[0].gameObject;
    }

    void ManageDoubleClicks()
    {
        if (clickDelayCoroutine == null) { clickDelayCoroutine = StartCoroutine(delayLeftClick()); }
        else { doubleClicked = true; Debug.Log("double clicked"); }
    }

    IEnumerator delayLeftClick()
    {
        yield return new WaitForSeconds(0.25f);
        doubleClicked = false;
        clickDelayCoroutine = null;
        
    }
}
