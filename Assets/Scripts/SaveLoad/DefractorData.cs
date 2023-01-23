using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefractorData
{
    public bool conesRotating;

    public DefractorData(DefractorStateMachine defractorStateMachine)
    {
        GetActivationCircleState(defractorStateMachine);
        GetConesRotationState(defractorStateMachine);
    }

    void GetConesRotationState(DefractorStateMachine defractorStateMachine)
    {
        conesRotating = defractorStateMachine.GetConesRotation();
    }

    void GetActivationCircleState(DefractorStateMachine defractorStateMachine)
    {
        
    }

}
