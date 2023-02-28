using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsStateMachine : MonoBehaviour
{
    [SerializeField] DoorOpener doorOpener;
    [SerializeField] MagicDoor magicDoor;
    [SerializeField] GameObject thirdDoor;


    public bool GetMinesEntranceState()
    {
        return doorOpener.DoorOpened;
    }

    public void ApplyMinesEntranceState(DoorsData doorsData)
    {
        doorOpener.UploadDoorState(doorsData.minesEntranceDoorOpened);
    }

    public bool GetHallDoorOpening()
    {
        return magicDoor.OpeningCoroutineGetter != null;
    }

    public bool GetHallDoorClosing()
    {
        return magicDoor.ClosingCoroutineGetter != null;
    }

    public bool GetHallDoorOpened()
    {
        return magicDoor.DoorOpened;
    }

    public float GetHallDoorStage()
    {
        return magicDoor.CoroutineStage;
    }

    public void ApplyHallDoorOpening(DoorsData doorsData)
    {
        magicDoor.UploadDoorOpening(doorsData.hallDoorStage);
    }

    public void ApplyHallDoorClosing(DoorsData doorsData)
    {
        magicDoor.UploadDoorClosing(doorsData.hallDoorStage);
    }

    public void ApplyHallDoorState(DoorsData doorsData)
    {
        magicDoor.UploadDoorState(doorsData.hallDoorOpened);
    }

    public bool GetThirdDoorState()
    {
        return thirdDoor.activeSelf;
    }

    public void ApplyThirdDoorState(DoorsData doorsData)
    {
        if (doorsData.thirdDoorActive)
        {
            if (!thirdDoor.activeSelf)
            {
                thirdDoor.gameObject.SetActive(true);
            }
        } else
        {
            if (thirdDoor.activeSelf)
            {
                thirdDoor.gameObject.SetActive(false);
            }
        }
        
    }

}
