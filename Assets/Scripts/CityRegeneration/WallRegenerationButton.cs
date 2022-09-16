using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallRegenerationButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isHolded;
    float buttonDownTime;

    public float ButtonDownTime { get { return buttonDownTime; } }

    public event Action ButtonDown = delegate { };
    public event Action ButtonUp = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        buttonDownTime = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolded)
        {
            buttonDownTime += Time.deltaTime * 10;
            if (ButtonDown != null) { ButtonDown(); }
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        buttonDownTime = 10;
        isHolded = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isHolded = false;
        buttonDownTime = 10;
        if (ButtonUp != null) { ButtonUp(); }
    }

}
