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
        if (contactedSkeleton.GetComponent<SmallSkeleton>() != null)
        {
            countSmallSkeletons = contactedSkeleton.GetComponent<SkeletonBehavior>().ProgressParameterSecond;
        }

        if (ContactedSkeleton != null) { ContactedSkeleton(contactedSkeletonsCount);  }
        if (ContactedSmallSkeleton != null) { ContactedSmallSkeleton(countSmallSkeletons); }
    }
}
