using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingSkeleton : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] SkeletonBehavior skeletonBehavior;
    [SerializeField] Animator skeletonAnimator;
    [SerializeField] float crouchingSpeed;
    [SerializeField] float normalSoundVolume;
    [SerializeField] float normalSoundPitch;
    float normalSpeed;
    
    bool isCrouching;

    void Start()
    {
        normalSpeed = skeletonBehavior.Speed;
    }

    bool IsCrouching
    {
        get { return isCrouching; }
        set
        {
            isCrouching = value;
            NotifySkeletonScript();
        }
    }

    public void StartCrouching()
    {
        IsCrouching = true;

    }

    public void StopCrouching()
    {
        IsCrouching = false;
    }

    void NotifySkeletonScript()
    {
        if (isCrouching) { skeletonBehavior.Speed = crouchingSpeed; skeletonBehavior.IsCrouching = true; ApplyCrouchingSound(); }
        else { skeletonBehavior.Speed = normalSpeed; skeletonBehavior.IsCrouching = false; ApplyNormalSound(); }
    }

    void ApplyCrouchingSound()
    {
        skeletonBehavior.WalkingGroundSound.pitch = normalSoundPitch / 1.5f;
        skeletonBehavior.WalkingGroundSound.volume = normalSoundVolume / 1.5f;
    }

    void ApplyNormalSound()
    {
        skeletonBehavior.WalkingGroundSound.pitch = normalSoundPitch;
        skeletonBehavior.WalkingGroundSound.volume = normalSoundVolume;
    }
}
