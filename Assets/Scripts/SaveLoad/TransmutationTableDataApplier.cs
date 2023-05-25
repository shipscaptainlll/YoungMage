using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransmutationTableDataApplier
{
    static TransmutationTableStateMachine transmutationTableStateMachineLoaded;
    static TransmutationTableData transmutationTableDataLoaded;

    public static void ApplyTransmutationTableData(TransmutationTableStateMachine transmutationTableStateMachine, TransmutationTableData transmutationTableData)
    {
        UpdateData(transmutationTableStateMachine, transmutationTableData);
        ApplyTransmutationTableState(transmutationTableStateMachine, transmutationTableData);
        DisconnectData();
    }

    static void UpdateData(TransmutationTableStateMachine transmutationTableStateMachine, TransmutationTableData transmutationTableData)
    {
        transmutationTableStateMachineLoaded = transmutationTableStateMachine;
        transmutationTableDataLoaded = transmutationTableData;
    }

    static void DisconnectData()
    {
        transmutationTableStateMachineLoaded = null;
        transmutationTableDataLoaded = null;
    }

    static void ApplyTransmutationTableState(TransmutationTableStateMachine transmutationTableStateMachine, TransmutationTableData transmutationTableData)
    {
        
        Debug.Log("Applying transmutation table state machine");
        transmutationTableStateMachine.ResetTransmutationTableState();
        transmutationTableStateMachine.ApplyRecipesState(transmutationTableData);
        transmutationTableStateMachine.ApplyInventoryState(transmutationTableData);
        transmutationTableStateMachine.ApplySlotState(transmutationTableData);
    }
}
