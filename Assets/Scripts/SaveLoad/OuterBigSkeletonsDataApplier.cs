using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OuterBigSkeletonsDataApplier
{
    static Transform skeletonsHolderLoad;
    static OuterBigSkeletonData outerBigSkeletonDataLoaded;

    public static void ApplySkeletonsData(Transform skeletonsHolder, OuterBigSkeletonData outerBigSkeletonData, SkeletonsDeleter skeletonsDeleter, CastlePositionsManager castlePositionsManager, CatapultArenaInstantiator catapultArenaInstantiator)
    {
        UpdateData(skeletonsHolder, outerBigSkeletonData);
        DeletePreviousSkeletons(skeletonsDeleter, skeletonsHolder, castlePositionsManager);
        InstantiateLoadedSkeleton(skeletonsHolder, catapultArenaInstantiator);
        ApplyState(skeletonsHolder);
        DisconnectData();
    }

    static void UpdateData(Transform skeletonsHolder, OuterBigSkeletonData outerBigSkeletonData)
    {
        skeletonsHolderLoad = skeletonsHolder;
        outerBigSkeletonDataLoaded = outerBigSkeletonData;
    }

    static void DisconnectData()
    {
        skeletonsHolderLoad = null;
        outerBigSkeletonDataLoaded = null;
    }

    static void DeletePreviousSkeletons(SkeletonsDeleter skeletonsDeleter, Transform skeletonsHolder, CastlePositionsManager castlePositionsManager)
    {
        skeletonsDeleter.DeleteCatapultSkeletons();
        //skeletonsDeleter.ResetSkeletonsStack();
        castlePositionsManager.ResetAllPositions();
        skeletonsDeleter.ResetCatapultArenaInstantiator();
    }

    static void InstantiateLoadedSkeleton(Transform skeletonsHolder, CatapultArenaInstantiator catapultArenaInstantiator)
    {
        catapultArenaInstantiator.CatapultsMaxCount = outerBigSkeletonDataLoaded.instantiatorMaxLevel;
        Debug.Log("Max count for big skeletons was " + outerBigSkeletonDataLoaded.instantiatorMaxLevel);

        int indexer = 0;
        while (indexer < outerBigSkeletonDataLoaded.positions.Length)
        {
            Vector3 skeletonPosition = new Vector3(outerBigSkeletonDataLoaded.positions[indexer][0], outerBigSkeletonDataLoaded.positions[indexer][1], outerBigSkeletonDataLoaded.positions[indexer][2]);
            Debug.Log("loaded one outer big skeleton positions");
            Vector3 skeletonRotation = new Vector3(outerBigSkeletonDataLoaded.rotations[indexer][0], outerBigSkeletonDataLoaded.rotations[indexer][1], outerBigSkeletonDataLoaded.rotations[indexer][2]);
            Debug.Log("loaded one outer big skeleton rotations");
            catapultArenaInstantiator.InstantiateUploadedSkeleton(skeletonPosition, Quaternion.Euler(skeletonRotation));
            Debug.Log("instantiated one big small skeleton");
            indexer++;
        }
    }

    static void ApplyState(Transform skeletonsHolder)
    {
        int indexer = 0;

        

        foreach (Transform skeleton in skeletonsHolder)
        {

            skeleton.GetComponent<SkeletonHealthDecreaser>().Health = outerBigSkeletonDataLoaded.currentHealth[indexer];
            Debug.Log("hello");
            if (outerBigSkeletonDataLoaded.onRoute[indexer])
            {
                Debug.Log("hello1");
                skeleton.GetComponent<SkeletonBehavior>().CastleNavroutActive = true;
                skeleton.GetComponent<SkeletonBehavior>().UploadCastleRouteNumber(outerBigSkeletonDataLoaded.routeState[indexer]);
            }
            else if (outerBigSkeletonDataLoaded.inCastle[indexer])
            {
                skeleton.GetComponent<SkeletonBehavior>().UploadCastleHitting();
            }
        }
    }
}
