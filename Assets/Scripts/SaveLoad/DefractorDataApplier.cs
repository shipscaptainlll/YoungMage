using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DefractorDataApplier
{
    static DefractorStateMachine defractorStateMachineLoaded;
    static DefractorData defractorDataLoaded;

    public static void ApplyDefractorData(DefractorStateMachine defractorStateMachine, DefractorData defractorData)
    {
        UpdateData(defractorStateMachine, defractorData);
        ApplyDefractorState(defractorStateMachine, defractorData);
        DisconnectData();
    }

    static void UpdateData(DefractorStateMachine defractorStateMachine, DefractorData defractorData)
    {
        defractorStateMachineLoaded = defractorStateMachine;
        defractorDataLoaded = defractorData;
    }

    static void DisconnectData()
    {
        defractorStateMachineLoaded = null;
        defractorDataLoaded = null;
    }

    static void ApplyDefractorState(DefractorStateMachine defractorStateMachine, DefractorData defractorData)
    {
        Debug.Log("Applying defractoring state: cones");
        defractorStateMachine.StopConesRotation();
        if (defractorData.conesRotating) { Debug.Log("cones rotation: true"); defractorStateMachine.ApplyConesRotation(); }
    }
}
