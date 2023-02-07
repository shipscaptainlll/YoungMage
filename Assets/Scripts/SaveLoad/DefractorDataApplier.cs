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
        if (defractorData.conesRotating) { Debug.Log("cones rotation: true With progress " + defractorData.rotationProgress); defractorStateMachine.ApplyConesRotation(defractorData.rotationProgress); }
        if (defractorData.conesSlowingDown) { Debug.Log("cones slowing down: true With progress " + defractorData.rotationProgress); defractorStateMachine.ApplyConesSlowingDown(defractorData.rotationProgress); }
        if (defractorData.circleShownPS) { Debug.Log("circle was on " + defractorData.circleShownPS); defractorStateMachine.StartCirclePS(); } else { Debug.Log("circle was off " + defractorData.circleShownPS); defractorStateMachine.StopCirclePS(); }
        defractorStateMachine.ClearDefractorObjectsState();
        if (defractorData.defractorObjectsPositions.Length != 0) { Debug.Log("was something in outlet " + defractorData.defractorObjectsPositions.Length); defractorStateMachine.ApplyDefractoringObjects(defractorData); } else { Debug.Log("something in defractor line " + defractorData.defractorObjectsPositions.Length); }
        if (defractorData.outletObjectsPositions.Length != 0) { Debug.Log("was something in outlet " + defractorData.outletObjectsPositions.Length); defractorStateMachine.ApplyOutletObjects(defractorData); } else { Debug.Log("something in defractor line " + defractorData.outletObjectsPositions.Length); }
        if (defractorData.catchPortalShown) { Debug.Log("portal was " + defractorData.catchPortalShown); defractorStateMachine.OpenCatchingPortal(defractorData.catchPortalElapsed); } else { Debug.Log("portal was on " + defractorData.catchPortalShown); defractorStateMachine.CloseCatchingPortal(defractorData.catchPortalElapsed); }
        if (defractorData.catchCircleShown) { Debug.Log("catch circle was on " + defractorData.catchCircleShown); defractorStateMachine.ShowCatchingCircle(); } else { Debug.Log("catch circle was on " + defractorData.catchCircleShown); defractorStateMachine.HideCatchingCircle(); }
    }
}
