using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMessagesInstantiator : MonoBehaviour
{
    [SerializeField] Transform messagesHolder;
    [SerializeField] PanelsManager panelsManager;
    [SerializeField] IntroEntering introEntering;
    int introIndex;
    int numberOfMessages;

    public int IntroIndex { get { return introIndex; } }
    public int NumberOfMessages { get { return numberOfMessages; } }
    // Start is called before the first frame update
    void Start()
    {
        numberOfMessages = messagesHolder.childCount;
    }

    public void ShowNextMessage()
    {
        
        if (introIndex < numberOfMessages)
        {
            if (introIndex != 0) { messagesHolder.GetChild(introIndex - 1).GetComponent<CanvasGroup>().alpha = 0; }
            messagesHolder.GetChild(introIndex).GetComponent<CanvasGroup>().alpha = 1;
            introIndex++;
        }
    }

    public void HideMessages()
    {
        messagesHolder.GetChild(numberOfMessages - 1).GetComponent<CanvasGroup>().alpha = 0;
    }
}
