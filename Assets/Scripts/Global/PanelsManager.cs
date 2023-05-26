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
    [SerializeField] Transform m_transmutationInventoryPanel;
    [SerializeField] Transform m_transmutationSlotsPanel;
    [SerializeField] Transform m_transmutationRecipesPanel;
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
    [SerializeField] Transform tutorialPanel;
    [SerializeField] Transform introPanel;
    [SerializeField] Transform invisiblePosition;
    [SerializeField] Transform defaultPosition;
    [SerializeField] Transform quickaccessinvisiblePosition;
    [SerializeField] Transform quickaccessdefaultPosition;
    [SerializeField] PortalOpener portalOpener;
    [SerializeField] private CursorManager m_cursorManager;
    [SerializeField] CameraVolumeController m_cameraVolumeController;

    [Header("Sound Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource settingsSubpanelSound;

    [Header("Default positions")] 
    [SerializeField] private Transform m_transmutationDefaultInventoryPosition;
    [SerializeField] private Transform m_transmutationDefaultSlotsPosition;
    [SerializeField] private Transform m_trasnmutationDefaultRecipesPosition;

    AudioSource whooshFirstSound;
    AudioSource inventoryOpenSound;
    Transform currentlyOpened;
    Transform currentSettingsSubpanel;
    Transform nextToOpen;
    float updateSpeed;
    bool introMode;
    bool tutorialMode;
    bool escapeMenuBlocked;
    
    public bool TutorialMode { get { return tutorialMode; } set { tutorialMode = value; } }
    public bool IntroMode { get { return introMode; } set { introMode = value; } }
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
        RelocateFarAway(m_transmutationInventoryPanel);
        
        RelocateFarAway(m_transmutationRecipesPanel);
        currentSettingsSubpanel = graphicsSettingPanel;
        updateSpeed = 0.1f;
        clickManager.IClicked += openInventory;
        clickManager.QClicked += OpenQuestPanel;
        clickManager.EscClicked += ChooseEscapeActions;
        contactManager.MidasCauldronDetected += openMidasCauldronTable;
        contactManager.DefractorDetected += openDefractorTable;
        whooshFirstSound = soundManager.FindSound("WhooshFirst");
        inventoryOpenSound = soundManager.FindSound("InventoryOpening");
        settingsSubpanelSound = soundManager.FindSound("SettingMainChange");
    }
    
    void decideNextState()
    {
        if (currentlyOpened == tutorialPanel || currentlyOpened == introPanel)
        {
            return;
        }
        if (nextToOpen != inventoryPanel)
        {
            whooshFirstSound.Play();
        }
        if (checkIfAlreadyOpened())
        {
            closeCurrentPanel();
            nextToOpen = null;
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
        if (panelToClose != null && panelToClose != tutorialPanel)
        {
            StartCoroutine(CacheClosePanel(panelToClose, false));
            if (panelToClose == m_transmutationInventoryPanel)
            {
                StartCoroutine(CacheClosePanel(m_transmutationSlotsPanel, false));
                StartCoroutine(CacheClosePanel(m_transmutationRecipesPanel, false));
            }
        }
    }

    void ChooseEscapeActions()
    {
        if (tutorialMode || introMode)
        {
            return;
        }
        if (currentlyOpened != null)
        {
            closeCurrentPanel();
            nextToOpen = null;
            m_cursorManager.CheckSomethingOpened();
            m_cameraVolumeController.UnBlurScreen();
        } else
        {
            if (portalOpener.PortalOpened)
            {
                return;
            }
            OpenEscapemenuPanel();
        }
    }

    public void CloseUploadPanel()
    {
        //currentlyOpened.GetComponent<CanvasGroup>().alpha = 0;
        if (nextToOpen != null)
        {
            nextToOpen = null;
        }
        if (currentlyOpened != null)
        {
            RelocateFarAway(currentlyOpened);
        }
        currentlyOpened = null;
        considerQuickAccessPanel();
        m_cameraVolumeController.UnBlurScreen();
    }

    public void closeCurrentPanel()
    {
        if (currentlyOpened != tutorialPanel)
        {
            closePanel(currentlyOpened);
            currentlyOpened = null;
            considerQuickAccessPanel();
        }
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
        if (currentlyOpened != escapeMenuPanel && !tutorialMode)
        {
            nextToOpen = inventoryPanel;
            inventoryOpenSound.Play();
            decideNextState();
        }
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
        if (tutorialMode)
        {
            return;
        }
        
        if (!escapeMenuBlocked)
        {
            m_cameraVolumeController.BlurScreen();
            nextToOpen = escapeMenuPanel;
            decideNextState();
        }
    }

    public void OpenSettingsPanel()
    {
        if (tutorialMode)
        {
            return;
        }
        nextToOpen = settingsMenuPanel;
        decideNextState();
    }

    public void ManageSettingsPanel(String subpanelName)
    {
        
        switch (subpanelName)
        {
            case "graphicsPanel":
                if (currentSettingsSubpanel != graphicsSettingPanel && currentSettingsSubpanel != null) { StartCoroutine(CacheClosePanel(currentSettingsSubpanel, true)); settingsSubpanelSound.Play(); }
                StartCoroutine(CacheOpenPanel(graphicsSettingPanel));
                showOnForeground(graphicsSettingPanel);
                currentSettingsSubpanel = graphicsSettingPanel;
                
                break;
            case "audioPanel":
                if (currentSettingsSubpanel != audioSettingsPanel && currentSettingsSubpanel != null) { StartCoroutine(CacheClosePanel(currentSettingsSubpanel, true)); settingsSubpanelSound.Play(); }
                StartCoroutine(CacheOpenPanel(audioSettingsPanel));
                showOnForeground(audioSettingsPanel);
                currentSettingsSubpanel = audioSettingsPanel;
                
                break;
            case "controlsPanel":
                if (currentSettingsSubpanel != controlsSettingsPanel && currentSettingsSubpanel != null) { StartCoroutine(CacheClosePanel(currentSettingsSubpanel, true)); settingsSubpanelSound.Play(); }
                StartCoroutine(CacheOpenPanel(controlsSettingsPanel));
                showOnForeground(controlsSettingsPanel);
                currentSettingsSubpanel = controlsSettingsPanel;
                
                break;
            case "miscellaneousPanel":
                if (currentSettingsSubpanel != miscellaneousSettingsPanel && currentSettingsSubpanel != null) { StartCoroutine(CacheClosePanel(currentSettingsSubpanel, true)); settingsSubpanelSound.Play(); }
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
        if (currentlyOpened != escapeMenuPanel)
        {
            nextToOpen = questPanel;
            decideNextState();
        }
    }

    public void OpenTransmutationPanel()
    {
        nextToOpen = m_transmutationInventoryPanel;
        
        StartCoroutine(CacheOpenPanel(m_transmutationSlotsPanel));
        StartCoroutine(CacheOpenPanel(m_transmutationRecipesPanel));
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

    public void OpenTutorialPanel()
    {
        Debug.Log("currently opened panel is " + currentlyOpened + " and next to open is " + nextToOpen);
        StopAllCoroutines();
        tutorialMode = true;
        decideNextState();
    }

    public void CloseTutorialPanel()
    {
        tutorialMode = false;
        if (currentlyOpened == tutorialPanel)
        {
            Debug.Log("exited tutorial");
            StartCoroutine(CacheClosePanel(currentlyOpened, false));
            currentlyOpened = null;
            closeCurrentPanel();
            nextToOpen = null;
            
            
            considerQuickAccessPanel();
        }
        
    }

    public void OpenIntroPanel()
    {
        nextToOpen = introPanel;
        decideNextState();
    }

    public void CloseIntroPanel()
    {
        Debug.Log("we are here2 ");
        if (currentlyOpened != null) { StartCoroutine(CacheClosePanel(currentlyOpened, false)); }
        currentlyOpened = null;
        nextToOpen = null;
        considerQuickAccessPanel();
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
        if (panelToMove == m_transmutationInventoryPanel)
        {
            m_transmutationInventoryPanel.position = m_transmutationDefaultInventoryPosition.position;
            m_transmutationSlotsPanel.position = m_transmutationDefaultSlotsPosition.position;
            m_transmutationRecipesPanel.position = m_trasnmutationDefaultRecipesPosition.position;
        }
        else
        {
            
            panelToMove.position = defaultPosition.position;
        }
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
