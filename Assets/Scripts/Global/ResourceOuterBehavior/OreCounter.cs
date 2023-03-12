using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreCounter : MonoBehaviour
{
    [SerializeField] Text oreCounterText;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] CanvasGroup canvasGroup;
    Coroutine popUpCoroutine;
    Transform oreCounter;
    int oreCount;
    bool counterOn;

    
    public bool CounterOn { get { return counterOn; } }
    public int OreCount { get { return oreCount; } set { oreCount = value; UpdateCounter(); } }
    void Start()
    {
        oreCounter = oreCounterText.transform;
        if (oreCount <= 1)
        {
            oreCount = 1;
        }
    }

    void UpdateCounter()
    {
        oreCounterText.text = oreCount.ToString();
    }

    void PopUpCounter()
    {
        if (popUpCoroutine != null) { StopCoroutine(popUpCoroutine); }
        popUpCoroutine = StartCoroutine(CounterPopingUp());
    }

    public void ShowCounter()
    {
        canvasGroup.alpha = 1;
        counterOn = true;
    }

    IEnumerator CounterPopingUp()
    {
        float elapsed = 0;
        float maxDuration = 0.5f;
        float currentScale = 0.1f;
        while (elapsed < maxDuration)
        {
            
            elapsed += Time.deltaTime;
            currentScale = Mathf.Lerp(0.1f, 0.15f, animationCurve.Evaluate(elapsed/maxDuration));
            Debug.Log(currentScale);
            oreCounter.localScale = new Vector3(currentScale, currentScale, 1);
            Debug.Log(oreCounter.localScale);
            yield return null;
        }
        currentScale = 0.1f;
        oreCounter.localScale = new Vector3(currentScale, currentScale, 1);
        popUpCoroutine = null;
        yield return null;
    }
}
