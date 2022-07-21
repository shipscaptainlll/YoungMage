using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiscPanel : MonoBehaviour
{
    public event Action TutorialResetRequested = delegate { };
    public event Action WarpBaseRequested = delegate { };
    public event Action<float> AutosaveTimeChangeRequested = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTutorial()
    {
        if (TutorialResetRequested != null) { TutorialResetRequested(); }
        Debug.Log("Tutorial has been reseted");
    }

    public void WarpBase()
    {
        if (WarpBaseRequested != null) { WarpBaseRequested(); }
        Debug.Log("Warped to the base");
    }

    public void SetAutosaveTime(Slider slider)
    {
        float newAutosaveDelay = slider.value;
        Text sliderValueRepresentation = slider.gameObject.transform.Find("Text").GetComponent<Text>();
        sliderValueRepresentation.text = newAutosaveDelay + " min";
        if (AutosaveTimeChangeRequested != null) { AutosaveTimeChangeRequested(newAutosaveDelay); }
        Debug.Log("AutosaveTimeSet on " + newAutosaveDelay + " mins");
    }
}
