using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArenaInstantiator : MonoBehaviour
{
    [SerializeField] Transform skeletonModel;
    [SerializeField] Transform instantiationPoint;
    [SerializeField] Transform skeletonsPotentionalPositions;
    [SerializeField] Transform castlePosition;
    [SerializeField] Transform skeletonsHolder;
    [SerializeField] Transform firstSceneSkeletonsPositions;
    [SerializeField] Transform secondSceneSkeletonsPositions;
    [SerializeField] int[] introNavroutIndexes;

    int skeletonsCount = 0;
    [SerializeField] int skeletonsMaxCount;
    [SerializeField] ClickManager clickManager;
    System.Random random;
    
    public int SkeletonsMaxCount { get { return skeletonsMaxCount; } set { skeletonsMaxCount = value; } }
    public int SkeletonsCount { get { return skeletonsCount; } set { skeletonsCount = value; } }
    public event Action<Transform> SkeletonInstantiated = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        StartCoroutine(DelayInstantiator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayInstantiator()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            InstantiateSkeleton();
        }
        
    }

    void InstantiateSkeleton()
    {
        if (skeletonsCount < skeletonsMaxCount)
        {
            skeletonsCount++;
            //Debug.Log("instantiated new one4");
            //Debug.Log("skeletons count" + skeletonsCount);
            //Debug.Log("skeletons max count" + skeletonsMaxCount);
            //Debug.Log(skeletonsCount < skeletonsMaxCount);
            
            float xPositionOffset = (float)random.Next(-10, 10);
            float zPositionOffset = (float)random.Next(-10, 10);
            Transform newSkeleton = Instantiate(skeletonModel, instantiationPoint.position + new Vector3(xPositionOffset, 0, zPositionOffset), skeletonModel.rotation);
            newSkeleton.gameObject.SetActive(true);
            foreach (Transform position in skeletonsPotentionalPositions)
            {
                if (position.gameObject.activeSelf)
                {

                    //Debug.Log(newSkeleton.GetComponent<SkeletonBehavior>());
                    newSkeleton.GetComponent<SkeletonBehavior>().ConnectToPosition(position);
                    newSkeleton.parent = skeletonsHolder;
                    newSkeleton.GetComponent<SkeletonBehavior>().CastlePosition = castlePosition;
                    //Debug.Log(newSkeleton.GetComponent<SkeletonBehavior>().Activity);
                    position.gameObject.SetActive(false);
                    if (SkeletonInstantiated != null) { SkeletonInstantiated(newSkeleton); }
                    return;
                }

            }
            
        }
        
    }

    public void ResetCounter()
    {
        skeletonsCount = 0;
    }

    public void InstantiateUploadedSkeleton(Vector3 skeletonPosition, Quaternion skeletonRotation)
    {

        if (skeletonsCount < skeletonsMaxCount)
        {
            Debug.Log("skeleton instantiated");
            skeletonsCount++;
            //Debug.Log("instantiated new one4");
            //Debug.Log("skeletons count" + skeletonsCount);
            //Debug.Log("skeletons max count" + skeletonsMaxCount);
            //Debug.Log(skeletonsCount < skeletonsMaxCount);

            float xPositionOffset = (float)random.Next(-10, 10);
            float zPositionOffset = (float)random.Next(-10, 10);
            Transform newSkeleton = Instantiate(skeletonModel, skeletonPosition + new Vector3(xPositionOffset, 0, zPositionOffset), skeletonRotation);

            newSkeleton.gameObject.SetActive(true);
            newSkeleton.parent = skeletonsHolder;
            if (SkeletonInstantiated != null) { SkeletonInstantiated(newSkeleton); }

        }

    }

    public void InstantiateIntroSkeletons(Vector3 introPosition)
    {

        float xPositionOffset = (float)random.Next(-10, 10);
        float zPositionOffset = (float)random.Next(-10, 10);
        Transform newSkeleton = Instantiate(skeletonModel, introPosition + new Vector3(xPositionOffset, 0, zPositionOffset), skeletonModel.rotation);
        newSkeleton.gameObject.SetActive(true);
        foreach (Transform position in skeletonsPotentionalPositions)
        {
            if (position.gameObject.activeSelf)
            {

                //Debug.Log(newSkeleton.GetComponent<SkeletonBehavior>());
                newSkeleton.GetComponent<SkeletonBehavior>().ConnectToPosition(position);
                newSkeleton.parent = skeletonsHolder;
                newSkeleton.GetComponent<SkeletonBehavior>().UploadCastleHitting();
                //Debug.Log(newSkeleton.GetComponent<SkeletonBehavior>().Activity);
                position.gameObject.SetActive(false);
                if (SkeletonInstantiated != null) { SkeletonInstantiated(newSkeleton); }
                return;
            }

        }
    }

    public void InstantiateIntroSkeletons(Vector3 introPosition, int castleNavroutIndex)
    {

        float xPositionOffset = (float)random.Next(-10, 10);
        float zPositionOffset = (float)random.Next(-10, 10);
        Transform newSkeleton = Instantiate(skeletonModel, introPosition + new Vector3(xPositionOffset, 0, zPositionOffset), skeletonModel.rotation);
        newSkeleton.gameObject.SetActive(true);
        foreach (Transform position in skeletonsPotentionalPositions)
        {
            if (position.gameObject.activeSelf)
            {

                //Debug.Log(newSkeleton.GetComponent<SkeletonBehavior>());
                newSkeleton.GetComponent<SkeletonBehavior>().ConnectToPosition(position);
                newSkeleton.parent = skeletonsHolder;
                newSkeleton.GetComponent<SkeletonBehavior>().UploadCastleRouteNumber(castleNavroutIndex);
                //Debug.Log(newSkeleton.GetComponent<SkeletonBehavior>().Activity);
                position.gameObject.SetActive(false);
                if (SkeletonInstantiated != null) { SkeletonInstantiated(newSkeleton); }
                return;
            }

        }
    }

    public void CreateIntroScene()
    {
        int indexer = 0;
        foreach (Transform position in firstSceneSkeletonsPositions)
        {
            InstantiateIntroSkeletons(position.position, introNavroutIndexes[indexer]);
            indexer++;
        }
        foreach (Transform position in secondSceneSkeletonsPositions)
        {
            InstantiateIntroSkeletons(position.position);
        }
    }
}
