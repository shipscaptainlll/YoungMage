using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverInventoryManager : MonoBehaviour
{
    [SerializeField] Transform inventoryElementsHolder;
    [SerializeField] Transform quickinventoryElementsHolder;
    [SerializeField] Transform UIMainHolder;
    [SerializeField] Transform SUIMainHolder;
    [SerializeField] Transform descriptionCanvas;
    [SerializeField] Transform descriptionTransform;
    [SerializeField] Text descriptionText;

    bool waitedEnough;
    Coroutine waitOnImage;                          
    Vector2 descriptionSizes;
    Transform foundObject;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform);
        foreach (Transform row in inventoryElementsHolder)
        {
            foreach (Transform slot in row)
            {
                slot.GetComponent<OnHoverHandler2D>().InventoryElementFound += Process;
                slot.GetComponent<OnHoverHandler2D>().InventoryElementExited += Unprocess;
            }
        }

        foreach (Transform row in quickinventoryElementsHolder)
        {
            foreach (Transform slot in row)
            {
                slot.GetComponent<OnHoverHandler2D>().InventoryElementFound += Process;
                slot.GetComponent<OnHoverHandler2D>().InventoryElementExited += Unprocess;
            }
        }
        //var text = descriptionText.text;
        //descriptionText.text = ManageParagraphs(text, 5);
        //ResizeContent();
    }

    // Update is called once per frame
    void Update()
    {
        if (foundObject != null && waitedEnough)
        {
            
        }
    }

    void Process(Transform element)
    {
        //Debug.Log("Started process");
        
        if (foundObject == null)
        {
            foundObject = element;
            //Debug.Log(element);

            descriptionText.text = element.Find("Borders").Find("Element").GetComponent<Image>().sprite.name;
            descriptionText.text = ManageParagraphs(descriptionText.text, 12);
            descriptionCanvas.GetComponent<CanvasGroup>().alpha = 1;
            descriptionCanvas.GetComponent<CanvasGroup>().alpha = 0;
            //Debug.Log(descriptionText);
            ResizeContent();
            descriptionCanvas.parent = UIMainHolder;
            
            //descriptionSizes = new Vector2(descriptionTransform.GetComponent<RectTransform>().rect.width, descriptionTransform.GetComponent<RectTransform>().rect.height);
            descriptionCanvas.SetAsLastSibling();

            waitOnImage = StartCoroutine(CountOnImage());
            
        }
    }

    void Unprocess()
    {
        if (foundObject != null)
        {
            //Debug.Log("Started process2");
            descriptionCanvas.parent = SUIMainHolder;
            descriptionCanvas.GetComponent<CanvasGroup>().alpha = 0;
            descriptionTransform.position = new Vector2(0, 0);
            descriptionCanvas.SetAsFirstSibling();
            StopCoroutine(waitOnImage);
            waitedEnough = false;
            foundObject = null;

        }
        
    }

    IEnumerator CountOnImage()
    {
        //Debug.Log("Started process1");
        float elapsed = 0;
        float target = 0.3f;
        waitedEnough = false;
        //descriptionCanvas.GetComponent<CanvasGroup>().alpha = 1;
        //descriptionTransform.position = new Vector2(Input.mousePosition.x + descriptionCanvas.GetComponent<RectTransform>().sizeDelta.x / 2, Input.mousePosition.y + descriptionCanvas.GetComponent<RectTransform>().sizeDelta.y);
        ResizeContent();
        while (foundObject != null)
        {
            if (Input.GetAxis("Mouse X") < 0.4f && Input.GetAxis("Mouse Y") < 0.4f)
            {
                elapsed += Time.deltaTime;
                if (elapsed >= target)
                {
                    waitedEnough = true;
                    
                    descriptionCanvas.GetComponent<CanvasGroup>().alpha = 1;
                    descriptionTransform.position = new Vector2(Input.mousePosition.x + descriptionCanvas.GetComponent<RectTransform>().sizeDelta.x / 2, Input.mousePosition.y + descriptionCanvas.GetComponent<RectTransform>().sizeDelta.y);
                }
            } else
            {
                elapsed = 0;
                descriptionTransform.position = new Vector2(0, 0);
                descriptionCanvas.GetComponent<CanvasGroup>().alpha =   0;
                //Debug.Log("restarted countdow");
                waitedEnough = false;
            }
            yield return null;
        }
    }

    string ManageParagraphs(string originalText, int sizeLimit)
    {
        if (originalText.Length > sizeLimit)
        {
            //Debug.Log("Length of text " + originalText.Length);
            string newString = "";
            float floatDivision = (float)originalText.Length / (float)sizeLimit;
            //Debug.Log("float division " + floatDivision);
            int divisionRemainder = originalText.Length % sizeLimit;
            //Debug.Log("Division remainder" + divisionRemainder);
            int substrCount = (int) Mathf.Ceil(floatDivision);

            //Debug.Log("Count of substrings " + substrCount);
            for (int i = 0; i <= substrCount - 1; i++)
            {
                //Debug.Log("entering loop");
                if (i == substrCount - 1 && divisionRemainder != 0)
                {
                    //Debug.Log("entering last loop");
                    newString += originalText.Substring(sizeLimit * i, divisionRemainder);
                    //Debug.Log("New line1 " + newString);
                } else
                {
                    //Debug.Log(originalText.Substring(sizeLimit * i, sizeLimit) != null);
                    newString += originalText.Substring(sizeLimit * i, sizeLimit);
                    if (i != substrCount - 1);
                    newString += "\n";
                    //Debug.Log("New line " + newString);
                }
                
            }
            return newString;
        }
        return originalText;

    }

    void ResizeContent()
    {
        Canvas.ForceUpdateCanvases();
        Vector2 textSize = new Vector2(descriptionText.transform.GetComponent<RectTransform>().sizeDelta.x + 15, descriptionText.transform.GetComponent<RectTransform>().sizeDelta.y + 15);
        descriptionCanvas.GetComponent<RectTransform>().sizeDelta = textSize;
        descriptionCanvas.GetChild(0).GetComponent<RectTransform>().sizeDelta = textSize;
        descriptionCanvas.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = textSize;
        descriptionCanvas.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = textSize;
        descriptionCanvas.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = textSize;
        Canvas.ForceUpdateCanvases();
    }
}
