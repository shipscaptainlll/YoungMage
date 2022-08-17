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
    [SerializeField] AttachObjectSkeleton attachObjectSkeleton;

    Transform contactedSkeleton;
    public event Action<Transform> TeleporterDetected = delegate { };
    public event Action<Transform> OreDetected = delegate { };
    public event Action<Transform, Transform> SkeletonDetected = delegate { };
    public event Action DefractorDetected = delegate { };
    public event Action<Transform> AlchemistTableDetected = delegate { };
    public event Action MidasCauldronDetected = delegate { };
    public event Action PotentialPositionerActivated = delegate { };
    public event Action PotentialPositionerDeactivated = delegate { };
    public event Action CityRegenerationEntered = delegate { };
    private void Start()
    {
        ClickManager.LMBClicked += ContactObject;
    }

    void ContactObject()
    {
        if (!cursorManager.SomethingOpened)
        {
            Transform contactedObject = CameraController.ObservedObject.transform;
            Debug.Log(contactedObject);
            if (contactedObject != null)
            {
                //Debug.Log(contactedObject);
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
                if (contactedObject.parent.GetComponent<IOre>() != null && quickAccessHandController.CurrentCustomID == 10)
                {
                    if (OreDetected != null)
                    {
                        if (potentialPositioner.IsActive) { PotentialPositionerDeactivated(); }
                        OreDetected(contactedObject.parent);
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
                else if (contactedObject.parent.GetComponent<Skeleton>() != null && quickAccessHandController.CurrentCustomID == 10)
                {
                    //Debug.Log("Found Skeleton");
                    SkeletonBehavior skeletonScript = contactedObject.transform.parent.GetComponent<SkeletonBehavior>();
                    if (skeletonScript.NavigationTarget != mainCharacter) { skeletonScript.NavigationTarget = mainCharacter; contactedSkeleton = contactedObject.parent; Debug.Log("told skeleton to follow mage"); }
                    else if (skeletonScript.NavigationTarget == mainCharacter) { skeletonScript.NavigationTarget = null; contactedSkeleton = null; Debug.Log("told skeleton to stop following mage"); }
                    if (SkeletonDetected != null)
                    {

                        TriggerPotentialPositioner(contactedObject.parent);
                        SkeletonDetected(contactedObject.parent, mainCharacter);
                    }
                }
                else if (contactedObject.parent.GetComponent<Skeleton>() != null
                    && (quickAccessHandController.CurrentCustomID == 11
                    || quickAccessHandController.CurrentCustomID == 12
                    || quickAccessHandController.CurrentCustomID == 13
                    || quickAccessHandController.CurrentCustomID == 14
                    || quickAccessHandController.CurrentCustomID == 15
                    || quickAccessHandController.CurrentCustomID == 16
                    || quickAccessHandController.CurrentCustomID == 17))
                {
                    attachObjectSkeleton.AttachObject(contactedObject.parent.GetComponent<SkeletonBehavior>(), quickAccessHandController.CurrentCustomID);
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
                    //Debug.Log("Potential product is visible");
                }
                else if (contactedObject.parent.Find("ChooseResource") != null && contactedObject.parent.Find("ChooseResource").GetComponent<TransmutationResourceChoose>() != null)
                {
                    if (AlchemistTableDetected != null)
                    {
                        AlchemistTableDetected(contactedObject.parent.Find("ChooseResource"));
                    }
                }
                else if (contactedObject.GetComponent<Portal2>() != null)
                {
                    //Debug.Log("looking at portal");
                }
                else if (contactedObject.GetComponent<CityRegenerationEnter>() != null)
                {
                    if (CameraController.ObservedObject.distance < CityRegenerationEnter.CameraMinimalDistance)
                    {
                        {
                            if (CityRegenerationEntered != null) { CityRegenerationEntered(); }
                        }
                    }
                    
                    
                    
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
