using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectableDataApplier
{
    static CollectableObjectsInstantiator collectableInstantiatorLoad;
    static CollectableData collectableDataLoaded;

    public static void ApplyCollectableData(CollectableObjectsDeleter collectableObjectsDeleter, CollectableObjectsInstantiator collectableObjectsInstantiator, CollectableData collectableData)
    {
        UpdateData(collectableObjectsInstantiator, collectableData);
        InstantiateLoadedCollectable(collectableObjectsDeleter, collectableObjectsInstantiator, collectableData);
        DisconnectData();
    }

    static void UpdateData(CollectableObjectsInstantiator collectableObjectsInstantiator, CollectableData collectableData)
    {
        collectableInstantiatorLoad = collectableObjectsInstantiator;
        collectableDataLoaded = collectableData;
    }

    static void DisconnectData()
    {
        collectableInstantiatorLoad = null;
        collectableDataLoaded = null;
    }

    static void InstantiateLoadedCollectable(CollectableObjectsDeleter collectableObjectsDeleter, CollectableObjectsInstantiator collectableObjectsInstantiator, CollectableData collectableData)
    {
        int indexer = 0;
        collectableObjectsDeleter.DestroyCollectableObjects();
        while (indexer < collectableDataLoaded.positions.Length)
        {
            Vector3 collectablePosition = new Vector3(collectableDataLoaded.positions[indexer][0], collectableDataLoaded.positions[indexer][1], collectableDataLoaded.positions[indexer][2]);
            Debug.Log("loaded one collectable positions");
            Vector3 collectableRotation = new Vector3(collectableDataLoaded.rotations[indexer][0], collectableDataLoaded.rotations[indexer][1], collectableDataLoaded.rotations[indexer][2]);
            Debug.Log("loaded one collectable rotations");
            int collectableID = collectableDataLoaded.ids[indexer];
            int collectableCount = collectableDataLoaded.counts[indexer];

            Debug.Log("loaded id " + collectableID);
            Debug.Log("loaded count " + collectableCount);
            collectableObjectsInstantiator.InstantiateLoadedCollectable(collectableID, collectablePosition, Quaternion.Euler(collectableRotation), collectableCount);

            Debug.Log("instantiated one collectable");
            indexer++;
        }
    }
}
