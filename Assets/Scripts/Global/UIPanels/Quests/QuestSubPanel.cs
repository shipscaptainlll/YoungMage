using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSubPanel : MonoBehaviour
{
    [SerializeField] QuestsDatabase questsDatabase;
    [SerializeField] Transform questRowFirst;
    [SerializeField] Transform questRowSecond;
    [SerializeField] Transform questRowThird;

    // Start is called before the first frame update
    void Start()
    {
        questsDatabase.newQuestAdded += UpdateQuestElements;
        foreach (Transform questElement in questRowFirst)
        {
            questElement.GetComponent<QuestElement>().QuestElementUpdate();
        }

        foreach (Transform questElement in questRowSecond)
        {
            questElement.GetComponent<QuestElement>().QuestElementUpdate();
        }

        foreach (Transform questElement in questRowThird)
        {
            questElement.GetComponent<QuestElement>().QuestElementUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateQuestElements()
    {
        foreach (Transform questElement in questRowFirst)
        {
            questElement.GetComponent<QuestElement>().QuestElementUpdate();
        }

        foreach (Transform questElement in questRowSecond)
        {
            questElement.GetComponent<QuestElement>().QuestElementUpdate();
        }

        foreach (Transform questElement in questRowThird)
        {
            questElement.GetComponent<QuestElement>().QuestElementUpdate();
        }
    }
}
