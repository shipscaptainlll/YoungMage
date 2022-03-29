using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountUI : MonoBehaviour
{
    GlobalResource localOreMainscript;
    Text elementText;
    Transform elementTextHolder;
    RectTransform elementRecttransform;
    Coroutine currentCoroutine;
    float elapsedTarget = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        elementText = transform.Find("Text").GetComponent<Text>();
        elementTextHolder = transform.Find("Text");
        localOreMainscript = transform.parent.GetComponent<GlobalResource>();
        elementRecttransform = transform.Find("Text").GetComponent<RectTransform>();
        localOreMainscript.CountChanged += UpdateText;
        UpdateText();
        //Debug.Log("Initialised");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    public void UpdateText()
    {
        elementText.text = localOreMainscript.Count.ToString();
        if (currentCoroutine != null)
        {
            StopAllCoroutines();
            currentCoroutine = StartCoroutine(PopupText());
        } else { currentCoroutine = StartCoroutine(PopupText()); }

        Debug.Log("Count updated");
    }

    IEnumerator PopupText()
    {
        float elapsed = 0;
        float targetScale = 250;
        float normalScale = 200;
        float currentScale;
        while (elapsed < elapsedTarget)
        {
            elapsed += Time.deltaTime;
            currentScale = Mathf.Lerp(normalScale, targetScale, elapsed / elapsedTarget);
            elementRecttransform.sizeDelta = new Vector2(200 * currentScale, elementRecttransform.sizeDelta.y);
            yield return null;
        }
        elementTextHolder.localScale = new Vector3(targetScale, targetScale, 1);
        StartFading();
    }

    void StartFading()
    {
        currentCoroutine = StartCoroutine(PopbackText());
    }

    IEnumerator PopbackText()
    {
        float elapsed = 0;
        float targetScale = 200;
        float normalScale = 250;
        float currentScale;
        while (elapsed < elapsedTarget)
        {
            elapsed += Time.deltaTime;
            currentScale = Mathf.Lerp(normalScale, targetScale, 1);
            elementTextHolder.localScale = new Vector3(currentScale, currentScale, 1);
            yield return null;
        }
        elementTextHolder.localScale = new Vector3(targetScale, targetScale, 1);
    }
}
