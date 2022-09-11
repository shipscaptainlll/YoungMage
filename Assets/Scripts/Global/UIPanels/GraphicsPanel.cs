using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsPanel : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] float pointLightShadows;
    [SerializeField] string graphicsQuality;
    [SerializeField] bool activePointLightShadows;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;

    AudioSource chooseSound;


    public event Action<int> SettingChanged = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        chooseSound = soundManager.FindSound("SettingElement");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPointLightShadows(Slider slider)
    {
        pointLightShadows = slider.value;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Point light shadows changed: " + pointLightShadows);
    }

    public void SetGraphicsQuality(Dropdown dropdown)
    {
        graphicsQuality = dropdown.value.ToString();
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Graphics quality is: " + graphicsQuality);
    }

    public void SetActivePointLightShadows(Toggle toggle)
    {
        activePointLightShadows = toggle.isOn;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Point light shadows are active " + activePointLightShadows);
    }
}
