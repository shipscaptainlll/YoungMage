using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransmutationAmulet : MonoBehaviour, IResource, IShowClickable
{
    [SerializeField] Transform amuletProductsHolder;
    [SerializeField] ClickManager clickManager;
    [SerializeField] int id;
    [SerializeField] float zAxisOffset;
    Transform showedElement;
    bool activated;
    bool isObserved;
    bool usedForProduction;
    public bool UsedForProduction { get { return usedForProduction; } set { usedForProduction = value; } }
    public bool Activated { get { return activated; } set { activated = value; } }
    public int ID { get { return id; } set { id = value; HideAmuletProduct(); usedForProduction = false; } }
    public event Action<string> ObjectFound = delegate { };
    public event Action<string> ObjectUnfound = delegate { };
    public event Action<Transform> AmuletChoosen = delegate { };
    public event Action<Transform> StopedAutomaticTransmutation = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        clickManager.EClicked += NotifyAmuletChoosen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NotifyAmuletChoosen()
    {
        if (!usedForProduction && isObserved && AmuletChoosen != null)
        {
            usedForProduction = true;
            ShowAmuletProduct();
            AmuletChoosen(transform);
            
        } else if (usedForProduction && isObserved)
        {
            usedForProduction = false;
            HideAmuletProduct();
            StopedAutomaticTransmutation(null);
        }
    }

    public void Show()
    {
        ShowAmuletProduct();
        ShowOutline();
    }

    public void Hide()
    {
        HideAmuletProduct();
        HideOutline();
    }

    public void ShowAmuletProduct()
    {
        if (!showedElement && activated)
        {
            foreach (Transform element in amuletProductsHolder)
            {
                if (element.GetComponent<TransmutationProduct>().ID == id)
                {
                    element.position = transform.position + new Vector3(0, zAxisOffset, 0);
                    element.GetComponent<MeshRenderer>().enabled = true;
                    //element.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                    showedElement = element;

                }
            }
            usedForProduction = false;
        }
        
    }

    void ShowOutline()
    {
        if (activated)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            ObjectFound("TransmutationAmulet");
            isObserved = true;
        }
        
    }
    
    void HideAmuletProduct()
    {
        if (!usedForProduction && activated)
        {
            if (showedElement != null)
            {
                showedElement.GetComponent<MeshRenderer>().enabled = false;
                //showedElement.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                showedElement = null;
            }
        }
        
    }

    void HideOutline()
    {
        if (activated)
        {
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            ObjectUnfound("TransmutationAmulet");
            isObserved = false;
        }
        
    }
}
