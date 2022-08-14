using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsCounterData
{
    public float[] oreCount;
    public float[] otherCount;
    public float[] skeletonItemsCount;
    public float[] oreProductsCount;

    public ItemsCounterData(Transform countersHolder)
    {
        GetOresCount(countersHolder);
        GetOthersCount(countersHolder);
        GetSkeletonItemsCount(countersHolder);
        GetOreProductsCount(countersHolder);
    }

    void GetOresCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(0);
        oreCount = new float[subtypeHolder.childCount];
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            oreCount[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().Count;
        }
    }

    void GetOthersCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(1);
        oreCount = new float[subtypeHolder.childCount];
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            oreCount[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().Count;
        }
    }

    void GetSkeletonItemsCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(2);
        oreCount = new float[subtypeHolder.childCount];
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            oreCount[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().Count;
        }
    }

    void GetOreProductsCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(3);
        oreCount = new float[subtypeHolder.childCount];
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            oreCount[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().Count;
        }
    }
}
