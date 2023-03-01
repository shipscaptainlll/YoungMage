using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonsDeleter : MonoBehaviour
{
    [SerializeField] SkeletonsStack skeletonsStack;

    public void DeleteInhouseSkeeletons(Transform skeletonsHolder)
    {
        skeletonsStack.DeleteHouseSkeletons();
        int count = skeletonsHolder.childCount;
        Debug.Log("count is " + count);
        for (int i = 0; i < count; i++)
        {
            Debug.Log("before deleted and count in holder is " + skeletonsHolder.childCount);
            skeletonsHolder.GetChild(0).GetComponent<SkeletonBehavior>().DestroyUploadSkeleton();
            Debug.Log("was deleted and count in holder is " + skeletonsHolder.childCount);
        }
    }
}
