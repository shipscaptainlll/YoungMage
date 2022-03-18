using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactManager : MonoBehaviour
{
    [SerializeField] QuickAccessHandController quickAccessHandController;
    [SerializeField] ClickManager ClickManager;
    [SerializeField] CursorManager cursorManager;
    [SerializeField] CameraController CameraController;
    [SerializeField] Transform mainCharacter;
    [SerializeField] PotentialPositioner potentialPositioner;

    public event Action<Transform> TeleporterDetected = delegate { };
    public event Action<Transform> OreDetected = delegate { };
    public event Action<Transform, Transform> SkeletonDetected = delegate { };
    public event Action DefractorDetected = delegate { };
    public event Action<Transform> AlchemistTableDetected = delegate { };
    public event Action MidasCauldronDetected = delegate { };
    public event Action PotentialPositionerActivated = delegate { };
    public event Action PotentialPositionerDeactivated = delegate { };
    private void Start()
    {
        ClickManager.LMBClicked += ContactObject;
    }

    void ContactObject()
    {
        if (!cursorManager.SomethingOpened)
        {
            Transform contactedObject = CameraController.ObservedObject.transform;
            if (contactedObject != null)
            {
                Debug.Log(contactedObject);
                if (contactedObject.GetComponent<Portal2>() != null)
                {
                    if (TeleporterDetected != null)
                    {
                        TeleporterDetected(contactedObject);
                    }
                }
                if (contactedObject.GetComponent<IOre>() != null && quickAccessHandController.CurrentCustomID == 10)
                {
                    if (OreDetected != null)
                    {
                        if (potentialPositioner.IsActive) { PotentialPositionerDeactivated(); }
                        OreDetected(contactedObject);
                    }
                }
                else if (contactedObject.GetComponent<Skeleton>() != null && quickAccessHandController.CurrentCustomID == 10)
                {
                    if (SkeletonDetected != null)
                    {
                        TriggerPotentialPositioner(contactedObject);
                        SkeletonDetected(contactedObject, mainCharacter);
                    }
                }
                else if (contactedObject.parent.GetComponent<Defractor>() != null)
                {
                    if (DefractorDetected != null)
                    {
                        DefractorDetected();
                    }
                }
                else if (contactedObject.parent.GetComponent<MidasCauldron>() != null)
                {
                    if (MidasCauldronDetected != null)
                    {
                        MidasCauldronDetected();
                    }
                }
                else if (contactedObject.GetComponent<TransmutationResourceChoose>() != null)
                {
                    if (AlchemistTableDetected != null)
                    {
                        AlchemistTableDetected(contactedObject);
                    }
                }
                else if (contactedObject.GetComponent<AlchemistPotentialProduct>() != null)
                {
                    Debug.Log("Potential product is visible");
                }
                else if (contactedObject.GetComponent<Portal2>() != null)
                {
                    Debug.Log("looking at portal");
                }
            }
        }
    }

    void TriggerPotentialPositioner(Transform contactedObject)
    {
        if (contactedObject.GetComponent<SkeletonBehavior>().Activity != "ChasingMage")
        {
            if (PotentialPositionerActivated != null)
            {
                PotentialPositionerActivated();
            }
        }
        else if (contactedObject.GetComponent<SkeletonBehavior>().Activity == "ChasingMage")
        {
            if (PotentialPositionerDeactivated != null)
            {
                PotentialPositionerDeactivated();
            }
        }
    }
}
