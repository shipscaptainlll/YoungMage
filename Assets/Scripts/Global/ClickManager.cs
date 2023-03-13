using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public event Action LMBClicked = delegate { };
    public event Action RMBClicked = delegate { };
    public event Action VClicked = delegate { };
    public event Action IClicked = delegate { };
    public event Action AClicked = delegate { };
    public event Action DClicked = delegate { };
    public event Action EscClicked = delegate { };
    public event Action PClicked = delegate { };
    public event Action EClicked = delegate { };
    public event Action RHolded = delegate { };
    public event Action RUp = delegate { };
    public event Action QClicked = delegate { };
    public event Action QLClicked = delegate { };
    public event Action TabClicked = delegate { };
    public event Action EnterClicked = delegate { };
    public event Action SpaceClicked = delegate { };
    public event Action<int> OneClicked = delegate { };
    public event Action<int> TwoClicked = delegate { };
    public event Action<int> ThreeClicked = delegate { };
    public event Action<int> FourClicked = delegate { };
    public event Action<int> FiveClicked = delegate { };
    public event Action<int> SixClicked = delegate { };
    public event Action<int> SevenClicked = delegate { };
    public event Action<int> EightClicked = delegate { };
    public event Action<int> NineClicked = delegate { };
    public event Action FFiveClicked = delegate { };
    public event Action FSixClicked = delegate { };
    public event Action FNineClicked = delegate { };
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
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (VClicked != null)
            {
                VClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (IClicked != null)
            {
                IClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EscClicked != null)
            {
                EscClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PClicked != null)
            {
                PClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (EClicked != null)
            {
                EClicked();
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            if (RHolded != null)
            {
                RHolded();
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (RUp != null)
            {
                RUp();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (QClicked != null)
            {
                QClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (TabClicked != null)
            {
                TabClicked();
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (QLClicked != null)
            {
                QLClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (AClicked != null)
            {
                AClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (DClicked != null)
            {
                DClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (EnterClicked != null)
            {
                EnterClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (SpaceClicked != null)
            {
                SpaceClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (OneClicked != null)
            {
                OneClicked(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (TwoClicked != null)
            {
                TwoClicked(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (ThreeClicked != null)
            {
                ThreeClicked(3);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            if (FourClicked != null)
            {
                FourClicked(4);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            if (FiveClicked != null)
            {
                FiveClicked(5);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            if (SixClicked != null)
            {
                SixClicked(6);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (SevenClicked != null)
            {
                SevenClicked(7);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (EightClicked != null)
            {
                EightClicked(8);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            if (NineClicked != null)
            {
                NineClicked(9);
            }
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (FFiveClicked != null)
            {
                FFiveClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            if (FSixClicked != null)
            {
                FSixClicked();
            }
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            if (FNineClicked != null)
            {
                FNineClicked();
            }
        }
    }
}
