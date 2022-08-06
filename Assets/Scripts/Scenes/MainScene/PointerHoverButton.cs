using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Material materialHDR;
    [SerializeField] Image outlineImage;
    [SerializeField] Image backgroundImage;
    [SerializeField] Text elementText;
    [SerializeField] float elementSizeScale;
    Coroutine showOutlineCoroutine;
    Coroutine hideOutlineCoroutine;
    Material outlineHDR;
    RectTransform outlineRecttransform;
    Vector3 elementSize;
    Vector2 currentSize;
    float currentOutlineValue;
    float currentOutlineScale;
    Color outlineColor;

    float currentOutlineColor;
    float elementTextWhite;
    float elementTextBlack;

    float currentBackgroundColor;

    // Start is called before the first frame update
    void Start()
    {
        outlineHDR = new Material(outlineImage.material);
        outlineImage.material = outlineHDR;
        outlineColor = outlineHDR.color;
        elementSize = outlineImage.rectTransform.localScale;
        outlineRecttransform = outlineImage.rectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hideOutlineCoroutine != null) { StopCoroutine(hideOutlineCoroutine); }
        showOutlineCoroutine = StartCoroutine(ShowOutline());
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        if (showOutlineCoroutine != null) { StopCoroutine(showOutlineCoroutine); }
        hideOutlineCoroutine = StartCoroutine(HideOutline());
    }

    public void TurnOffMaterial()
    {
        StopAllCoroutines();
        transform.gameObject.SetActive(false);
        Debug.Log("turned off");
    }

    IEnumerator ShowOutline()
    {
        float elapsed = 0;
        float target = 0.24f;
        elementText.material = outlineHDR;
        while (elapsed < target)
        {
            elapsed += Time.deltaTime;
            currentOutlineValue = Mathf.Lerp(0,1,elapsed/target);
            outlineColor.a = currentOutlineValue;
            outlineHDR.SetColor("_Color", outlineColor);

            currentOutlineScale = Mathf.Lerp(1, elementSizeScale, elapsed / target);
            outlineRecttransform.localScale = elementSize * currentOutlineScale;

            currentOutlineColor = Mathf.Lerp(0, 1, elapsed / target);
            elementText.color = new Color(currentOutlineColor, currentOutlineColor, currentOutlineColor);

            currentBackgroundColor = Mathf.Lerp(0.75f, 1, elapsed / target);
            backgroundImage.color = new Color(currentBackgroundColor, currentBackgroundColor, currentBackgroundColor);
            yield return null;
        }
        outlineColor.a = 1;
        outlineHDR.SetColor("_Color", outlineColor);
        outlineRecttransform.localScale = elementSize * elementSizeScale;
        elementText.color = new Color(1, 1, 1);
        backgroundImage.color = new Color(1, 1, 1);
        showOutlineCoroutine = null;
    }
    IEnumerator HideOutline()
    {
        float elapsed = 0;
        float target = 0.24f;
        elementText.material = null;
        while (elapsed < target)
        {
            elapsed += Time.deltaTime;
            currentOutlineValue = Mathf.Lerp(1, 0, elapsed / target);
            outlineColor.a = currentOutlineValue;
            outlineHDR.SetColor("_Color", outlineColor);

            currentOutlineScale = Mathf.Lerp(elementSizeScale, 1, elapsed / target);
            outlineRecttransform.localScale = elementSize * currentOutlineScale;

            currentOutlineColor = Mathf.Lerp(1, 0.659f, elapsed / target);
            elementText.color = new Color(1, currentOutlineColor, 0);

            currentBackgroundColor = Mathf.Lerp(1, 0.75f, elapsed / target);
            backgroundImage.color = new Color(currentBackgroundColor, currentBackgroundColor, currentBackgroundColor);
            yield return null;
        }
        outlineColor.a = 0;
        outlineHDR.SetColor("_Color", outlineColor);
        outlineRecttransform.localScale = elementSize * 1.0f;
        elementText.color = new Color(1, 0.6559231f, 0);
        backgroundImage.color = new Color(0.75f, 0.75f, 0.75f);
        hideOutlineCoroutine = null;
    }

}
