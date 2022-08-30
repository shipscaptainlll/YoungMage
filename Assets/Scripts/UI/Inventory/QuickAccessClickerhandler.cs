using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickAccessClickerhandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Transform deleteButton;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] Transform element;
    List<RaycastResult> hitObjects = new List<RaycastResult>();
    Coroutine changeVisibilityCoroutine;
    Coroutine changeSizeCoroutine;

    void Start()
    {
        MinimizeDeleteButton();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ObjectUnderMouse(0) != null && Input.GetKey(KeyCode.LeftControl))
        {
            DeleteCustomID(eventData);
        } else if (ObjectUnderMouse(0) != null && ObjectUnderMouse(2) == deleteButton.gameObject)
        {
            DeleteCustomID(eventData);
        }
    }

    void MinimizeDeleteButton()
    {
        deleteButton.GetComponent<CanvasGroup>().alpha = 0;
        deleteButton.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
    }

    GameObject ObjectUnderMouse(int number)
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects[number].gameObject;
    }
     
    void DeleteCustomID(PointerEventData eventData)
    {
        element.GetComponent<Element>().CustomID = 0;
        MinimizeDeleteButton();
    }

    public void DeleteElementButton()
    {
        transform.parent.Find("Element").GetComponent<Element>().CustomID = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CheckIfFilled())
        {
            if (changeVisibilityCoroutine != null) { StopCoroutine(changeVisibilityCoroutine); }
            if (changeSizeCoroutine != null) { StopCoroutine(changeSizeCoroutine); }
            changeVisibilityCoroutine = StartCoroutine(ChangeVisibility(deleteButton, 0.35f, 1));
            changeSizeCoroutine = StartCoroutine(ChangeSize(deleteButton, 0.35f, 10));
        }
    }

    bool CheckIfFilled()
    {
        return (element.GetComponent<Element>().CustomID != 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (CheckIfFilled())
        {
            if (changeVisibilityCoroutine != null) { StopCoroutine(changeVisibilityCoroutine); }
            if (changeSizeCoroutine != null) { StopCoroutine(changeSizeCoroutine); }
            changeVisibilityCoroutine = StartCoroutine(ChangeVisibility(deleteButton, 0.35f, 0));
            changeSizeCoroutine = StartCoroutine(ChangeSize(deleteButton, 0.35f, 1));
        }
    }

    IEnumerator ChangeVisibility(Transform targetUIElement, float duration, float stopValue)
    {
        float elapsed = 0;
        float currentValue = 0;
        
        CanvasGroup targetCanvasGroup = targetUIElement.GetComponent<CanvasGroup>();
        float startValue = targetCanvasGroup.alpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentValue = Mathf.Lerp(startValue, stopValue, elapsed/duration);
            targetCanvasGroup.alpha = currentValue;
            yield return null;
        }

        targetCanvasGroup.alpha = stopValue;
        yield return null;
    }

    IEnumerator ChangeSize(Transform targetUIElement, float duration, float stopValue)
    {
        float elapsed = 0;
        float currentValue = 0;

        RectTransform elementRect = targetUIElement.GetComponent<RectTransform>();
        Vector2 elementSize = targetUIElement.GetComponent<RectTransform>().sizeDelta;
        float startValue = elementSize.x;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentValue = Mathf.Lerp(startValue, stopValue, animationCurve.Evaluate(elapsed / duration));
            elementRect.sizeDelta = new Vector2(currentValue, currentValue);
            yield return null;
        }

        elementRect.sizeDelta = new Vector2(currentValue, currentValue);
        changeSizeCoroutine = null;
        yield return null;
    }
}
