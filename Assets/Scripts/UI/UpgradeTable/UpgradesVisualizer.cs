using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesVisualizer : MonoBehaviour
{
    [SerializeField] Transform stonehandsUpgradePanel;
    [SerializeField] Transform leggingsUpgradePanel;
    [SerializeField] Transform platearmorUpgradePanel;
    [SerializeField] Transform shoesUpgradePanel;
    [SerializeField] Transform helmUpgradePanel;
    [SerializeField] Transform beltUpgradePanel;
    [SerializeField] Transform bracersUpgradePanel;
    [SerializeField] StoneOreCounter stoneOreCounter;
    [SerializeField] MetalOreCounter metalOreCounter;
    [SerializeField] CursedOreCounter cursedOreCounter;
    [SerializeField] LeggingsCounter leggingsCounter;
    [SerializeField] PlateArmorCounter plateArmorCounter;
    [SerializeField] ShoesCounter shoesCounter;
    [SerializeField] HelmCounter helmCounter;
    [SerializeField] BracersCounter bracersCounter;
    List<Transform> openedPanels = new List<Transform>();

    void Start()
    {
        stoneOreCounter.StoneOreCreated += openStonehandsPanel;
        metalOreCounter.MetalOreCreated += openLeggingsPanel;
        cursedOreCounter.CursedOreCreated += openBracersPanel;
        leggingsCounter.ItemFirstCreated += openPlateArmorPanel;
        plateArmorCounter.ItemFirstCreated += openShoesPanel;
        shoesCounter.ItemFirstCreated += openHelmPanel;
        helmCounter.ItemFirstCreated += openBeltPanel;
    }

    void openStonehandsPanel()
    {
        stonehandsUpgradePanel.gameObject.SetActive(true);
        openedPanels.Add(stonehandsUpgradePanel);
        for (int i = 0; i < openedPanels.Count; i++)
        {
            Debug.Log(openedPanels[i]);
        }
    }

    void openLeggingsPanel()
    {
        leggingsUpgradePanel.gameObject.SetActive(true);
    }

    void openPlateArmorPanel()
    {
        platearmorUpgradePanel.gameObject.SetActive(true);
    }

    void openShoesPanel()
    {
        shoesUpgradePanel.gameObject.SetActive(true);
    }

    void openHelmPanel()
    {
        helmUpgradePanel.gameObject.SetActive(true);
    }

    void openBeltPanel()
    {
        beltUpgradePanel.gameObject.SetActive(true);
    }

    void openBracersPanel()
    {
        bracersUpgradePanel.gameObject.SetActive(true);
    }
}
