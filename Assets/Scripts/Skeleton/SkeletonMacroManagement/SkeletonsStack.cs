using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonsStack : MonoBehaviour
{
    [SerializeField] SkeletonArenaInstantiator skeletonArenaInstantiator;
    [SerializeField] CrossbowCatapultArenaInstantiator crossbowCatapultArenaInstantiator;
    [SerializeField] CatapultArenaInstantiator catapultArenaInstantiator;
    [SerializeField] SkeletonHouseInstantiator skeletonHouseInstantiator;
    List<Transform> skeletonsStack = new List<Transform>();//in field
    List<Transform> skeletonsHouseStack = new List<Transform>();
    List<Transform> skeletonsArena = new List<Transform>();


    public List<Transform> SkeletonStack { get { return skeletonsStack; } }
    public List<Transform> SkeletonsArena { get { return skeletonsArena; } }

    public event Action SkeletonArenaAdded = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        skeletonArenaInstantiator.SkeletonInstantiated += SaveSkeleton;
        crossbowCatapultArenaInstantiator.SkeletonInstantiated += SaveSkeleton;
        catapultArenaInstantiator.SkeletonInstantiated += SaveSkeleton;
        skeletonHouseInstantiator.HouseSkeletonCreated += SaveHouseSkeleton;
        skeletonHouseInstantiator.SkeletonDestroyed += DeleteSkeleton;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log(SkeletonsArena.Count);
        }
    }

    void SaveSkeleton(Transform newSkeleton)
    {
        skeletonsStack.Add(newSkeleton);

    }

    public void SaveSkeletonArena(Transform newSkeleton)
    {
        skeletonsArena.Add(newSkeleton);
        if (SkeletonArenaAdded != null) { SkeletonArenaAdded(); }
    }

    public void DeleteSkeleton(Transform deletedSkeleton)
    {
        skeletonsStack.Remove(deletedSkeleton);
        deletedSkeleton.GetComponent<SkeletonBehavior>().UnsubscribeBeforeDestruction();
        //Debug.Log("Count in lists: " + skeletonsStack.Count);

        if (deletedSkeleton.GetComponent<SkeletonBehavior>().FracturedSkeleton != null) { 
            Destroy(deletedSkeleton.GetComponent<SkeletonBehavior>().FracturedSkeleton.gameObject); }
        Destroy(deletedSkeleton.gameObject);
    }

    void SaveHouseSkeleton(Transform newSkeleton)
    {
        skeletonsHouseStack.Add(newSkeleton);

    }

    public void DeleteHouseSkeletons()
    {
        skeletonsStack = new List<Transform>();
    }

    void DeleteHouseSkeleton(Transform deletedSkeleton)
    {
        skeletonsHouseStack.Remove(deletedSkeleton);
        Debug.Log("Count in lists: " + skeletonsHouseStack.Count);
        Destroy(deletedSkeleton.gameObject);
    }
}
