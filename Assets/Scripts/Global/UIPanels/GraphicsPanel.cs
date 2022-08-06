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


    public event Action<int> SettingChanged = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPointLightShadows(Slider slider)
    {
        pointLightShadows = slider.value;
        if (SettingChanged != null) { SettingChanged(1); }
        Debug.Log("Point light shadows changed: " + pointLightShadows);
    }

    public void SetGraphicsQuality(Dropdown dropdown)
    {
        graphicsQuality = dropdown.value.ToString();
        if (SettingChanged != null) { SettingChanged(1); }
        Debug.Log("Graphics quality is: " + graphicsQuality);
    }

    public void SetActivePointLightShadows(Toggle toggle)
    {
        activePointLightShadows = toggle.isOn;
        if (SettingChanged != null) { SettingChanged(1); }
        Debug.Log("Point light shadows are active " + activePointLightShadows);
    }
}
