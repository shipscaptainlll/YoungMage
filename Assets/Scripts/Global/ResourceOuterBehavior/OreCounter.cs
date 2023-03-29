using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreCounter : MonoBehaviour
{
    [SerializeField] Text oreCounterText;
    [SerializeField] AnimationCurve animationCurve;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] bool isMidasCounter;
    [SerializeField] float settingFinalScale;
    [SerializeField] float settingStartScale;
    Coroutine popUpCoroutine;
    Transform oreCounter;
    int oreCount;
    bool counterOn;

    bool catchedByTornado;

    public bool CatchedByTornado { get { return catchedByTornado; } set { catchedByTornado = value; } }
    public bool CounterOn { get { return counterOn; } }
    public int OreCount { get { return oreCount; } set { oreCount = value; UpdateCounter(); } }
    void Start()
    {
        Debug.Log("it is midasCounter " + isMidasCounter);
        oreCounter = oreCounterText.transform;
        if (oreCount <= 1 && !isMidasCounter)
        {
            oreCount = 1;
        }
        if (settingStartScale == 0)
        {
            settingStartScale = 0.1f;
        }
        if (settingFinalScale == 0)
        {
            settingFinalScale = 0.15f;
        }

    }

    void UpdateCounter()
    {
        PopUpCounter();
        oreCounterText.text = oreCount.ToString();
    }

    void PopUpCounter()
    {
        if (oreCounter == null) { return; }
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
        float currentScale = settingStartScale;
        while (elapsed < maxDuration)
        {
            
            elapsed += Time.deltaTime;
            currentScale = Mathf.Lerp(settingStartScale, settingFinalScale, animationCurve.Evaluate(elapsed/maxDuration));
            //Debug.Log(currentScale);
            oreCounter.localScale = new Vector3(currentScale, currentScale, 1);
            //Debug.Log(oreCounter.localScale);
            yield return null;
        }
        currentScale = settingStartScale;
        oreCounter.localScale = new Vector3(currentScale, currentScale, 1);
        popUpCoroutine = null;
        yield return null;
    }
}
