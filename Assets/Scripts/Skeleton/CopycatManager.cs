using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopycatManager : MonoBehaviour
{
    Transform origin;
    Transform copycatPortal;
    Animator originAnimator;
    string originAnimatorState;
    Animator localAnimator;
    string localAnimatorState;
    Vector3 spawnOffset;

    public Transform Origin
    {
        get
        {
            if (origin != null)
            {
                return origin;
            } else
            {
                return null;
            }
            
        }
        set
        {
            origin = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        localAnimator = transform.GetComponent<Animator>();
        originAnimator = origin.GetComponent<Animator>();
        spawnOffset = origin.GetComponent<CopycatCreator>().SpawnOffset;
        copycatPortal = origin.GetComponent<CopycatCreator>().CopycatPortal;
    }

    // Update is called once per frame
    void Update()
    {
        spawnOffset = origin.GetComponent<CopycatCreator>().SpawnOffset;
        transform.rotation = origin.rotation;
        transform.position = copycatPortal.position + spawnOffset;
        getOriginAnimation();
        if (checkDifference()) { changeCurrentAnimation(); }
    }

    void getOriginAnimation()
    {
        if (originAnimator.GetCurrentAnimatorStateInfo(0).IsName("SkelShakeHand"))
            {
            originAnimatorState = "SkelShakeHand";
        } else if (originAnimator.GetCurrentAnimatorStateInfo(0).IsName("SkelIdle"))
        {
            originAnimatorState = "SkelIdle";
        }
        else if (originAnimator.GetCurrentAnimatorStateInfo(0).IsName("SkelMove"))
        {
            originAnimatorState = "SkelMove";
        }
        else if (originAnimator.GetCurrentAnimatorStateInfo(0).IsName("SkelTPose"))
        {
            originAnimatorState = "SkelTPose";
        }
        else if (originAnimator.GetCurrentAnimatorStateInfo(0).IsName("SkelMine"))
        {
            originAnimatorState = "SkelMine";
        }
    }

    bool checkDifference()
    {
        if (localAnimatorState == originAnimatorState)
        {
            return false;
        } else { return true; }
    }

    void changeCurrentAnimation()
    {
        localAnimator.Play(originAnimatorState);
    }
}
