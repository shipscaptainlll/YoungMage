using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorCircleAnimation : MonoBehaviour
{
    [SerializeField] Image circleImage;

    bool isFinished;
    Coroutine circleAnimationCoroutine;

    public bool IsFinished { get { return isFinished; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCircleAnimation()
    {
        transform.GetComponent<CanvasGroup>().alpha = 1;
        if (circleAnimationCoroutine != null)
        {
            //StopCoroutine(circleAnimationCoroutine);
        } else
        {
            circleAnimationCoroutine = StartCoroutine(CircleAnimation());
        }
        
    }

    public void StopCircleAnimation()
    {
        transform.GetComponent<CanvasGroup>().alpha = 0;
        isFinished = false;
        if (circleAnimationCoroutine != null)
        {
            StopCoroutine(circleAnimationCoroutine);
        }
        circleAnimationCoroutine = null;
        circleImage.fillAmount = 0;
    }

    public void HideCircle()
    {
        circleImage.fillAmount = 0;
    }

    IEnumerator CircleAnimation()
    {
        float elapsed = 0;
        float targetTime = 3;
        while (elapsed < targetTime)
        {
            elapsed += Time.deltaTime;
            circleImage.fillAmount = Mathf.Lerp(0, 1, elapsed/targetTime);
            yield return null;
        }
        circleImage.fillAmount = 1;
        isFinished = true;
        StopCoroutine(circleAnimationCoroutine);
        yield return null;
    }
}
