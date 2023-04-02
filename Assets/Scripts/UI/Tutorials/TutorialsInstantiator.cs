using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsInstantiator : MonoBehaviour
{
    [SerializeField] PanelsManager panelsManager;
    [SerializeField] Transform tutorialsHolder;
    [SerializeField] Transform tutorialInvokersHolder;
    [SerializeField] TutorialModeActivator tutorialModeActivator;
    [SerializeField] private LearningCityRegeneration m_learningCityRegeneration;
    [SerializeField] private LearningModeFlow m_learningModeFlow;
    [SerializeField] private Transform m_tutorialShowedPosition;
    [SerializeField] private Transform m_tutorialFinishedPosition;
    [SerializeField] private CityRegenerationEnter m_cityRegenerationEnter;
    List<TutorialElement> tutorialElements = new List<TutorialElement>();
    TutorialElement currentTutorial;
    int currentlyOpenedTutorialId;

    public int CurrentlyOpenedTutorial { get { return currentlyOpenedTutorialId; } }
    public Transform TutorialInvokersHolder { get { return tutorialInvokersHolder; } }
    public Transform TutorialsHolder { get { return tutorialsHolder; } }

    void Start()
    {
        InitializeTutorialElements();
    }

    public void ActivateTutorial(int id)
    {
        TutorialElement tutorial = FindTutorial(id);
        SaveData(id, tutorial);
        ShowTutorialPanel(tutorial);
        tutorialModeActivator.ApplyTutorialReadingMode(id);
    }

    public void FinishTutorial()
    {
        Debug.Log("tutorials: we are here 3 " + transform);
        if (currentTutorial != null && currentlyOpenedTutorialId != 0)
        {
            int currentID = currentTutorial.GetComponent<TutorialElement>().ID;
            tutorialModeActivator.DisengageTutorialReadingMode();
            CloseTutorialPanel();
            currentTutorial.IsFinished = true;
            ReleaseData();
            if (currentID == 11
                || currentID == 12
                || currentID == 13)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }
    
    public void FinishTutorial(int tutorialID)
    {
        Debug.Log("tutorials: we are here ");
        if (currentTutorial != null && currentlyOpenedTutorialId != 0)
        {
            if (tutorialID == 1)
            {
                Debug.Log("tutorials: we are here 2");
                LearningModeFlow.TryInitiateNextTutorial();
            }
            else if (tutorialID == 2)
            {
                Debug.Log("tutorials: we are here 4");
                m_learningCityRegeneration.ShowNextStep();
            } else if (tutorialID == 3)
            {
                Debug.Log("tutorials: we are here 4");
                LearningModeFlow.TryInitiateNextTutorial();
            } else if (tutorialID == 4)
            {
                Debug.Log("tutorials: we are here 4");
                LearningModeFlow.TryInitiateNextTutorial();
            } else if (tutorialID == 9)
            {
                Debug.Log("tutorials: we are here 4");
                LearningModeFlow.TryInitiateNextTutorial();
            } else if (tutorialID == 10)
            {
                m_cityRegenerationEnter.ExitCityRegeneration();
                LearningModeFlow.TryInitiateNextTutorial();
            }
            tutorialModeActivator.DisengageTutorialReadingMode();
            CloseTutorialPanel();
            currentTutorial.IsFinished = true;
            ReleaseData();
        }
    }

    void SaveData(int tutorialId, TutorialElement tutorial)
    {
        currentlyOpenedTutorialId = tutorialId;
        currentTutorial = tutorial;
    }

    void ReleaseData()
    {
        currentlyOpenedTutorialId = 0;
        currentTutorial = null;
    }

    void ShowTutorialPanel(TutorialElement tutorial)
    {
        tutorial.transform.position = m_tutorialShowedPosition.position;
        tutorial.Panel.GetComponent<CanvasGroup>().alpha = 1;
        panelsManager.OpenTutorialPanel();
    }

    

    void CloseTutorialPanel()
    {
        currentTutorial.transform.position = m_tutorialFinishedPosition.position;
        currentTutorial.Panel.GetComponent<CanvasGroup>().alpha = 0;
        panelsManager.CloseTutorialPanel();
    }

    TutorialElement FindTutorial(int id)
    {
        foreach (TutorialElement element in tutorialElements)
        {
            if (element.ID == id)
            {
                return element;
            }
        }
        return null;
    }
    
    void InitializeTutorialElements()
    {
        foreach (Transform element in tutorialsHolder)
        {
            tutorialElements.Add(element.GetComponent<TutorialElement>());
        }
    }

}
