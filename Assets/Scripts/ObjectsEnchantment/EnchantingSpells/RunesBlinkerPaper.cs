using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunesBlinkerPaper : MonoBehaviour
{
    [SerializeField] RunesDictionaryPaper runesDictionaryPaper;
    [SerializeField] VoiceReader voiceReader;
    [SerializeField] AnimationCurve animationCurve;
    Coroutine blinkingRuneCoroutine;

    public List<Transform> runes = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        voiceReader.runeFound += BlinkRune;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BlinkRune(string runeName)
    {
        Transform runeTransform = FindRune(runeName);
        if (blinkingRuneCoroutine != null) { StopCoroutine(blinkingRuneCoroutine); }
        CanvasGroup runeIntensityHolder = runeTransform.GetChild(0).GetComponent<CanvasGroup>();
        blinkingRuneCoroutine = StartCoroutine(BlinkingRune(runeIntensityHolder));
    }

    Transform FindRune(string runeName)
    {
        foreach (Transform rune in runes)
        {
            if (rune.name == runeName) { return rune; }
        }
        return null;
    }

    IEnumerator BlinkingRune(CanvasGroup runeIntensityHolder)
    {
        float elapsed = 0;
        float duration = 0.5f;
        float intensity;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            intensity = Mathf.Lerp(0.15f, 1f, animationCurve.Evaluate(elapsed / duration));
            runeIntensityHolder.alpha = intensity;
            //Debug.Log(intensity);
            yield return null;
        }
    }
}
