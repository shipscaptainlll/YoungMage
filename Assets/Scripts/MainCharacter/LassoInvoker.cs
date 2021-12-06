using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassoInvoker : MonoBehaviour
{
    [SerializeField] ClickManager ClickManager;
    [SerializeField] ContactManager ContactManager;
    [SerializeField] Transform FirstPoint;
    [SerializeField] Transform SecondPoint;
    [SerializeField] LineRenderer lassoRenderer;
    string state;
    bool isContactingSkeleton;
    Transform rememberedSkeleton;
    Transform mageTransform;

    public event Action<Transform, Transform> UsedWand = delegate { };
    // Start is called before the first frame update
    void Start()
    {
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
        GetComponent<Animator>().Play("WandCast");
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
        CalculateBehavior();
        isContactingSkeleton = false;
        if (UsedWand != null)
        {
            UsedWand(rememberedSkeleton, mageTransform);
        }
    }
}
