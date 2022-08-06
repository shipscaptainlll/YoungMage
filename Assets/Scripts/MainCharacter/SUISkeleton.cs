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
            isActivated = true;
            transform.localScale = new Vector3(1, 1, 1);
            transform.GetComponent<CanvasGroup>().alpha = 1;
            objectName.text = newGameObject.transform.parent.GetComponent<Skeleton>().ObjectType;
            objectImage.sprite = newGameObject.transform.parent.GetComponent<Skeleton>().SkeletonImage;
            objectPower.text = newGameObject.transform.parent.GetComponent<Skeleton>().Power.ToString();
            objectInventoryPower.text = "(+" + newGameObject.transform.parent.GetComponent<Skeleton>().InventoryPower.ToString() + ")";
            objectSpeed.text = newGameObject.transform.parent.GetComponent<Skeleton>().Speed.ToString();
            objectInventorySpeed.text = "(+" + newGameObject.transform.parent.GetComponent<Skeleton>().InventorySpeed.ToString() + ")";
            string appliedInventoryItems = "";
            if (newGameObject.transform.parent.GetComponent<Skeleton>().AppliedInventory != null)
            {
                foreach (var item in newGameObject.transform.parent.GetComponent<Skeleton>().AppliedInventory)
                {
                    appliedInventoryItems += (item.name + "\n").ToString();
                }
            }
            objectAppliedInventory.text = appliedInventoryItems;
            objectOccupation.text = newGameObject.transform.parent.GetComponent<Skeleton>().Occupation.ToString();
        }
    }

    void UpdateElementSize(float currentDistance)
    {
        if (cameraSUIInformer.ObservedType == "Skeleton")
        {
            Debug.Log(currentDistance);
            float koefficient = 2 / currentDistance;
            Debug.Log(koefficient);
            koefficient = Mathf.Clamp(koefficient, 0.3f, 1);
            Debug.Log(koefficient);
            transform.localScale = new Vector3(koefficient * 1, koefficient * 1, koefficient * 1);
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
