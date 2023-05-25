using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] Transform quickaccessElementsHolder;
    [SerializeField] private Transform m_transmutationSlotElementsHolder;
    [SerializeField] Transform quickAccessPanel;
    [SerializeField] private bool isTransmutationSlot;
    [SerializeField] private bool isTransmutationInventorySlot;
    [SerializeField] private Transform m_slotsHolder;
    [SerializeField] private Transform m_inventoryHolder;
    [SerializeField] private SoundManager m_soundManager;
    int typeSiblingIndex;
    int rowSiblingIndex;
    int slotSiblingIndex;
    AudioSource changeElementSound;

    Coroutine clickDelayCoroutine;
    bool doubleClicked;

    List<RaycastResult> hitObjects = new List<RaycastResult>();
    
    public event Action QuickAccessElementFilled = delegate { };
    public event Action<int> TransmutationSlotElementFilled = delegate { };

    void Start()
    {
        if (isTransmutationSlot || isTransmutationInventorySlot)
        {
            changeElementSound = m_soundManager.FindSound("NewObjectAppearingUI");
        }
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        typeSiblingIndex = transform.parent.parent.parent.parent.GetSiblingIndex();
        rowSiblingIndex = transform.parent.parent.parent.GetSiblingIndex();
        slotSiblingIndex = transform.parent.parent.GetSiblingIndex();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isTransmutationSlot)
        {
            //m_slotsHolder.SetAsLastSibling();
        }
        transform.parent.position = Input.mousePosition;
        //transform.parent.Find("AmountCounter").position = Input.mousePosition;
        transform.parent.parent.SetAsLastSibling();
        transform.parent.parent.parent.SetAsLastSibling();
        transform.parent.parent.parent.parent.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isTransmutationSlot)
        {
            //m_slotsHolder.SetAsFirstSibling();
        }
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
            
            if (GetObjectUnderMouse().GetComponent<Element>().ElementType == Element.ElementTypeEnum.transmutationInventorySlot.ToString() && GetObjectUnderMouse().GetComponent<Element>().CustomID != 0)
            {

                //Debug.Log(" is free22");
                foreach (Transform element in m_transmutationSlotElementsHolder)
                {
                    //Debug.Log(" is free2");
                    if (element.Find("Borders").Find("Element").GetComponent<Element>().CustomID == 0)
                    {
                        GameObject targetObject = GetObjectUnderMouse();
                        if (targetObject.GetComponent<Element>().AttachedCounter != null
                            && targetObject.GetComponent<Element>().AttachedCounter.GetComponent<ICounter>().Count <= 0)
                        {
                            return;
                        }
                        else
                        {
                            
                        }
                        
                        CopyCustomID(eventData, element.GetChild(0).Find("Element"));
                        if (changeElementSound != null)
                        {
                            changeElementSound.Play();
                        }
                        
                        targetObject.GetComponent<Element>().AttachedCounter.GetComponent<ICounter>().AddResource(-1);
                        
                        return;
                    }
                }
            }
            
            if (GetObjectUnderMouse().GetComponent<Element>().ElementType == Element.ElementTypeEnum.transmutationSlotSlot.ToString() && GetObjectUnderMouse().GetComponent<Element>().CustomID != 0)
            {
                GameObject transmutationSlot = GetObjectUnderMouse();
                transmutationSlot.GetComponent<Element>().AttachedCounter.GetComponent<ICounter>().AddResource(1);
                transmutationSlot.GetComponent<Element>().CustomID = 0;
                //TransmutationSlotElementFilled?.Invoke(transmutationSlot.GetComponent<Element>().TransmutationSlotID);
            }
            
        }
    }
    void CopyCustomID(PointerEventData eventData, Transform freeQuickElement)
    {
        GameObject targetObject = GetObjectUnderMouse();
        
        freeQuickElement.GetComponent<Element>().CustomID = targetObject.GetComponent<Element>().CustomID;
        //TransmutationSlotElementFilled?.Invoke(freeQuickElement.GetComponent<Element>().TransmutationSlotID);
        int slotNumber = freeQuickElement.transform.parent.parent.GetSiblingIndex();

        if (targetObject.GetComponent<Element>().ElementType == Element.ElementTypeEnum.inventorySlot.ToString())
        {
            CopyIDToQuickAccess(eventData, slotNumber, freeQuickElement);
        }
        
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
