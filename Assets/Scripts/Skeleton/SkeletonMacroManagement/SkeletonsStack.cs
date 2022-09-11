using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonsStack : MonoBehaviour
{
    [SerializeField] SkeletonArenaInstantiator skeletonArenaInstantiator;
    [SerializeField] SkeletonHouseInstantiator skeletonHouseInstantiator;
    List<Transform> skeletonsStack = new List<Transform>();//in field
    List<Transform> skeletonsHouseStack = new List<Transform>();


    public List<Transform> SkeletonStack { get { return skeletonsStack; } }
    // Start is called before the first frame update
    void Start()
    {
        skeletonArenaInstantiator.SkeletonInstantiated += SaveSkeleton;
        skeletonHouseInstantiator.HouseSkeletonCreated += SaveHouseSkeleton;
        skeletonHouseInstantiator.SkeletonDestroyed += DeleteSkeleton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveSkeleton(Transform newSkeleton)
    {
        skeletonsStack.Add(newSkeleton);

    }

    void DeleteSkeleton(Transform deletedSkeleton)
    {
        skeletonsStack.Remove(deletedSkeleton);
        deletedSkeleton.GetComponent<SkeletonBehavior>().UnsubscribeBeforeDestruction();
        Debug.Log("Count in lists: " + skeletonsStack.Count);
        Destroy(deletedSkeleton.gameObject);
    }

    void SaveHouseSkeleton(Transform newSkeleton)
    {
        skeletonsHouseStack.Add(newSkeleton);

    }

    void DeleteHouseSkeleton(Transform deletedSkeleton)
    {
        skeletonsHouseStack.Remove(deletedSkeleton);
        Debug.Log("Count in lists: " + skeletonsHouseStack.Count);
        Destroy(deletedSkeleton.gameObject);
    }
}
