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

    public void JustShow(int delay)
    {
        fadeEffects.StartEffects();
        if (showingCoroutine != null) { StopCoroutine(showingCoroutine); }
        showingCoroutine = StartCoroutine(DelayedShowing(delay));
    }

    public void JustHide()
    {
        fadeEffects.ResetEffects();

    }

    IEnumerator DelayedShowing()
    {
        yield return new WaitForSeconds(5f);
        JustHide();
        yield return null;
    }

    IEnumerator DelayedShowing(int delay)
    {
        yield return new WaitForSeconds(delay);
        JustHide();
        yield return null;
    }

}
