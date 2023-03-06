using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowCatapultArenaInstantiator : MonoBehaviour
{
    [SerializeField] Transform catapultModel;
    [SerializeField] Transform skeletonModel;
    [SerializeField] Transform instantiationPoint;
    [SerializeField] Transform castlePotentionalPositions;
    [SerializeField] Transform castlePosition;
    [SerializeField] Transform skeletonsHolder;
    [SerializeField] Transform crossbowCatapultsHolder;
    [SerializeField] CastleHealthDecreaser castleHealthDecreaser;

    int catapultsCount = 0;
    int skeletonsCount = 0;
    [SerializeField] int catapultsMaxCount;
    [SerializeField] ClickManager clickManager;
    System.Random random;
    
    public int CatapultsMaxCount { get { return catapultsMaxCount; } set { catapultsMaxCount = value; } }
    public int CatapultsCount { get { return catapultsCount; } set { catapultsCount = value; } }
    public int SkeletonsCount { get { return skeletonsCount; } set { skeletonsCount = value; } }
    public event Action<Transform> SkeletonInstantiated = delegate { };
    public event Action<Transform> CatapultInstantiated = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        StartCoroutine(DelayInstantiator());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayInstantiator()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            InstantiateCatapult();
        }
        
    }

    void InstantiateCatapult()
    {
        if (catapultsCount < catapultsMaxCount)
        {
            catapultsCount++;
            float xPositionOffset = (float)random.Next(-10, 10);
            float zPositionOffset = (float)random.Next(-10, 10);

            Transform newSkeleton = Instantiate(skeletonModel, instantiationPoint.position + new Vector3(xPositionOffset, 0, zPositionOffset), skeletonModel.rotation);
            newSkeleton.gameObject.SetActive(true);
            Transform newCatapult = Instantiate(catapultModel, instantiationPoint.position + new Vector3(xPositionOffset, 0, zPositionOffset), catapultModel.rotation);
            newSkeleton.GetComponent<SkeletonBehavior>().ConnectedCatapult = newCatapult;
            newCatapult.GetComponent<CatapultMovement>().InstantiationSetUp();
            newCatapult.GetComponent<CatapultMovement>().SubscribeOnSkeleton(newSkeleton);
            newCatapult.gameObject.SetActive(true);
            newCatapult.GetChild(0).GetChild(0).GetComponent<CrossbowFire>().CastleHealthDecreaser = castleHealthDecreaser;
            foreach (Transform position in castlePotentionalPositions)
            {
                if (position.gameObject.activeSelf)
                {
                    newSkeleton.GetComponent<SkeletonBehavior>().ConnectToPosition(position);
                    newSkeleton.parent = skeletonsHolder;
                    newSkeleton.GetComponent<SkeletonBehavior>().CastlePosition = castlePosition;
                    position.gameObject.SetActive(false);
                    if (SkeletonInstantiated != null) { SkeletonInstantiated(newSkeleton); }
                    newCatapult.parent = crossbowCatapultsHolder;
                    position.gameObject.SetActive(false);
                    if (CatapultInstantiated != null) { CatapultInstantiated(newCatapult); }
                    return;
                }

            }
        }


    }

    public void ResetCounter()
    {
        catapultsCount = 0;
    }

    public void InstantiateUploadedSkeleton(Vector3 skeletonPosition, Quaternion skeletonRotation)
    {

        if (catapultsCount < catapultsMaxCount)
        {
            catapultsCount++;
            float xPositionOffset = (float)random.Next(-10, 10);
            float zPositionOffset = (float)random.Next(-10, 10);

            Transform newSkeleton = Instantiate(skeletonModel, skeletonPosition + new Vector3(xPositionOffset, 0, zPositionOffset), skeletonRotation);
            newSkeleton.gameObject.SetActive(true);
            Transform newCatapult = Instantiate(catapultModel, skeletonPosition + new Vector3(xPositionOffset, 0, zPositionOffset), skeletonRotation);
            newSkeleton.GetComponent<SkeletonBehavior>().ConnectedCatapult = newCatapult;
            newCatapult.GetComponent<CatapultMovement>().InstantiationSetUp();
            newCatapult.GetComponent<CatapultMovement>().SubscribeOnSkeleton(newSkeleton);
            newCatapult.gameObject.SetActive(true);
            newCatapult.GetChild(0).GetChild(0).GetComponent<CrossbowFire>().CastleHealthDecreaser = castleHealthDecreaser;

            newSkeleton.parent = skeletonsHolder;
            if (SkeletonInstantiated != null) { SkeletonInstantiated(newSkeleton); }
            newCatapult.parent = crossbowCatapultsHolder;
            if (CatapultInstantiated != null) { CatapultInstantiated(newCatapult); }
        }

    }

}
