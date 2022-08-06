using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedQuestsNotificator : MonoBehaviour
{
    [SerializeField] Transform questElementTemplate;
    [SerializeField] Transform questElementsHolder;
    [SerializeField] Transform startPosition;
    Vector2 elementSize;



    List<Transform> questElements = new List<Transform>();
    public event Action startedHidingElement = delegate { };

    public List<Transform> QuestElements { get { return questElements; } }
    public Vector2 ElementSize { get { return elementSize; } }
    public Transform StartPosition { get { return startPosition; } }
    // Start is called before the first frame update

    void Awake()
    {

    }
    void Start()
    {
        elementSize = new Vector2(questElementTemplate.GetComponent<RectTransform>().rect.width,
            questElementTemplate.GetComponent<RectTransform>().rect.height);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {

        }
    }

    public void InstantiateQuestElement(string questDescription)
    {
        Transform newElement = null;
        if (questElements.Count == 0)
        {
            newElement = Instantiate(questElementTemplate,
            questElementsHolder.position,
            startPosition.rotation,
            questElementsHolder);
            newElement.gameObject.SetActive(true);
            questElements.Add(newElement);
            newElement.localPosition -= new Vector3(0, elementSize.y, 0);
        }
        else
        {
            newElement = Instantiate(questElementTemplate,
            questElements[questElements.Count - 1].position,
            startPosition.rotation,
            questElementsHolder);
            newElement.gameObject.SetActive(true);
            newElement.position = new Vector3(questElementsHolder.position.x, newElement.position.y, newElement.position.z);
            newElement.localPosition -= new Vector3(0, elementSize.y, 0);
            questElements.Add(newElement);
            newElement.GetComponent<CompletedQuestElement>().RecalibratePosition();
        }
        newElement.GetComponent<CompletedQuestElement>().StartShowingElement();
        newElement.GetComponent<CompletedQuestElement>().QuestDescription = questDescription;
    }

    public void StartHidingElement()
    {
        if (startedHidingElement != null) { startedHidingElement(); }
    }
}
