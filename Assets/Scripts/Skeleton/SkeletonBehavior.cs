using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour
{
    Animator localAnimator;
    
    void Start()
    {
        localAnimator = transform.GetComponent<Animator>();
        localAnimator.Play("SkeletonMine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
