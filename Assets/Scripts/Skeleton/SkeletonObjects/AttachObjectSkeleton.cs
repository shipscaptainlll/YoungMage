using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachObjectSkeleton : MonoBehaviour
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

    [Header("Skeleton points positions")]
    [SerializeField] Transform stoneHandsPosition;
    [SerializeField] Transform glovesPosition;
    [SerializeField] Transform leggingsPosition;
    [SerializeField] Transform plateArmorPosition;
    [SerializeField] Transform shoesPosition;
    [SerializeField] Transform helmPosition;
    [SerializeField] Transform bracersPosition;

    [SerializeField] Vector3 stoneHandsRotation;
    [SerializeField] Vector3 glovesRotation;
    [SerializeField] Vector3 leggingsRotation;
    [SerializeField] Vector3 plateArmorRotation;
    [SerializeField] Vector3 shoesRotation;
    [SerializeField] Vector3 helmRotation;
    [SerializeField] Vector3 bracersRotation;

    [Header("Other")]
    [SerializeField] float materializationDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void AttachObject(SkeletonBehavior skeleton, int id)
    {
        Debug.Log("Hello1");
        SkeletonAttachedObjects skeletonAttachedObjects = skeleton.transform.GetComponent<SkeletonAttachedObjects>();
        Transform item = null;
        switch (id)
        {
            case 11:
                if (!skeleton.IsConnectedHands)
                {
                    stoneHandsCounter.AddResource(-1);
                    skeleton.IsConnectedHands = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = stoneHandsPosition;
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(stoneHandsRotation);
                    skeletonAttachedObjects.ConnectedHands = item;
                }
                break;
            case 16:
                if (!skeleton.IsConnectedHands)
                {
                    glovesCounter.AddResource(-1);
                    skeleton.IsConnectedHands = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = glovesPosition;
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(glovesRotation);
                    skeletonAttachedObjects.ConnectedGloves = item;
                }
                break;
            case 12:
                if (!skeleton.IsConnectedLeggings)
                {
                    leggingsCounter.AddResource(-1);
                    skeleton.IsConnectedLeggings = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = leggingsPosition;
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(leggingsRotation);
                    skeletonAttachedObjects.ConnectedLeggings = item;
                }
                break;
            case 13:
                if (!skeleton.IsConnectedArmor)
                {
                    plateArmorCounter.AddResource(-1);
                    skeleton.IsConnectedArmor = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = plateArmorPosition;
                    item.localPosition = new Vector3(0, -5.5f, -2.2f);
                    item.localRotation = Quaternion.Euler(plateArmorRotation);
                    skeletonAttachedObjects.ConnectedArmor = item;
                }
                break;
            case 14:
                if (!skeleton.IsConnectedShoes)
                {
                    shoesCounter.AddResource(-1);
                    skeleton.IsConnectedShoes = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = shoesPosition;
                    item.localPosition = new Vector3(-6.1f, 4.9f, -5.9f);
                    item.localRotation = Quaternion.Euler(shoesRotation);
                    skeletonAttachedObjects.ConnectedShoes = item;
                }
                break;
            case 15:
                if (!skeleton.IsConnectedHelm)
                {
                    helmCounter.AddResource(-1);
                    skeleton.IsConnectedHelm = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = helmPosition;
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(helmRotation);
                    skeletonAttachedObjects.ConnectedHelm = item;
                }
                break;
            case 17:
                if (!skeleton.IsConnectedBracers)
                {
                    bracersCounter.AddResource(-1);
                    skeleton.IsConnectedBracers = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = bracersPosition;
                    item.localPosition = new Vector3(0, 7.6f, 0);
                    item.localRotation = Quaternion.Euler(bracersRotation);
                    skeletonAttachedObjects.ConnectedBracers = item;
                }
                break;
        }
        item.GetChild(0).gameObject.AddComponent<SkeletonItem>();
        item.GetChild(0).gameObject.AddComponent<MeshCollider>();

        item.GetChild(0).GetComponent<SkeletonItem>().ItemID = id;
        item.GetChild(0).GetComponent<SkeletonItem>().SkeletonScript = skeleton;

        StartCoroutine(MaterializeItem(item, materializationDuration));
    }

    IEnumerator MaterializeItem(Transform productTransform, float duration)
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
            currentMaterialization = Mathf.Lerp(0.7f, 0.05f, elapsed / duration);
            productMaterial.SetFloat("_Clip", currentMaterialization);
            productMeshrenderer.material = productMaterial;
            Debug.Log(productTransform.GetChild(0).GetComponent<MeshRenderer>().material.GetFloat("_Clip"));
            yield return null;
        }
        productMaterial.SetFloat("_Clip", 0f);
        productMeshrenderer.material = productMaterial;
        //Destroy(productTransform.gameObject);
        yield return null;
    }
}
