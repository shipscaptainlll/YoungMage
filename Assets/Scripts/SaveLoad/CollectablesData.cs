using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectablesData
{
    public float[] health;

    public CollectablesData(Transform oresHolder)
    {
        GetHealth(oresHolder);
    }

    void GetHealth(Transform oresHolder)
    {
        health = new float[oresHolder.childCount];
        for (int i = 0; i < oresHolder.childCount; i++)
        {
            health[i] = oresHolder.GetChild(i).GetChild(1).Find("OreHealth").GetComponent<OreHealthDecreaser>().CurrentHealth;
        }

    }
}
