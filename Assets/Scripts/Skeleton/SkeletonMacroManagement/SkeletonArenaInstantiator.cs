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

    int skeletonsCount = 0;
    [SerializeField] int skeletonsMaxCount;
    [SerializeField] ClickManager clickManager;
    System.Random random;

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
}
