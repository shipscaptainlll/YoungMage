using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationHandController : MonoBehaviour
{
    [SerializeField] private Animator m_handAnimator;
    [SerializeField] private ParticleSystem m_transmutationPS;
    [SerializeField] private ParticleSystem m_impactPS;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void ShowHandTransmutation()
    {
        m_handAnimator.enabled = true;
        m_transmutationPS.gameObject.SetActive(true);
        m_transmutationPS.Play();
        m_handAnimator.CrossFade("Armature_Transmutationg", 0.1f);
    }

    public void ShowHandProcessing()
    {
        m_handAnimator.CrossFade("Armature_TransmutationImpact 1", 0.1f);
    }

    public void ShowImpact()
    {
        m_impactPS.Play();
        m_handAnimator.CrossFade("Armature_TransmutationImpact", 0.1f);
    }
    
    public void HideHand()
    {
        m_handAnimator.CrossFade("Armature_TPose_001", 0.1f);
        m_handAnimator.enabled = false;
        
        m_transmutationPS.Stop();
        m_transmutationPS.gameObject.SetActive(false);
        
        
        gameObject.SetActive(false);
    }
}
