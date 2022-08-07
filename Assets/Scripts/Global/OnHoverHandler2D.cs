using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverHandler2D : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    GameObject currentObject;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    public event Action<Transform> InventoryElementFound = delegate { };
    public event Action InventoryElementExited = delegate { };
    public event Action<Transform> SettingsElementFound = delegate { };
    public event Action SettingsElementExited = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentObject == null)
        {
            currentObject = DetectObject();
            if (currentObject.GetComponent<Element>() != null) { 
                if (currentObject.GetComponent<Image>().sprite.name != "Nothing" && InventoryElementFound != null) { 
                    //Debug.Log(currentObject.GetComponent<Image>().sprite); 
                    InventoryElementFound(transform); } }
            if (currentObject.GetComponent<SettingsElement>() != null)
            {
                SettingsElementFound(transform);
            }
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentObject = null;
        if (InventoryElementExited != null) { InventoryElementExited(); }
        if (SettingsElementExited != null) { SettingsElementExited(); }

    }

    GameObject DetectObject()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) {
            
            return null;
        } 
        if (hitObjects.Count >= 1)
        {
            foreach (var result in hitObjects)
            {
                //Debug.Log(result + " " + hitObjects.IndexOf(result));
            }
        }

        Debug.Log(hitObjects[0].gameObject);
        return hitObjects[0].gameObject;
    }
}
