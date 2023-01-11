using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectableData
{
    public float[][] positions;
    public float[][] rotations;
    public int[] counts;
    public int[] ids;

    public CollectableData(Transform collectablesHolder)
    {
        GetPositions(collectablesHolder);
        GetRotations(collectablesHolder);
        GetIDs(collectablesHolder);
        GetCounts(collectablesHolder);
    }

    void GetPositions(Transform collectablesHolder)
    {
        positions = new float[collectablesHolder.childCount][];
        int indexer = 0;
        foreach (Transform collectable in collectablesHolder)
        {
            float[] position = new float[3];
            position[0] = collectable.gameObject.transform.position.x;
            position[1] = collectable.gameObject.transform.position.y;
            position[2] = collectable.gameObject.transform.position.z;
            positions[indexer++] = position;
            Debug.Log("saved one collectable positions");
        }
    }

    void GetRotations(Transform collectablesHolder)
    {
        rotations = new float[collectablesHolder.childCount][];
        int indexer = 0;
        foreach (Transform collectable in collectablesHolder)
        {
            float[] rotation = new float[3];
            rotation[0] = collectable.gameObject.transform.eulerAngles.x;
            rotation[1] = collectable.gameObject.transform.eulerAngles.y;
            rotation[2] = collectable.gameObject.transform.eulerAngles.z;
            rotations[indexer++] = rotation;
            Debug.Log("saved one collectable rotations");
        }
    }

    void GetIDs(Transform collectablesHolder)
    {
        ids = new int[collectablesHolder.childCount];
        int indexer = 0;
        foreach (Transform collectable in collectablesHolder)
        {
            ids[indexer++] = collectable.GetComponent<GlobalResource>().ID;
            Debug.Log("id was " + collectable.GetComponent<GlobalResource>().ID);
            Debug.Log("saved one collectable id");
        }
    }

    void GetCounts(Transform collectablesHolder)
    {
        counts = new int[collectablesHolder.childCount];
        int indexer = 0;
        foreach (Transform collectable in collectablesHolder)
        {
            counts[indexer++] = collectable.GetComponent<OreCounter>().OreCount;
            Debug.Log("count was " + collectable.GetComponent<OreCounter>().OreCount);
            Debug.Log("saved one collectable count");
        }
    }
}
