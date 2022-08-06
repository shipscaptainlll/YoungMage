using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedSkeletonsCounter : MonoBehaviour
{
    int destroyedSkeletonsCount;
    int countSmallSkeletons;
    int countBigSkeletons;
    int countLizardSkeletons;

    public int DestroyedSkeletonsCount { get { return destroyedSkeletonsCount; } }
    public int CountSmallSkeletons { get { return countSmallSkeletons; } }
    public int CountBigSkeletons { get { return countBigSkeletons; } }
    public int CountLizardSkeletons { get { return countLizardSkeletons;  } }

    public event Action<int> DestroyedSkeleton = delegate { };
    public event Action<int> DestroyedSmallSkeleton = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountDestroyedSkeleton(Transform destroyedSkeleton)
    {
        destroyedSkeletonsCount = destroyedSkeleton.GetComponent<SkeletonBehavior>().CountDestroyedSkeletons;
        if (destroyedSkeleton.GetComponent<SmallSkeleton>() != null)
        {
            countSmallSkeletons = destroyedSkeleton.GetComponent<SkeletonBehavior>().DestroyedSmallSkeletons;
        }

        if (DestroyedSkeleton != null) { DestroyedSkeleton(destroyedSkeletonsCount);  }
        if (DestroyedSmallSkeleton != null) { DestroyedSmallSkeleton(countSmallSkeletons); }
    }
}
