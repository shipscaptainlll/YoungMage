using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoorsDataApplier
{
    static DoorsStateMachine doorsStateMachineLoaded;
    static DoorsData doorsDataLoaded;

    public static void ApplyDoorsData(DoorsStateMachine doorsStateMachine, DoorsData doorsData)
    {
        UpdateData(doorsStateMachine, doorsData);
        ApplyDoorsState(doorsStateMachine, doorsData);
        DisconnectData();
    }

    static void UpdateData(DoorsStateMachine doorsStateMachine, DoorsData doorsData)
    {
        doorsStateMachineLoaded = doorsStateMachine;
        doorsDataLoaded = doorsData;
    }

    static void DisconnectData()
    {
        doorsStateMachineLoaded = null;
        doorsDataLoaded = null;
    }

    static void ApplyDoorsState(DoorsStateMachine doorsStateMachine, DoorsData doorsData)
    {
        Debug.Log("Applying doors state");
        Debug.Log("mines entrance doors were opened " + doorsData.minesEntranceDoorOpened); doorsStateMachine.ApplyMinesEntranceState(doorsData);
        if (doorsData.hallDoorOpening) { Debug.Log("halls doors were opening"); doorsStateMachine.ApplyHallDoorOpening(doorsData); }
        else if (doorsData.hallDoorClosing) { Debug.Log("halls doors were closing"); doorsStateMachine.ApplyHallDoorClosing(doorsData); } 
        else { Debug.Log("halls doors were opened " + doorsData.hallDoorOpened); doorsStateMachine.ApplyHallDoorState(doorsData); }
        doorsStateMachine.ApplyFirstDoorState(doorsData);
        Debug.Log("Current health of first door is " + doorsData.firstDoorHealth); doorsStateMachine.ApplyFirstDoorHealth(doorsData);
        doorsStateMachine.ApplySecondDoorState(doorsData);
        Debug.Log("Current health of second door is " + doorsData.secondDoorHealth); doorsStateMachine.ApplySecondDoorHealth(doorsData);
        doorsStateMachine.ApplyThirdDoorState(doorsData);
        Debug.Log("Current health of third door is " + doorsData.thirdDoorHealth); doorsStateMachine.ApplyThirdDoorHealth(doorsData);
    }
}
