using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestNotificatorElement : MonoBehaviour
{
    [SerializeField] QuestsDatabase questsDatabase;
    [SerializeField] NewQuestsNotificator newQuestsNotificator;
    [SerializeField] Text questDescriptionHolder;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource questAppearanceSound;
    AudioSource questDisappearanceSound;

    Coroutine hidingElemets = null;
    Coroutine calibratingElement = null;
    bool coroutineIsRunning = false;
    bool calibrationCoroutineIsRunning = false;
    float positionChangeElapsed = 0;
    string questDescription = null;

    public float PositionChangeElapsed { get { return positionChangeElapsed; } }
    public string QuestDescription { set { 
            questDescription = value;
            questDescriptionHolder.text = questDescription;
        } }
    // Start is called before the first frame update
    void Start()
    {
        newQuestsNotificator.startedHidingElement += StartRelocations;
        StartCoroutine(StartTimer(transform));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTimer(Transform element)
    {
        
        yield return new WaitForSeconds(5f);
        
        newQuestsNotificator.StartHidingElement();
        
        StartCoroutine(HideQuestElement());
        yield return null;
    }

    public void StartShowingElement()
    {
        StartCoroutine(ShowQuestElement());
    }

    IEnumerator ShowQuestElement()
    {
        questAppearanceSound = soundManager.LocateAudioSource("QuestUIAppear", transform);
        
        questAppearanceSound.Play();
        float elapsed = 0;
        float maxTime = 0.22f;

        float red = 0;
        float green = 0;
        float blue = 0;
        float alpha = 1;
        float yScale = 0;
        float currentPosition = 0;
        float startingPosition = transform.localPosition.x;
        float finalPosition = transform.localPosition.x - 30;

        Color color = Color.white;
        color = new Color(1, 1, 1, 0.9f);
        transform.GetComponent<Image>().color = color;
        while (elapsed < maxTime)
        {
            elapsed += Time.deltaTime;
            transform.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, elapsed / maxTime);
            yScale = Mathf.Lerp(0 , 1, elapsed / maxTime);
            transform.GetComponent<RectTransform>().localScale = new Vector3(1, yScale, 1);

            currentPosition = Mathf.Lerp(startingPosition, finalPosition, (elapsed * 0.85f) / maxTime);
            transform.localPosition = new Vector3(currentPosition, transform.localPosition.y, 0);
            yield return null;
        }
        transform.GetComponent<CanvasGroup>().alpha = 1;
        transform.localPosition = new Vector3(finalPosition, transform.localPosition.y, 0);

        elapsed = 0;
        while (elapsed < maxTime)
        {
            elapsed += Time.deltaTime;

            red = Mathf.Lerp(1, 0, elapsed / maxTime);
            green = Mathf.Lerp(1, 0, elapsed / maxTime);
            blue = Mathf.Lerp(1, 0, elapsed / maxTime);
            alpha = Mathf.Lerp(1, 0.48f, elapsed / maxTime);
            color = new Color(red, green, blue, alpha);
            transform.GetComponent<Image>().color = color;
            yield return null;
        }
        transform.GetComponent<Image>().color = color;

        
        yield return null;
    }

    IEnumerator HideQuestElement()
    {
        questDisappearanceSound = soundManager.LocateAudioSource("QuestUIDisappear", transform);
        questDisappearanceSound.Play();
        float elapsed = 0;
        float maxTime = 0.24f;
        while (elapsed < maxTime)
        {
            elapsed += Time.deltaTime;
            transform.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, elapsed / maxTime);
            yield return null;
        }
        transform.GetComponent<CanvasGroup>().alpha = 0;

        newQuestsNotificator.startedHidingElement -= StartRelocations;
        StopAllCoroutines();
        newQuestsNotificator.QuestElements.Remove(transform);
        newQuestsNotificator.StartHidingElement();
        questsDatabase.ShowResiduaryQuests();
        yield return new WaitForSeconds(0.3f);
        Destroy(transform.gameObject);
        yield return null;
    }


    void StartRelocations()
    {
        if (coroutineIsRunning) { StopCoroutine(hidingElemets); coroutineIsRunning = false; }
        if (calibrationCoroutineIsRunning) { StopCoroutine(calibratingElement); }
        hidingElemets = StartCoroutine(RelocateElement());
        coroutineIsRunning = true;
    }

    public void RecalibratePosition()
    {
        if (newQuestsNotificator.QuestElements[0].GetComponent<QuestNotificatorElement>().coroutineIsRunning)
        {
            if (coroutineIsRunning) { StopCoroutine(hidingElemets); coroutineIsRunning =false; }
            if (calibrationCoroutineIsRunning) { StopCoroutine(calibratingElement); }
            calibratingElement = StartCoroutine(RelocateElement(newQuestsNotificator.QuestElements[0].GetComponent<QuestNotificatorElement>().positionChangeElapsed));
            calibrationCoroutineIsRunning = true;
        }
    }

    IEnumerator RelocateElement()
    {

        positionChangeElapsed = 0f;
        float maxTime = 0.24f;

        int index = newQuestsNotificator.QuestElements.IndexOf(transform);
        float startingPosition = transform.localPosition.y;
        float currentPosition = 0;

        float finalPosition;
        if (coroutineIsRunning)
        {
            finalPosition = -((index + 0) * newQuestsNotificator.ElementSize.y);
        } else
        {
            finalPosition = -((index + 1) * newQuestsNotificator.ElementSize.y);
        }
        
        while (positionChangeElapsed < maxTime)
        {
            positionChangeElapsed += Time.deltaTime;
            currentPosition = Mathf.Lerp(startingPosition, finalPosition, positionChangeElapsed / maxTime);
            transform.localPosition = new Vector3(transform.localPosition.x, currentPosition, 0);
            yield return null;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, finalPosition, 0);
        coroutineIsRunning = false;
        yield return null;
    }

    IEnumerator RelocateElement(float calibratedElapsed)
    {
        float elapsed = calibratedElapsed;
        float maxTime = 0.24f;

        int index = newQuestsNotificator.QuestElements.IndexOf(transform);
        float startingPosition = transform.localPosition.y;
        float currentPosition = 0;

        float finalPosition;
        if (coroutineIsRunning)
        {
            finalPosition = -((index + 0) * newQuestsNotificator.ElementSize.y);
        }
        else
        {
            finalPosition = -((index + 1) * newQuestsNotificator.ElementSize.y);
        }

        while (elapsed < maxTime)
        {
            elapsed += Time.deltaTime;
            currentPosition = Mathf.Lerp(startingPosition, finalPosition, elapsed / maxTime);
            transform.localPosition = new Vector3(transform.localPosition.x, currentPosition, 0);
            yield return null;
        }
        transform.localPosition = new Vector3(transform.localPosition.x, finalPosition, 0);
        calibrationCoroutineIsRunning = false;
        yield return null;
    }

}
