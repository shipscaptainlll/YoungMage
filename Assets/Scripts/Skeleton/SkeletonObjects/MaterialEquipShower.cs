using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEquipShower : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] Material transparentMaterial;
    [SerializeField] Material addingMaterial;
    [SerializeField] Material swappingMaterial;
    [SerializeField] QuickAccessHandController quickAccessHandController;
    [SerializeField] ObjectManager objectManager;
    Transform lastObservedItem;
    Material lastObservedMaterial;

    Transform addingItem;

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

    public Transform AddingItem { set {
            if (addingItem != null)
            {
                Destroy(addingItem.gameObject);
            }
            
            addingItem = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform contactedObject = cameraController.ObservedObject.transform;
        //Debug.Log(contactedObject);
        if (contactedObject != null && contactedObject.GetComponent<SkeletonItem>() != null)
        {

            if (lastObservedItem != null && lastObservedItem != contactedObject) { RestoreLastItem(); }
            SkeletonItem currentSkeletonItem = contactedObject.GetComponent<SkeletonItem>();

            if (CheckIfDeleting() && CheckMaterialDelete(currentSkeletonItem))
            {
                SaveLastObserved(currentSkeletonItem);
                ChangeMaterialDelete(currentSkeletonItem);

            }
            else if (CheckIfSwapping())
            {

            }

        }
        else
        {
            if (lastObservedItem != null)
            {
                RestoreLastItem();
            }
        }
        
        if (contactedObject != null 
            && contactedObject.parent != null 
            && contactedObject.parent.GetComponent<Skeleton>() != null
            && (quickAccessHandController.CurrentCustomID == 11
                    || quickAccessHandController.CurrentCustomID == 12
                    || quickAccessHandController.CurrentCustomID == 13
                    || quickAccessHandController.CurrentCustomID == 14
                    || quickAccessHandController.CurrentCustomID == 15
                    || quickAccessHandController.CurrentCustomID == 16
                    || quickAccessHandController.CurrentCustomID == 17))
        {
            //Debug.Log(contactedObject);
            //Debug.Log(CheckIfAdding());
            //Debug.Log(CheckIfEquiped(contactedObject));
            if (addingItem == null && CheckIfAdding())
            {
                Debug.Log(!CheckIfEquiped(contactedObject));
                if (!CheckIfEquiped(contactedObject))
                {
                    AttachObject(quickAccessHandController.CurrentCustomID, "adding");
                } else
                {
                    AttachObject(quickAccessHandController.CurrentCustomID, "swapping");
                }
                
            }

        } else
        {
            if (addingItem != null)
            {
                Destroy(addingItem.gameObject);
                addingItem = null;
            }
        }
    } 

    bool CheckIfDeleting()
    {
        return Input.GetKey(KeyCode.O);
    }

    bool CheckIfAdding()
    {
        return Input.GetKey(KeyCode.G);
    }

    bool CheckIfSwapping()
    {
        return Input.GetKey(KeyCode.J);
    }

    bool CheckMaterialDelete(SkeletonItem currentSkeletonItem)
    {
        return currentSkeletonItem.SkeletonMaterialState != SkeletonItem.MaterialState.deletion;
    }

    bool CheckMaterialAdd(SkeletonItem currentSkeletonItem)
    {
        return currentSkeletonItem.SkeletonMaterialState != SkeletonItem.MaterialState.adding;
    }

    void ChangeMaterialDelete(SkeletonItem currentSkeletonItem)
    {
        currentSkeletonItem.SkeletonMaterialState = SkeletonItem.MaterialState.deletion;
        Transform itemTransform = currentSkeletonItem.transform;
        Material currentMaterial = itemTransform.GetComponent<MeshRenderer>().material;
        Material newMaterial = transparentMaterial;
        newMaterial.color = Color.red;
        itemTransform.GetComponent<MeshRenderer>().material = newMaterial;
    }

    void SaveLastObserved(SkeletonItem currentSkeletonItem)
    {
        lastObservedItem = currentSkeletonItem.transform;
        lastObservedMaterial = lastObservedItem.GetComponent<MeshRenderer>().material;
    }

    void RestoreLastItem()
    {
        if (!lastObservedItem.GetComponent<SkeletonItem>().BeingEdited)
        {
            lastObservedItem.GetComponent<SkeletonItem>().SkeletonMaterialState = SkeletonItem.MaterialState.normal;
            lastObservedItem.GetComponent<MeshRenderer>().material = lastObservedMaterial;

        }
        
        lastObservedItem = null;
        lastObservedMaterial = null;

    }

    bool CheckIfEquiped(Transform connectedSkeleton)
    {
        SkeletonBehavior skeletonScript = connectedSkeleton.parent.GetComponent<SkeletonBehavior>();
        switch (quickAccessHandController.CurrentCustomID)
        {
            case 11:
                return skeletonScript.IsConnectedHands;
                break;
            case 16:
                return skeletonScript.IsConnectedHands;
                break;
            case 12:
                return skeletonScript.IsConnectedLeggings;
                break;
            case 13:
                return skeletonScript.IsConnectedArmor;
                break;
            case 14:
                return skeletonScript.IsConnectedShoes;
                break;
            case 15:
                return skeletonScript.IsConnectedHelm;
                break;
            case 17:
                return skeletonScript.IsConnectedBracers;
                break;
        }
        return false;
    }
    
    public void AttachObject(int id, string typeOfAction)
    {
        Transform item = null;
        switch (id)
        {
            case 11:
                item = Instantiate(objectManager.TakeObject(id).transform);
                item.parent = stoneHandsPosition;
                item.localPosition = new Vector3(0, 0, 0);
                item.localRotation = Quaternion.Euler(stoneHandsRotation);
                break;
            case 16:
                item = Instantiate(objectManager.TakeObject(id).transform);
                item.parent = glovesPosition;
                item.localPosition = new Vector3(0, 0, 0);
                item.localRotation = Quaternion.Euler(glovesRotation);
                break;
            case 12:
                item = Instantiate(objectManager.TakeObject(id).transform);
                item.parent = leggingsPosition;
                item.localPosition = new Vector3(0, 0, 0);
                item.localRotation = Quaternion.Euler(leggingsRotation);
                break;
            case 13:
                item = Instantiate(objectManager.TakeObject(id).transform);
                item.parent = plateArmorPosition;
                item.localPosition = new Vector3(0, -5.5f, -2.2f);
                item.localRotation = Quaternion.Euler(plateArmorRotation);
                break;
            case 14:
                item = Instantiate(objectManager.TakeObject(id).transform);
                item.parent = shoesPosition;
                item.localPosition = new Vector3(-6.1f, 4.9f, -5.9f);
                item.localRotation = Quaternion.Euler(shoesRotation);
                break;
            case 15:
                item = Instantiate(objectManager.TakeObject(id).transform);
                item.parent = helmPosition;
                item.localPosition = new Vector3(0, 0, 0);
                item.localRotation = Quaternion.Euler(helmRotation);
                break;
            case 17:
                item = Instantiate(objectManager.TakeObject(id).transform);
                item.parent = bracersPosition;
                item.localPosition = new Vector3(0, 7.6f, 0);
                item.localRotation = Quaternion.Euler(bracersRotation);
                break;
        }
        Debug.Log(typeOfAction);
        
        
        if (typeOfAction == "adding")
        {
            Debug.Log("Hello there 1");
            item.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = addingMaterial;
        } else if (typeOfAction == "swapping")
        {
            Debug.Log("Hello there 2");
            item.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = swappingMaterial;
        }
        
        addingItem = item;
    }
    
}
