using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhishlistBegger : MonoBehaviour
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] IngameTimer ingameTimer;
    [SerializeField] TurnOffVisualiser turnOffVisualiser;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] Transform whishlistTransform;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;

    Coroutine delayedShowCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        ingameTimer.playedWishlistTime += ShowWhishList;
        clickManager.FNineClicked += OpenWebWhishlist;
    }

    void ShowWhishList()
    {
        if (delayedShowCoroutine != null) { 
            StopCoroutine(delayedShowCoroutine); 
            turnOffVisualiser.JustHide();
            whishlistTransform.GetComponent<CanvasGroup>().alpha = 0;
            whishlistTransform.position = startPosition.position;
        }
        delayedShowCoroutine = StartCoroutine(ShowRepositionIE());
    }

    void HideWhishlist()
    {
        StopCoroutine(delayedShowCoroutine);
        delayedShowCoroutine = StartCoroutine(HideRepositionIE());
    }

    void OpenWebWhishlist()
    {
        Application.OpenURL("https://store.steampowered.com/app/2334920/Young_mage/");
    }

    IEnumerator ShowRepositionIE()
    {
        float elapsed = 0;
        float max = 4;
        float xPosition;
        float xStart = startPosition.position.x;
        float xTarget = endPosition.position.x;
        whishlistTransform.GetComponent<CanvasGroup>().alpha = 1;
        whishlistTransform.position = startPosition.position;
        while (elapsed < max)
        {
            elapsed += Time.deltaTime;
            xPosition = Mathf.Lerp(xStart, xTarget, animationCurve.Evaluate(elapsed / max));
            whishlistTransform.position = new Vector3(xPosition, whishlistTransform.position.y, whishlistTransform.position.z);
            yield return null;
        }
        whishlistTransform.position = endPosition.position;
        turnOffVisualiser.JustShow(30);
        yield return new WaitForSeconds(25);
        HideWhishlist();
        yield return null;
    }

    IEnumerator HideRepositionIE()
    {
        turnOffVisualiser.JustHide();
        whishlistTransform.GetComponent<CanvasGroup>().alpha = 1;
        float elapsed = 0;
        float max = 3;
        float xPosition;
        float xStart = endPosition.position.x;
        float xTarget = startPosition.position.x;
        while (elapsed < max)
        {
            elapsed += Time.deltaTime;
            xPosition = Mathf.Lerp(xStart, xTarget, elapsed / max);
            whishlistTransform.position = new Vector3(xPosition, whishlistTransform.position.y, whishlistTransform.position.z);
            yield return null;
        }
        whishlistTransform.position = startPosition.position;
        
        yield return null;
    }
}
