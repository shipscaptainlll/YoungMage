using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersEmotionsShower : MonoBehaviour
{
    [SerializeField] Animator soldierAnimator;
    [SerializeField] Animator oldMageAnimator;

    public void ShowIdle()
    {
        soldierAnimator.Play("Idle");
        oldMageAnimator.Play("IdleSitting");
    }

    public void ShowAnEmotion(int currentMessageIndex)
    {
        //animator.Play("SpeakingExplaining");
        if (currentMessageIndex == 1) { ShowSoldierSpeakingExplaining(); ShowMageSpeakingExplaining(); }
        if (currentMessageIndex == 2) { ShowSoldierSpeakingExplaining(); ShowMageSpeakingExplaining(); }
        if (currentMessageIndex == 3) { ShowSoldierSpeakingReject(); ShowMageNodding(); }
        if (currentMessageIndex == 8) { ShowSoldierSpeakingExplaining(); ShowMageSpeakingExplaining(); }
        if (currentMessageIndex == 9) { ShowSoldierSpeakingExplaining(); ShowMageSpeakingExplaining(); }
        if (currentMessageIndex == 10) { ShowSoldierSpeakingExplaining(); ShowMageSpeakingExplaining(); }
        if (currentMessageIndex == 11) { ShowSoldierSpeakingItalian(); ShowMageShowing(); }
        if (currentMessageIndex == 12) { ShowSoldierSpeakingReject(); ShowMageNodding(); }
    }

    public void ShowSoldierSpeakingExplaining()
    {
        soldierAnimator.CrossFade("SpeakingExplaining", 0.1f);
    }

    public void ShowSoldierSpeakingItalian()
    {
        soldierAnimator.CrossFade("SpeakingItalian", 0.1f);
    }

    public void ShowSoldierSpeakingReject()
    {
        soldierAnimator.CrossFade("SpeakingReject", 0.1f);
    }

    public void ShowMageSpeakingExplaining()
    {
        oldMageAnimator.CrossFade("SpeakingSitting", 0.1f);
    }

    public void ShowMageShowing()
    {
        oldMageAnimator.CrossFade("SittingShowing", 0.1f);
    }

    public void ShowMageNodding()
    {
        oldMageAnimator.CrossFade("Nodding", 0.1f);
        //oldMageAnimator.Play("Nodding");
    }
}
