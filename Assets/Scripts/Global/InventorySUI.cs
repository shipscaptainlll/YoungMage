using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySUI : MonoBehaviour
{
    [SerializeField] Vector3 offset;

    string description;
    bool isActive;
    public void Show(Transform element)
    {
        if (element.Find("Borders").Find("Element").GetComponent<Element>().CustomID != 0)
        {
            isActive = true;
            transform.GetComponent<CanvasGroup>().alpha = 1;
            int currentCustomID = element.Find("Borders").Find("Element").GetComponent<Element>().CustomID;
            description = ((ItemsList.Items)currentCustomID).ToString();
            transform.GetChild(0).GetChild(0).GetComponent<Text>().text = description;
        }
    }

    public void Unshow(Transform element)
    {

        isActive = false;
        transform.GetComponent<CanvasGroup>().alpha = 0;
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "";
        transform.position = new Vector3(1380, 0, 0);
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
            transform.position = Input.mousePosition + offset;
        }
    }
}
