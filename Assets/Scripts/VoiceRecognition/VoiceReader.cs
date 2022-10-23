using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;
using System.Text;

public class VoiceReader : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] RunesKeywordsCreator runesKeywordsCreator;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, int> actions = new Dictionary<string, int>();

    Coroutine showingCoroutine;
    public Dictionary<string, int> Actions { get { return actions; } set { actions = value; } }
    public event Action<string> runeFound = delegate { };
    void Start()
    {
        //actions.Add("Show feather", Sol);
        //actions.Add("Hide feather", Wor);
        //actions.Add("Man", Main);
        //actions.Add("Sol Wor Main", Main);
        

        runesKeywordsCreator.InitializeKeywordsVoicereader();
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.Low);
        
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        
    }


    void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        if (runeFound != null) { runeFound(speech.text); }
        Debug.Log(speech.text + " with certainty level " + speech.confidence);
    }

    public void Notify()
    {
        Debug.Log("hello there");
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
