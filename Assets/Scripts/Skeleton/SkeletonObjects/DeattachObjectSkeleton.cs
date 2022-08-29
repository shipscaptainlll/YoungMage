using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeattachObjectSkeleton : MonoBehaviour
{
    [Header("Main scripts")]
    [SerializeField] ObjectManager objectManager;

    [Header("Counters")]
    [SerializeField] StoneHandsCounter stoneHandsCounter;
    [SerializeField] MagicGlovesCounterUpdated glovesCounter;
    [SerializeField] LeggingsCounter leggingsCounter;
    [SerializeField] PlateArmorCounter plateArmorCounter;
    [SerializeField] ShoesCounter shoesCounter;
    [SerializeField] HelmCounter helmCounter;
    [SerializeField] BracersCounter bracersCounter;

    [Header("Other")]
    [SerializeField] float dematerializationDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void DeattachObject(SkeletonBehavior skeleton, int id)
    {
        if (Input.GetKey(KeyCode.O))
        {
            Debug.Log("hello there");
            SkeletonAttachedObjects skeletonAttachedObjects = skeleton.transform.GetComponent<SkeletonAttachedObjects>();
            switch (id)
            {
                case 11:
                    if (skeleton.IsConnectedHands)
                    {
                        stoneHandsCounter.AddResource(1);
                        skeleton.IsConnectedHands = false;
                        StartCoroutine(DematerializeProduct(skeletonAttachedObjects.ConnectedHands, dematerializationDuration));
                        skeletonAttachedObjects.ConnectedHands = null;
                    }
                    break;
                case 16:
                    if (skeleton.IsConnectedHands)
                    {
                        glovesCounter.AddResource(1);
                        skeleton.IsConnectedHands = false;
                        StartCoroutine(DematerializeProduct(skeletonAttachedObjects.ConnectedGloves, dematerializationDuration));
                        skeletonAttachedObjects.ConnectedGloves = null;
                    }
                    break;
                case 12:
                    if (skeleton.IsConnectedLeggings)
                    {
                        leggingsCounter.AddResource(1);
                        skeleton.IsConnectedLeggings = false;
                        StartCoroutine(DematerializeProduct(skeletonAttachedObjects.ConnectedLeggings, dematerializationDuration));
                        skeletonAttachedObjects.ConnectedLeggings = null;
                    }
                    break;
                case 13:
                    if (skeleton.IsConnectedArmor)
                    {
                        plateArmorCounter.AddResource(1);
                        skeleton.IsConnectedArmor = false;
                        StartCoroutine(DematerializeProduct(skeletonAttachedObjects.ConnectedArmor, dematerializationDuration));
                        skeletonAttachedObjects.ConnectedArmor = null;
                    }
                    break;
                case 14:
                    if (skeleton.IsConnectedShoes)
                    {
                        shoesCounter.AddResource(1);
                        skeleton.IsConnectedShoes = false;
                        StartCoroutine(DematerializeProduct(skeletonAttachedObjects.ConnectedShoes, dematerializationDuration));
                        skeletonAttachedObjects.ConnectedShoes = null;
                    }
                    break;
                case 15:
                    if (skeleton.IsConnectedHelm)
                    {
                        helmCounter.AddResource(1);
                        skeleton.IsConnectedHelm = false;
                        StartCoroutine(DematerializeProduct(skeletonAttachedObjects.ConnectedHelm, dematerializationDuration));
                        skeletonAttachedObjects.ConnectedHelm = null;
                    }
                    break;
                case 17:
                    if (skeleton.IsConnectedBracers)
                    {
                        bracersCounter.AddResource(1);
                        skeleton.IsConnectedBracers = false;
                        StartCoroutine(DematerializeProduct(skeletonAttachedObjects.ConnectedBracers, dematerializationDuration));
                        skeletonAttachedObjects.ConnectedBracers = null;
                    }
                    break;
            }
            Debug.Log("hello there1");
        }
    }

    IEnumerator DematerializeProduct(Transform productTransform, float duration)
    {
        productTransform.GetChild(0).GetComponent<SkeletonItem>().BeingEdited = true;
        float elapsed = 0;
        Debug.Log("hello");
        MeshRenderer productMeshrenderer = productTransform.GetChild(0).GetComponent<MeshRenderer>();
        Debug.Log("hello");
        Material productMaterial = productMeshrenderer.material;
        Debug.Log("hello");
        float currentMaterialization;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentMaterialization = Mathf.Lerp(0, 1f, elapsed / duration);
            productMaterial.SetFloat("_Clip", currentMaterialization);
            productMeshrenderer.material = productMaterial;
            Debug.Log(productTransform.GetChild(0).GetComponent<MeshRenderer>().material.GetFloat("_Clip"));
            yield return null;
        }
        productMaterial.SetFloat("_Clip", 1f);
        productMeshrenderer.material = productMaterial;
        Destroy(productTransform.gameObject);
        yield return null;
    }

}
