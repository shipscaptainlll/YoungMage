using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverInventoryManager : MonoBehaviour
{
    [SerializeField] Transform inventoryElementsHolder;
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
        foreach (Transform row in inventoryElementsHolder)
        {
            foreach (Transform slot in row)
            {
                slot.GetComponent<OnHoverHandler2D>().InventoryElementFound += Process;
                slot.GetComponent<OnHoverHandler2D>().InventoryElementExited += Unprocess;
            }
        }
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
        Debug.Log("Started process");
        
        if (foundObject == null)
        {
            foundObject = element;

            descriptionText.text = element.Find("Borders").Find("Element").GetComponent<Image>().sprite.name;
            descriptionCanvas.parent = UIMainHolder;
            descriptionSizes = new Vector2(descriptionTransform.GetComponent<RectTransform>().rect.width, descriptionTransform.GetComponent<RectTransform>().rect.height);
            descriptionCanvas.SetAsLastSibling();
            waitOnImage = StartCoroutine(CountOnImage());
            
        }
    }

    void Unprocess()
    {
        if (foundObject != null)
        {
            Debug.Log("Started process2");
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
        Debug.Log("Started process1");
        float elapsed = 0;
        float target = 0.3f;
        waitedEnough = false;
        while (foundObject != null)
        {
            if (Input.GetAxis("Mouse X") < 0.2f && Input.GetAxis("Mouse Y") < 0.2f)
            {
                elapsed += Time.deltaTime;
                if (elapsed >= target)
                {
                    waitedEnough = true;
                    descriptionCanvas.GetComponent<CanvasGroup>().alpha = 1;
                    descriptionTransform.position = new Vector2(Input.mousePosition.x + descriptionSizes.x * 1.5f, Input.mousePosition.y + descriptionSizes.y * 3);
                }
            } else
            {
                elapsed = 0;
                descriptionTransform.position = new Vector2(0, 0);
                descriptionCanvas.GetComponent<CanvasGroup>().alpha =   0;
                Debug.Log("restarted countdow");
                waitedEnough = false;
            }
            yield return null;
        }
        

    }
}
