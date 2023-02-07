using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiscPanel : MonoBehaviour
{
    [SerializeField] Slider slider;
    
    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;

    AudioSource chooseSound;


    public event Action TutorialResetRequested = delegate { };
    public event Action WarpBaseRequested = delegate { };
    public event Action<float> AutosaveTimeChangeRequested = delegate { };
    public event Action<int> SettingChanged = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        chooseSound = soundManager.FindSound("SettingElement");
        UploadPlayerPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTutorial()
    {
        if (TutorialResetRequested != null) { TutorialResetRequested(); }
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Tutorial has been reseted");
    }

    public void WarpBase()
    {
        if (WarpBaseRequested != null) { WarpBaseRequested(); }
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Warped to the base");
    }

    public void SetAutosaveTime(Slider slider)
    {
        float newAutosaveDelay = slider.value;
        Text sliderValueRepresentation = slider.gameObject.transform.Find("Text").GetComponent<Text>();
        sliderValueRepresentation.text = newAutosaveDelay + " min";
        if (AutosaveTimeChangeRequested != null) { AutosaveTimeChangeRequested(newAutosaveDelay); }
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        //Debug.Log("AutosaveTimeSet on " + newAutosaveDelay + " mins");
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("autoSaveDelay", slider.value);
        Debug.Log("all misc settings was saved");
    }

    public void UploadPlayerPrefs()
    {
        slider.value = PlayerPrefs.GetFloat("autoSaveDelay", 3);
    }
}
