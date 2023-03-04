using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OuterSmallSkeletonData
{
    public float[][] positions;
    public float[][] rotations;
    public bool[] onRoute;
    public int[] routeState;
    public bool[] inCastle;
    public int[] currentHealth;
    public bool[] portalConnected;

    public OuterSmallSkeletonData(Transform skeletonsHolder)
    {
        GetPositions(skeletonsHolder);
        GetRotations(skeletonsHolder);
        GetState(skeletonsHolder);
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
            Debug.Log("saved one small outer skeleton positions");
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
            Debug.Log("saved one small outer skeleton rotations");
        }
    }

    void GetState(Transform skeletonsHolder)
    {
        int indexer = 0;
        onRoute = new bool[skeletonsHolder.childCount];
        routeState = new int[skeletonsHolder.childCount];
        inCastle = new bool[skeletonsHolder.childCount];
        currentHealth = new int[skeletonsHolder.childCount];
        portalConnected = new bool[skeletonsHolder.childCount];
        foreach (Transform skeleton in skeletonsHolder)
        {
            onRoute[indexer] = false;
            routeState[indexer] = 0;
            inCastle[indexer] = false;
            currentHealth[indexer] = (int) skeleton.GetComponent<SkeletonHealthDecreaser>().Health;
            portalConnected[indexer] = false;

            if (skeleton.GetComponent<SkeletonBehavior>().CastleNavroutActive)
            {
                onRoute[indexer] = true;
                routeState[indexer] = skeleton.GetComponent<SkeletonBehavior>().CastleNavpointNumber;
            } else if (skeleton.GetComponent<SkeletonBehavior>().HittingCastle)
            {
                inCastle[indexer] = true;
            } else if (skeleton.GetComponent<SkeletonBehavior>().ConnectedToPortal)
            {
                portalConnected[indexer] = true;
            }
        }
    }

}
