using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    List<RaycastResult> hitObjects = new List<RaycastResult>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform inventoryPanel = transform as RectTransform;

        if (RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, Input.mousePosition))
        {
            SwapProperties(eventData);
        }
        
    }

    void SwapProperties(PointerEventData eventData)
    {
        GameObject targetObject = GetObjectUnderMouse();

        int cacheCustomId = targetObject.GetComponent<Element>().CustomID;
        targetObject.GetComponent<Element>().CustomID = eventData.pointerDrag.transform.GetComponent<Element>().CustomID;
        eventData.pointerDrag.transform.GetComponent<Element>().CustomID = cacheCustomId;
    }

    GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects[1].gameObject;
    }
}
