using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandlerMidasInventory : MonoBehaviour, IDropHandler
{
    List<RaycastResult> hitObjects = new List<RaycastResult>();

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
                TransferProperties(eventData);
            }
        }
    }

    void TransferProperties(PointerEventData eventData)
    {
        CopyCustomID(eventData);
    }

    void CopyCustomID(PointerEventData eventData)
    {
        GameObject targetObject = GetObjectUnderMouse();
        targetObject.GetComponent<IBasicElement>().CustomID = eventData.pointerDrag.transform.parent.GetComponent<MidasInventoryElement>().CustomID;
    }

    GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        

        if (hitObjects.Count <= 0) return null;

        for (int i = 0; i < hitObjects.Count; i++)
        {
            if (hitObjects[i].gameObject.transform.GetComponent<IBasicElement>() != null)
            {
                return hitObjects[i].gameObject;
            }
        }
        return null;
    }
}
