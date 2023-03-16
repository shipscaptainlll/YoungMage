using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDamageManager : MonoBehaviour
{
    [SerializeField] SkeletonBehavior skeletonBehavior;
    int currentDamage;

    public int CurrentDamage { get { return currentDamage; } set { currentDamage = value; } }

    // Start is called before the first frame update
    void Start()
    {
        currentDamage = skeletonBehavior.SkeletonDamage;
    }

    public void UpdateCurrentDamage(int upgrade)
    {
        currentDamage += upgrade;
        skeletonBehavior.SkeletonDamage = currentDamage;
        //Debug.Log("upgraded damage by " + upgrade + " current damage is " + currentDamage);
    }

}
