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

    [Header("Other")]
    [SerializeField] float materializationDuration;
    [SerializeField] MaterialEquipShower materialEquipShower;

    public void AttachObject(SkeletonBehavior skeleton, int id)
    {
        SkeletonAttachedObjects skeletonAttachedObjects = skeleton.transform.GetComponent<SkeletonAttachedObjects>();
        SkeletonObjectPositions skeletonObjectPositions = skeleton.transform.GetComponent<SkeletonObjectPositions>();
        Transform item = null;
        switch (id)
        {
            case 11:
                if (!skeleton.IsConnectedHands)
                {
                    stoneHandsCounter.AddResource(-1);
                    skeleton.IsConnectedHands = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedHands = item;
                } else
                {
                    stoneHandsCounter.AddResource(-1);
                    Destroy(skeletonAttachedObjects.ConnectedHands.gameObject);
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedHands = item;
                }
                break;
            case 16:
                if (!skeleton.IsConnectedHands)
                {
                    glovesCounter.AddResource(-1);
                    skeleton.IsConnectedHands = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedGloves = item;
                }
                else
                {
                    stoneHandsCounter.AddResource(-1);
                    Destroy(skeletonAttachedObjects.ConnectedHands.gameObject);
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedGloves = item;
                }
                break;
            case 12:
                if (!skeleton.IsConnectedLeggings)
                {
                    leggingsCounter.AddResource(-1);
                    skeleton.IsConnectedLeggings = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedLeggings = item;
                }
                else
                {
                    Destroy(skeletonAttachedObjects.ConnectedLeggings.gameObject);
                    leggingsCounter.AddResource(-1);
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedLeggings = item;
                }
                break;
            case 13:
                if (!skeleton.IsConnectedArmor)
                {
                    plateArmorCounter.AddResource(-1);
                    skeleton.IsConnectedArmor = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, -5.5f, -2.2f);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedArmor = item;
                }
                else
                {
                    Destroy(skeletonAttachedObjects.ConnectedHands.gameObject);
                    plateArmorCounter.AddResource(-1);
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, -5.5f, -2.2f);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedArmor = item;
                }
                break;
            case 14:
                if (!skeleton.IsConnectedShoes)
                {
                    shoesCounter.AddResource(-1);
                    skeleton.IsConnectedShoes = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(-6.1f, 4.9f, -5.9f);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedShoes = item;
                }
                else
                {
                    Destroy(skeletonAttachedObjects.ConnectedShoes.gameObject);
                    shoesCounter.AddResource(-1);
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(-6.1f, 4.9f, -5.9f);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedShoes = item;
                }
                break;
            case 15:
                if (!skeleton.IsConnectedHelm)
                {
                    helmCounter.AddResource(-1);
                    skeleton.IsConnectedHelm = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedHelm = item;
                }
                else
                {
                    Destroy(skeletonAttachedObjects.ConnectedHelm.gameObject);
                    helmCounter.AddResource(-1);
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedHelm = item;
                }
                break;
            case 17:
                if (!skeleton.IsConnectedBracers)
                {
                    bracersCounter.AddResource(-1);
                    skeleton.IsConnectedBracers = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 7.6f, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedBracers = item;
                }
                else
                {
                    Destroy(skeletonAttachedObjects.ConnectedBracers.gameObject);
                    bracersCounter.AddResource(-1);
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 7.6f, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedBracers = item;
                }
                break;
        }

        if (item.GetChild(0).gameObject.GetComponent<SkeletonItem>() == null)
        {
            item.GetChild(0).gameObject.AddComponent<SkeletonItem>();
        }
        
        item.GetChild(0).gameObject.AddComponent<MeshCollider>();

        item.GetChild(0).GetComponent<SkeletonItem>().ItemID = id;
        item.GetChild(0).GetComponent<SkeletonItem>().SkeletonScript = skeleton;
        
        StartCoroutine(MaterializeItem(item, materializationDuration));
    }

    IEnumerator MaterializeItem(Transform productTransform, float duration)
    {
        productTransform.GetChild(0).GetComponent<SkeletonItem>().BeingEdited = true;
        float elapsed = 0;
        MeshRenderer productMeshrenderer = productTransform.GetChild(0).GetComponent<MeshRenderer>();
        Material productMaterial = productMeshrenderer.material;
        productMaterial.SetFloat("_Clip", 0.7f);
        productMeshrenderer.material = productMaterial;
        materialEquipShower.AddingItem = null;
        float currentMaterialization;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentMaterialization = Mathf.Lerp(0.7f, 0.05f, elapsed / duration);
            productMaterial.SetFloat("_Clip", currentMaterialization);
            productMeshrenderer.material = productMaterial;
            yield return null;
        }
        productMaterial.SetFloat("_Clip", 0f);
        productMeshrenderer.material = productMaterial;
        yield return null;
    }
}
