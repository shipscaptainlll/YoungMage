using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] MiscPanel miscPanel;
    [SerializeField] Transform tutorialsHolder;

    // Start is called before the first frame update
    void Start()
    {
        miscPanel.TutorialResetRequested += ResetTutorial;
    }

    void ResetTutorial()
    {
        foreach (Transform element in tutorialsHolder)
        {
            element.GetComponent<TutorialInvoker>().TurnOnInvoker();
        }
    }
}
