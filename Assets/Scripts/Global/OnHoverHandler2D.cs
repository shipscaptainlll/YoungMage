using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverHandler2D : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] HoverInventoryManager hoverInventoryManager;
    [SerializeField] string tag;
    [SerializeField] PanelsManager panelsManager;
    GameObject currentObject;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    public event Action<Transform> InventoryElementFound = delegate { };
    public event Action<Transform> InventoryElementExited = delegate { };
    public event Action<Transform> SettingsElementFound = delegate { };
    public event Action SettingsElementExited = delegate { };

    Coroutine detectionCoroutine;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tag == "quickAccess")
        {
            return;
        }
        //Debug.Log("process 01: " + transform + " we are on " + currentObject == null);
        if (currentObject == null)
        {
            
            currentObject = DetectObject();
            if (currentObject == null) { return; }
            //Debug.Log(currentObject);
            Transform elementTransform = currentObject.transform.GetChild(0).GetChild(1);
            if (elementTransform.GetComponent<Element>() != null)
            {
                //Debug.Log(currentObject.GetComponent<Image>().sprite.name);
                if (elementTransform.GetComponent<Image>().sprite.name != "Nothing" && InventoryElementFound != null)
                {
                    //Debug.Log(currentObject.GetComponent<Image>().sprite); 
                    hoverInventoryManager.Process(transform);
                    //InventoryElementFound(transform); }
                }
                if (currentObject.GetComponent<SettingsElement>() != null)
                {
                    SettingsElementFound(transform);
                }

                detectionCoroutine = StartCoroutine(DetectionMouseExited());
            }

        }
    }

        IEnumerator DetectionMouseExited()
        {

            while (true)
            {
                //Debug.Log("working " + transform);
                var pointer = new PointerEventData(EventSystem.current);

                pointer.position = Input.mousePosition;

                EventSystem.current.RaycastAll(pointer, hitObjects);
                bool found = false;
            int indexer = 0;
                foreach (RaycastResult element in hitObjects)
                {
                //Debug.Log(element.gameObject.name + " " + indexer);
                indexer++;
                    if (element.gameObject == currentObject)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    //Debug.Log("current object was " + currentObject);
                    //Debug.Log("exited because " + hitObjects[0].gameObject);
                    if (hitObjects.Count > 1) { 
                    //Debug.Log(hitObjects[1]); 
                }
                    if (hitObjects.Count > 2) { 
                    //Debug.Log(hitObjects[2]);
                }
                    if (hitObjects.Count > 3) { 
                    //Debug.Log(hitObjects[3]);
                }
                    currentObject = null;
                    if (InventoryElementExited != null)
                    {
                        hoverInventoryManager.Unprocess(transform);
                        //InventoryElementExited();
                    }
                    if (SettingsElementExited != null) { SettingsElementExited(); }
                    StopAllCoroutines();
                    detectionCoroutine = null;
                    break;
                }

                yield return new WaitForSeconds(0.017f);
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

            foreach (RaycastResult element in hitObjects)
            {
                //Debug.Log(element);
                if (element.gameObject.GetComponent<OnHoverHandler2D>() != null && 
                    element.gameObject.transform.GetChild(0) != null &&
                    element.gameObject.transform.GetChild(0).GetChild(1) != null &&
                    element.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Element>() != null &&
                    element.gameObject.transform.GetChild(0).GetChild(1).GetComponent<Element>().CustomID != 0)
                {
                    return element.gameObject;
                }
            }

            return null;
            /*

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

            //Debug.Log(hitObjects[0].gameObject);
            return hitObjects[0].gameObject;
            */
        }
    }

