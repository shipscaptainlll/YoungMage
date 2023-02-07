using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphicsPanel : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] int pointLightShadows;
    [SerializeField] Slider slider;
    [SerializeField] int graphicsQuality;
    [SerializeField] Dropdown dropdown;
    [SerializeField] bool activePointLightShadows;
    [SerializeField] Toggle toggle;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;

    AudioSource chooseSound;


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

    public void SetPointLightShadows(Slider slider)
    {
        pointLightShadows = (int) slider.value;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Point light shadows changed: " + pointLightShadows);
    }

    public void SetGraphicsQuality(Dropdown dropdown)
    {
        graphicsQuality = dropdown.value;
        dropdown.Hide();
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        //Debug.Log("Graphics quality is: " + graphicsQuality);
    }

    public void SetActivePointLightShadows(Toggle toggle)
    {
        activePointLightShadows = toggle.isOn;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Point light shadows are active " + activePointLightShadows);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("pointLightShadows", pointLightShadows);
        Debug.Log("point light shadows are " + pointLightShadows);
        Debug.Log("point light shadows are " + PlayerPrefs.GetFloat("pointLightShadows"));
        PlayerPrefs.SetInt("graphicsQuality", graphicsQuality);
        int cacheActivePointLightShadows = activePointLightShadows == true ? 1 : 0;
        PlayerPrefs.SetInt("activePointLightShadows", cacheActivePointLightShadows);
        chooseSound.Play();
        Debug.Log("all graphics setting was saved");
    }

    void ApplyDefaultSettings()
    {
        
    }

    void UploadPlayerPrefs()
    {
        pointLightShadows = PlayerPrefs.GetInt("pointLightShadows", 50);
        //Debug.Log("point light shadows are " + PlayerPrefs.GetInt("pointLightShadows"));
        //Debug.Log("point light shadows are " + PlayerPrefs.GetInt("pointLightShadows", 50));
        slider.value = pointLightShadows;
        graphicsQuality = PlayerPrefs.GetInt("graphicsQuality", 1);
        dropdown.value = graphicsQuality;
        int cacheActivePointLightShadows = PlayerPrefs.GetInt("activePointLightShadows", 0);
        activePointLightShadows = cacheActivePointLightShadows == 1 ? true : false;
        toggle.isOn = activePointLightShadows;
    }
}
