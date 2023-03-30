using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterAnimationSphere : MonoBehaviour
{
    [SerializeField] private Animator m_sphereAnimator;
    [SerializeField] private ParticleSystem m_particleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateAnimation()
    {
        m_sphereAnimator.Play("SphereShackle");
    }

    public void ActivateParticles()
    {
        m_particleSystem.Play();
    }
}
