using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHouseInstantiator : MonoBehaviour
{
    [SerializeField] SkeletonArenaInstantiator skeletonArenaInstantiator;
    [SerializeField] SkeletonsStack skeletonsStack;
    [SerializeField] Transform homeSkeletonsHolder;
    [SerializeField] CopycatCatcher copycatCatcher;
    [SerializeField] Transform skeletonModel;


    public event Action<Transform> SkeletonDestroyed = delegate { };
    public event Action<Transform> HouseSkeletonCreated = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        copycatCatcher.SkeletonTeleported += CountTeleportedSkeleton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CountTeleportedSkeleton(Transform teleportedSkeleton)
    {
        InstantiateNewInhouse(teleportedSkeleton);
        DestroySkeleton(teleportedSkeleton);
    }

    void InstantiateNewInhouse(Transform teleportedSkeleton)
    {
        Transform newSkeleton = Instantiate(skeletonModel, teleportedSkeleton.position, teleportedSkeleton.rotation);
        newSkeleton.gameObject.SetActive(true);
        newSkeleton.GetComponent<SkeletonBehavior>().SubscribeAfterInstantiation();
        newSkeleton.GetComponent<SkeletonBehavior>().Activity = "Idle";
        newSkeleton.parent = homeSkeletonsHolder;
        if (HouseSkeletonCreated != null) { HouseSkeletonCreated(newSkeleton); }
    }

    void DestroySkeleton(Transform teleportedSkeleton)
    {
        if (SkeletonDestroyed != null) { SkeletonDestroyed(teleportedSkeleton.transform.GetComponent<Copycat>().ConnectedInstance); }
        skeletonArenaInstantiator.SkeletonsCount -= 1;
    }
}
