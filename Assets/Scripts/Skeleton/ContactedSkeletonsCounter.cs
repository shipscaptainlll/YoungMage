using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactedSkeletonsCounter : MonoBehaviour
{
    int contactedSkeletonsCount;
    int countSmallSkeletons;
    int countBigSkeletons;
    int countLizardSkeletons;

    public int ContactedSkeletonsCount { get { return contactedSkeletonsCount; } }
    public int CountSmallSkeletons { get { return countSmallSkeletons; } }
    public int CountBigSkeletons { get { return countBigSkeletons; } }
    public int CountLizardSkeletons { get { return countLizardSkeletons;  } }

    public event Action<int> ContactedSkeleton = delegate { };
    public event Action<int> ContactedSmallSkeleton = delegate { };
    public event Action<int> ContactedBigSkeleton = delegate { };
    public event Action<int> ContactedLizardSkeleton = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountContactedSkeleton(Transform contactedSkeleton)
    {
        contactedSkeletonsCount = contactedSkeleton.GetComponent<SkeletonBehavior>().ProgressParameter;
        if (ContactedSkeleton != null) { ContactedSkeleton(contactedSkeletonsCount); }

        if (contactedSkeleton.GetComponent<SmallSkeleton>() != null)
        {
            countSmallSkeletons = contactedSkeleton.GetComponent<SkeletonBehavior>().ProgressParameterSecond;
            if (ContactedSmallSkeleton != null) { ContactedSmallSkeleton(countSmallSkeletons); }
        } else if (contactedSkeleton.GetComponent<BigSkeleton>() != null)
        {
            countBigSkeletons = contactedSkeleton.GetComponent<SkeletonBehavior>().ProgressParameterThird;
            if (ContactedBigSkeleton != null) { ContactedBigSkeleton(countBigSkeletons); }
        } else if (contactedSkeleton.GetComponent<LizardSkeleton>() != null)
        {
            countLizardSkeletons = contactedSkeleton.GetComponent<SkeletonBehavior>().ProgressParameterFourth;
            if (ContactedLizardSkeleton != null) { ContactedLizardSkeleton(countLizardSkeletons); }
        }

        
        
        
        
    }
}
