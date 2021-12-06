using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactManager : MonoBehaviour
{
    [SerializeField] ClickManager ClickManager;
    [SerializeField] CameraController CameraController;
    [SerializeField] Transform mainCharacter;

    public event Action<Transform> OreDetected = delegate { };
    public event Action<Transform, Transform> SkeletonDetected = delegate { };
    public event Action DefractorDetected = delegate { };
    private void Start()
    {
        ClickManager.LMBClicked += ContactObject;
    }

    void ContactObject()
    {
        Transform contactedObject = CameraController.ObservedObject.transform;
        if (contactedObject != null)
        {
            Debug.Log(contactedObject);
            if (contactedObject.GetComponent<IOre>() != null)
            {
                if (OreDetected != null)
                {
                    //send to skeletonInvoker
                    OreDetected(contactedObject);
                }
            } else if (contactedObject.GetComponent<Skeleton>() != null)
            {
                if (SkeletonDetected != null)
                {
                    SkeletonDetected(contactedObject, mainCharacter);
                }
            } else if (contactedObject.parent.GetComponent<Defractor>() != null)
            {
                if (DefractorDetected != null)
                {
                    Debug.Log("Hellohello");
                    DefractorDetected();
                }
            }
        }
    }
}
