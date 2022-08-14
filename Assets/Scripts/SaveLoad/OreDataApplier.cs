using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OreDataApplier
{
    static Transform oreDataHolderLoaded;
    static OreData oreDataLoaded;

    public static void ApplyOreData(Transform oreDataHolder, OreData oreData)
    {
        UpdateData(oreDataHolder, oreData);
        ApplyHealth(oreDataHolder);
        DisconnectData();
    }

    static void UpdateData(Transform oreDataHolder, OreData oreData)
    {
        oreDataHolderLoaded = oreDataHolder;
        oreDataLoaded = oreData;
    }

    static void DisconnectData()
    {
        oreDataHolderLoaded = null;
        oreDataLoaded = null;
    }

    static void ApplyHealth(Transform oresHolder)
    {
        for (int i = 0; i < oresHolder.childCount; i++)
        {
            oresHolder.GetChild(i).GetChild(1).Find("OreHealth").GetComponent<OreHealthDecreaser>().CurrentHealth = oreDataLoaded.health[i];
        }

    }
}
