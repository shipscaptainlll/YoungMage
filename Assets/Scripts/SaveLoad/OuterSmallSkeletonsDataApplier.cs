using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OuterSmallSkeletonsDataApplier
{
    static Transform skeletonsHolderLoad;
    static OuterSmallSkeletonData outerSmallSkeletonDataLoaded;

    public static void ApplySkeletonsData(Transform skeletonsHolder, OuterSmallSkeletonData outerSmallSkeletonData, SkeletonsDeleter skeletonsDeleter, CastlePositionsManager castlePositionsManager, SkeletonArenaInstantiator skeletonArenaInstantiator)
    {
        UpdateData(skeletonsHolder, outerSmallSkeletonData);
        DeletePreviousSkeletons(skeletonsDeleter, skeletonsHolder, castlePositionsManager);
        InstantiateLoadedSkeleton(skeletonsHolder, skeletonArenaInstantiator);
        ApplyState(skeletonsHolder);
        DisconnectData();
    }

    static void UpdateData(Transform skeletonsHolder, OuterSmallSkeletonData outerSmallSkeletonData)
    {
        skeletonsHolderLoad = skeletonsHolder;
        outerSmallSkeletonDataLoaded = outerSmallSkeletonData;
    }

    static void DisconnectData()
    {
        skeletonsHolderLoad = null;
        outerSmallSkeletonDataLoaded = null;
    }

    static void DeletePreviousSkeletons(SkeletonsDeleter skeletonsDeleter, Transform skeletonsHolder, CastlePositionsManager castlePositionsManager)
    {
        skeletonsDeleter.DeleteArenaSkeletons();
        skeletonsDeleter.ResetSkeletonsStack();
        castlePositionsManager.ResetAllPositions();
        skeletonsDeleter.ResetSkeletonsArenaInstatiator();
    }

    static void InstantiateLoadedSkeleton(Transform skeletonsHolder, SkeletonArenaInstantiator skeletonArenaInstantiator)
    {
        int indexer = 0;
        while (indexer < outerSmallSkeletonDataLoaded.positions.Length)
        {
            Vector3 skeletonPosition = new Vector3(outerSmallSkeletonDataLoaded.positions[indexer][0], outerSmallSkeletonDataLoaded.positions[indexer][1], outerSmallSkeletonDataLoaded.positions[indexer][2]);
            Debug.Log("loaded one outer small skeleton positions");
            Vector3 skeletonRotation = new Vector3(outerSmallSkeletonDataLoaded.rotations[indexer][0], outerSmallSkeletonDataLoaded.rotations[indexer][1], outerSmallSkeletonDataLoaded.rotations[indexer][2]);
            Debug.Log("loaded one outer small skeleton rotations");
            skeletonArenaInstantiator.InstantiateUploadedSkeleton(skeletonPosition, Quaternion.Euler(skeletonRotation));
            Debug.Log("instantiated one outer small skeleton");
            indexer++;
        }
    }

    static void ApplyState(Transform skeletonsHolder)
    {
        int indexer = 0;
        //onRoute = new bool[skeletonsHolder.childCount];
        //routeState = new int[skeletonsHolder.childCount];
        //inCastle = new bool[skeletonsHolder.childCount];
        //portalConnected = new bool[skeletonsHolder.childCount];


        foreach (Transform skeleton in skeletonsHolder)
        {

            skeleton.GetComponent<SkeletonHealthDecreaser>().Health = outerSmallSkeletonDataLoaded.currentHealth[indexer];
            Debug.Log("hello");
            if (outerSmallSkeletonDataLoaded.onRoute[indexer])
            {
                Debug.Log("hello1");
                skeleton.GetComponent<SkeletonBehavior>().CastleNavroutActive = true;
                skeleton.GetComponent<SkeletonBehavior>().UploadCastleRouteNumber(outerSmallSkeletonDataLoaded.routeState[indexer]);
            }
            else if (outerSmallSkeletonDataLoaded.inCastle[indexer])
            {
                skeleton.GetComponent<SkeletonBehavior>().UploadCastleHitting();
            }
            else if (outerSmallSkeletonDataLoaded.portalConnected[indexer])
            {
                //skeleton.GetComponent<SkeletonBehavior>().ConnectedToPortal = true;
            }
        }
    }
}
