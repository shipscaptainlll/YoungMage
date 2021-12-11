using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    int rowSiblingIndex;
    int slotSiblingIndex;

    public void OnBeginDrag(PointerEventData eventData)
    {
        rowSiblingIndex = transform.parent.parent.parent.GetSiblingIndex();
        slotSiblingIndex = transform.parent.parent.GetSiblingIndex();
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = Input.mousePosition;
        transform.parent.parent.SetAsLastSibling();
        transform.parent.parent.parent.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        transform.parent.parent.SetSiblingIndex(slotSiblingIndex);
        transform.parent.parent.parent.SetSiblingIndex(rowSiblingIndex);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    
}
