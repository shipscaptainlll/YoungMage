using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransmutationTableData
{
    public int[] choosenResourcesID;
    public bool[] elementVisible;
    public int chosenProductID;
    public bool portalOpened;
    public int instantiatedProductID;
    public Vector3 instantiatedProductPosition;


    public TransmutationTableData(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        GetElementsCurrentID(transmutationTableStateMachine);
        GetElementsVisibility(transmutationTableStateMachine);
        GetChoosenProductID(transmutationTableStateMachine);
        GetPortalState(transmutationTableStateMachine);
        GetInstantiatedProductID(transmutationTableStateMachine);
        GetInstantiatedProductPosition(transmutationTableStateMachine);
    }

    public void GetElementsCurrentID(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        choosenResourcesID = new int[transmutationTableStateMachine.ElementsHolder.childCount];
        int indexer = 0;

        foreach (Transform element in transmutationTableStateMachine.ElementsHolder)
        {
            choosenResourcesID[indexer] = element.GetChild(1).GetComponent<TransmutationResourceChoose>().
        }

    }

    public void GetElementsVisibility(TransmutationTableStateMachine transmutationTableStateMachine)
    {

    }

    public void GetChoosenProductID(TransmutationTableStateMachine transmutationTableStateMachine)
    {

    }

    public void GetPortalState(TransmutationTableStateMachine transmutationTableStateMachine)
    {

    }

    public void GetInstantiatedProductID(TransmutationTableStateMachine transmutationTableStateMachine)
    {

    }

    public void GetInstantiatedProductPosition(TransmutationTableStateMachine transmutationTableStateMachine)
    {

    }


}
