using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SUIObject : MonoBehaviour
{
    [SerializeField] CameraSUIInformer cameraSUIInformer;
    [SerializeField] Text objectName;
    [SerializeField] Image objectImage;
    [SerializeField] Text objectCount;
    [SerializeField] Text objectType;

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
        if (cameraSUIInformer.ObservedType == "Object" && !isActivated)
        {
            isActivated = true;
            transform.localScale = new Vector3(1, 1, 1);
            transform.GetComponent<CanvasGroup>().alpha = 1;
            objectName.text = newGameObject.transform.GetComponent<IObjectSUI>().ObjectName;
            objectImage.sprite = newGameObject.transform.GetComponent<IObjectSUI>().ObjectImage;
            objectCount.text = newGameObject.transform.GetComponent<IObjectSUI>().Count.ToString();
            objectType.text = newGameObject.transform.GetComponent<IObjectSUI>().ObjectType;
        }
    }

    void UpdateElementSize(float currentDistance)
    {
        if (cameraSUIInformer.ObservedType == "Object")
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
