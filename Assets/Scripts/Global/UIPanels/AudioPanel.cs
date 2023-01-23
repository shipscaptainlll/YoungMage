using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPanel : MonoBehaviour
{
    float overallVolume;
    [SerializeField] Slider overallVolumeSlider;
    float effectsVolume;
    [SerializeField] Slider effectsVolumeSlider;
    float charactersSpeechVolume;
    [SerializeField] Slider charactersSpeechVolumeSlider;
    float musicVolume;
    [SerializeField] Slider musicVolumeSlider;
    bool soundsTurnedOff;
    [SerializeField] Toggle soundsTurnedOffToggle;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;

    AudioSource chooseSound;

    public event Action<int> SettingChanged = delegate { };
    // Start is called before the first frame update

    void Awake()
    {
        chooseSound = soundManager.FindSound("SettingElement");
    }

    void Start()
    {
        UploadPlayerPrefs();
        
        //ApplyDefaultSettings();
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

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("overallVolume", overallVolume);
        PlayerPrefs.SetFloat("effectsVolume", effectsVolume);
        PlayerPrefs.SetFloat("charactersSpeechVolume", charactersSpeechVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        int cacheSoundsTurnedOff = soundsTurnedOff == true ? 1 : 0;
        PlayerPrefs.SetInt("soundsTurnedOff", cacheSoundsTurnedOff);
        chooseSound.Play();
        Debug.Log("all sounds settings was saved");
    }

    void UploadPlayerPrefs()
    {
        overallVolume = PlayerPrefs.GetFloat("overallVolume", 50);
        overallVolumeSlider.value = overallVolume;
        effectsVolume = PlayerPrefs.GetFloat("effectsVolume", 50);
        effectsVolumeSlider.value = effectsVolume;
        charactersSpeechVolume = PlayerPrefs.GetFloat("charactersSpeechVolume", 50);
        charactersSpeechVolumeSlider.value = charactersSpeechVolume;
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 50);
        musicVolumeSlider.value = musicVolume;
        int cacheSoundsTurnedOff = PlayerPrefs.GetInt("soundsTurnedOff", 0);
        soundsTurnedOff = cacheSoundsTurnedOff == 1 ? true : false;
        soundsTurnedOffToggle.isOn = soundsTurnedOff;
        Debug.Log(overallVolume);
        Debug.Log(effectsVolume);
        Debug.Log(charactersSpeechVolume);
        Debug.Log(musicVolume);
        Debug.Log(cacheSoundsTurnedOff);
    }
}
