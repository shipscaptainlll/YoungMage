using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    [SerializeField] ContactManager contactManager;
    [SerializeField] ClickManager clickManager;
    [SerializeField] Transform inventoryPanel;
    [SerializeField] Transform upgradeTablePanel;
    [SerializeField] Transform midasCauldronTablePanel;
    [SerializeField] Transform defractorTablePanel;
    [SerializeField] Transform quickAccessPanel;
    [SerializeField] Transform invisiblePosition;
    [SerializeField] Transform defaultPosition;
    [SerializeField] Transform quickaccessinvisiblePosition;
    [SerializeField] Transform quickaccessdefaultPosition;
    Transform currentlyOpened;
    Transform nextToOpen;
    float updateSpeed;

    public Transform CurrentlyOpened
    {
        get
        {
            return currentlyOpened;
        }
    }

    public event Action PanelsUpdated = delegate { };
    void Start()
    {
        updateSpeed = 0.1f;
        clickManager.IClicked += openInventory;
        clickManager.EscClicked += closeCurrentPanel;
        contactManager.MidasCauldronDetected += openMidasCauldronTable;
        contactManager.DefractorDetected += openDefractorTable;
    }

    void decideNextState()
    {
        if (checkIfAlreadyOpened())
        {
            closeCurrentPanel();
        }
        else if (!checkIfAlreadyOpened())
        {
            if (currentlyOpened != null)
            {
                closePanel(currentlyOpened);
            }
            openPanel(nextToOpen);
            showOnForeground(nextToOpen);
            currentlyOpened = nextToOpen;
        }
        considerQuickAccessPanel();
    }

    bool checkIfAlreadyOpened()
    {
        return (nextToOpen == currentlyOpened);
    }

    void considerQuickAccessPanel()
    {
        
        if (currentlyOpened == null)
        {
            StartCoroutine(openQuickAccess(quickAccessPanel));
        }
        else { StartCoroutine(closeQuickAccess(quickAccessPanel)); }
        notifySubscribers();
    }

    void closePanel(Transform panelToClose)
    {
        if (panelToClose != null)
        {
            StartCoroutine(CacheClosePanel(panelToClose));
        }
    }

    void closeCurrentPanel()
    {
        closePanel(currentlyOpened);
        currentlyOpened = null;
        considerQuickAccessPanel();
    }

    void openPanel(Transform panelToOpen)
    {
        StartCoroutine(CacheOpenPanel(panelToOpen));
    }

    void showOnForeground(Transform panelToForeground)
    {
        panelToForeground.SetAsLastSibling();
    }

    void openInventory()
    {
        nextToOpen = inventoryPanel;
        decideNextState();
    }

    void openUpgradeTable()
    {
        nextToOpen = upgradeTablePanel;
        decideNextState();
    }

    void openMidasCauldronTable()
    {
        nextToOpen = midasCauldronTablePanel;
        decideNextState();
    }

    void openDefractorTable()
    {
        nextToOpen = defractorTablePanel;
        decideNextState();
    }

    IEnumerator CacheOpenPanel(Transform panelToOpen)
    {
        CanvasGroup panelCanvasGroup = panelToOpen.GetComponent<CanvasGroup>();
        RelocateDefaultPosition(panelToOpen);
        float elapsed = 0;
        float alphaMaxValue = 1;
        float alphaZeroValue = 0;
        if (panelCanvasGroup.alpha < 1)
        {
            while (elapsed < updateSpeed)
            {
                elapsed += Time.deltaTime;
                panelCanvasGroup.alpha = Mathf.Lerp(alphaZeroValue, alphaMaxValue, elapsed / updateSpeed);
                yield return null;
            }
        }
    }
    IEnumerator CacheClosePanel(Transform panelToClose)
    {
        CanvasGroup panelCanvasGroup = panelToClose.GetComponent<CanvasGroup>();
        float elapsed = 0;
        float alphaMaxValue = 1;
        float alphaZeroValue = 0;
        if (panelCanvasGroup.alpha > 0)
        {
            while (elapsed < updateSpeed)
            {
                elapsed += Time.deltaTime;
                panelCanvasGroup.alpha = Mathf.Lerp(alphaMaxValue, alphaZeroValue, elapsed / updateSpeed);
                yield return null;
            }
        }
        RelocateFarAway(panelToClose);
    }

    void RelocateFarAway(Transform panelToMove)
    {
        panelToMove.position = invisiblePosition.position;
    }

    void RelocateDefaultPosition(Transform panelToMove)
    {
        panelToMove.position = defaultPosition.position;
    }

    IEnumerator openQuickAccess(Transform panelToOpen)
    {
        CanvasGroup panelCanvasGroup = panelToOpen.GetComponent<CanvasGroup>();
        RelocateQuickAccessDefaultPosition(panelToOpen);
        float elapsed = 0;
        float alphaMaxValue = 1;
        float alphaZeroValue = 0;
        if (panelCanvasGroup.alpha < 1)
        {
            while (elapsed < updateSpeed)
            {
                elapsed += Time.deltaTime;
                panelCanvasGroup.alpha = Mathf.Lerp(alphaZeroValue, alphaMaxValue, elapsed / updateSpeed);
                yield return null;
            }
        }
    }
    IEnumerator closeQuickAccess(Transform panelToClose)
    {
        CanvasGroup panelCanvasGroup = panelToClose.GetComponent<CanvasGroup>();
        float elapsed = 0;
        float alphaMaxValue = 1;
        float alphaZeroValue = 0;
        if (panelCanvasGroup.alpha > 0)
        {
            while (elapsed < updateSpeed)
            {
                elapsed += Time.deltaTime;
                panelCanvasGroup.alpha = Mathf.Lerp(alphaMaxValue, alphaZeroValue, elapsed / updateSpeed);
                yield return null;
            }
        }
        RelocateQuickAccessFarAway(panelToClose);
    }

    void RelocateQuickAccessFarAway(Transform panelToMove)
    {
        panelToMove.position = quickaccessinvisiblePosition.position;
    }

    void RelocateQuickAccessDefaultPosition(Transform panelToMove)
    {
        panelToMove.position = quickaccessdefaultPosition.position;
    }

    void notifySubscribers()
    {
        if (PanelsUpdated != null)
        {
            PanelsUpdated();
        }
    }
}
