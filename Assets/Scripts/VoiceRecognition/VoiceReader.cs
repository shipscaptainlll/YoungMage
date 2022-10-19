using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;

public class VoiceReader : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("Sol", Sol);
        actions.Add("Wor", Wor);
        actions.Add("Main", Main);
        actions.Add("Sol Wor Main", Main);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text + " with certainty level " + speech.confidence);
        actions[speech.text].Invoke();
    }

    void Sol()
    {
        Debug.Log(1);
    }

    void Wor()
    {
        Debug.Log(2);
    }

    void Main()
    {
        Debug.Log(3);
    }
}
