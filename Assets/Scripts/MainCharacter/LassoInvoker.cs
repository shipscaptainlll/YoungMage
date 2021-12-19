using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoInvoker : MonoBehaviour
{
    ClickManager ClickManager;
    ContactManager ContactManager;
    [SerializeField] Transform FirstPoint;
    Transform SecondPoint;
    LineRenderer lassoRenderer;
    string state;
    bool isContactingSkeleton;
    Transform rememberedSkeleton;
    Transform mageTransform;
    Animator localAnimator;

    public event Action<Transform, Transform> UsedWand = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        ClickManager = transform.parent.parent.parent.Find("Script").Find("ClickManager").GetComponent<ClickManager>();
        ContactManager = transform.parent.parent.parent.Find("Script").Find("ContactManager").GetComponent<ContactManager>();
        lassoRenderer = transform.parent.parent.Find("Line").GetComponent<LineRenderer>();
        localAnimator = transform.GetComponent<Animator>();
        ContactManager.SkeletonDetected += ChangeTarget;
        ContactManager.OreDetected += CalculateBehavior;
        state = "idle";
        isContactingSkeleton = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case "idle":
                break;
            case "castingLasso":
                InvokeLasso();
                break;
        }
    }

    void CalculateBehavior()
    {
        if (state == "castingLasso")
        {
            state = "idle";
            LineToDefaults();
            LoseTarget();
        } else if (state == "idle")
        {
            if (SecondPoint != null)
            {
                state = "castingLasso";
            }
        }
    }

    void CalculateBehavior(Transform ignoreGarbage)
    {
        if (state == "castingLasso")
        {
            state = "idle";
            LineToDefaults();
            LoseTarget();
        }
        else if (state == "idle")
        {
            if (SecondPoint != null)
            {
                state = "castingLasso";
            }
        }
    }

    void InvokeLasso()
    {
        lassoRenderer.SetPosition(0, FirstPoint.position + new Vector3(0, -0.2f, 0));
        lassoRenderer.SetPosition(1, SecondPoint.position);
    }

    void ChangeTarget(Transform nextTarget, Transform mage)
    {
        rememberedSkeleton = nextTarget;
        mageTransform = mage;
        isContactingSkeleton = true;
        var localAnimatorCache = transform.GetComponent<Animator>();
        localAnimatorCache.Play("WandCast");
        SecondPoint = nextTarget;
        SearchForNeck();
        
    }

    void LineToDefaults()
    {
        lassoRenderer.SetPosition(0, new Vector3(0,0,0));
        lassoRenderer.SetPosition(1, new Vector3(0, 0, 0));
    }

    void LoseTarget()
    {
        SecondPoint = null;
    }

    void SearchForNeck()
    {
        SecondPoint = SecondPoint.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
    }

    void Hello()
    {
        var skeletonCache = rememberedSkeleton.GetComponent<SkeletonBehavior>();
        CalculateBehavior();
        isContactingSkeleton = false;
        skeletonCache.ConnectToMage(rememberedSkeleton, mageTransform);
    }
    public void Unsubscribe()
    {
        ContactManager.SkeletonDetected -= ChangeTarget;
        ContactManager.OreDetected -= CalculateBehavior;
    }

    
}
