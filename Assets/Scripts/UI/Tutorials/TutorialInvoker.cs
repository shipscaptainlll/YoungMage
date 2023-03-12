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
    bool isCompleted;

    public bool IsCompleted { get { return isCompleted; } set { isCompleted = value; } }
    public int InvokedTutorialId { get { return invokedTutorialId; } }


    void OnTriggerStay(Collider other)
    {
        if (!isCompleted && other.gameObject.layer == 11 && cameraController.SeeingTutorial)
        {
            if(cameraController.HitThird.transform.GetComponent<TutorialInvoker>().InvokedTutorialId == invokedTutorialId)
            {
                InvokeTutorial();
            }
        }
    }

    public void InvokeTutorial()
    {
        tutorialsInstantiator.ActivateTutorial(invokedTutorialId);
        TurnOffInvoker();
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
