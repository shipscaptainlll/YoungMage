using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OuterLizardSkeletonsDataApplier
{
    static Transform skeletonsHolderLoad;
    static OuterLizardSkeletonData outerLizardSkeletonDataLoaded;

    public static void ApplySkeletonsData(Transform skeletonsHolder, OuterLizardSkeletonData outerLizardSkeletonData, SkeletonsDeleter skeletonsDeleter, CastlePositionsManager castlePositionsManager, CrossbowCatapultArenaInstantiator crossbowCatapultArenaInstantiator)
    {
        UpdateData(skeletonsHolder, outerLizardSkeletonData);
        DeletePreviousSkeletons(skeletonsDeleter, skeletonsHolder, castlePositionsManager);
        InstantiateLoadedSkeleton(skeletonsHolder, crossbowCatapultArenaInstantiator);
        ApplyState(skeletonsHolder);
        DisconnectData();
    }

    static void UpdateData(Transform skeletonsHolder, OuterLizardSkeletonData outerLizardSkeletonData)
    {
        skeletonsHolderLoad = skeletonsHolder;
        outerLizardSkeletonDataLoaded = outerLizardSkeletonData;
    }

    static void DisconnectData()
    {
        skeletonsHolderLoad = null;
        outerLizardSkeletonDataLoaded = null;
    }

    static void DeletePreviousSkeletons(SkeletonsDeleter skeletonsDeleter, Transform skeletonsHolder, CastlePositionsManager castlePositionsManager)
    {
        skeletonsDeleter.DeleteCrossbowSkeletons();
        //skeletonsDeleter.ResetSkeletonsStack();
        castlePositionsManager.ResetAllPositions();
        skeletonsDeleter.ResetCrossbowArenaInstatiator();
    }

    static void InstantiateLoadedSkeleton(Transform skeletonsHolder, CrossbowCatapultArenaInstantiator crossbowCatapultArenaInstantiator)
    {
        int indexer = 0;
        crossbowCatapultArenaInstantiator.CatapultsMaxCount = outerLizardSkeletonDataLoaded.instantiatorMaxCount;
        Debug.Log("Max count for lizard skeletons was " + outerLizardSkeletonDataLoaded.instantiatorMaxCount);

        while (indexer < outerLizardSkeletonDataLoaded.positions.Length)
        {
            Vector3 skeletonPosition = new Vector3(outerLizardSkeletonDataLoaded.positions[indexer][0], outerLizardSkeletonDataLoaded.positions[indexer][1], outerLizardSkeletonDataLoaded.positions[indexer][2]);
            Debug.Log("loaded one outer lizard skeleton positions");
            Vector3 skeletonRotation = new Vector3(outerLizardSkeletonDataLoaded.rotations[indexer][0], outerLizardSkeletonDataLoaded.rotations[indexer][1], outerLizardSkeletonDataLoaded.rotations[indexer][2]);
            Debug.Log("loaded one outer lizard skeleton rotations");
            crossbowCatapultArenaInstantiator.InstantiateUploadedSkeleton(skeletonPosition, Quaternion.Euler(skeletonRotation));
            Debug.Log("instantiated one lizard skeleton");
            indexer++;
        }
    }

    static void ApplyState(Transform skeletonsHolder)
    {
        int indexer = 0;


        foreach (Transform skeleton in skeletonsHolder)
        {

            skeleton.GetComponent<SkeletonHealthDecreaser>().Health = outerLizardSkeletonDataLoaded.currentHealth[indexer];
            Debug.Log("hello");
            if (outerLizardSkeletonDataLoaded.onRoute[indexer])
            {
                Debug.Log("hello1");
                skeleton.GetComponent<SkeletonBehavior>().CastleNavroutActive = true;
                skeleton.GetComponent<SkeletonBehavior>().UploadCastleRouteNumber(outerLizardSkeletonDataLoaded.routeState[indexer]);
            }
            else if (outerLizardSkeletonDataLoaded.inCastle[indexer])
            {
                skeleton.GetComponent<SkeletonBehavior>().UploadCastleHitting();
            }
        }
    }
}
