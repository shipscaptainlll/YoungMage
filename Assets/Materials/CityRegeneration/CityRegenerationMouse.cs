using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityRegenerationMouse : MonoBehaviour
{

    static bool isActive;

    bool elementIsActive;
    RegenerationElementOutline lastEncounteredElement;
    RegenerationElementOutline.RegenerationElementType? lastEncounteredType;
    public static bool IsActive { get { return isActive; } set { isActive = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Transform foundObject = FindObjectUnderMouse();
            if (foundObject != null)
            {
                ConsiderNext(foundObject);
            } else
            {
                TurnOff();
            }
        }
    }

    Transform FindObjectUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hit = Physics.RaycastAll(ray);
        foreach (RaycastHit hitElement in hit)
        {
            if (hitElement.transform.GetComponent<RegenerationElementOutline>() != null)
            {
                return hitElement.transform;
            }
        }
        return null;
    }

    void ConsiderNext(Transform foundObject)
    {
        if (!elementIsActive)
        {
            Debug.Log("hello1");
            elementIsActive = true;
            lastEncounteredElement = foundObject.GetComponent<RegenerationElementOutline>();
            lastEncounteredType = foundObject.GetComponent<RegenerationElementOutline>().ElementType;
            lastEncounteredElement.StartShowingOutline();
            lastEncounteredElement.transform.GetComponent<IRegenerationSUI>().Activate();
        }
        else
        {
            if (foundObject.GetComponent<RegenerationElementOutline>().ElementType != lastEncounteredType)
            {
                Debug.Log("hello2");
                lastEncounteredElement.StopShowingOutline();
                lastEncounteredElement.transform.GetComponent<IRegenerationSUI>().Deactivate();
                lastEncounteredType = foundObject.GetComponent<RegenerationElementOutline>().ElementType;
                lastEncounteredElement = foundObject.GetComponent<RegenerationElementOutline>();
                lastEncounteredElement.StartShowingOutline();
                lastEncounteredElement.transform.GetComponent<IRegenerationSUI>().Activate();
            }
        }

    }

    void TurnOff()
    {
        Debug.Log("hello3");
        elementIsActive = false;
        if (lastEncounteredElement != null)
        {
            lastEncounteredElement.StopShowingOutline();
            lastEncounteredElement.transform.GetComponent<IRegenerationSUI>().Deactivate();
        }
        lastEncounteredType = null;
        lastEncounteredElement = null;
    }
    
}
