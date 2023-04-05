using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationDesintegrationModeActivator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateDesintegrationMode()
    {
        CursorManager.ForceCursorDisabled();
    }

    public void DeactivateDesintegrationMode()
    {
        CursorManager.ForceCursorEnabled();
    }
}
