using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonsStack : MonoBehaviour
{
    [SerializeField] SkeletonArenaInstantiator skeletonArenaInstantiator;
    List<Transform> skeletonsStack = new List<Transform>();


    public List<Transform> SkeletonStack { get { return skeletonsStack; } }
    // Start is called before the first frame update
    void Start()
    {
        skeletonArenaInstantiator.SkeletonInstantiated += SaveSkeleton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveSkeleton(Transform newSkeleton)
    {
        skeletonsStack.Add(newSkeleton);

    }
}
