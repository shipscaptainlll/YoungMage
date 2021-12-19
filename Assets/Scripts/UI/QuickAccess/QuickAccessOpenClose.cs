using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAccessOpenClose : MonoBehaviour
{
    /*
    [SerializeField] OpenClose openClose;
    [SerializeField] UpgradeTableOpenClose upgradeTableOpenClose;
    [SerializeField] CursorManager cursorManager;
    [SerializeField] Transform panel;
    [SerializeField] Transform invisiblePosition;
    [SerializeField] Transform defaultPosition;
    CanvasGroup panelCanvasgroup;
    float updateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        openClose.InventoryOpened += Close;
        openClose.InventoryClosed += Open;
        upgradeTableOpenClose.UpgradeTableOpened += Close;
        upgradeTableOpenClose.UpgradeTableClosed += Open;
        updateSpeed = 0.1f;
        panelCanvasgroup = panel.GetComponent<CanvasGroup>();
    }
    
    void Open()
    {
        if (!cursorManager.CheckSomethingOpened())
        {
            StartCoroutine(CacheOpenPanel());
        }
    }

    void Close()
    {
        StartCoroutine(CacheClosePanel());
    }

    void RelocateFarAway()
    {
        panel.position = invisiblePosition.position;
    }

    void RelocateDefaultPosition()
    {
        panel.position = defaultPosition.position;
    }

    IEnumerator CacheOpenPanel()
    {
        RelocateDefaultPosition();
        float elapsed = 0;
        float alphaMaxValue = 1;
        float alphaZeroValue = 0;
        if (panelCanvasgroup.alpha < 1)
        {
            while (elapsed < updateSpeed)
            {
                elapsed += Time.deltaTime;
                panelCanvasgroup.alpha = Mathf.Lerp(alphaZeroValue, alphaMaxValue, elapsed / updateSpeed);
                yield return null;
            }
        }
    }
    IEnumerator CacheClosePanel()
    {
        float elapsed = 0;
        float alphaMaxValue = 1;
        float alphaZeroValue = 0;
        if (panelCanvasgroup.alpha > 0)
        {
            while (elapsed < updateSpeed)
            {
                elapsed += Time.deltaTime;
                panelCanvasgroup.alpha = Mathf.Lerp(alphaMaxValue, alphaZeroValue, elapsed / updateSpeed);
                yield return null;
            }
        }
        RelocateFarAway();
    }
    */
}
