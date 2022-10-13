using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHouseInstantiator : MonoBehaviour
{
    [SerializeField] SkeletonArenaInstantiator skeletonArenaInstantiator;
    [SerializeField] CastlePositionsManager castlePositionsManager;
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            InstantiateNewInhouse();
        }
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
        //newSkeleton.GetComponent<SkeletonBehavior>().InstantiateSounds();
        newSkeleton.GetComponent<SkeletonBehavior>().Activity = "Idle";
        newSkeleton.parent = homeSkeletonsHolder;
        if (HouseSkeletonCreated != null) { HouseSkeletonCreated(newSkeleton); }
    }

    void InstantiateNewInhouse()
    {
        Transform newSkeleton = Instantiate(skeletonModel, transform.position, transform.rotation);
        newSkeleton.gameObject.SetActive(true);
        newSkeleton.GetComponent<SkeletonBehavior>().SubscribeAfterInstantiation();
        //newSkeleton.GetComponent<SkeletonBehavior>().InstantiateSounds();
        newSkeleton.GetComponent<SkeletonBehavior>().Activity = "Idle";
        newSkeleton.parent = homeSkeletonsHolder;
        if (HouseSkeletonCreated != null) { HouseSkeletonCreated(newSkeleton); }
    }

    public void DestroySkeleton(Transform teleportedSkeleton)
    {
        skeletonArenaInstantiator.SkeletonsCount -= 1;
        Debug.Log(teleportedSkeleton);
        Debug.Log(teleportedSkeleton + " position " + teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().OccupiedArenaPositions);
        castlePositionsManager.RegeneratePositions(teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().OccupiedArenaPositions);
        if (SkeletonDestroyed != null && teleportedSkeleton.transform.GetComponent<Copycat>() != null) { SkeletonDestroyed(teleportedSkeleton.transform.GetComponent<Copycat>().ConnectedInstance); }
        //Debug.Log("1");
        
        Debug.Log("here: " + castlePositionsManager.CastlePotentialPositions.Count);
        teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonHealthDecreaser>().UnscubscribeSkeletonSoldier();
        //Debug.Log("3");
        
        //Debug.Log("4");
        foreach (Transform skeleton in skeletonsStack.SkeletonsArena)
        {
            if (skeleton == teleportedSkeleton) { 
                skeletonsStack.SkeletonsArena.Remove(teleportedSkeleton);
                //Debug.Log("5");
                return; }
        }
        //Debug.Log("6");
    }

    public void DestroySkeleton(SkeletonBehavior teleportedSkeleton)
    {
        skeletonArenaInstantiator.SkeletonsCount -= 1;
        castlePositionsManager.RegeneratePositions(teleportedSkeleton.OccupiedArenaPositions);

        //Debug.Log("4");
        foreach (Transform skeleton in skeletonsStack.SkeletonsArena)
        {
            if (skeleton == teleportedSkeleton.transform)
            {
                skeletonsStack.SkeletonsArena.Remove(teleportedSkeleton.transform);
                //Debug.Log("5");
                return;
            }
        }
        //Debug.Log("6");
    }
}
