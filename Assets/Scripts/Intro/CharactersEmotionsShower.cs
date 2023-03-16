using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersEmotionsShower : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void ShowIdle()
    {
        animator.Play("Idle");
    }

    public void ShowAnEmotion(int currentMessageIndex)
    {
        //animator.Play("SpeakingExplaining");
        if (currentMessageIndex == 1) { ShowSpeakingExplaining(); }
        if (currentMessageIndex == 2) { ShowSpeakingExplaining(); }
        if (currentMessageIndex == 3) { ShowSpeakingReject(); }
        if (currentMessageIndex == 8) { ShowSpeakingExplaining(); }
        if (currentMessageIndex == 9) { ShowSpeakingExplaining(); }
        if (currentMessageIndex == 10) { ShowSpeakingExplaining(); }
        if (currentMessageIndex == 11) { ShowSpeakingItalian(); }
        if (currentMessageIndex == 12) { ShowSpeakingReject(); }
    }

    public void ShowSpeakingExplaining()
    {
        animator.Play("SpeakingExplaining");
    }

    public void ShowSpeakingItalian()
    {
        animator.Play("SpeakingItalian");
    }

    public void ShowSpeakingReject()
    {
        animator.Play("SpeakingReject");
    }
}
