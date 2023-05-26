using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelsManagerMainmenu : MonoBehaviour
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] LoadGameScene loadGameScene;
    [SerializeField] Transform escapeMenuPanel;
    [SerializeField] Transform settingsMenuPanel;
    [SerializeField] Transform graphicsSettingPanel;
    [SerializeField] Transform audioSettingsPanel;
    [SerializeField] Transform controlsSettingsPanel;
    [SerializeField] Transform miscellaneousSettingsPanel;
    [SerializeField] Transform loadMenuPanel;
    [SerializeField] Transform newGamePanel;
    [SerializeField] Transform creditsPanel;
    [SerializeField] Transform invisiblePosition;
    [SerializeField] Transform defaultPosition;
    Transform currentlyOpened;
    Transform currentSettingsSubpanel;
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
        currentSettingsSubpanel = graphicsSettingPanel;
        OpenEscapemenuPanel();
        updateSpeed = 0.1f;
        clickManager.EscClicked += ChooseEscapeActions;
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
    }

    bool checkIfAlreadyOpened()
    {
        return (nextToOpen == currentlyOpened);
    }

    void closePanel(Transform panelToClose)
    {

        if (panelToClose != null)
        {
            StartCoroutine(CacheClosePanel(panelToClose, false));
        }
    }

    public void HideBeforeLoading()
    {
        closeCurrentPanel();
    }

    void ChooseEscapeActions()
    {
        if (currentlyOpened != null)
        {
            closeCurrentPanel();
            StartCoroutine(ReturnToMainmenu());
        } else
        {
            OpenEscapemenuPanel();
        }
    }

    IEnumerator ReturnToMainmenu()
    {
        yield return new WaitForSeconds(updateSpeed);
        OpenEscapemenuPanel();
    }

    public void closeCurrentPanel()
    {
        closePanel(currentlyOpened);
        currentlyOpened = null;
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

    void OpenEscapemenuPanel()
    {
        nextToOpen = escapeMenuPanel;
        decideNextState();
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

    public void OpenLoadPanel()
    {
        nextToOpen = loadMenuPanel;
        decideNextState();
    }

    public void OpenNewgamePanel()
    {
        loadGameScene.ShowGameScene();
        escapeMenuPanel.GetComponent<CanvasGroup>().alpha = 0;
        //nextToOpen = newGamePanel;
        //decideNextState();
    }

    public void OpenCreditsPanel()
    {
        nextToOpen = creditsPanel;
        decideNextState();
    }

    public void OpenMainmenuPanel()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
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
        if (currentlyOpened == escapeMenuPanel)
        {
            updateSpeed = 0;
        }
        else
        {
            updateSpeed = 0.1f;
        }
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
        updateSpeed = 0.1f;
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

    }

    void notifySubscribers()
    {
        if (PanelsUpdated != null)
        {
            PanelsUpdated();
        }
    }
}
