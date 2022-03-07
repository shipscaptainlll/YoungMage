using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EClickVariations : MonoBehaviour
{
    [SerializeField] Transform positionsHolder;
    [SerializeField] Transform chooseElementsHolder;
    [SerializeField] Transform amuletsHolder;
    [SerializeField] CameraController cameraController;
    [SerializeField] Transform potentialProductsHolder;
    bool isTransmutating;
    bool isOpeningPortal;

    public bool IsTransmutating { get { return isTransmutating; } }
    public bool IsOpeningPortal { get { return isOpeningPortal; } }

    public event Action EneteredTransmutationMode = delegate { };
    public event Action LeftTransmutationMode = delegate { };
    public event Action EnteredChooseMode = delegate { };
    public event Action LeftChooseMode = delegate { };
    public event Action EnteredAmulet = delegate { };
    public event Action LeftAmulet = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform element in positionsHolder)
        {
            if (element.GetComponent<EClickPosition>() != null)
            {
                element.GetComponent<EClickPosition>().SomethingEntered += Activate;
                element.GetComponent<EClickPosition>().SomethingLeaved += Deactivate;
            }
        }
        foreach (Transform element in potentialProductsHolder)
        {
            if (element.GetComponent<AlchemistPotentialProduct>() != null)
            {
                element.GetComponent<AlchemistPotentialProduct>().ObjectFound += Activate;
                element.GetComponent<AlchemistPotentialProduct>().ObjectUnfound += Deactivate;
            }
        }
        foreach (Transform element in chooseElementsHolder)
        {
            if (element.GetChild(0).GetComponent<TransmutationResourcePack>() != null)
            {
                element.GetChild(1).GetComponent<TransmutationResourceChoose>().ObjectFound += Activate;
                element.GetChild(1).GetComponent<TransmutationResourceChoose>().ObjectUnfound += Deactivate;
            }
        }
        foreach (Transform element in amuletsHolder)
        {
            if (element.GetComponent<TransmutationAmulet>() != null)
            {
                element.GetComponent<TransmutationAmulet>().ObjectFound += Activate;
                element.GetComponent<TransmutationAmulet>().ObjectUnfound += Deactivate;
            }
        }
    }
    
    void Activate(string enteredObject)
    {
        if (enteredObject == "PortalOpener")
        {
            
            isOpeningPortal = true;
            


        } else if (enteredObject == "TransmutationTable")
        {
            isTransmutating = true;
            EneteredTransmutationMode();
        }
        else if (enteredObject == "TransmutationSlot")
        {
            //Debug.Log("Entered hello");
            EnteredChooseMode();
        }
        else if (enteredObject == "TransmutationAmulet")
        {
            EnteredAmulet();
        }
    }

    void Deactivate(string enteredObject)
    {
        if (enteredObject == "PortalOpener")
        {
            isOpeningPortal = false;
            
        }
        else if (enteredObject == "TransmutationTable")
        {
            isTransmutating = false;
            LeftTransmutationMode();
        }
        else if (enteredObject == "TransmutationSlot")
        {
            LeftChooseMode();
        }
        else if (enteredObject == "TransmutationAmulet")
        {
            LeftAmulet();
        }
    }

    private void Update()
    {

    }
}
