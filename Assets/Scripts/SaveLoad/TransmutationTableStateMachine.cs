using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationTableStateMachine : MonoBehaviour
{
    [SerializeField] Transform inventoryRowsHolder;
    [SerializeField] Transform slotRowsHolder;
    [SerializeField] TransmutationRecipesPanel transmutationRecipesPanel;
    [SerializeField] TransmutationElementsManager transmutationElementsManager;


    public Transform GetInventoryRows()
    {
        return inventoryRowsHolder;
    }

    public Transform GetSlotRows()
    {
        return slotRowsHolder;
    }

    public Dictionary<int, bool> GetActivatedRecipes()
    {
        return transmutationRecipesPanel.ActivatedRecipesDictionary;
    }

    public void ApplyInventoryState(TransmutationTableData transmutationTableData)
    {
        int indexer = 0;
        foreach (Transform row in inventoryRowsHolder)
        {
            foreach (Transform slot in row)
            {
                slot.Find("Borders").Find("Element").GetComponent<Element>().CustomID = transmutationTableData.inventoryIDs[indexer++];
            }
        }
    }

    public void ApplySlotState(TransmutationTableData transmutationTableData)
    {
        int indexer = 0;
        foreach (Transform row in slotRowsHolder)
        {
            foreach (Transform slot in row)
            {
                slot.Find("Borders").Find("Element").GetComponent<Element>().CustomID = transmutationTableData.slotsIDs[indexer++];
            }
        }
    }

    public void ApplyRecipesState(TransmutationTableData transmutationTableData)
    {
        transmutationRecipesPanel.UploadRecipes(transmutationTableData.uploadedRecipes);
    }

    public void ResetTransmutationTableState()
    {
        transmutationElementsManager.ResetElementsList();
    }

}
