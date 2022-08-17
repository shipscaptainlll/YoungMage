using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Regeneration2DSUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform SUIElement;
    [SerializeField] string description;
    [SerializeField] Vector3 offset;

    bool isActive;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isActive = true;
        SUIElement.GetComponent<CanvasGroup>().alpha = 1;
        SUIElement.GetChild(0).GetChild(0).GetComponent<Text>().text = description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        isActive = false;
        SUIElement.GetComponent<CanvasGroup>().alpha = 0;
        SUIElement.GetChild(0).GetChild(0).GetComponent<Text>().text = "";
        SUIElement.position = new Vector3(1380, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isActive + " " + name);
        if (isActive)
        {
            SUIElement.position = Input.mousePosition + offset;
        }
    }

    
}
