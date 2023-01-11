using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OreData
{
    public float[] health;
    public int[] healthVesibility;

    public OreData(Transform oresHolder)
    {
        GetHealthVisibility(oresHolder);
        GetHealth(oresHolder);
        
    }

    void GetHealth(Transform oresHolder)
    {
        List<float> healthCache = new List<float>();

        foreach (Transform row in oresHolder)
        {
            foreach (Transform ore in row)
            {
                healthCache.Add(ore.GetChild(1).Find("OreHealth").GetComponent<OreHealthDecreaser>().CurrentHealth);
            }
        }
        
        health = new float[healthCache.Count];
        health = healthCache.ToArray();
    }

    void GetHealthVisibility(Transform oresHolder)
    {
        List<int> healthCache = new List<int>();

        foreach (Transform row in oresHolder)
        {
            foreach (Transform ore in row)
            {
                bool cache = ore.GetChild(1).GetComponent<OreMiningManager>().HealthVisible;
                if (cache)
                {
                    healthCache.Add(1);
                }
                else
                {
                    healthCache.Add(0);
                }
            }
        }

        healthVesibility = new int[healthCache.Count];
        healthVesibility = healthCache.ToArray();
    }
}
