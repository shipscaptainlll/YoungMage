using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SUIOre : MonoBehaviour
{
    [SerializeField] CameraSUIInformer cameraSUIInformer;
    [SerializeField] Text objectName;
    [SerializeField] Image objectImage;
    [SerializeField] Image objectSecondImage;
    [SerializeField] Text objectChance;
    [SerializeField] Text objectSecondChance;
    [SerializeField] Text objectHardness;
    [SerializeField] Text objectRegeneration;
    [SerializeField] Text objectOccupation;
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
        if (cameraSUIInformer.ObservedType == "Ore" && !isActivated)
        {
            isActivated = true;
            transform.localScale = new Vector3(1, 1, 1);
            transform.GetComponent<CanvasGroup>().alpha = 1;
            objectName.text = newGameObject.transform.GetComponent<IOre>().Type;
            objectImage.sprite = newGameObject.transform.GetComponent<IOre>().FirstProductImage;
            if (newGameObject.transform.GetComponent<IOre>().SecondProductImage != null)
            {
                objectSecondImage.gameObject.SetActive(true);
                objectSecondImage.sprite = newGameObject.transform.GetComponent<IOre>().SecondProductImage;
            } else
            {
                objectSecondImage.sprite = null;
                objectSecondImage.gameObject.SetActive(false);
            }
            objectChance.text = newGameObject.transform.GetComponent<IOre>().FirstProductChances.ToString();
            if (newGameObject.transform.GetComponent<IOre>().SecondProductChances != 0)
            {
                objectSecondChance.gameObject.SetActive(true);
                objectSecondChance.text = newGameObject.transform.GetComponent<IOre>().SecondProductChances.ToString();
            } else
            {
                objectSecondChance.text = null;
                objectSecondChance.gameObject.SetActive(false);
            }
            objectHardness.text = newGameObject.transform.GetComponent<IOre>().Hardness.ToString();
            objectRegeneration.text = newGameObject.transform.GetComponent<IOre>().Regeneration.ToString();
            objectOccupation.text = newGameObject.transform.GetComponent<IOre>().ObjectOccupation;
            objectReference = newGameObject.transform.Find("Point").gameObject;
        }
    }

    void UpdateElementSize(float currentDistance)
    {
        if (cameraSUIInformer.ObservedType == "Ore")
        {
            //Debug.Log(currentDistance);
            float koefficient = 2 / currentDistance;
            //Debug.Log(koefficient);
            koefficient = Mathf.Clamp(koefficient, 0.3f, 1);
            //Debug.Log(koefficient);
            transform.localScale = new Vector3(koefficient * 1, koefficient * 1, koefficient * 1);
            UpdateElementPosition();
        }
        
    }

    void UpdateElementPosition()
    {
        Vector3 objectPosition = Camera.main.WorldToScreenPoint(objectReference.transform.position);
        borderedCanvas.transform.position = objectPosition;
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
