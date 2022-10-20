using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;

public class VoiceReader : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, Action> actions = new Dictionary<string, Action>();

    Coroutine showingCoroutine;
    void Start()
    {
        actions.Add("Show feather", Sol);
        actions.Add("Hide feather", Wor);
        actions.Add("Man", Main);
        actions.Add("Sol Wor Main", Main);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        PhraseRecognitionSystem.OnStatusChanged += Notify;
    }

    void Update()
    {


    }

    void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text + " with certainty level " + speech.confidence);
        actions[speech.text].Invoke();
    }

    void Notify(SpeechSystemStatus status)
    {
        Debug.Log(status);
    }

    void Sol()
    {
        if (showingCoroutine != null) { StopCoroutine(showingCoroutine); }
        showingCoroutine = StartCoroutine(ShowSomething());
    }

    IEnumerator ShowSomething()
    {
        float expired = 0;
        float time = 1;
        float currentValue = 0;
        while (expired < time)
        {
            expired += Time.deltaTime;
            currentValue = Mathf.Lerp(0, 1, expired / time);
            canvasGroup.alpha = currentValue;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
    IEnumerator HideSomething()
    {
        float expired = 0;
        float time = 1;
        float currentValue = 1;
        while (expired < time)
        {
            expired += Time.deltaTime;
            currentValue = Mathf.Lerp(1, 0, expired / time);
            canvasGroup.alpha = currentValue;
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    void Wor()
    {
        if (showingCoroutine != null) { StopCoroutine(showingCoroutine); }
        showingCoroutine = StartCoroutine(HideSomething());
    }

    void Main()
    {
        Debug.Log(3);
    }
}
