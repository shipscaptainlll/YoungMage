using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    int typeSiblingIndex;
    int rowSiblingIndex;
    int slotSiblingIndex;

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
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    
}
