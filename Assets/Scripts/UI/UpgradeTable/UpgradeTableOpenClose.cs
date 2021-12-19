using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTableOpenClose : MonoBehaviour
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] ContactManager contactManager;
    [SerializeField] Transform panel;
    [SerializeField] Transform invisiblePosition;
    [SerializeField] Transform defaultPosition;
    CanvasGroup panelCanvasgroup;
    float updateSpeed;
    bool isOpened;

    public bool IsOpened
    {
        get
        {
            return isOpened;
        }
    }

    public event Action UpgradeTableOpened = delegate { };
    public event Action UpgradeTableClosed = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
        updateSpeed = 0.1f;
        contactManager.UpgradeTableDetected += ChooseOpenClose;
        clickManager.EscClicked += Close;
        panelCanvasgroup = GetComponent<CanvasGroup>();
    }

    void ChooseOpenClose()
    {
        if (isOpened)
        {
            StartCoroutine(CacheClosePanel());
            isOpened = false;
            if (UpgradeTableClosed != null)
            {
                UpgradeTableClosed();
            }
        } else if (!isOpened)
        {
            StartCoroutine(CacheOpenPanel());
            isOpened = true;
            if (UpgradeTableOpened != null)
            {
                UpgradeTableOpened();
            }
        }
    }

    void Close()
    {
        if (isOpened)
        {
            StartCoroutine(CacheClosePanel());
            isOpened = false;
            if (UpgradeTableClosed != null)
            {
                UpgradeTableClosed();
            }
        }
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

}
