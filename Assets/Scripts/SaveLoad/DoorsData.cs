using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DoorsData
{
    public bool minesEntranceDoorOpened;
    public bool hallDoorOpening;
    public bool hallDoorClosing;
    public bool hallDoorOpened;
    public float hallDoorStage;
    public bool thirdDoorActive;
    public float thirdDoorHealth;


    public DoorsData(DoorsStateMachine doorsStateMachine)
    {
        GetMinesEntranceState(doorsStateMachine);
        GetHallDoorState(doorsStateMachine);
        GetThirdDoorState(doorsStateMachine);
        GetThirdDoorHealth(doorsStateMachine);
    }

    void GetMinesEntranceState(DoorsStateMachine doorsStateMachine)
    {
        minesEntranceDoorOpened = doorsStateMachine.GetMinesEntranceState();
    }

    void GetHallDoorState(DoorsStateMachine doorsStateMachine)
    {
        GetHallDoorOpening(doorsStateMachine);
        GetHallDoorClosing(doorsStateMachine);
        GetHallDoorOpened(doorsStateMachine);
        GetHallDoorStage(doorsStateMachine);
    }

    void GetThirdDoorState(DoorsStateMachine doorsStateMachine)
    {
        thirdDoorActive = doorsStateMachine.GetThirdDoorState();
    }

    void GetHallDoorOpening(DoorsStateMachine doorsStateMachine)
    {
        hallDoorOpening = doorsStateMachine.GetHallDoorOpening();
    }

    void GetHallDoorClosing(DoorsStateMachine doorsStateMachine)
    {
        hallDoorClosing = doorsStateMachine.GetHallDoorClosing();
    }

    void GetHallDoorOpened(DoorsStateMachine doorsStateMachine)
    {
        hallDoorOpened = doorsStateMachine.GetHallDoorOpened();
    }
    void GetHallDoorStage(DoorsStateMachine doorsStateMachine)
    {
        hallDoorStage = doorsStateMachine.GetHallDoorStage();
    }

    void GetThirdDoorHealth(DoorsStateMachine doorsStateMachine)
    {
        thirdDoorHealth = doorsStateMachine.GetThirdDoorHealth();
    }

}
