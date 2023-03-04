using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsStateMachine : MonoBehaviour
{
    [SerializeField] DoorOpener doorOpener;
    [SerializeField] MagicDoor magicDoor;
    [SerializeField] GameObject firstDoor;
    [SerializeField] DoorHealthDecreaser firstDoorHealthDecreaser;
    [SerializeField] DoorConnectionManager firstDoorConnectionManager;
    [SerializeField] DoorTacklingManager firstDoorTacklingManager;
    [SerializeField] GameObject secondDoor;
    [SerializeField] DoorHealthDecreaser secondDoorHealthDecreaser;
    [SerializeField] DoorConnectionManager secondDoorConnectionManager;
    [SerializeField] DoorTacklingManager secondDoorTacklingManager;
    [SerializeField] GameObject thirdDoor;
    [SerializeField] DoorHealthDecreaser thirdDoorHealthDecreaser;
    [SerializeField] DoorConnectionManager thirdDoorConnectionManager;
    [SerializeField] DoorTacklingManager thirdDoorTacklingManager;


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

    public bool GetFirstDoorState()
    {
        return firstDoor.activeSelf;
    }

    public bool GetFirstDoorHealthVisible()
    {
        return firstDoorTacklingManager.HealthIsVisible;
    }

    public float GetFirstDoorHealth()
    {
        return firstDoorHealthDecreaser.CurrentHealth;
    }

    public void ApplyFirstDoorState(DoorsData doorsData)
    {
        firstDoorConnectionManager.ResetPositions();
        if (doorsData.firstDoorActive)
        {
            if (!firstDoor.activeSelf)
            {
                firstDoor.gameObject.SetActive(true);
            }
        }
        else
        {
            if (firstDoor.activeSelf)
            {
                firstDoor.gameObject.SetActive(false);
            }
        }

    }

    public void ApplyFirstDoorHealth(DoorsData doorsData)
    {
        firstDoorHealthDecreaser.UploadDoorsHealth(doorsData.firstDoorHealth);
        if (doorsData.firstDoorHealthVisible) { firstDoorTacklingManager.VisualiseOreHealthbar(); } else { firstDoorTacklingManager.HideOreHealthbar(); }
    }

    public bool GetSecondDoorState()
    {
        return secondDoor.activeSelf;
    }

    public bool GetSecondDoorHealthVisible()
    {
        return secondDoorTacklingManager.HealthIsVisible;
    }

    public float GetSecondDoorHealth()
    {
        return secondDoorHealthDecreaser.CurrentHealth;
    }

    public void ApplySecondDoorState(DoorsData doorsData)
    {
        secondDoorConnectionManager.ResetPositions();
        if (doorsData.secondDoorActive)
        {
            if (!secondDoor.activeSelf)
            {
                secondDoor.gameObject.SetActive(true);
            }
        }
        else
        {
            if (secondDoor.activeSelf)
            {
                secondDoor.gameObject.SetActive(false);
            }
        }

    }

    public void ApplySecondDoorHealth(DoorsData doorsData)
    {
        secondDoorHealthDecreaser.UploadDoorsHealth(doorsData.secondDoorHealth);
        if (doorsData.secondDoorHealthVisible) { secondDoorTacklingManager.VisualiseOreHealthbar(); } else { secondDoorTacklingManager.HideOreHealthbar(); }
    }

    public bool GetThirdDoorState()
    {
        return thirdDoor.activeSelf;
    }

    public bool GetThirdDoorHealthVisible()
    {
        return thirdDoorTacklingManager.HealthIsVisible;
    }

    public float GetThirdDoorHealth()
    {
        return thirdDoorHealthDecreaser.CurrentHealth;
    }

    public void ApplyThirdDoorState(DoorsData doorsData)
    {
        thirdDoorConnectionManager.ResetPositions();
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

    public void ApplyThirdDoorHealth(DoorsData doorsData)
    {
        thirdDoorHealthDecreaser.UploadDoorsHealth(doorsData.thirdDoorHealth);
        if (doorsData.thirdDoorHealthVisible) { thirdDoorTacklingManager.VisualiseOreHealthbar(); } else { thirdDoorTacklingManager.HideOreHealthbar(); }
    }

    

}
