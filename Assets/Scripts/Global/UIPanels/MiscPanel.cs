using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiscPanel : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Slider languageSlider;
    [SerializeField] Text languageComment;

    [SerializeField] Transform tutorialButton;
    [SerializeField] Transform stuckButton;
    
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
        UpdateLanguageParametersRepresentation();

        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            tutorialButton.gameObject.SetActive(false);
            stuckButton.gameObject.SetActive(false);
        }
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
    
    public void SetGameLanguage(Slider slider)
    {
        int newLocalizationId = (int) slider.value;
        Text sliderValueRepresentation = slider.gameObject.transform.Find("Text").GetComponent<Text>();
        sliderValueRepresentation.text = LocalizationChanger.GetLocalizationName(newLocalizationId);
        LocalizationChanger.ApplyLocalization(newLocalizationId);
        PlayerPrefs.SetString("selected-locale", LocalizationSettings.SelectedLocale.ToString());
        //if (AutosaveTimeChangeRequested != null) { AutosaveTimeChangeRequested(newAutosaveDelay); }
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        //Debug.Log("Localization changed on is now " + sliderValueRepresentation.text);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("autoSaveDelay", slider.value);
        Debug.Log("all misc settings was saved");
    }

    public void UpdateLanguageParametersRepresentation()
    {
        languageComment.text = LocalizationChanger.GetLocalizationName(LocalizationSettings.SelectedLocale.LocaleName);
        languageSlider.value = LocalizationChanger.GetLocalizationId(LocalizationSettings.SelectedLocale.LocaleName);
        PlayerPrefs.SetString("selected-locale", LocalizationSettings.SelectedLocale.ToString());
    }

    public void UploadPlayerPrefs()
    {
        
        slider.value = PlayerPrefs.GetFloat("autoSaveDelay", 3);
        Debug.Log("current language is " + LocalizationSettings.SelectedLocale.LocaleName + " while player prefs is " + PlayerPrefs.GetString("selected-locale"));
    }
}
