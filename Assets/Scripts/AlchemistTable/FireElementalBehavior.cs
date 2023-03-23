using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElementalBehavior : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem conjurationPSFirst;
    [SerializeField] private ParticleSystem conjurationPSSecond;

    public void ShowIdleAnimation()
    {
        animator.CrossFade("Armature_Idle", 0.2f);
    }

    public void ShowConjuringAnimation()
    {
        animator.CrossFade("Armature_Summoning", 0.2f);
    }

    public void ShowConjurationPS()
    {
        conjurationPSFirst.Play();
        conjurationPSSecond.Play();
    }
    
    
}
