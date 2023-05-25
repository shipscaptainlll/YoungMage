using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonComingMark : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = transform.GetComponent<Animator>();
        meshRenderer = transform.GetComponent<MeshRenderer>();
    }

    // needs
    // to adjust with save/loading

    public void ActivateAnimation()
    {
        Debug.Log("Starting animation");
        meshRenderer.enabled = true;
        animator.Play("ComingMarkAnimation");
    }

    public void StopAnimation()
    {
        Debug.Log("Stoping animation");
        meshRenderer.enabled = false;
        animator.Play("Idle");
    }
}
