using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SUIElement : MonoBehaviour
{
    [SerializeField] CameraSUIInformer cameraSUIInformer;
    [SerializeField] Text objectName;

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
        if (cameraSUIInformer.ObservedType == "null")
        {
            isActivated = true;
            transform.localScale = new Vector3(1, 1, 1);
            transform.GetComponent<CanvasGroup>().alpha = 1;
            objectName.text = newGameObject.name;
        }
    }

    void UpdateElementSize(float currentDistance)
    {
        if (cameraSUIInformer.ObservedType == "null")
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
