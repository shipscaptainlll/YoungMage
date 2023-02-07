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
        //Debug.Log(oreData.health.Length);
        //Debug.Log(oreData.healthVesibility.Length);
        ApplyHealth(oreDataHolder);
        ApplyHealthVisibility(oreDataHolder);
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
        int indexer = 0;
        foreach (Transform row in oresHolder)
        {
            foreach (Transform ore in row)
            {
                ore.GetChild(1).Find("OreHealth").GetComponent<OreHealthDecreaser>().CurrentHealth = oreDataLoaded.health[indexer++];
                ore.GetChild(1).Find("OreHealth").GetComponent<OreHealthDecreaser>().UpdateOreHealth();
            }
        }
    }

    static void ApplyHealthVisibility(Transform oresHolder)
    {
        int indexer = 0;
        foreach (Transform row in oresHolder)
        {
            foreach (Transform ore in row)
            {
                if (oreDataLoaded.healthVesibility[indexer] == 1)
                {
                    ore.GetChild(1).GetComponent<OreMiningManager>().VisualiseOreHealthbar();
                }
                else
                {
                    ore.GetChild(1).GetComponent<OreMiningManager>().HideOreHealthbar();
                }

                indexer++;
            }
        }
    }
}
