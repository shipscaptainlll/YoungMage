using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleLookCatcher : MonoBehaviour
{
    [SerializeField] LearningModeFlow learningModeFlow;

    void Start()
    {
        CheckLearningsState();
    }

    void CheckLearningsState()
    {
        if (!learningModeFlow.ActivateLearningMode)
        {
            transform.GetComponent<BoxCollider>().enabled = false;
        } else
        {
            transform.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void Refresh()
    {
        CheckLearningsState();
    }


}
