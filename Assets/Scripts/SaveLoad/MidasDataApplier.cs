using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MidasDataDataApplier
{
    static MidasStateMachine midasStateMachineLoaded;
    static MidasData midasDataLoaded;

    public static void ApplyMidasData(MidasStateMachine midasStateMachine, MidasData midasData)
    {
        UpdateData(midasStateMachine, midasData);
        ApplyMidasState(midasStateMachine, midasData);
        DisconnectData();
    }

    static void UpdateData(MidasStateMachine midasStateMachine, MidasData midasData)
    {
        midasStateMachineLoaded = midasStateMachine;
        midasDataLoaded = midasData;
    }

    static void DisconnectData()
    {
        midasStateMachineLoaded = null;
        midasDataLoaded = null;
    }

    static void ApplyMidasState(MidasStateMachine midasStateMachine, MidasData midasData)
    {
        Debug.Log("Applying defractoring state: cones");
        if (midasData.circleShownPS) { Debug.Log("circle was on " + midasData.circleShownPS); midasStateMachine.ShowCircle(); } else { Debug.Log("circle was off " + midasData.circleShownPS); midasStateMachine.HideCircle(); }
        midasStateMachine.ClearMidasObjectsState();
        if (midasData.inletObjectsPositions.Length != 0) { Debug.Log("was something in midas inlet " + midasData.inletObjectsPositions.Length); midasStateMachine.ApplyInletObjects(midasData); } else { Debug.Log("something in midas inlet " + midasData.inletObjectsPositions.Length); }
        midasStateMachine.ClearMaterialsObjectsState();
        if (midasData.pipeMaterialsPositions.Length != 0) { Debug.Log("was something in midas materials " + midasData.inletObjectsPositions.Length); midasStateMachine.ApplyMaterialsObjects(midasData); } else { Debug.Log("something in midas materials " + midasData.pipeMaterialsPositions.Length); }
        midasStateMachine.ClearCoinsObjectsState();
        if (midasData.pipeCoinsPositions.Length != 0) { Debug.Log("was something in midas coins " + midasData.pipeCoinsPositions.Length); midasStateMachine.ApplyCoinsObjects(midasData); } else { Debug.Log("something in midas coins " + midasData.pipeCoinsPositions.Length); }
        Debug.Log("Was something in midas coins collector " + midasData.coinsAmmount); midasStateMachine.ApplyCoinsCount(midasData);
    }
}
