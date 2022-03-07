using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField] Transform targetObject;
    [SerializeField] float updateSpeed;
    CanvasGroup targetCanvasGroup;
    Coroutine fade;
    Coroutine appear;
    
    // Start is called before the first frame update
    void Start()
    {
        targetCanvasGroup = targetObject.GetComponent<CanvasGroup>();
        ResetEffects();
        
    }

    // Update is called once per frame
    public void ResetEffects()
    {
        StopAllCoroutines();
        fade = null;
        appear = null;
        targetCanvasGroup.alpha = 0;
    }

    public void StartEffects()
    {
        targetCanvasGroup.alpha = 1;
        fade = StartCoroutine(Smoothfade());
        
    }

    IEnumerator Smoothfade()
    {
        float elapsed = 0;
        float currentAlpha = targetCanvasGroup.alpha;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            targetCanvasGroup.alpha = Mathf.Lerp(currentAlpha, 0.1f, elapsed / updateSpeed);
            yield return null;
        }
        appear = StartCoroutine(SmoothAppear());
        fade = null;
        
    }
    IEnumerator SmoothAppear()
    {
        float elapsed = 0;
        float currentAlpha = targetCanvasGroup.alpha;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            targetCanvasGroup.alpha = Mathf.Lerp(currentAlpha, 1f, elapsed / updateSpeed);
            yield return null;
        }
        
        fade = StartCoroutine(Smoothfade());
        appear = null;
    }
}
