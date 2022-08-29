using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonItem : MonoBehaviour
{
    int itemID;
    SkeletonBehavior skeletonScript;
    MaterialState skeletonMaterialState = MaterialState.normal;
    bool beingEdited;
    public enum MaterialState
    {
        normal,
        deletion,
        adding
    }

    
    public int ItemID { get { return itemID; } set { itemID = value; } }
    public SkeletonBehavior SkeletonScript { get { return skeletonScript; } set { skeletonScript = value; } }
    public MaterialState SkeletonMaterialState { get { return skeletonMaterialState; } set { skeletonMaterialState = value; } }
    public bool BeingEdited { get { return beingEdited; } set { beingEdited = value; } }
}
