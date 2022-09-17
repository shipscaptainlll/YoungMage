using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update

    GameObject currentObject;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerCurrentRaycast);
        if (currentObject == null)
        {
            currentObject = DetectObject();
            Debug.Log(currentObject);
            if (currentObject != null) { currentObject.GetComponent<HoverSoundElement>().StartSound(); }
            currentObject = null;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    GameObject DetectObject()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0)
        {

            return null;
        }
        if (hitObjects.Count >= 1)
        {
            foreach (RaycastResult result in hitObjects)
            {
                Debug.Log(result);
                if (result.gameObject.GetComponent<HoverSoundElement>() != null) { return result.gameObject; }
            }
        }

        return null;
    }
}
