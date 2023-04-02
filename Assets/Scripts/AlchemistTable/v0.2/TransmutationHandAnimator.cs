using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationHandAnimator : MonoBehaviour
{
    [SerializeField] private Animator m_handAnimator;
    private bool m_animationFinished;
    private bool m_animationBeingPlayed;

    public bool AnimationFinished { get => m_animationFinished; set => m_animationFinished = value; }
    public bool AnimationBeingFinished { get => m_animationBeingPlayed; set => m_animationBeingPlayed = value; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowTakingObject()
    {
        //rewriteIt
        m_animationBeingPlayed = true;
        m_handAnimator.Play("TakeObject");
    }

    public void FinishAnimation()
    {
        m_animationBeingPlayed = false;
        m_animationFinished = true;
    }

    public void ShowPlacingObject()
    {
        //rewriteIt
        m_animationBeingPlayed = true;
        m_handAnimator.Play("PlaceObject");
    }

    public void InterruptAnimation()
    {
        //rewriteIt
        m_handAnimator.Play("Idle");
        m_animationBeingPlayed = false;
        m_animationFinished = true;
    }
}
