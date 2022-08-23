using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowCatapultArenaInstantiator : MonoBehaviour
{
    [SerializeField] Transform catapultModel;
    [SerializeField] Transform instantiationPoint;
    [SerializeField] Transform castlePotentionalPositions;
    [SerializeField] Transform castlePosition;
    [SerializeField] Transform crossbowCatapultsHolder;
    [SerializeField] CastleHealthDecreaser castleHealthDecreaser;

    int catapultsCount = 0;
    [SerializeField] int catapultsMaxCount;
    [SerializeField] ClickManager clickManager;
    System.Random random;

    public int CatapultsCount { get { return catapultsCount; } set { catapultsCount = value; } }
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
            yield return new WaitForSeconds(18f);
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
            Transform newCatapult = Instantiate(catapultModel, instantiationPoint.position + new Vector3(xPositionOffset, 0, zPositionOffset), catapultModel.rotation);
            newCatapult.gameObject.SetActive(true);
            newCatapult.GetChild(0).GetChild(0).GetComponent<CrossbowFire>().CastleHealthDecreaser = castleHealthDecreaser;
            foreach (Transform position in castlePotentionalPositions)
            {
                if (position.gameObject.activeSelf)
                {

                    //Debug.Log(newSkeleton.GetComponent<SkeletonBehavior>());
                    newCatapult.parent = crossbowCatapultsHolder;
                    //Debug.Log(newSkeleton.GetComponent<SkeletonBehavior>().Activity);
                    position.gameObject.SetActive(false);
                    if (CatapultInstantiated != null) { CatapultInstantiated(newCatapult); }
                    return;
                }

            }
            
        }
        
    }
}
