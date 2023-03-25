using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonsScanner : MonoBehaviour
{
    [SerializeField] private Animator m_scannerAnimator;
    [SerializeField] private ParticleSystem m_scannerPS;

    public void ActivateScanner()
    {
        m_scannerAnimator.enabled = true;
        m_scannerPS.Play();
        m_scannerAnimator.CrossFade("Scanning", 0.1f);
    }
    
    public void DeactivateScanner()
    {
        m_scannerPS.Stop();
        m_scannerAnimator.CrossFade("Idle", 0.1f);
    }
    
}
