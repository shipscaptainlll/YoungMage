using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelsManager : MonoBehaviour
{
    [SerializeField] ContactManager contactManager;
    [SerializeField] ClickManager clickManager;
    [SerializeField] Transform inventoryPanel;
    [SerializeField] Transform escapeMenuPanel;
    [SerializeField] Transform settingsMenuPanel;
    [SerializeField] Transform graphicsSettingPanel;
    [SerializeField] Transform audioSettingsPanel;
    [SerializeField] Transform controlsSettingsPanel;
    [SerializeField] Transform miscellaneousSettingsPanel;
    [SerializeField] Transform saveMenuPanel;
    [SerializeField] Transform loadMenuPanel;
    [SerializeField] Transform questPanel;
    [SerializeField] Transform upgradeTablePanel;
    [SerializeField] Transform midasCauldronTablePanel;
    [SerializeField] Transform defractorTablePanel;
    [SerializeField] Transform quickAccessPanel;
    [SerializeField] Transform invisiblePosition;
    [SerializeField] Transform defaultPosition;
    [SerializeField] Transform quickaccessinvisiblePosition;
    [SerializeField] Transform quickaccessdefaultPosition;

    [Header("Sound Manager")]
    [SerializeField] SoundManager soundManager;

    AudioSource whooshFirstSound;
    Transform currentlyOpened;
    Transform currentSettingsSubpanel;
    Transform nextToOpen;
    float updateSpeed;

    bool escapeMenuBlocked;
    public Transform CurrentlyOpened
    {
        get
        {
            return currentlyOpened;
        }
    }

    public bool EscapeMenuBlocked { get { return escapeMenuBlocked; } set { escapeMenuBlocked = value; } }
    public event Action PanelsUpdated = delegate { };
    void Start()
    {
        RelocateFarAway(settingsMenuPanel);
        RelocateFarAway(inventoryPanel);
        RelocateFarAway(escapeMenuPanel);
        RelocateFarAway(saveMenuPanel);
        RelocateFarAway(loadMenuPanel);
        RelocateFarAway(questPanel);
        RelocateFarAway(upgradeTablePanel);
        RelocateFarAway(midasCauldronTablePanel);
        RelocateFarAway(defractorTablePanel);
        currentSettingsSubpanel = graphicsSettingPanel;
        updateSpeed = 0.1f;
        clickManager.IClicked += openInventory;
        clickManager.QClicked += OpenQuestPanel;
        clickManager.EscClicked += ChooseEscapeActions;
        contactManager.MidasCauldronDetected += openMidasCauldronTable;
        contactManager.DefractorDetected += openDefractorTable;
        whooshFirstSound = soundManager.FindSound("WhooshFirst");
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
        whooshFirstSound.Play();
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
            StartCoroutine(CacheClosePanel(panelToClose, false));
        }
    }

    void ChooseEscapeActions()
    {
        if (currentlyOpened != null)
        {
            closeCurrentPanel();
            CameraVolumeController.UnBlurScreen();
        } else
        {
            OpenEscapemenuPanel();
        }
    }

    public void closeCurrentPanel()
    {
        closePanel(currentlyOpened);
        currentlyOpened = null;
        considerQuickAccessPanel();
    }

    void openPanel(Transform panelToOpen)
    {
        
        RelocateDefaultPosition(panelToOpen);
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

    void OpenEscapemenuPanel()
    {
        if (!escapeMenuBlocked)
        {
            CameraVolumeController.BlurScreen();
            nextToOpen = escapeMenuPanel;
            decideNextState();
        }
    }

    public void OpenSettingsPanel()
    {
        nextToOpen = settingsMenuPanel;
        decideNextState();
    }

    public void ManageSettingsPanel(String subpanelName)
    {
        
        switch (subpanelName)
        {
            case "graphicsPanel":
                if (currentSettingsSubpanel != graphicsSettingPanel && currentSettingsSubpanel != null) { StartCoroutine(CacheClosePanel(currentSettingsSubpanel, true)); }
                StartCoroutine(CacheOpenPanel(graphicsSettingPanel));
                showOnForeground(graphicsSettingPanel);
                currentSettingsSubpanel = graphicsSettingPanel;
                break;
            case "audioPanel":
                if (currentSettingsSubpanel != audioSettingsPanel && currentSettingsSubpanel != null) { StartCoroutine(CacheClosePanel(currentSettingsSubpanel, true)); }
                StartCoroutine(CacheOpenPanel(audioSettingsPanel));
                showOnForeground(audioSettingsPanel);
                currentSettingsSubpanel = audioSettingsPanel;
                break;
            case "controlsPanel":
                if (currentSettingsSubpanel != controlsSettingsPanel && currentSettingsSubpanel != null) { StartCoroutine(CacheClosePanel(currentSettingsSubpanel, true)); }
                StartCoroutine(CacheOpenPanel(controlsSettingsPanel));
                showOnForeground(controlsSettingsPanel);
                currentSettingsSubpanel = controlsSettingsPanel;
                break;
            case "miscellaneousPanel":
                if (currentSettingsSubpanel != miscellaneousSettingsPanel && currentSettingsSubpanel != null) { StartCoroutine(CacheClosePanel(currentSettingsSubpanel, true)); }
                StartCoroutine(CacheOpenPanel(miscellaneousSettingsPanel));
                showOnForeground(miscellaneousSettingsPanel);
                currentSettingsSubpanel = miscellaneousSettingsPanel;
                break;
        }
    }

    public void OpenSavePanel()
    {
        nextToOpen = saveMenuPanel;
        decideNextState();
    }

    public void OpenLoadPanel()
    {
        nextToOpen = loadMenuPanel;
        decideNextState();
    }

    public void OpenQuestPanel()
    {
        nextToOpen = questPanel;
        decideNextState();
    }

    public void OpenMainmenuPanel()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void OpenQuitPanel()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    IEnumerator CacheOpenPanel(Transform panelToOpen)
    {
        CanvasGroup panelCanvasGroup = panelToOpen.GetComponent<CanvasGroup>();
        //RelocateDefaultPosition(panelToOpen);
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
    IEnumerator CacheClosePanel(Transform panelToClose, bool isSubpanel)
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
        if (!isSubpanel) { RelocateFarAway(panelToClose); }
        
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
