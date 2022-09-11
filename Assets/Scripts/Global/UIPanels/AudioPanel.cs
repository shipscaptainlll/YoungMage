using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPanel : MonoBehaviour
{
    float overallVolume;
    float effectsVolume;
    float charactersSpeechVolume;
    float musicVolume;
    bool soundsTurnedOff;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;

    AudioSource chooseSound;

    public event Action<int> SettingChanged = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        chooseSound = soundManager.FindSound("SettingElement");
        ApplyDefaultSettings();
    }

    public void OverallVolumeChange(Slider slider)
    {
        overallVolume = slider.value;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        //Debug.Log("Overall volume now is " + overallVolume);
    }

    public void EffectsVolumeChange(Slider slider)
    {
        effectsVolume = slider.value;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        //Debug.Log("Effects volume now is " + effectsVolume);
    }

    public void CharactersSpeechChange(Slider slider)
    {
        charactersSpeechVolume = slider.value;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        //Debug.Log("Characters speech now is " + charactersSpeechVolume);
    }

    public void MusicVolumeChange(Slider slider)
    {
        musicVolume = slider.value;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        //Debug.Log("Music volume now is " + musicVolume);
    }

    public void TurnOffSounds(Toggle toggle)
    {
        soundsTurnedOff = toggle.isOn;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        //Debug.Log("Sounds turned off: " + soundsTurnedOff);
    }

    void ApplyDefaultSettings()
    {
        overallVolume = 50;
        //Debug.Log("Overall volume now is " + overallVolume);
        effectsVolume = 50;
        //Debug.Log("Effects volume now is " + effectsVolume);
        charactersSpeechVolume = 50;
        //Debug.Log("Characters speech now is " + charactersSpeechVolume);
        musicVolume = 50;
        //Debug.Log("Music volume now is " + musicVolume);
        soundsTurnedOff = false;
        //Debug.Log("Sounds are now " + soundsTurnedOff);
        chooseSound.Play();
    }
}
