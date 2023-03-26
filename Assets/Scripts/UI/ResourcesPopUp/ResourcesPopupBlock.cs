using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesPopupBlock : MonoBehaviour
{
    int blockID;
    Image blockImage;
    string blockName;
    Text blockNameText;
    Text blockCountText;
    CanvasGroup blockCanvasgroup;
    Coroutine delay;
    Coroutine textExpand;
    int count;
    float timeEnumeration = 0.2f;
    float delayLength = 6f;
    float textExpandTime = 0.2f;
    float textDecreaseTime = 1f;

    public int BlockID { get { return blockID; } set { blockID = value; SetBlockData(); } }
    public int Count { get { return count; } set { count = value; UpdateBlockData(); } }
    public event Action<Transform> PreparedForAutodestruction = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateStart()
    {
        blockImage = transform.Find("Borders").Find("HorizontalLayout").Find("Image").GetComponent<Image>();
        blockNameText = transform.Find("Borders").Find("HorizontalLayout").Find("Name").GetComponent<Text>();
        blockCountText = transform.Find("Borders").Find("HorizontalLayout").Find("Count").GetComponent<Text>();
        blockCanvasgroup = transform.GetComponent<CanvasGroup>();
        blockCanvasgroup.alpha = 0;
        transform.localScale = new Vector3(0.01f, 0.01f, 1);
    }

    void SetBlockData()
    {
        LateStart();
        UpdateBlockImage();
        UpdateBlockName();
    }
    void UpdateBlockData()
    {
        UpdateBlockCount();
        UpdateVisibilityParameters();
    }

    void UpdateVisibilityParameters()
    {
        if (delay == null)
        {
            StartCoroutine(ShowBlock());
            delay = StartCoroutine(Delay());
        }
        else
        {
            StopCoroutine(delay);
            
            delay = StartCoroutine(Delay());
        }
    }


    void UpdateBlockImage()
    {
        blockImage.sprite = SpriteManager.GetSprite(blockID);
    }

    void UpdateBlockName()
    {
        blockName = ItemsNames.GetName(blockID);
        blockNameText.text = blockName;
    }

    void UpdateBlockCount()
    {
        blockCountText.text = " x " + count.ToString() ;
        if (textExpand != null)
        {
            StopCoroutine(textExpand);
        }
        ResetTextSize();
        textExpand = StartCoroutine(ExpandText());
    }

    void ResetTextSize()
    {
        blockCountText.transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator ExpandText()
    {
        float elapsed = 0;
        while (elapsed < textExpandTime)
        {
            elapsed += Time.deltaTime;
            float scaleCurrent = Mathf.Lerp(1, 1.2f, elapsed / textExpandTime);

            blockCountText.transform.localScale = new Vector3(scaleCurrent, scaleCurrent, 1);
            yield return null;
        }
        blockCountText.transform.localScale = new Vector3(2, 2, 1);
        StartCoroutine(DecreaseText());
    }

    IEnumerator DecreaseText()
    {
        float elapsed = 0;
        while (elapsed < textExpandTime)
        {
            elapsed += Time.deltaTime;
            float scaleCurrent = Mathf.Lerp(1.2f, 1, elapsed / textExpandTime);

            blockCountText.transform.localScale = new Vector3(scaleCurrent, scaleCurrent, 1);
            yield return null;
        }
        blockCountText.transform.localScale = new Vector3(1, 1, 1);
    }

    IEnumerator ShowBlock()
    {
        float elapsed = 0f;
        while (elapsed < timeEnumeration)
        {
            elapsed += Time.deltaTime;
            float currentScale = Mathf.Lerp(0f, 1f, elapsed / timeEnumeration);
            //Debug.Log(elapsed);
            transform.localScale = new Vector3(currentScale, currentScale, 1);
            blockCanvasgroup.alpha = Mathf.Lerp(0f, 1f, elapsed / timeEnumeration);
            yield return null;
        }
        //Debug.Log("ready");
        transform.localScale = new Vector3(1, 1, 1);
        blockCanvasgroup.alpha = 1;
        //Debug.Log("Hello");
        transform.Find("Borders").Find("HorizontalLayout").GetComponent<HorizontalLayoutGroup>().enabled = false;
        transform.Find("Borders").Find("HorizontalLayout").GetComponent<HorizontalLayoutGroup>().enabled = true;
    }

    IEnumerator HideBlock()
    {
        float elapsed = 0f;
        while (elapsed < timeEnumeration)
        {
            elapsed += Time.deltaTime;
            float currentScale = Mathf.Lerp(1, 0.01f, elapsed / timeEnumeration);
            transform.localScale = new Vector3(currentScale, currentScale, 1);
            blockCanvasgroup.alpha = Mathf.Lerp(1f, 0f, elapsed / timeEnumeration);
            yield return null;
        }
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        blockCanvasgroup.alpha = 0;
        Autodestruction();
    }

    void Autodestruction()
    {
        if (PreparedForAutodestruction != null) { PreparedForAutodestruction(transform); }
    }

    IEnumerator Delay()
    {

        yield return new WaitForSeconds(delayLength);
        StartCoroutine(HideBlock());
        
    }
}
