using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandlerMidasCaouldron : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] Transform scrollRectElement;
    [SerializeField] Transform cacheObject;
    int slotSiblingIndex;
    int panelSiblingIndex;

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        cacheObject.GetComponent<CanvasGroup>().alpha = 1.0f;
        cacheObject.GetComponent<Image>().sprite = transform.GetComponent<Image>().sprite;
        cacheObject.SetAsLastSibling();
        scrollRectElement.GetComponent<ScrollRect>().scrollSensitivity = 0f;
        //slotSiblingIndex = transform.parent.GetSiblingIndex();
        panelSiblingIndex = transform.parent.parent.parent.parent.GetSiblingIndex();
    }

    public void OnDrag(PointerEventData eventData)
    {
        cacheObject.Find("Counter").GetComponent<Text>().text = transform.Find("Counter").GetComponent<Text>().text;
        cacheObject.position = Input.mousePosition;
        //transform.position = Input.mousePosition;
        
        transform.parent.parent.parent.parent.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRectElement.GetComponent<ScrollRect>().scrollSensitivity = 7.5f;
        cacheObject.GetComponent<CanvasGroup>().alpha = 0f;
        cacheObject.SetAsFirstSibling();
        //transform.localPosition = Vector3.zero;
        //transform.parent.SetSiblingIndex(slotSiblingIndex);
        transform.parent.parent.parent.parent.SetSiblingIndex(slotSiblingIndex);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        cacheObject.SetAsFirstSibling();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    
}
