using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransmutationTableData
{
    public int[] inventoryIDs;
    public int[] slotsIDs;
    public int[] uploadedRecipes;


    public TransmutationTableData(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        GetInventoryIDs(transmutationTableStateMachine);
        GetSlotsIDs(transmutationTableStateMachine);
        GetAvailableRecipes(transmutationTableStateMachine);
    }

    void GetInventoryIDs(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        List<int> cache = new List<int>();
        Transform inventoryRowsHolder = transmutationTableStateMachine.GetInventoryRows();
        foreach (Transform row in inventoryRowsHolder)
        {
            foreach (Transform slot in row)
            {
                int slotCustomID = slot.Find("Borders").Find("Element").GetComponent<Element>().CustomID;
                cache.Add(slotCustomID);
                //Debug.Log("main inventory id " + slotCustomID);
            }
        }
        inventoryIDs = new int[cache.Count];
        inventoryIDs = cache.ToArray();
    }

    void GetSlotsIDs(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        List<int> cache = new List<int>();
        Transform slotRowsHolder = transmutationTableStateMachine.GetSlotRows();
        foreach (Transform row in slotRowsHolder)
        {
            foreach (Transform slot in row)
            {
                int slotCustomID = slot.Find("Borders").Find("Element").GetComponent<Element>().CustomID;
                cache.Add(slotCustomID);
                //Debug.Log("main inventory id " + slotCustomID);
            }
        }
        slotsIDs = new int[cache.Count];
        slotsIDs = cache.ToArray();
    }

    public void GetAvailableRecipes(TransmutationTableStateMachine transmutationTableStateMachine)
    {
        Dictionary<int, bool> activateRecipes = transmutationTableStateMachine.GetActivatedRecipes();
        List<int> cache = new List<int>();

        foreach (var element in activateRecipes)
        {
            if (element.Value == true)
            {
                cache.Add(element.Key);
            }
        }

        uploadedRecipes = new int[cache.Count];
        uploadedRecipes = cache.ToArray();

    }


}
