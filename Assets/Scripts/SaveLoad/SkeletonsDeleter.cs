using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonsDeleter : MonoBehaviour
{
    [SerializeField] SkeletonsStack skeletonsStack;
    [SerializeField] Transform fracturedSkeletonsHolder;
    [SerializeField] SkeletonArenaInstantiator skeletonArenaInstantiator;
    [SerializeField] CrossbowCatapultArenaInstantiator crossbowCatapultArenaInstantiator;
    [SerializeField] CatapultArenaInstantiator catapultArenaInstantiator;
    

    public void ResetSkeletonsStack()
    {
        skeletonsStack.ResetSkeletonsStack();
    }

    public void DeleteArenaSkeletons()
    {
        skeletonsStack.ResetSkeletonsArena();
    }

    public void DeleteCatapultSkeletons()
    {
        skeletonsStack.ResetCatapultsArena();
        skeletonsStack.DeleteCatapultsParts();
    }

    public void DeleteCrossbowSkeletons()
    {
        skeletonsStack.ResetCrossbowsArena();
        skeletonsStack.DeleteCrossbowCatapultsParts();
    }

    public void ResetCrossbowArenaInstatiator()
    {
        crossbowCatapultArenaInstantiator.ResetCounter();
    }

    public void ResetCatapultArenaInstantiator()
    {
        catapultArenaInstantiator.ResetCounter();
    }

    public void ResetSkeletonsArenaInstatiator()
    {
        skeletonArenaInstantiator.ResetCounter();
    }

    public void DeleteInhouseSkeeletons(Transform skeletonsHolder)
    {
        skeletonsStack.DeleteHouseSkeletons();
        int count = skeletonsHolder.childCount;
        //Debug.Log("count is " + count);
        for (int i = 0; i < count; i++)
        {
            //Debug.Log("before deleted and count in holder is " + skeletonsHolder.childCount);
            skeletonsHolder.GetChild(0).GetComponent<SkeletonBehavior>().DestroyUploadSkeleton();
            //Debug.Log("was deleted and count in holder is " + skeletonsHolder.childCount);
        }
    }

    public void DeleteFracturedSkeletons()
    {
        for (int i = 0; i < fracturedSkeletonsHolder.childCount; i++)
        {
            DestroyImmediate(fracturedSkeletonsHolder.GetChild(0).gameObject);
        }
    }
}
