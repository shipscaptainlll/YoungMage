using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransmutationTableData
{
    public bool[] packShown;
    public float[] packAngle;
    public int[] choosenResourcesID;
    public bool[] elementVisible;
    public int chosenProductID;
    public bool portalOpened;
    public bool circleActive;
    public int instantiatedProductID;
    public float[] instantiatedProductPosition;


    public TransmutationTableData(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        GetPackVisibility(transmutationTableStateMachine);
        GetPackAngle(transmutationTableStateMachine);
        GetElementsCurrentID(transmutationTableStateMachine);
        GetElementsVisibility(transmutationTableStateMachine);
        GetChoosenProductID(transmutationTableStateMachine);
        GetPortalState(transmutationTableStateMachine);
        GetCircleState(transmutationTableStateMachine);
        GetInstantiatedProductID(transmutationTableStateMachine);
        GetInstantiatedProductPosition(transmutationTableStateMachine);
    }

    public void GetPackVisibility(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        packShown = new bool[transmutationTableStateMachine.ElementsHolder.childCount];
        int indexer = 0;

        foreach (Transform element in transmutationTableStateMachine.ElementsHolder)
        {
            packShown[indexer] = element.GetChild(1).GetComponent<TransmutationResourceChoose>().ResourcePackShown;
            Debug.Log("visibility of " + indexer + " pack is: Visible " + packShown[indexer]);
            indexer++;
        }

    }

    public void GetPackAngle(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        packAngle = new float[transmutationTableStateMachine.ElementsHolder.childCount];
        int indexer = 0;

        foreach (Transform element in transmutationTableStateMachine.ElementsHolder)
        {
            packAngle[indexer] = element.GetChild(1).GetComponent<TransmutationResourceChoose>().CurrentAngle;
            Debug.Log("angle of " + indexer + " pack is: " + packAngle[indexer]);
            indexer++;
        }

    }

    public void GetElementsCurrentID(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        choosenResourcesID = new int[transmutationTableStateMachine.ElementsHolder.childCount];
        int indexer = 0;

        foreach (Transform element in transmutationTableStateMachine.ElementsHolder)
        {
            choosenResourcesID[indexer] = element.GetChild(1).GetComponent<TransmutationResourceChoose>().CurrentID;
            Debug.Log("choosen resource ID of " + indexer + " element is:  " + choosenResourcesID[indexer]);
            indexer++;
        }

    }

    public void GetElementsVisibility(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        elementVisible = new bool[transmutationTableStateMachine.ElementsHolder.childCount];
        int indexer = 0;

        foreach (Transform element in transmutationTableStateMachine.ElementsHolder)
        {
            elementVisible[indexer] = element.GetChild(1).GetComponent<TransmutationResourceChoose>().ResourceShown;
            Debug.Log("visibility of " + indexer + " element is: Visible " + elementVisible[indexer]);
            indexer++;
        }
    }

    public void GetChoosenProductID(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        chosenProductID = transmutationTableStateMachine.GetPotentialProductID();
        Debug.Log("choosen product ID is " + chosenProductID);
    }

    public void GetPortalState(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        portalOpened = transmutationTableStateMachine.CheckPortalOpened();
        Debug.Log("portal is opened " + portalOpened);
    }

    public void GetCircleState(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        circleActive = transmutationTableStateMachine.CheckCircleActive();
        Debug.Log("circle is active " + circleActive);
    }

    public void GetInstantiatedProductID(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        instantiatedProductID = transmutationTableStateMachine.GetInstantiatedProductID();
        Debug.Log("instantiated product ID is " + instantiatedProductID);
    }

    public void GetInstantiatedProductPosition(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        instantiatedProductPosition = transmutationTableStateMachine.GetCreatedObjectPosition();
        Debug.Log("instantiated product position is " + instantiatedProductPosition[0] + ", " + +instantiatedProductPosition[1] + ", " + instantiatedProductPosition[2]);
    }


}
