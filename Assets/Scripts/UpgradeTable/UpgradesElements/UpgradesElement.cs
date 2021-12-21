using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesElement : MonoBehaviour, IUpgradesElement
{
    [SerializeField] int customID;
    [SerializeField] Button chooseUpgradeButton;

    public int CustomID
    {
        get
        {
            return customID;
        }
    }

    public event Action<int> buttonClicked = delegate { };
    void Start()
    {
        chooseUpgradeButton.onClick.AddListener(sendNotification);
    }

    void sendNotification()
    {
        if (buttonClicked != null)
        {
            buttonClicked(customID);
        }
    }
}
