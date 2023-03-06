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
    [SerializeField] Transform arenaSkeletonsHolder;
    [SerializeField] Transform catapultkeletonsHolder;
    [SerializeField] Transform crossbowkeletonsHolder;
    [SerializeField] Transform catapultsPartsHolder;
    [SerializeField] Transform crossbowPartsHolder;
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

    public void ResetSkeletonsStack()
    {
        skeletonsStack = new List<Transform>();
    }

    public void SaveSkeletonArena(Transform newSkeleton)
    {
        skeletonsArena.Add(newSkeleton);
        if (SkeletonArenaAdded != null) { SkeletonArenaAdded(); }
    }

    public void DeleteCatapultsParts()
    {
        
        int cacheCount = catapultsPartsHolder.childCount;
        for (int i = 0; i < cacheCount; i++)
        {
            Destroy(catapultsPartsHolder.GetChild(i).gameObject);
        }

        Debug.Log("Big catapults were destroyed ");
    }

    public void DeleteCrossbowCatapultsParts()
    {

        int cacheCount = crossbowPartsHolder.childCount;
        for (int i = 0; i < cacheCount; i++)
        {
            Destroy(crossbowPartsHolder.GetChild(i).gameObject);
        }

        Debug.Log("Crossbow catapults were destroyed ");
    }

    public void ResetSkeletonsArena()
    {
        int cacheCount = arenaSkeletonsHolder.childCount;
        for (int i = 0; i < cacheCount; i++)
        {
            arenaSkeletonsHolder.GetChild(i).GetComponent<SkeletonBehavior>().UnsubscribeBeforeDestruction();
            

            if (arenaSkeletonsHolder.GetChild(i).GetComponent<SkeletonBehavior>().FracturedSkeleton != null)
            {
                Destroy(arenaSkeletonsHolder.GetChild(i).GetComponent<SkeletonBehavior>().FracturedSkeleton.gameObject);
            }
            Destroy(arenaSkeletonsHolder.GetChild(i).gameObject);
        }

        Debug.Log("Small skeletons were destroyed ");
        skeletonsArena = new List<Transform>();
    }

    public void ResetCatapultsArena()
    {
        int cacheCount = catapultkeletonsHolder.childCount;
        for (int i = 0; i < cacheCount; i++)
        {
            catapultkeletonsHolder.GetChild(i).GetComponent<SkeletonBehavior>().DestroyCatapult();
            catapultkeletonsHolder.GetChild(i).GetComponent<SkeletonBehavior>().UnsubscribeBeforeDestruction();
            //Debug.Log("Count in lists: " + skeletonsStack.Count);

            if (catapultkeletonsHolder.GetChild(i).GetComponent<SkeletonBehavior>().FracturedSkeleton != null)
            {
                Destroy(catapultkeletonsHolder.GetChild(i).GetComponent<SkeletonBehavior>().FracturedSkeleton.gameObject);
            }
            Destroy(catapultkeletonsHolder.GetChild(i).gameObject);
        }

        Debug.Log("Catapult skeletons were destroyed ");
        skeletonsArena = new List<Transform>();
    }

    public void ResetCrossbowsArena()
    {
        int cacheCount = crossbowkeletonsHolder.childCount;
        for (int i = 0; i < cacheCount; i++)
        {
            crossbowkeletonsHolder.GetChild(i).GetComponent<SkeletonBehavior>().DestroyCatapult();
            crossbowkeletonsHolder.GetChild(i).GetComponent<SkeletonBehavior>().UnsubscribeBeforeDestruction();
            //Debug.Log("Count in lists: " + skeletonsStack.Count);

            if (crossbowkeletonsHolder.GetChild(i).GetComponent<SkeletonBehavior>().FracturedSkeleton != null)
            {
                Destroy(crossbowkeletonsHolder.GetChild(i).GetComponent<SkeletonBehavior>().FracturedSkeleton.gameObject);
            }
            Destroy(crossbowkeletonsHolder.GetChild(i).gameObject);
        }

        Debug.Log("Crossbow skeletons were destroyed ");
        skeletonsArena = new List<Transform>();
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
