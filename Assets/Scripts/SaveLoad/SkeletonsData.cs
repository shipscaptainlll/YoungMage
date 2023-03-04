using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkeletonData
{
    public float[][] positions;
    public float[][] rotations;
    public bool[] connectedToOre;
    public bool[] connectedToPerson;
    public bool[] isIdle;
    public bool[] isExcomunicated;
    public bool[] isTacklingDoor;
    public int[] indexerOreSaved;
    public int[] connectedDoorIndex;

    public SkeletonData(Transform skeletonsHolder, Transform oresHolder)
    {
        GetPositions(skeletonsHolder);
        GetRotations(skeletonsHolder);
        GetState(skeletonsHolder, oresHolder);
    }

    void GetPositions(Transform skeletonsHolder)
    {
        positions = new float[skeletonsHolder.childCount][];
        int indexer = 0;
        foreach (Transform skeleton in skeletonsHolder)
        {
            float[] position = new float[3];
            position[0] = skeleton.gameObject.transform.position.x;
            position[1] = skeleton.gameObject.transform.position.y;
            position[2] = skeleton.gameObject.transform.position.z;
            positions[indexer++] = position;
            Debug.Log("saved one skeleton positions");
        }
    }

    void GetRotations(Transform skeletonsHolder)
    {
        rotations = new float[skeletonsHolder.childCount][];
        int indexer = 0;
        foreach (Transform skeleton in skeletonsHolder)
        {
            float[] rotation = new float[3];
            rotation[0] = skeleton.gameObject.transform.eulerAngles.x;
            rotation[1] = skeleton.gameObject.transform.eulerAngles.y;
            rotation[2] = skeleton.gameObject.transform.eulerAngles.z;
            rotations[indexer++] = rotation;
            Debug.Log("saved one skeleton rotations");
        }
    }

    void GetState(Transform skeletonsHolder,Transform oresHolder)
    {
        int indexer = 0;
        connectedToOre = new bool[skeletonsHolder.childCount];
        connectedToPerson = new bool[skeletonsHolder.childCount];
        isIdle = new bool[skeletonsHolder.childCount];
        isExcomunicated = new bool[skeletonsHolder.childCount];
        isTacklingDoor = new bool[skeletonsHolder.childCount];
        indexerOreSaved = new int[skeletonsHolder.childCount];
        connectedDoorIndex = new int[skeletonsHolder.childCount];
        foreach (Transform skeleton in skeletonsHolder)
        {
            connectedToOre[indexer] = false;
            connectedToPerson[indexer] = false;
            isIdle[indexer] = false;
            isExcomunicated[indexer] = false;
            isTacklingDoor[indexer] = false;
            indexerOreSaved[indexer] = 0;

            if (!skeleton.GetComponent<SkeletonBehavior>().IsConjured)
            {
                isIdle[indexer] = true;
                Debug.Log("skeleton was idle");
            } else if (skeleton.GetComponent<SkeletonBehavior>().NavigationTarget != null && skeleton.GetComponent<SkeletonBehavior>().NavigationTarget.GetComponent<PersonMovement>() != null)
            {
                Debug.Log("skeleton was following mage");
                connectedToPerson[indexer] = true;
            } else if (skeleton.GetComponent<SkeletonBehavior>().NavigationTarget != null && skeleton.GetComponent<SkeletonBehavior>().NavigationTarget.GetComponent<IOre>() != null)
            {
                Debug.Log("skeleton was ore connected");
                connectedToOre[indexer] = true;
                int indexerOre = 0;
                foreach (Transform row in oresHolder)
                {
                    foreach (Transform ore in row)
                    {
                        if (skeleton.GetComponent<SkeletonBehavior>().NavigationTarget == ore.GetChild(1))
                        {
                            indexerOreSaved[indexer] = indexerOre;
                            Debug.Log("saved one skeleton state");
                            Debug.Log("ore number was " + indexerOre);
                            return;
                        }
                        indexerOre++;
                    }
                }
            } else if (skeleton.GetComponent<SkeletonBehavior>().NavigationTarget != null && skeleton.GetComponent<SkeletonBehavior>().NavigationTarget.parent.name == "SkeletonPositions")
            {
                Debug.Log("skeleton was tackling door");
                isTacklingDoor[indexer] = true;
                Debug.Log(skeleton);
                Debug.Log(skeleton.GetComponent<SkeletonBehavior>());
                Debug.Log(skeleton.GetComponent<SkeletonBehavior>().NavigationTarget);
                Debug.Log(skeleton.GetComponent<SkeletonBehavior>().NavigationTarget.parent);
                Debug.Log(skeleton.GetComponent<SkeletonBehavior>().NavigationTarget.parent.parent);
                Debug.Log(skeleton.GetComponent<SkeletonBehavior>().NavigationTarget.parent.parent.GetComponent<DoorTacklingManager>().DoorLevel);
                connectedDoorIndex[indexer] = skeleton.GetComponent<SkeletonBehavior>().NavigationTarget.parent.parent.GetComponent<DoorTacklingManager>().DoorLevel;
            } else if (skeleton.GetComponent<SkeletonBehavior>().BeingUnconjured)
            {
                Debug.Log("skeleton was unconjured");
                isExcomunicated[indexer] = true;
            }
            indexer++;
            Debug.Log("saved one skeleton state");
        }
    }

}
