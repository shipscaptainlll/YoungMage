using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform m_transmutationSlotPanel;
    
    List<RaycastResult> hitObjects = new List<RaycastResult>();
    public event Action QuitAccessChanged = delegate { };
    [SerializeField] Transform quickAccessPanel;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource changeElementSound;
    
    
    public event Action QuickAccessElementFilled = delegate { };
    
    // Start is called before the first frame update
    void Start()
    {
        changeElementSound = soundManager.FindSound("NewObjectAppearingUI");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        RectTransform inventoryPanel = transform as RectTransform;
        if (eventData.pointerDrag.transform.GetComponent<Element>().ElementType == "transmutationSlotSlot")
        {
            bool onTransmutationLayer = FindTransmutationSlotLayer();

            if (onTransmutationLayer)
            {
                return;
            } else
            {
                eventData.pointerDrag.transform.GetComponent<Element>().AttachedCounter.GetComponent<ICounter>().AddResource(1);
                eventData.pointerDrag.transform.GetComponent<Element>().CustomID = 0;
                return;
            }

        }
        if (RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, Input.mousePosition))
        {
            GameObject foundObject = GetObjectUnderMouse();
            if (foundObject == null)
            {
                Debug.Log("nothing here");
                return;
                
            }
            
            Debug.Log("object under mouse was " + foundObject);
            string foundObjectType = CheckElementType(foundObject);
            if (foundObject != eventData.pointerDrag.transform)
            {
                if (foundObjectType == "inventorySlot")
                {
                    SwapProperties(eventData);
                } else if (foundObjectType == "quickAccessSlot")
                {
                    
                    TransferProperties(eventData, foundObjectType);
                } else if (foundObjectType == "transmutationSlotSlot")
                {
                    
                    GameObject targetObject = GetObjectUnderMouse();
                    if (targetObject.GetComponent<Element>().CustomID != 0)
                    {
                        targetObject.GetComponent<Element>().AttachedCounter.GetComponent<ICounter>().AddResource(1);
                    }
                    TransferProperties(eventData, foundObjectType);
                } else if (foundObjectType == "transmutationInventorySlot")
                {
                    
                    SwapProperties(eventData);
                }
                else
                {
                    Debug.Log("didnt find anything ");
                }
            }
        }
    }

    void SwapProperties(PointerEventData eventData)
    {
        SwapCustomID(eventData);
    }

    void TransferProperties(PointerEventData eventData, string objectType)
    {
        if (CheckForRepeating(eventData) != null)
        {
            Transform copyToErase = CheckForRepeating(eventData);
            EraseCopy(copyToErase);
        }
        CopyCustomID(eventData, objectType);
    }

    void SwapCustomID(PointerEventData eventData)
    {
        GameObject targetObject = GetObjectUnderMouse();

        int cacheCustomId = targetObject.GetComponent<Element>().CustomID;
        targetObject.GetComponent<Element>().CustomID = eventData.pointerDrag.transform.GetComponent<Element>().CustomID;
        eventData.pointerDrag.transform.GetComponent<Element>().CustomID = cacheCustomId;

        changeElementSound.Play();
    }

    void CopyCustomID(PointerEventData eventData, string objectType)
    {
        
        GameObject targetObject = GetObjectUnderMouse();
        
        targetObject.GetComponent<Element>().CustomID = eventData.pointerDrag.transform.GetComponent<Element>().CustomID;
        int slotNumber = targetObject.transform.parent.parent.GetSiblingIndex();

        
        changeElementSound.Play();

        if (objectType == "transmutationSlotSlot")
        {
            eventData.pointerDrag.transform.GetComponent<Element>().AttachedCounter.GetComponent<ICounter>().AddResource(-1);
        }
        
        if (objectType == "transmutationInventorySlot")
        {
            
            return;
        }
        
        Transform m_quickAccessPanel;
        if (transform.Find("QuickAccess") != null)
        {
            m_quickAccessPanel = transform.Find("QuickAccess");
        }
        else
        {
            return;
            //quickAccessPanel = m_transmutationSlotPanel;
        }
        
        CopyIDToQuickAccess(eventData, slotNumber);

        
    }

    void CopyIDToQuickAccess(PointerEventData eventData, int slotNumber)
    {
        if (QuickAccessElementFilled != null && eventData.pointerDrag.transform.GetComponent<Element>().CustomID == 2)
        {
            QuickAccessElementFilled();
        }
        quickAccessPanel.GetChild(slotNumber).Find("Borders").Find("Element").GetComponent<QuickAccessElement>().CustomID = eventData.pointerDrag.transform.GetComponent<Element>().CustomID;
    }

    Transform CheckForRepeating(PointerEventData eventData)
    {
        Transform m_quickAccessPanel;
        if (transform.Find("QuickAccess") != null)
        {
            m_quickAccessPanel = transform.Find("QuickAccess");
        }
        else
        {
            return null;
            //quickAccessPanel = m_transmutationSlotPanel;
        }
        
        Transform targetObject = GetObjectUnderMouse().transform.parent.parent;
        Debug.Log("we are here " + eventData.pointerDrag.transform.GetComponent<Element>().CustomID + transform);
        foreach (Transform slot in m_quickAccessPanel.GetChild(0))
        {
            Debug.Log("we are here " + slot.GetChild(0).Find("Element").GetComponent<Element>().CustomID );
            if (slot.GetChild(0).Find("Element").GetComponent<Element>().CustomID == eventData.pointerDrag.transform.GetComponent<Element>().CustomID && targetObject.GetSiblingIndex() != slot.GetSiblingIndex())
            {
                return slot;
            } 
        }
        return null;
    }

    void EraseCopy(Transform slotToErase)
    {
        slotToErase.GetChild(0).Find("Element").GetComponent<Element>().CustomID = 0;

        int numberOfSlot = slotToErase.GetSiblingIndex();
        
        EraseQuickAccessCopy(numberOfSlot);
        
    }

    void EraseQuickAccessCopy(int numberOfSlot)
    {
        quickAccessPanel.GetChild(numberOfSlot).Find("Borders").Find("Element").GetComponent<QuickAccessElement>().CustomID = 0;
    }
    

    bool FindTransmutationSlotLayer()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);


        if (hitObjects[0].gameObject.GetComponent<TransmutationSlotsLayer>())
        {
            return true;
        }

        if (hitObjects.Count > 1 && hitObjects[1].gameObject.GetComponent<TransmutationSlotsLayer>() != null)
        {
            return true;
        }
        else if (hitObjects.Count > 2 && hitObjects[2].gameObject.GetComponent<TransmutationSlotsLayer>() != null)
        {
            return true;
        }
        else if (hitObjects.Count > 3 && hitObjects[3].gameObject.GetComponent<TransmutationSlotsLayer>() != null)
        {
            return true;
        }
        else if (hitObjects.Count > 4 && hitObjects[4].gameObject.GetComponent<TransmutationSlotsLayer>() != null)
        {
            return true;
        }
        else if (hitObjects.Count > 5 && hitObjects[5].gameObject.GetComponent<TransmutationSlotsLayer>() != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);
        
        
        if (hitObjects.Count <= 0) return null;
        Debug.Log("under mouse was 00" + hitObjects[0]);
        Debug.Log("under mouse was 0" + hitObjects[1]);
        if (hitObjects.Count > 2)
        {
            Debug.Log("under mouse was 1" + hitObjects[2]);
        }
        if (hitObjects.Count > 3)
        {
            Debug.Log("under mouse was 2" + hitObjects[3]);
        }
        if (hitObjects.Count > 4)
        {
            Debug.Log("under mouse was 3" + hitObjects[4]);
        }
        
        if (hitObjects[0].gameObject.GetComponent<Element>() != null && hitObjects[0].gameObject.GetComponent<Element>().ElementType.ToString() == "quickAccessSlot")
        {
            return hitObjects[0].gameObject;
        }
        if (hitObjects[0].gameObject.GetComponent<Element>() != null && hitObjects[0].gameObject.GetComponent<Element>().ElementType.ToString() == "transmutationSlotSlot")
        {
            return hitObjects[0].gameObject;
        } 
        
        if (hitObjects.Count > 1 && hitObjects[1].gameObject.GetComponent<Element>() != null)
        {
            return hitObjects[1].gameObject;
        } 
        else if (hitObjects.Count > 2 && hitObjects[2].gameObject.GetComponent<Element>() != null)
        {
            return hitObjects[2].gameObject;
        }
        else if (hitObjects.Count > 3 && hitObjects[3].gameObject.GetComponent<Element>() != null)
        {
            return hitObjects[3].gameObject;
        } 
        else if (hitObjects.Count > 4 && hitObjects[4].gameObject.GetComponent<Element>() != null)
        {
            return hitObjects[4].gameObject;
        }
        else if (hitObjects.Count > 5 && hitObjects[5].gameObject.GetComponent<Element>() != null)
        {
            return hitObjects[5].gameObject;
        } else
        {
            return null;
        }
        
    }

    string CheckElementType(GameObject foundObject)
    {
        //meObject objectUnderMouse = GetObjectUnderMouse();
        //Debug.Log("we are here " + objectUnderMouse.gameObject.name);
        string objectType = "";
        if (foundObject != null && foundObject.GetComponent<Element>() != null)
        {
            objectType = foundObject.GetComponent<Element>().ElementType;
        } else
        {
            
        }
        
        
        
        return objectType;
    }
}
