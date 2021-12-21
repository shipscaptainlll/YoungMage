using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stoneHandsCreatePanel : MonoBehaviour, ICreatePanel
{
    [SerializeField] Transform connectedUpgradeButton;
    [SerializeField] Button createButton;

    public Transform ConnectedUpgradeButton
    {
        get
        {
            return connectedUpgradeButton;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        createButton.onClick.AddListener(createProduct);
    }

    void createProduct()
    {

    }
}
