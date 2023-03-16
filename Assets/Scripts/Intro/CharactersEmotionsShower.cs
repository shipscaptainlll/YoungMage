using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersEmotionsShower : MonoBehaviour
{
    [SerializeField] Animator soldierAnimator;
    [SerializeField] Animator mageAnimator;

    public void ShowIdle()
    {
        soldierAnimator.Play("Idle");
        mageAnimator.Play("IdleSleeping");
    }

    public void ShowAnEmotion(int currentMessageIndex)
    {
        //animator.Play("SpeakingExplaining");
        if (currentMessageIndex == 1) { ShowSoldierExplaining(); ShowMageExplaining(); }
        if (currentMessageIndex == 2) { ShowSoldierExplaining(); ShowMageExplaining(); }
        if (currentMessageIndex == 3) { ShowSoldierReject(); ShowMageNodding(); }
        if (currentMessageIndex == 8) { ShowSoldierExplaining(); ShowMageExplaining(); }
        if (currentMessageIndex == 9) { ShowSoldierExplaining(); ShowMageExplaining(); }
        if (currentMessageIndex == 10) { ShowSoldierExplaining(); ShowMageExplaining(); }
        if (currentMessageIndex == 11) { ShowSoldierSpeakingItalian(); ShowMageNodding(); }
        if (currentMessageIndex == 12) { ShowSoldierReject(); ShowMageShowing(); }
    }

    public void ShowSoldierExplaining()
    {
        soldierAnimator.CrossFade("SpeakingExplaining", 0.1f);
    }

    public void ShowSoldierSpeakingItalian()
    {
        soldierAnimator.CrossFade("SpeakingItalian", 0.1f);
    }

    public void ShowSoldierReject()
    {
        soldierAnimator.CrossFade("SpeakingReject", 0.1f);
    }

    public void ShowMageExplaining()
    {
        mageAnimator.CrossFade("SpeakingSitting", 0.1f);
    }

    public void ShowMageNodding()
    {
        mageAnimator.CrossFade("Nodding", 0.1f);
    }

    public void ShowMageShowing()
    {
        mageAnimator.CrossFade("SittingShowing", 0.1f);
    }

    public void ShowMageSleeping()
    {
        mageAnimator.CrossFade("SpeakingExplaining", 0.1f);
    }
}
