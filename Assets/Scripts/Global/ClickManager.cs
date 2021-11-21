using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public event Action LMBClicked = delegate { };
    public event Action RMBClicked = delegate { };
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (LMBClicked != null)
            {
                LMBClicked();
            }
        } else if (Input.GetMouseButtonDown(1))
        {
            if (RMBClicked != null)
            {
                RMBClicked();
            }
        }
    }
}
