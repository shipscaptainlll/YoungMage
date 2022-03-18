using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArenaInstantiator : MonoBehaviour
{
    [SerializeField] Transform skeletonModel;
    [SerializeField] Transform instantiationPoint;

    [SerializeField] ClickManager clickManager;
    System.Random random;

    public event Action<Transform> SkeletonInstantiated = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        clickManager.EClicked += InstantiateSkeleton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateSkeleton()
    {
        float xPositionOffset = (float)random.Next(-10, 10);
        float zPositionOffset = (float)random.Next(-10, 10);
        Transform newSkeleton = Instantiate(skeletonModel, instantiationPoint.position + new Vector3(xPositionOffset, 0, zPositionOffset) , skeletonModel.rotation);
        if (SkeletonInstantiated != null) { SkeletonInstantiated(newSkeleton); }
    }
}
