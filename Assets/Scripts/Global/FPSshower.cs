using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSshower : MonoBehaviour
{
    [SerializeField] private Text m_text;
    [SerializeField] private bool m_enabled;
    [SerializeField] private CanvasGroup m_canvasGroup;
    private int m_count;

    public bool EnabledFPS { 
        
        get => m_enabled;
        
        set
        {
            m_enabled = value;
            ActivateVisibility();
        }
    }

    void Start()
    {
        ActivateVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_enabled)
        {
            m_count = (int)(1f / Time.unscaledDeltaTime);
            m_text.text = m_count.ToString();
        }
    }

    void ActivateVisibility()
    {
        if (m_enabled == true)
        {
            m_canvasGroup.alpha = 1;
        }
        else
        {
            m_canvasGroup.alpha = 0;
        }
    }
}
