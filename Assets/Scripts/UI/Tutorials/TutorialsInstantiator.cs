using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsInstantiator : MonoBehaviour
{
    [SerializeField] PanelsManager panelsManager;
    [SerializeField] Transform tutorialsHolder;
    [SerializeField] Transform tutorialInvokersHolder;
    [SerializeField] TutorialModeActivator tutorialModeActivator;
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
        Debug.Log("well hello thr " + tutorial.transform);
        SaveData(id, tutorial);
        ShowTutorialPanel(tutorial);
        tutorialModeActivator.ApplyTutorialReadingMode();
    }

    public void FinishTutorial()
    {
        if (currentTutorial != null && currentlyOpenedTutorialId != 0)
        {
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
        tutorial.Panel.GetComponent<CanvasGroup>().alpha = 1;
        panelsManager.OpenTutorialPanel();
    }

    

    void CloseTutorialPanel()
    {
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
