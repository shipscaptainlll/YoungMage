using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TutorialsDataApplier
{
    static TutorialsInstantiator tutorialsInstantiatorUploaded;
    static TutorialsData tutorialsDataUploaded;

    public static void ApplyTutorialsData(TutorialsInstantiator tutorialsInstantiator, TutorialsData tutorialsData)
    {
        UpdateData(tutorialsInstantiator, tutorialsData);
        ApplyTutorialsState(tutorialsInstantiator, tutorialsData);
        DisconnectData();
    }

    static void UpdateData(TutorialsInstantiator tutorialsInstantiator, TutorialsData tutorialsData)
    {
        tutorialsInstantiatorUploaded = tutorialsInstantiator;
        tutorialsDataUploaded = tutorialsData;
    }

    static void DisconnectData()
    {
        tutorialsInstantiatorUploaded = null;
        tutorialsDataUploaded = null;
    }

    static void ApplyTutorialsState(TutorialsInstantiator tutorialsInstantiator, TutorialsData tutorialsData)
    {
        tutorialsInstantiator.FinishTutorial();
        int indexer = 0;
        foreach (Transform element in tutorialsInstantiator.TutorialsHolder)
        {
            element.GetComponent<TutorialElement>().IsFinished = tutorialsData.tutorialWasFinished[indexer];
            indexer++;
        }
        indexer = 0;
        foreach (Transform element in tutorialsInstantiator.TutorialInvokersHolder)
        {
            if (tutorialsData.tutorialInvokerWasTriggered[indexer])
            {
                element.GetComponent<TutorialInvoker>().TurnOffInvoker();
            } else
            {
                element.GetComponent<TutorialInvoker>().TurnOnInvoker();
            }
            indexer++;
        }
        Debug.Log(tutorialsData.currentTutorial + " tutorial was not zero");
        if (tutorialsData.currentTutorial != 0) {
            Debug.Log("tutorial was not zero");
            tutorialsInstantiator.ActivateTutorial(tutorialsData.currentTutorial); }
    }
}
