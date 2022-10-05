using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingYoungmage : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] PersonMovement personMovement;
    [SerializeField] Animator mageAnimator;
    [SerializeField] float crouchingSpeed;
    float normalSpeed;
    bool isCrouching;

    void Start()
    {
        normalSpeed = personMovement.BasicSpeed;
    }

    bool IsCrouching
    {
        get { return isCrouching; }
        set { isCrouching = value;
            NotifyCharacterScript();
        }
    }

    public void StartCrouching()
    {
        if (personMovement.SkeletonAttached)
        {
            mageAnimator.Play("StartCrouchingYoung");
            IsCrouching = true;
        }
        
    }

    public void StopCrouching()
    {
        if (personMovement.SkeletonAttached)
        {
            mageAnimator.Play("StopCrouchingYoung");
            IsCrouching = false;
        }
    }

    void NotifyCharacterScript()
    {
        if (isCrouching) { personMovement.BasicSpeed = crouchingSpeed; personMovement.IsCrouching = true; } 
        else { personMovement.BasicSpeed = normalSpeed; personMovement.IsCrouching = false; }
    }
}
