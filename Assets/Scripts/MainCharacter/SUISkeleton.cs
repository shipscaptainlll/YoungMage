using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SUISkeleton : MonoBehaviour
{
    [SerializeField] CameraSUIInformer cameraSUIInformer;
    [SerializeField] Text objectName;
    [SerializeField] Image objectImage;
    [SerializeField] Text objectPower;
    [SerializeField] Text objectInventoryPower;
    [SerializeField] Text objectSpeed;
    [SerializeField] Text objectInventorySpeed;
    [SerializeField] Text objectAppliedInventory;
    [SerializeField] Text objectOccupation;
    [SerializeField] Text appliedObjects;
    [SerializeField] Transform borderedCanvas;

    GameObject objectReference;
    bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        cameraSUIInformer.SeeingNewObject += ShowNewObject;
        cameraSUIInformer.DistanceChanged += UpdateElementSize;
        cameraSUIInformer.StoppedSeeingAnything += HideElement;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowNewObject(GameObject newGameObject)
    {
        if (cameraSUIInformer.ObservedType == "Skeleton" && !isActivated)
        {
            Skeleton skeletonBasicInfo = newGameObject.transform.parent.GetComponent<Skeleton>();
            isActivated = true;
            transform.localScale = new Vector3(1, 1, 1);
            transform.GetComponent<CanvasGroup>().alpha = 1;
            objectName.text = skeletonBasicInfo.ObjectType;
            objectImage.sprite = skeletonBasicInfo.SkeletonImage;
            objectPower.text = skeletonBasicInfo.Power.ToString();
            objectInventoryPower.text = "(+" + skeletonBasicInfo.InventoryPower.ToString() + ")";
            objectSpeed.text = skeletonBasicInfo.Speed.ToString();
            objectInventorySpeed.text = "(+" + skeletonBasicInfo.InventorySpeed.ToString() + ")";
            objectReference = newGameObject.transform.parent.Find("Point").gameObject;

            CountSkeletonItems(newGameObject.transform.parent.GetComponent<SkeletonBehavior>());
            string appliedInventoryItems = "";
            if (newGameObject.transform.parent.GetComponent<Skeleton>().AppliedInventory != null)
            {
                foreach (var item in newGameObject.transform.parent.GetComponent<Skeleton>().AppliedInventory)
                {
                    appliedInventoryItems += (item.name + "\n").ToString();
                }
            }
            //objectAppliedInventory.text = appliedInventoryItems;
            objectOccupation.text = newGameObject.transform.parent.GetComponent<Skeleton>().Occupation.ToString();
        }
    }

    void CountSkeletonItems(SkeletonBehavior skeletonInfo)
    {
        string itemsString = "";
        if (skeletonInfo.IsConnectedHands)
        {
            itemsString += "\nhands";
        }
        if (skeletonInfo.IsConnectedLeggings)
        {
            itemsString += "\nleggings";
        }
        if (skeletonInfo.IsConnectedArmor)
        {
            itemsString += "\narmor";
        }
        if (skeletonInfo.IsConnectedShoes)
        {
            itemsString += "\nshoes";
        }
        if (skeletonInfo.IsConnectedHelm)
        {
            itemsString += "\nhelm";
        }
        if (skeletonInfo.IsConnectedGloves)
        {
            itemsString += "\ngloves";
        }
        if (skeletonInfo.IsConnectedBracers)
        {
            itemsString += "\nbracers";
        }
        //Debug.Log(appliedObjects.text);
        appliedObjects.text = itemsString;
        //Debug.Log(appliedObjects.text);
    }

    void UpdateElementSize(float currentDistance)
    {
        if (cameraSUIInformer.ObservedType == "Skeleton")
        {
            //Debug.Log(currentDistance);
            float koefficient = 2 / currentDistance;
            //Debug.Log(koefficient);
            koefficient = Mathf.Clamp(koefficient, 0.3f, 1);
            //Debug.Log(koefficient);
            transform.localScale = new Vector3(koefficient * 1, koefficient * 1, koefficient * 1);
        }
        UpdateElementPosition();
    }

    void UpdateElementPosition()
    {
        if (cameraSUIInformer.ObservedType == "Skeleton")
        {
            Vector3 objectPosition = Camera.main.WorldToScreenPoint(objectReference.transform.position);
            borderedCanvas.transform.position = objectPosition;
        }
        
    }

    void HideElement()
    {
        if (isActivated)
        {
            transform.GetComponent<CanvasGroup>().alpha = 0;
            isActivated = false;
        }
    }
}
