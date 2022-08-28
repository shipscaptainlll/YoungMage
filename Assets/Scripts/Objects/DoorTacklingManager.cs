using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTacklingManager : MonoBehaviour
{
    [SerializeField] DoorHealthDecreaser doorHealthDecreaser;
    [SerializeField] DoorOpen leftDoorOpener;
    [SerializeField] DoorOpen rightDoorOpener;
    SkeletonBehavior connectedSkeleton;

    public SkeletonBehavior ConnectedSkeleton
    {
        get
        {
            return connectedSkeleton;
        }
        set
        {
            if (value == null)
            {
                DisconnectSkeleton();
            } else
            {
                ConnectSkeleton(value);
            }
            connectedSkeleton = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ConnectSkeleton(SkeletonBehavior connectingSkeleton)
    {
        VisualiseOreHealthbar();
        Debug.Log("Connected to main door");
        ConnectScriptsInterraction(connectingSkeleton);
    }

    void DisconnectSkeleton()
    {
        HideOreHealthbar();
        DisconnectScriptsInterraction(connectedSkeleton);
    }

    void DecreaseOreHealth()
    {
        //Debug.Log("Hitted");
        doorHealthDecreaser.DealDamage(100);
    }

    void VisualiseOreHealthbar()
    {
        doorHealthDecreaser.gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }

    void ConnectScriptsInterraction(SkeletonBehavior connectingSkeleton)
    {
        connectingSkeleton.OreHitted += InitiateTacklingAnimation;
        connectingSkeleton.OreHitted += DecreaseOreHealth;
        connectingSkeleton.ObjectConnected += NotifyHealthDecreser;
    }

    void NotifyHealthDecreser()
    {
        doorHealthDecreaser.CalculateDamage(connectedSkeleton);
    }

    void HideOreHealthbar()
    {
        doorHealthDecreaser.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    void DisconnectScriptsInterraction(SkeletonBehavior connectingSkeleton)
    {
        connectingSkeleton.OreHitted -= InitiateTacklingAnimation;
        connectedSkeleton.OreHitted -= DecreaseOreHealth;
    }

    void InitiateTacklingAnimation()
    {
        leftDoorOpener.TryOpenDoor();
        rightDoorOpener.TryOpenDoor();
    }
}
