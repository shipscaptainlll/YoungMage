using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInvoker : MonoBehaviour
{
    [SerializeField] TutorialsInstantiator tutorialsInstantiator;
    [SerializeField] int invokedTutorialId;
    [SerializeField] CameraController cameraController;
    [SerializeField] Transform turnedOnPosition;
    [SerializeField] Transform turnedOffPosition;
    [SerializeField] private LearningSkeletonsCatching m_learningSkeletonsCatching;
    [SerializeField] private LearningMakeMoney m_learningMakeMoney;
    [SerializeField] private LearningBreakingOre m_learningBreakingOre;
    [SerializeField] private LearningCreatingObjects m_learningCreatingObjects;
    
    bool isCompleted;

    public bool IsCompleted { get { return isCompleted; } set { isCompleted = value; } }
    public int InvokedTutorialId { get { return invokedTutorialId; } }


    void OnTriggerStay(Collider other)
    {
        if (!isCompleted && other.gameObject.layer == 11 && cameraController.SeeingTutorial)
        {
            //Debug.Log("invoked tutorial " + invokedTutorialId + " " + cameraController.HitThird.transform);
            if(cameraController.HitThird.transform != null 
               && cameraController.HitThird.transform.GetComponent<TutorialInvoker>() != null 
               && cameraController.HitThird.transform.GetComponent<TutorialInvoker>().InvokedTutorialId == invokedTutorialId)
            {
                
                if (invokedTutorialId == 2)
                {
                    if (m_learningSkeletonsCatching.NextStep == 1)
                    {
                        isCompleted = true;
                        //m_learningSkeletonsCatching.ShowNextStep();
                    }
                } else if (invokedTutorialId == 3)
                {
                    if (m_learningBreakingOre.NextStep == 1)
                    {
                        isCompleted = true;
                        m_learningBreakingOre.ShowNextStep();
                    }
                } else if (invokedTutorialId == 4)
                {
                    if (m_learningCreatingObjects.NextStep == 1)
                    {
                        isCompleted = true;
                        m_learningCreatingObjects.ShowNextStep();
                    }
                } else if (invokedTutorialId == 6)
                {
                    if (m_learningMakeMoney.NextStep == 1)
                    {
                        isCompleted = true;
                        m_learningMakeMoney.ShowNextStep();
                    }
                } else if (invokedTutorialId == 7)
                {
                    InvokeTutorial();
                }
                
            }
        }
    }

    public void InvokeTutorial()
    {
        if (!isCompleted)
        {
            tutorialsInstantiator.ActivateTutorial(invokedTutorialId);
            TurnOffInvoker();
        }
        
    }

    public void TurnOffInvoker()
    {
        Debug.Log("turning off invoker");
        isCompleted = true;
        transform.position = turnedOffPosition.position;
    }

    public void TurnOnInvoker()
    {
        Debug.Log("turning on invoker");
        isCompleted = false;
        transform.position = turnedOnPosition.position;
    }

}
