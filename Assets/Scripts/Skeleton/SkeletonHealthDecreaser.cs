using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealthDecreaser : MonoBehaviour
{
    [SerializeField] SkeletonHouseInstantiator skeletonHouseInstantiator;
    [SerializeField] SkeletonsStack skeletonStack;
    [SerializeField] PortalOpener portalOpener;
    SkeletonBehavior skeletonBehavior;
    bool isDestroyable;
    float health;

    public event Action SkeletonUnsubscribed = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        skeletonBehavior = transform.GetComponent<SkeletonBehavior>();
        isDestroyable = true;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHealth()
    {
        health -= 7;
        //Debug.Log(transform + " current health " + health);
        if (health < 0 && isDestroyable)
        {
            isDestroyable = false;
            UnscubscribeSkeletonSoldier();
            DestroySkeleton();
            skeletonBehavior.DestroyManually();
            
        }
    }

    void DestroySkeleton()
    {
        if (transform.GetComponent<SkeletonBehavior>().AttachedCopycat != null)
        {
            Debug.Log("wasnt null bo ");
            transform.GetComponent<CopycatCreator>().destroyCopycat(transform);
        }
        skeletonStack.DeleteSkeleton(transform);
        skeletonHouseInstantiator.DestroySkeleton(transform.GetComponent<SkeletonBehavior>());
        if (portalOpener.ChoosenSkeleton == transform) { portalOpener.InitiatePortalOpening(); }
    }

    public void DestroyUploading()
    {
        //isDestroyable = false;
        //UnscubscribeSkeletonSoldier();
        //DestroySkeleton();
        //skeletonBehavior.DestroyUploadSkeleton();
    }

    public void UnscubscribeSkeletonSoldier()
    {
        if (SkeletonUnsubscribed != null) { SkeletonUnsubscribed(); }
    }
}
