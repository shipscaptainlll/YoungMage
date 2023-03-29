using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallRegenerationButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isHolded;
    float buttonDownTime;
    private Coroutine m_countingHealthCoroutine;

    public float ButtonDownTime { get { return buttonDownTime; } }

    public event Action ButtonDown = delegate { };
    public event Action ButtonUp = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        buttonDownTime = 1;
    }

    IEnumerator CountingHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            Debug.Log("coroutine is working");
            if (ButtonDown != null) { ButtonDown(); }
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        m_countingHealthCoroutine = StartCoroutine(CountingHealth());
        
        buttonDownTime = 1;
        isHolded = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (m_countingHealthCoroutine != null)
        {
            StopCoroutine(m_countingHealthCoroutine);
            m_countingHealthCoroutine = null;
        }
        
        
        isHolded = false;
        buttonDownTime = 1;
        if (ButtonUp != null) { ButtonUp(); }
    }

}
