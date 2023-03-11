using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffVisualiser : MonoBehaviour
{
    [SerializeField] Fade fadeEffects;
    bool isActive;

    Coroutine showingCoroutine;
    public bool IsActive
    {
        get { return isActive; }
    }


    public void JustShow()
    {
        fadeEffects.StartEffects();
        if (showingCoroutine != null) { StopCoroutine(showingCoroutine); }
        showingCoroutine = StartCoroutine(DelayedShowing());
    }

    public void JustHide()
    {
        fadeEffects.ResetEffects();

    }

    IEnumerator DelayedShowing()
    {
        yield return new WaitForSeconds(1f);
        JustHide();
        yield return null;
    }

}
