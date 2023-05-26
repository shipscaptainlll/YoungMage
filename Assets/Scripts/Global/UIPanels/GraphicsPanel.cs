using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GraphicsPanel : MonoBehaviour
{
    [Header("Basic Settings")] 
    [SerializeField] private Camera m_mainCamera;
    [SerializeField] private Camera m_secondCamera;
    [SerializeField] private Camera m_thirdCamera;
    [SerializeField] private Volume m_volume;
    [SerializeField] private int m_totalQuality;
    [SerializeField] private Slider m_totalSlider;
    [SerializeField] private Transform m_totalComments;
    [SerializeField] private int m_viewDistanceQuality;
    [SerializeField] private Slider m_viewDistanceSlider;
    [SerializeField] private Transform m_viewDistanceComments;
    [SerializeField] private int m_antiAliasingQuality;
    [SerializeField] private Slider m_antiAliasingSlider;
    [SerializeField] private Transform m_antiAliasingComments;
    [SerializeField] private int m_postProcessingQuality;
    [SerializeField] private Slider m_postProcessingSlider;
    [SerializeField] private Transform m_postProcessingComments;
    [SerializeField] private int m_shadowsQuality;
    [SerializeField] private Slider m_shadowsSlider;
    [SerializeField] private Transform m_shadowsComments;
    [SerializeField] private int m_texturesQuality;
    [SerializeField] private Slider m_texturesSlider;
    [SerializeField] private Transform m_texturesComments;
    [SerializeField] private int m_effectsQuality;
    [SerializeField] private Slider m_effectsSlider;
    [SerializeField] private Transform m_effectsComments;

    [SerializeField] private int m_monitorResolutionLevel;
    [SerializeField] private Dropdown m_monitorResolutionDropdown;
    [SerializeField] private int m_maxFramerateLevel;
    [SerializeField] private Dropdown m_maxFramerateDropdown;
    
    [SerializeField] private bool m_windowMode;
    [SerializeField] private Toggle m_windowModeToggle;
    [SerializeField] private bool m_activatedVSync;
    [SerializeField] private Toggle m_activatedVSyncToggle;
    [SerializeField] private bool m_activatedFPSshower;
    [SerializeField] private Toggle m_toggleFPSshower;
    [SerializeField] private FPSshower m_FPSshower;
    
    [SerializeField] private int m_gammaLevel;
    [SerializeField] private Slider m_gammaSlider;
    [SerializeField] private Text m_gammaComments;
    [SerializeField] private int m_FOVLevel;
    [SerializeField] private Slider m_FOVSlider;
    [SerializeField] private Text m_FOVComments;
    private ColorAdjustments m_colorAdjustments;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;

    [Header("Other")] 
    [SerializeField] private PanelsManager m_panelsManager;
    
    AudioSource chooseSound;
    
    public event Action<int> SettingChanged = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        chooseSound = soundManager.FindSound("SettingElement");
        UploadPlayerPrefs();
        if (m_panelsManager != null)
        {
            m_panelsManager.ManageSettingsPanel("graphicsPanel");
        } else
        {
            Debug.Log("fix later m_panelsManager none here");
        }
        
        
        if (m_volume != null && m_volume.profile.TryGet<ColorAdjustments>(out m_colorAdjustments))
        {
            
        }
        QualitySettings.vSyncCount = 0;
    }


    
    public void SetTotalGraphicsQuality(Slider slider)
    {
        m_totalQuality = (int) slider.value;
        
        foreach (Transform element in m_totalComments)
        {
            element.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (m_totalQuality == 1)
        {
            m_totalComments.Find("Low").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_totalQuality == 2)
        {
            m_totalComments.Find("Medium").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_totalQuality == 3)
        {
            m_totalComments.Find("High").GetComponent<CanvasGroup>().alpha = 1;
        } 
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Total quality changed: " + m_totalQuality);
    }

    void TotalQualityToCustom()
    {
        m_totalQuality = 4;
        foreach (Transform element in m_totalComments)
        {
            element.GetComponent<CanvasGroup>().alpha = 0;
        }
        m_totalComments.Find("Custom").GetComponent<CanvasGroup>().alpha = 1;
    }
    
    public void SetViewDistanceQuality(Slider slider)
    {
        m_viewDistanceQuality = (int) slider.value;

        TotalQualityToCustom();
        
        foreach (Transform element in m_viewDistanceComments)
        {
            element.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (m_viewDistanceQuality == 1)
        {
            m_viewDistanceComments.Find("Low").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_viewDistanceQuality == 2)
        {
            m_viewDistanceComments.Find("Medium").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_viewDistanceQuality == 3)
        {
            m_viewDistanceComments.Find("High").GetComponent<CanvasGroup>().alpha = 1;
        } 
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("View distance quality changed: " + m_viewDistanceQuality);
    }
    
    public void SetAntiAliasingQuality(Slider slider)
    {
        m_antiAliasingQuality = (int) slider.value;
        
        TotalQualityToCustom();
        
        foreach (Transform element in m_antiAliasingComments)
        {
            element.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (m_antiAliasingQuality == 1)
        {
            m_antiAliasingComments.Find("Low").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_antiAliasingQuality == 2)
        {
            m_antiAliasingComments.Find("Medium").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_antiAliasingQuality == 3)
        {
            m_antiAliasingComments.Find("High").GetComponent<CanvasGroup>().alpha = 1;
        } 
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Anti aliasing quality changed: " + m_antiAliasingQuality);
    }
    
    public void SetPostProcessingQuality(Slider slider)
    {
        m_postProcessingQuality = (int) slider.value;
        
        TotalQualityToCustom();
        
        foreach (Transform element in m_postProcessingComments)
        {
            element.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (m_postProcessingQuality == 1)
        {
            m_postProcessingComments.Find("Low").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_postProcessingQuality == 2)
        {
            m_postProcessingComments.Find("Medium").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_postProcessingQuality == 3)
        {
            m_postProcessingComments.Find("High").GetComponent<CanvasGroup>().alpha = 1;
        } 
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Post processing quality changed: " + m_postProcessingQuality);
    }
    
    public void SetShadowsQuality(Slider slider)
    {
        m_shadowsQuality = (int) slider.value;
        
        TotalQualityToCustom();
        
        foreach (Transform element in m_shadowsComments)
        {
            element.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (m_shadowsQuality == 1)
        {
            m_shadowsComments.Find("Low").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_shadowsQuality == 2)
        {
            m_shadowsComments.Find("Medium").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_shadowsQuality == 3)
        {
            m_shadowsComments.Find("High").GetComponent<CanvasGroup>().alpha = 1;
        } 
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Shadows quality changed: " + m_shadowsQuality);
    }
    
    public void SetTexturesQuality(Slider slider)
    {
        m_texturesQuality = (int) slider.value;
        
        TotalQualityToCustom();
        
        foreach (Transform element in m_texturesComments)
        {
            element.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (m_texturesQuality == 1)
        {
            m_texturesComments.Find("Low").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_texturesQuality == 2)
        {
            m_texturesComments.Find("Medium").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_texturesQuality == 3)
        {
            m_texturesComments.Find("High").GetComponent<CanvasGroup>().alpha = 1;
        } 
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("textures quality changed: " + m_texturesQuality);
    }
    
    public void SetEffectsQuality(Slider slider)
    {
        m_effectsQuality = (int) slider.value;
        
        TotalQualityToCustom();
        
        foreach (Transform element in m_effectsComments)
        {
            element.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (m_effectsQuality == 1)
        {
            m_effectsComments.Find("Low").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_effectsQuality == 2)
        {
            m_effectsComments.Find("Medium").GetComponent<CanvasGroup>().alpha = 1;
        } else if (m_effectsQuality == 3)
        {
            m_effectsComments.Find("High").GetComponent<CanvasGroup>().alpha = 1;
        } 
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("effects quality changed: " + m_effectsQuality);
    }

    public void SetMonitorResolution(Dropdown dropdown)
    {
        m_monitorResolutionLevel = dropdown.value;
        dropdown.Hide();
        FullScreenMode tmp;
        
        if (m_windowMode)
        {
            tmp = FullScreenMode.Windowed;
        }
        else
        {
            tmp = FullScreenMode.FullScreenWindow;
        }
        
        if (m_monitorResolutionLevel == 0)
        {
            Screen.SetResolution(1024, 768, tmp);
            Debug.Log("Monitor resolution now is 1024 x 768");
        } else if (m_monitorResolutionLevel == 1)
        {
            Screen.SetResolution(1280, 800, tmp);
            Debug.Log("Monitor resolution now is 1280 x 800");
        } else if (m_monitorResolutionLevel == 2)
        {
            Screen.SetResolution(1280, 720, tmp);
            Debug.Log("Monitor resolution now is 1280 x 720");
        } else if (m_monitorResolutionLevel == 3)
        {
            Screen.SetResolution(1280, 1024, tmp);
            Debug.Log("Monitor resolution now is 1280 x 1024");
        } else if (m_monitorResolutionLevel == 4)
        {
            Screen.SetResolution(1360, 768, tmp);
            Debug.Log("Monitor resolution now is 1360 x 768");
        } else if (m_monitorResolutionLevel == 5)
        {
            Screen.SetResolution(1366, 768, tmp);
            Debug.Log("Monitor resolution now is 1366 x 768");
        } else if (m_monitorResolutionLevel == 6)
        {
            Screen.SetResolution(1440, 900, tmp);
            Debug.Log("Monitor resolution now is 1440 x 900");
        } else if (m_monitorResolutionLevel == 7)
        {
            Screen.SetResolution(1600, 900, tmp);
            Debug.Log("Monitor resolution now is 1600 x 900");
        } else if (m_monitorResolutionLevel == 8)
        {
            Screen.SetResolution(1680, 1050, tmp);
            Debug.Log("Monitor resolution now is 1680 x 1050");
        } else if (m_monitorResolutionLevel == 9)
        {
            Screen.SetResolution(1920, 1080, tmp);
            Debug.Log("Monitor resolution now is 1920 x 1080");
        } else if (m_monitorResolutionLevel == 10)
        {
            Screen.SetResolution(1920, 1200, tmp);
            Debug.Log("Monitor resolution now is 1920 x 1200");
        } else if (m_monitorResolutionLevel == 11)
        {
            Screen.SetResolution(2560, 1080, tmp);
            Debug.Log("Monitor resolution now is 2560 x 1080");
        } else if (m_monitorResolutionLevel == 12)
        {
            Screen.SetResolution(2560, 1600, tmp);
            Debug.Log("Monitor resolution now is 2560 x 1600");
        } else if (m_monitorResolutionLevel == 13)
        {
            Screen.SetResolution(2560, 1440, tmp);
            Debug.Log("Monitor resolution now is 2560 x 1440");
        } else if (m_monitorResolutionLevel == 14)
        {
            Screen.SetResolution(3440, 1440, tmp);
            Debug.Log("Monitor resolution now is 3440 x 1440");
        } else if (m_monitorResolutionLevel == 15)
        {
            Screen.SetResolution(3840, 2160, tmp);
            Debug.Log("Monitor resolution now is 3840 x 2160");
        }
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("monitor resolution changed: " + m_effectsQuality);
    }
    
    public void SetWindowMode(Toggle toggle)
    {
        m_windowMode = toggle.isOn;
        
        if (m_windowMode)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            
            Debug.Log("window mode was activated");
        } else 
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Debug.Log("window mode was deactivated");
        } 
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("window mode info was changed: " + m_windowMode);
    }
    
    public void SetMaxFramerate(Dropdown dropdown)
    {
        m_maxFramerateLevel = dropdown.value;
        dropdown.Hide();
Debug.Log("max framerate is " + m_maxFramerateLevel);
        if (m_maxFramerateLevel == 0)
        {
            Application.targetFrameRate = 60;
            Debug.Log("max framerate was changed to 60");
        } else if (m_maxFramerateLevel == 1)
        {
            Application.targetFrameRate = 120;
            Debug.Log("max framerate was changed to 120");
        } else if (m_maxFramerateLevel == 2)
        {
            Application.targetFrameRate = 144;
            Debug.Log("max framerate was changed to 144");
        }
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("max framerate level changed: " + m_maxFramerateLevel);
    }
    
    public void SetVSync(Toggle toggle)
    {
        m_activatedVSync = toggle.isOn;

        if (m_activatedVSync)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("VSync is activated in setting: " + m_activatedVSync);
    }
    
    public void SetActiveFPSshower(Toggle toggle)
    {
        m_activatedFPSshower = toggle.isOn;
        m_FPSshower.EnabledFPS = m_activatedFPSshower;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("FPS shower is activated in setting: " + m_activatedFPSshower);
    }

    public void SetGammaLevel(Slider slider)
    {
        m_gammaLevel = (int) slider.value;

        m_colorAdjustments.postExposure.value = -5 + (m_gammaLevel / 10);
        
        m_gammaComments.text = m_gammaLevel.ToString();
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Gamma level changed: " + m_gammaLevel);
    }
    
    public void SetFOV(Slider slider)
    {
        m_FOVLevel = (int) slider.value;

        if (m_mainCamera != null && m_secondCamera != null && m_thirdCamera != null)
        {
            m_mainCamera.fieldOfView = m_FOVLevel;
            m_secondCamera.fieldOfView = m_FOVLevel;
            m_thirdCamera.fieldOfView = m_FOVLevel;
        }

        
        m_FOVComments.text = m_FOVLevel.ToString();
        
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("FOV level changed: " + m_FOVLevel);
    }
    
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("totalQuality", m_totalQuality);
        Debug.Log("total quality is " + m_totalQuality);
        
        PlayerPrefs.SetInt("viewDistanceQuality", m_viewDistanceQuality);
        Debug.Log("view distance quality is " + m_viewDistanceQuality);
        
        PlayerPrefs.SetInt("antiAliasingQuality", m_antiAliasingQuality);
        Debug.Log("antialiasing quality is " + m_antiAliasingQuality);
        
        PlayerPrefs.SetInt("postProcessingQuality", m_postProcessingQuality);
        Debug.Log("post processing quality is " + m_postProcessingQuality);
        
        PlayerPrefs.SetInt("shadowsQuality", m_shadowsQuality);
        Debug.Log("shadows quality is " + m_shadowsQuality);
        
        PlayerPrefs.SetInt("texturesQuality", m_texturesQuality);
        Debug.Log("textures quality is " + m_texturesQuality);
        
        PlayerPrefs.SetInt("effectsQuality", m_effectsQuality);
        Debug.Log("effects quality is " + m_effectsQuality);
        
        PlayerPrefs.SetInt("monitorResolution", m_monitorResolutionLevel);
        Debug.Log("monitor resolution level is " + m_monitorResolutionLevel);
        
        int cacheActiveWindowMode = m_windowMode == true ? 1 : 0;
        PlayerPrefs.SetInt("windowMode", cacheActiveWindowMode);
        Debug.Log("window mode is on " + m_windowMode);
        
        PlayerPrefs.SetInt("maxFramerateLevel", m_maxFramerateLevel);
        Debug.Log("max framerate level is " + m_maxFramerateLevel);
        
        int cacheVSyncActivated = m_activatedVSync == true ? 1 : 0;
        PlayerPrefs.SetInt("vSyncActivated", cacheVSyncActivated);
        Debug.Log("VSync is on " + m_activatedVSync);
        
        int cacheActiveFPSshower = m_activatedFPSshower == true ? 1 : 0;
        PlayerPrefs.SetInt("activeFPSshower", cacheActiveFPSshower);
        Debug.Log("FPS shower is on " + m_activatedFPSshower);
        
        PlayerPrefs.SetInt("gammaLevel", m_gammaLevel);
        Debug.Log("gamma level is " + m_gammaLevel);
        
        PlayerPrefs.SetInt("FOVlevel", m_FOVLevel);
        Debug.Log("FOV level is " + m_FOVLevel);
        
        chooseSound.Play();
        Debug.Log("all graphics setting was saved");
    }

    void ApplyDefaultSettings()
    {
        
    }

    void UploadPlayerPrefs()
    {
        m_totalQuality = PlayerPrefs.GetInt("totalQuality", 2);
        if (m_totalQuality != 4) { m_totalSlider.value = m_totalQuality; }

        m_viewDistanceQuality = PlayerPrefs.GetInt("viewDistanceQuality", 2);
        m_viewDistanceSlider.value = m_viewDistanceQuality;
        
        m_antiAliasingQuality = PlayerPrefs.GetInt("antiAliasingQuality", 2);
        m_antiAliasingSlider.value = m_antiAliasingQuality;
        
        m_postProcessingQuality = PlayerPrefs.GetInt("postProcessingQuality", 2);
        m_postProcessingSlider.value = m_postProcessingQuality;
        
        m_shadowsQuality = PlayerPrefs.GetInt("shadowsQuality", 2);
        m_shadowsSlider.value = m_shadowsQuality;
        
        m_texturesQuality = PlayerPrefs.GetInt("texturesQuality", 2);
        m_texturesSlider.value = m_texturesQuality;
        
        m_effectsQuality = PlayerPrefs.GetInt("effectsQuality", 2);
        m_effectsSlider.value = m_effectsQuality;
        
        m_monitorResolutionLevel = PlayerPrefs.GetInt("monitorResolution", 9);
        m_monitorResolutionDropdown.value = m_monitorResolutionLevel;
        
        int windowModeActivated = PlayerPrefs.GetInt("windowMode", 0);
        m_windowMode = windowModeActivated == 1 ? true : false;
        m_windowModeToggle.isOn = m_windowMode;
        
        m_maxFramerateLevel = PlayerPrefs.GetInt("maxFramerateLevel", 3);
        m_maxFramerateDropdown.value = m_maxFramerateLevel;
        
        int vSyncActivated = PlayerPrefs.GetInt("vSyncActivated", 0);
        m_activatedVSync = vSyncActivated == 1 ? true : false;
        m_activatedVSyncToggle.isOn = m_activatedVSync;
        
        int cacheActiveFPSshower = PlayerPrefs.GetInt("activeFPSshower", 0);
        m_activatedFPSshower = cacheActiveFPSshower == 1 ? true : false;
        m_toggleFPSshower.isOn = m_activatedFPSshower;
        
        m_gammaLevel = PlayerPrefs.GetInt("gammaLevel", 50);
        m_gammaSlider.value = m_gammaLevel;
        
        m_FOVLevel = PlayerPrefs.GetInt("FOVlevel", 72);
        m_FOVSlider.value = m_FOVLevel;
        
    }
}
