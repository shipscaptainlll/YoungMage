using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactManager : MonoBehaviour
{
    [SerializeField] ClickManager ClickManager;
    [SerializeField] CameraController CameraController;

    public event Action<Transform> OreDetected = delegate { };
    private void Start()
    {
        ClickManager.LMBClicked += ContactObject;
    }

    void ContactObject()
    {
        Transform contactedObject = CameraController.ObservedObject.transform;
        if (contactedObject != null)
        {
            if (contactedObject.GetComponent<IOre>() != null)
            {
                if (OreDetected != null)
                {
                    //send to skeletonInvoker
                    OreDetected(contactedObject);
                }
            }
        }
    }
}
