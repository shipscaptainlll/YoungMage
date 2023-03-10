using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInvoker : MonoBehaviour
{
    [SerializeField] TutorialsInstantiator tutorialsInstantiator;
    [SerializeField] int invokedTutorialId;
    bool isCompleted;

    public bool IsCompleted { get { return isCompleted; } set { isCompleted = value; } }
    public int InvokedTutorialId { get { return invokedTutorialId; } }

    void OnTriggerEnter(Collider other)
    {
        if (!isCompleted && other.gameObject.layer == 11)
        {
            InvokeTutorial();
        }
    }

    public void InvokeTutorial()
    {
        tutorialsInstantiator.ActivateTutorial(invokedTutorialId);
        isCompleted = true;
    }

}
