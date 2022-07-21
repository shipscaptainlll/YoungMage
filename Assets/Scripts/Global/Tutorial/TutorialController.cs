using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] MiscPanel miscPanel;
    // Start is called before the first frame update
    void Start()
    {
        StartTutorial();
        miscPanel.TutorialResetRequested += ResetTutorial;
    }


    void StartTutorial()
    {
        Debug.Log("Hello there!");
    }

    void ResetTutorial()
    {
        Debug.Log("Tutorial has been restarted");
        StartTutorial();
    }
}
