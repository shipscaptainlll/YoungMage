using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindowShower : MonoBehaviour
{
    [SerializeField] Transform upgradesButtonsParent;
    [SerializeField] Transform processorPanelsParent;
    List<Transform> processorPanels = new List<Transform>();

    Transform currentlyOpenedPanel;
    Transform nextOpenPanel;
    void Start()
    {
        initializeUpgradesButtons();
        initializeProcessorPanels();
    }

    

    void showPanel(int CustomID)
    {
        for (int i = 0; i < processorPanels.Count; i++)
        {
            if (processorPanels[i].GetComponent<IProcessorPanel>().CustomID == CustomID)
            {
                closeCurrentPanel();
                processorPanels[i].GetComponent<CanvasGroup>().alpha = 1;
                processorPanels[i].SetAsLastSibling();
                currentlyOpenedPanel = processorPanels[i];
                break;
            }
        }
    }

    void closeCurrentPanel()
    {
        if (currentlyOpenedPanel != null)
        {
            currentlyOpenedPanel.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    void initializeUpgradesButtons()
    {
        foreach (Transform slot in upgradesButtonsParent)
        {
            slot.GetComponent<IUpgradesElement>().buttonClicked += showPanel;
        }
    }

    void initializeProcessorPanels()
    {
        foreach (Transform slot in processorPanelsParent)
        {
            processorPanels.Add(slot);
        }
    }
}
