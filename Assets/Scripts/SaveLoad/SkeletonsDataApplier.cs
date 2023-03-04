using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkeletonsDataApplier
{
    static Transform skeletonsHolderLoad;
    static SkeletonData skeletonDataLoaded;

    public static void ApplySkeletonsData(Transform skeletonsHolder, SkeletonHouseInstantiator skeletonHouseInstantiator, PersonMovement personScript, Transform oresHolder, Transform tackledDoor, SkeletonData skeletonData, SkeletonsDeleter skeletonsDeleter)
    {
        UpdateData(skeletonsHolder, skeletonData);
        DeletePreviousSkeletons(skeletonsDeleter, skeletonsHolder);
        InstantiateLoadedSkeleton(skeletonsHolder, skeletonHouseInstantiator);
        ApplyState(skeletonsHolder, personScript, tackledDoor, oresHolder);
        DisconnectData();
    }

    static void UpdateData(Transform skeletonsHolder, SkeletonData skeletonData)
    {
        skeletonsHolderLoad = skeletonsHolder;
        skeletonDataLoaded = skeletonData;
    }

    static void DisconnectData()
    {
        skeletonsHolderLoad = null;
        skeletonDataLoaded = null;
    }

    static void DeletePreviousSkeletons(SkeletonsDeleter skeletonsDeleter, Transform skeletonsHolder)
    {
        skeletonsDeleter.DeleteFracturedSkeletons();
        skeletonsDeleter.DeleteInhouseSkeeletons(skeletonsHolder);
    }

    static void InstantiateLoadedSkeleton(Transform skeletonsHolder, SkeletonHouseInstantiator skeletonHouseInstantiator)
    {
        int indexer = 0;
        while (indexer < skeletonDataLoaded.positions.Length)
        {
            Vector3 skeletonPosition = new Vector3(skeletonDataLoaded.positions[indexer][0], skeletonDataLoaded.positions[indexer][1], skeletonDataLoaded.positions[indexer][2]);
            Debug.Log("loaded one skeleton positions");
            Vector3 skeletonRotation = new Vector3(skeletonDataLoaded.rotations[indexer][0], skeletonDataLoaded.rotations[indexer][1], skeletonDataLoaded.rotations[indexer][2]);
            Debug.Log("loaded one skeleton rotations");
            skeletonHouseInstantiator.InstantiateNewInhouse(skeletonPosition, Quaternion.Euler(skeletonRotation));
            Debug.Log("instantiated one skeleton");
            indexer++;
        }
    }

    static void ApplyState(Transform skeletonsHolder, PersonMovement personScript, Transform tackledDoor, Transform oresHolder)
    {
        int indexer = 0;
        //Debug.Log("applied one skeleton state");
        Debug.Log("skeletons in holder " + skeletonsHolder.childCount);
        
        foreach (Transform skeleton in skeletonsHolder)
        {
            Debug.Log("weel hello there0 + indexer " + indexer);
            if (skeletonDataLoaded.isIdle[indexer])
            {
                Debug.Log("weel hello there");
            }
            else if (skeletonDataLoaded.connectedToPerson[indexer])
            {
                Debug.Log("weel hello there1");
                Debug.Log(personScript.transform);
                //skeleton.GetComponent<SkeletonBehavior>().NavigationTarget = null;
                skeleton.GetComponent<SkeletonBehavior>().NavigationTarget = personScript.transform;
                Debug.Log(personScript.transform);
            }
            else if (skeletonDataLoaded.connectedToOre[indexer])
            {
                int indexerOre = 0;
                Debug.Log("weel hello there2");
                foreach (Transform row in oresHolder)
                {
                    foreach (Transform ore in row)
                    {
                        if (indexerOre == skeletonDataLoaded.indexerOreSaved[indexer])
                        {
                            skeleton.GetComponent<SkeletonBehavior>().NavigationTarget = ore.GetChild(1);
                            return;
                        }
                        indexerOre++;
                    }
                }
            }
            else if (skeletonDataLoaded.isTacklingDoor[indexer])
            {
                Debug.Log("weel hello there3");
                Transform searchedDoor = null;
                foreach (Transform door in tackledDoor)
                {
                    Debug.Log(door.name);
                    Debug.Log(door.GetComponent<DoorTacklingManager>().DoorLevel);
                    Debug.Log(skeletonDataLoaded.connectedDoorIndex[indexer]);
                    if (door.GetComponent<DoorTacklingManager>().DoorLevel == skeletonDataLoaded.connectedDoorIndex[indexer])
                    {
                        Debug.Log("skeleton was connected to the door number " + skeletonDataLoaded.connectedDoorIndex[indexer]);
                        searchedDoor = door;
                        break;
                    }
                    
                }
                skeleton.GetComponent<SkeletonBehavior>().AddTarget(searchedDoor.GetComponent<DoorConnectionManager>());
            }
            else if (skeletonDataLoaded.isExcomunicated[indexer])
            {
                Debug.Log("weel hello there4");
                skeleton.GetComponent<SkeletonBehavior>().IsConjured = true;
                skeleton.GetComponent<SkeletonBehavior>().NavigationTarget = null;
            }
            indexer++;
            Debug.Log("applied one skeleton state");
        }
    }
}
