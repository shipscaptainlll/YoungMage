using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBlocker : MonoBehaviour
{
    [SerializeField] PanelsManager panelsManager;
    [SerializeField] Transform quickAccessPanel;
    [SerializeField] SavePanel savePanel;

    public void BlockUI()
    {
        panelsManager.EscapeMenuBlocked = true;
        CursorManager.ForceCursorEnabled();

    }

    public void BlockQuickAccessPanel()
    {
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void BlockSavingLoading()
    {
        savePanel.IsTutorialMode = true;
    }

    public void UnBlockUI()
    {
        panelsManager.EscapeMenuBlocked = false;
        CursorManager.ForceCursorDisabled();
    }

    public void UnBlockQuickAccessPanel()
    {
        quickAccessPanel.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void UnblockSavingLoading()
    {
        savePanel.IsTutorialMode = false;
    }

}
