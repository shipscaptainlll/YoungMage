using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTacklingManager : MonoBehaviour
{
    [SerializeField] DoorHealthDecreaser doorHealthDecreaser;
    [SerializeField] DoorOpen leftDoorOpener;
    [SerializeField] DoorOpen rightDoorOpener;
    [SerializeField] int doorLevel;

    [Header("Audio Connection")]
    [SerializeField] SoundManager soundManager;
    AudioSource doorTacklingSound;
    SkeletonBehavior connectedSkeleton;

    bool heatlhIsVisible;
    public bool HealthIsVisible { get { return heatlhIsVisible; } }

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

    public int DoorLevel { get { return doorLevel; } }
    // Start is called before the first frame update
    void Start()
    {
        doorTacklingSound = soundManager.LocateAudioSource("DoorCaveBump", transform);
        
    }

    void ConnectSkeleton(SkeletonBehavior connectingSkeleton)
    {
        VisualiseOreHealthbar();
        //Debug.Log("Connected to main door");
        ConnectScriptsInterraction(connectingSkeleton);
    }

    public void DisconnectSkeleton()
    {
        HideOreHealthbar();
        DisconnectScriptsInterraction(connectedSkeleton);
    }

    void DecreaseOreHealth()
    {
        //Debug.Log("Hitted");
        PlayMiningSound();
        doorHealthDecreaser.DealDamage(100);
    }

    void PlayMiningSound()
    {
        doorTacklingSound.Play();
    }

    public void VisualiseOreHealthbar()
    {
        doorHealthDecreaser.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        heatlhIsVisible = true;
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

    public void HideOreHealthbar()
    {
        doorHealthDecreaser.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        heatlhIsVisible = false;
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
