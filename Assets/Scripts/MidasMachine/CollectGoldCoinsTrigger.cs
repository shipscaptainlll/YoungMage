using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGoldCoinsTrigger : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] PressEVisualiser pressEVisualiser;
    [SerializeField] ClickManager clickManager;
    [SerializeField] MidasCoinsCatcher midasCoinsCatcher;
    bool isActive;

    public bool IsActive { get { return isActive; } }

    public event Action GoldCollected = delegate { };

    void Start()
    {
        clickManager.EClicked += CollectGold;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 11 && cameraController.ObservedObject.transform != null)
        {
            //Debug.Log(cameraController.ObservedObject.collider.gameObject);
            if (!isActive && cameraController.ObservedObject.collider.GetComponent<AdditionalCoinsCatcher>() != null)
            {
                isActive = true;
                pressEVisualiser.JustShow();
            }
            else if (isActive && cameraController.ObservedObject.collider.GetComponent<AdditionalCoinsCatcher>() == null)
            {
                isActive = false;
                pressEVisualiser.JustHide();
            }
        }
        
    }

    void CollectGold()
    {
        if (isActive)
        {
            if (GoldCollected != null)
            {
                GoldCollected();
            }
            midasCoinsCatcher.CollectAccumulatedGold();
        }
    }
}
