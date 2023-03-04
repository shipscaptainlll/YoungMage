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
    public bool firstDoorActive;
    public bool firstDoorHealthVisible;
    public float firstDoorHealth;
    public bool secondDoorActive;
    public bool secondDoorHealthVisible;
    public float secondDoorHealth;
    public bool thirdDoorActive;
    public bool thirdDoorHealthVisible;
    public float thirdDoorHealth;


    public DoorsData(DoorsStateMachine doorsStateMachine)
    {
        GetMinesEntranceState(doorsStateMachine);
        GetHallDoorState(doorsStateMachine);
        GetFirstDoorHealth(doorsStateMachine);
        GetFirstDoorState(doorsStateMachine);
        GetSecondDoorHealth(doorsStateMachine);
        GetSecondDoorState(doorsStateMachine);
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

    void GetFirstDoorState(DoorsStateMachine doorsStateMachine)
    {
        firstDoorActive = doorsStateMachine.GetFirstDoorState();
        firstDoorHealthVisible = doorsStateMachine.GetFirstDoorHealthVisible();
    }

    void GetFirstDoorHealth(DoorsStateMachine doorsStateMachine)
    {
        firstDoorHealth = doorsStateMachine.GetFirstDoorHealth();
    }

    void GetSecondDoorState(DoorsStateMachine doorsStateMachine)
    {
        secondDoorActive = doorsStateMachine.GetSecondDoorState();
        secondDoorHealthVisible = doorsStateMachine.GetSecondDoorHealthVisible();
    }

    void GetSecondDoorHealth(DoorsStateMachine doorsStateMachine)
    {
        secondDoorHealth = doorsStateMachine.GetSecondDoorHealth();
    }

    void GetThirdDoorState(DoorsStateMachine doorsStateMachine)
    {
        thirdDoorActive = doorsStateMachine.GetThirdDoorState();
        thirdDoorHealthVisible = doorsStateMachine.GetThirdDoorHealthVisible();
    }

    void GetThirdDoorHealth(DoorsStateMachine doorsStateMachine)
    {
        thirdDoorHealth = doorsStateMachine.GetThirdDoorHealth();
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

    

}
