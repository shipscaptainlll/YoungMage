using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHouseInstantiator : MonoBehaviour
{
    [SerializeField] SkeletonArenaInstantiator skeletonArenaInstantiator;
    [SerializeField] CatapultArenaInstantiator catapultArenaInstantiator;
    [SerializeField] CrossbowCatapultArenaInstantiator crossbowCatapultArenaInstantiator;
    [SerializeField] CastlePositionsManager smallskeletonsPositionsManager;
    [SerializeField] CastlePositionsManager bigskeletonsPositionsManager;
    [SerializeField] CastlePositionsManager lizardskeletonsPositionsManager;
    [SerializeField] SkeletonsStack skeletonsStack;
    [SerializeField] Transform homeSkeletonsHolder;
    [SerializeField] CopycatCatcher copycatCatcher;
    [SerializeField] Transform smallskeletonModel;
    [SerializeField] Transform bigskeletonModel;
    [SerializeField] Transform lizardskeletonModel;


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
        Transform skeletonToInstantiate = null;
        if (teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().IsSmallSkeleton)
        {
            skeletonToInstantiate = smallskeletonModel;
        }
        else if (teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().IsBigSkeleton)
        {
            skeletonToInstantiate = bigskeletonModel;
        }
        else if (teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().IsLizardSkeleton)
        {
            skeletonToInstantiate = lizardskeletonModel;
        }

        Transform newSkeleton = Instantiate(skeletonToInstantiate, teleportedSkeleton.position, teleportedSkeleton.rotation);
        newSkeleton.gameObject.SetActive(true);
        newSkeleton.GetComponent<SkeletonBehavior>().SubscribeAfterInstantiation();
        //newSkeleton.GetComponent<SkeletonBehavior>().InstantiateSounds();
        newSkeleton.GetComponent<SkeletonBehavior>().Activity = "Idle";
        newSkeleton.parent = homeSkeletonsHolder;
        if (HouseSkeletonCreated != null) { HouseSkeletonCreated(newSkeleton); }
    }

    void InstantiateNewInhouse()
    {
        Transform newSkeleton = Instantiate(lizardskeletonModel, transform.position, transform.rotation);
        newSkeleton.gameObject.SetActive(true);
        newSkeleton.GetComponent<SkeletonBehavior>().SubscribeAfterInstantiation();
        //newSkeleton.GetComponent<SkeletonBehavior>().InstantiateSounds();
        newSkeleton.GetComponent<SkeletonBehavior>().Activity = "Idle";
        newSkeleton.parent = homeSkeletonsHolder;
        if (HouseSkeletonCreated != null) { HouseSkeletonCreated(newSkeleton); }
    }

    public void DestroySkeleton(Transform teleportedSkeleton)
    {
        if (teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().IsSmallSkeleton) { skeletonArenaInstantiator.SkeletonsCount -= 1; }
        else if (teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().IsBigSkeleton) { catapultArenaInstantiator.SkeletonsCount -= 1; catapultArenaInstantiator.CatapultsCount -= 1;
            Debug.Log("Catapults count " + catapultArenaInstantiator.CatapultsCount);
        }
        else if (teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().IsLizardSkeleton) { crossbowCatapultArenaInstantiator.SkeletonsCount -= 1; crossbowCatapultArenaInstantiator.CatapultsCount -= 1;
            Debug.Log("Catapults count " + crossbowCatapultArenaInstantiator.CatapultsCount);
        }
        //Debug.Log(teleportedSkeleton);
        //Debug.Log(teleportedSkeleton + " position " + teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().OccupiedArenaPositions);
        if (teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().IsSmallSkeleton) { smallskeletonsPositionsManager.RegeneratePositions(teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().OccupiedArenaPositions); }
        else if (teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().IsBigSkeleton) { bigskeletonsPositionsManager.RegeneratePositions(teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().OccupiedArenaPositions); }
        else if (teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().IsLizardSkeleton) { lizardskeletonsPositionsManager.RegeneratePositions(teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().OccupiedArenaPositions); }
        teleportedSkeleton.GetComponent<Copycat>().ConnectedInstance.GetComponent<SkeletonBehavior>().DestroyCatapult();
        if (SkeletonDestroyed != null && teleportedSkeleton.transform.GetComponent<Copycat>() != null) { SkeletonDestroyed(teleportedSkeleton.transform.GetComponent<Copycat>().ConnectedInstance); }
        //Debug.Log("1");
        
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
        if (teleportedSkeleton.IsSmallSkeleton) { 
            skeletonArenaInstantiator.SkeletonsCount -= 1;
            smallskeletonsPositionsManager.RegeneratePositions(teleportedSkeleton.OccupiedArenaPositions);
        }
        else if (teleportedSkeleton.IsBigSkeleton)
        {
            catapultArenaInstantiator.SkeletonsCount -= 1;
            catapultArenaInstantiator.CatapultsCount -= 1;
            Debug.Log("Catapults count " + catapultArenaInstantiator.CatapultsCount);
            bigskeletonsPositionsManager.RegeneratePositions(teleportedSkeleton.OccupiedArenaPositions);
        }
        else if (teleportedSkeleton.IsLizardSkeleton)
        {
            crossbowCatapultArenaInstantiator.SkeletonsCount -= 1;
            crossbowCatapultArenaInstantiator.CatapultsCount -= 1;
            Debug.Log("Catapults count " + crossbowCatapultArenaInstantiator.CatapultsCount);
            lizardskeletonsPositionsManager.RegeneratePositions(teleportedSkeleton.OccupiedArenaPositions);
        }
        teleportedSkeleton.DestroyCatapult();


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
