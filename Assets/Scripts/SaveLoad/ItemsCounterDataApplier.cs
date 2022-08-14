using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemsCounterDataApplier
{
    static Transform countersHolderLoaded;
    static ItemsCounterData itemsDataLoaded;

    public static void ApplyCountersData(Transform countersHolder, ItemsCounterData itemsCounterData)
    {
        UpdateData(countersHolder, itemsCounterData);
        ApplyOresCount(countersHolderLoaded);
        ApplyOthersCount(countersHolderLoaded);
        ApplySkeletonItemsCount(countersHolderLoaded);
        ApplyOreProductsCount(countersHolderLoaded);
        DisconnectData();
    }

    static void UpdateData(Transform countersHolder, ItemsCounterData itemsCounterData)
    {
        countersHolderLoaded = countersHolder;
        itemsDataLoaded = itemsCounterData;
    }

    static void DisconnectData()
    {
        countersHolderLoaded = null;
        itemsDataLoaded = null;
    }

    static void ApplyOresCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(0);
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            subtypeHolder.GetChild(i).GetComponent<ICounter>().Count = (int) itemsDataLoaded.oreCount[i];
        }
    }

    static void ApplyOthersCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(1);
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            subtypeHolder.GetChild(i).GetComponent<ICounter>().Count = (int)itemsDataLoaded.oreCount[i];
        }
    }

    static void ApplySkeletonItemsCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(2);
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            subtypeHolder.GetChild(i).GetComponent<ICounter>().Count = (int)itemsDataLoaded.oreCount[i];
        }
    }

    static void ApplyOreProductsCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(3);
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            subtypeHolder.GetChild(i).GetComponent<ICounter>().Count = (int)itemsDataLoaded.oreCount[i];
        }
    }
}
