using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningModeFlow : MonoBehaviour
{
    [SerializeField] private bool m_activateLearningMode;
    [SerializeField] private Transform m_learningQuestsHolder;
    [SerializeField] private CastleLookCatcher m_castleLookCatcher;
    static List<ILearningQuest> m_learningQuests = new List<ILearningQuest>();
    private static int m_nextTutorialID;
    
    public bool ActivateLearningMode => m_activateLearningMode;
    public int NextTutorialID => m_nextTutorialID;

    void Start()
    {
        foreach (Transform element in m_learningQuestsHolder)
        {
            m_learningQuests.Add(element.GetComponent<ILearningQuest>());
        }

        if (!m_activateLearningMode)
        {
            Destroy(m_castleLookCatcher.gameObject);
        }
        
        if (m_activateLearningMode)
        {
            StartCoroutine(StartTutorials());

        }
    }

    IEnumerator StartTutorials()
    {
        yield return new WaitForSeconds(0.2f);
        m_nextTutorialID = 0;
        Debug.Log("initiating learning tutorials");
        TryInitiateNextTutorial();
        yield return null;
    }

    public static void TryInitiateNextTutorial()
    {
        if (m_learningQuests.Count > m_nextTutorialID)
        {
            m_learningQuests[m_nextTutorialID].ActivateQuestSequence();
            m_nextTutorialID++;
        }
        else
        {
            Debug.Log("All tutorials are done");
        }
        
    }
    
}
