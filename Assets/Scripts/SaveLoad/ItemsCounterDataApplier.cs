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
            if (itemsDataLoaded.oreCount[i] > 0)
            {
                subtypeHolder.GetChild(i).GetComponent<ICounter>().ItemOpened = true;
            }
            //Debug.Log("if " + i + " count " + itemsDataLoaded.oreCount[i]);
        }
    }

    static void ApplyOthersCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(1);
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            subtypeHolder.GetChild(i).GetComponent<ICounter>().Count = (int)itemsDataLoaded.otherCount[i];
            if (itemsDataLoaded.otherCount[i] > 0)
            {
                subtypeHolder.GetChild(i).GetComponent<ICounter>().ItemOpened = true;
            }
            //Debug.Log("if " + i + " count " + itemsDataLoaded.otherCount[i]);
        }
    }

    static void ApplySkeletonItemsCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(2);
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            subtypeHolder.GetChild(i).GetComponent<ICounter>().Count = (int)itemsDataLoaded.skeletonItemsCount[i];
            if (itemsDataLoaded.skeletonItemsCount[i] > 0)
            {
                subtypeHolder.GetChild(i).GetComponent<ICounter>().ItemOpened = true;
            }
            //Debug.Log("if " + i + " count " + itemsDataLoaded.skeletonItemsCount[i]);
        }
    }

    static void ApplyOreProductsCount(Transform countersHolder)
    {
        Transform subtypeHolder = countersHolder.GetChild(3);
        for (int i = 0; i < subtypeHolder.childCount; i++)
        {
            subtypeHolder.GetChild(i).GetComponent<ICounter>().Count = (int)itemsDataLoaded.oreProductsCount[i];
            if (itemsDataLoaded.oreProductsCount[i] > 0)
            {
                subtypeHolder.GetChild(i).GetComponent<ICounter>().ItemOpened = true;
            }
            //Debug.Log("if " + i + " count " + itemsDataLoaded.oreProductsCount[i]);
        }
    }
}
