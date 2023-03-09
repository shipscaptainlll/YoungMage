using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemsCounterData
{
    public bool[] oreOpened;
    public float[] oreCount;
    public bool[] otherOpened;
    public float[] otherCount;
    public bool[] skeletonItemsOpened;
    public float[] skeletonItemsCount;
    public bool[] oreProductsOpened;
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
        oreOpened = new bool[subtypeHolder.childCount];
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            Debug.Log(subtypeHolder.GetChild(i) + " item count was " + subtypeHolder.GetChild(i).GetComponent<ICounter>().Count);
            Debug.Log(subtypeHolder.GetChild(i) + " item opened was " + subtypeHolder.GetChild(i).GetComponent<ICounter>().ItemOpened);
            oreOpened[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().ItemOpened;
            oreCount[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().Count;
        }
    }

    void GetOthersCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(1);
        otherCount = new float[subtypeHolder.childCount];
        otherOpened = new bool[subtypeHolder.childCount];
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            otherOpened[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().ItemOpened;
            otherCount[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().Count;
        }
    }

    void GetSkeletonItemsCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(2);
        skeletonItemsCount = new float[subtypeHolder.childCount];
        skeletonItemsOpened = new bool[subtypeHolder.childCount];
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            skeletonItemsOpened[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().ItemOpened;
            skeletonItemsCount[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().Count;
        }
    }

    void GetOreProductsCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(3);
        oreProductsCount = new float[subtypeHolder.childCount];
        oreProductsOpened = new bool[subtypeHolder.childCount];
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            oreProductsOpened[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().ItemOpened;
            oreProductsCount[i] = subtypeHolder.GetChild(i).GetComponent<ICounter>().Count;
        }
    }
}
