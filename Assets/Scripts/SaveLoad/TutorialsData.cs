using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TutorialsData
{
    public bool[] tutorialWasFinished;
    public bool[] tutorialInvokerWasTriggered;
    public int currentTutorial;

    public TutorialsData(TutorialsInstantiator tutorialsInstantiator)
    {
        PreInitialiseData(tutorialsInstantiator);
        GetFinishedTutorials(tutorialsInstantiator);
        GetTriggeredInvokers(tutorialsInstantiator);
        GetCurrentTutorial(tutorialsInstantiator);
    }

    public void PreInitialiseData(TutorialsInstantiator tutorialsInstantiator)
    {
        tutorialWasFinished = new bool[tutorialsInstantiator.TutorialsHolder.childCount];
        tutorialInvokerWasTriggered = new bool[tutorialsInstantiator.TutorialsHolder.childCount];
        currentTutorial = 0;
    }

    public void GetCurrentTutorial(TutorialsInstantiator tutorialsInstantiator)
    {
        currentTutorial = tutorialsInstantiator.CurrentlyOpenedTutorial;
    }

    public void GetFinishedTutorials(TutorialsInstantiator tutorialsInstantiator)
    {
        int indexer = 0;

        foreach (Transform element in tutorialsInstantiator.TutorialsHolder)
        {
            tutorialWasFinished[indexer] = element.GetComponent<TutorialElement>().IsFinished;
            indexer++;
        }
    }

    public void GetTriggeredInvokers(TutorialsInstantiator tutorialsInstantiator)
    {
        int indexer = 0;

        foreach (Transform element in tutorialsInstantiator.TutorialInvokersHolder)
        {
            Debug.Log(element);
            tutorialInvokerWasTriggered[indexer] = element.GetComponent<TutorialInvoker>().IsCompleted;
            indexer++;
        }
    }

}
