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

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource applyObjectSound;

    void Start()
    {
        applyObjectSound = soundManager.FindSound("ApplyElementSkeleton");
    }

    public void AttachObject(SkeletonBehavior skeleton, int id)
    {
        Debug.Log("here we are");
        AttachedItemsManager attachedItemsManager = skeleton.transform.GetComponent<AttachedItemsManager>();
        //SkeletonAttachedObjects skeletonAttachedObjects = skeleton.transform.GetComponent<SkeletonAttachedObjects>();
        //SkeletonObjectPositions skeletonObjectPositions = skeleton.transform.GetComponent<SkeletonObjectPositions>();
        //Transform item = null;
        switch (id)
        {
            case 11:
                if (!attachedItemsManager.StoneHandsEquiped)
                {
                    stoneHandsCounter.AddResource(-1);
                    attachedItemsManager.EquipStoneHands();
                    /*
                    
                    skeleton.IsConnectedHands = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedHands = item;
                    */
                } 
                break;
            case 16:
                if (!attachedItemsManager.GlovesEquiped)
                {
                    glovesCounter.AddResource(-1);
                    attachedItemsManager.EquipGloves();

                    /*
                    skeleton.IsConnectedHands = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedGloves = item;
                    */
                }
                break;
            case 12:
                if (!attachedItemsManager.LeggingsEquiped)
                {
                    leggingsCounter.AddResource(-1);
                    attachedItemsManager.EquipLeggings();

                    /*
                    skeleton.IsConnectedLeggings = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedLeggings = item;
                    */
                }
                break;
            case 13:
                if (!attachedItemsManager.ChainMailEquiped)
                {
                    plateArmorCounter.AddResource(-1);
                    attachedItemsManager.EquipChainMail();

                    /*
                    skeleton.IsConnectedArmor = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, -5.5f, -2.2f);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedArmor = item;
                    */
                }
                break;
            case 14:
                if (!attachedItemsManager.BootsEquiped)
                {
                    shoesCounter.AddResource(-1);
                    attachedItemsManager.EquipBoots();

                    /*
                    skeleton.IsConnectedShoes = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(-6.1f, 4.9f, -5.9f);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedShoes = item;
                    */
                }
                break;
            case 15:
                if (!attachedItemsManager.HelmEquiped)
                {
                    helmCounter.AddResource(-1);
                    attachedItemsManager.EquipHelm();

                    /*
                    skeleton.IsConnectedHelm = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 0, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedHelm = item;
                    */
                }
                break;
            case 17:
                if (!attachedItemsManager.VambraceEquiped)
                {
                    bracersCounter.AddResource(-1);
                    attachedItemsManager.EquipVambrace();

                    /*
                    skeleton.IsConnectedBracers = true;
                    item = Instantiate(objectManager.TakeObject(id).transform);
                    item.parent = skeletonObjectPositions.GetObjectPosition(id);
                    item.localPosition = new Vector3(0, 7.6f, 0);
                    item.localRotation = Quaternion.Euler(skeletonObjectPositions.GetObjectRotation(id));
                    skeletonAttachedObjects.ConnectedBracers = item;
                    */
                }
                break;
        }

        /*

        if (item.GetChild(0).gameObject.GetComponent<SkeletonItem>() == null)
        {
            item.GetChild(0).gameObject.AddComponent<SkeletonItem>();
        }
        
        item.GetChild(0).gameObject.AddComponent<MeshCollider>();

        item.GetChild(0).GetComponent<SkeletonItem>().ItemID = id;
        item.GetChild(0).GetComponent<SkeletonItem>().SkeletonScript = skeleton;
        
        StartCoroutine(MaterializeItem(item, materializationDuration));
        */
        applyObjectSound.Play();
    }

    
}
