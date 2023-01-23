using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlsPanel : MonoBehaviour
{
    public List<KeyCode> currentlyPressedKeys = new List<KeyCode>();
    float mouseSensitivity;
    float timeShiftPressed;
    bool mouseInverted;
    bool autorunToggled;
    Button activeButton;
    bool shiftPressed;
    bool changingSettings;
    [SerializeField] Slider mouseSensitivitySlider;
    [SerializeField] Toggle autorunToggle;
    [SerializeField] Toggle invertMouseToggle;
    [SerializeField] Text castSpellText;
    [SerializeField] Text interractText;
    [SerializeField] Text jumpText;
    [SerializeField] Text runText;
    [SerializeField] Text shiftSpellText;
    [SerializeField] Text forwardText;
    [SerializeField] Text backText;
    [SerializeField] Text leftText;
    [SerializeField] Text rightText;
    [SerializeField] Text inventoryText;
    [SerializeField] Text escapeText;


    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;

    AudioSource chooseSound;

    public bool AutorunToggled { get { return autorunToggled; } }
    public event Action<bool> autorunWasToggled = delegate { };
    public event Action<int> SettingChanged = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        chooseSound = soundManager.FindSound("SettingElement");
        timeShiftPressed = Time.time;
        UploadPlayerPrefs();
    }

    private void OnGUI()
    {
        if (changingSettings)
        {
            if (!Event.current.isKey) return;

            if (Event.current.keyCode != KeyCode.None)
            {
                if (Event.current.type == EventType.KeyDown && !currentlyPressedKeys.Contains(Event.current.keyCode))
                {
                    currentlyPressedKeys.Add(Event.current.keyCode);
                }
                if (Event.current.type == EventType.KeyUp && currentlyPressedKeys.Count != 0)
                {
                    changingSettings = false;

                    string settedButtons = null;

                    if (currentlyPressedKeys.Count == 1)
                    {
                        settedButtons = currentlyPressedKeys[0].ToString();
                    }
                    else if (currentlyPressedKeys.Count == 2)
                    {
                        settedButtons = currentlyPressedKeys[0] + " + " + currentlyPressedKeys[1];
                    }

                    SaveSettings(settedButtons);
                    currentlyPressedKeys = new List<KeyCode>();
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shiftPressed = true;
            var timeCurrentPressed = Time.time;
            if (timeCurrentPressed - timeShiftPressed < 0.30f)
            {
                if (activeButton != null)
                {
                    activeButton.transform.Find("Text").GetComponent<Text>().text = "LeftShift + LeftShift";
                    if (SettingChanged != null) { SettingChanged(1); }
                    chooseSound.Play();
                }
                
                shiftPressed = false;
            }
            timeShiftPressed = Time.time;

        }

        if (Time.time - timeShiftPressed > 0.30f)
        {
            shiftPressed = false;
            EventSystem.current.SetSelectedGameObject(null);

        }
    }

    public void StartEditingControls(Button button)
    {
        changingSettings = true;
        activeButton = button;
    }

    public void SaveSettings(string settedButtons)
    {
        if (activeButton != null)
        {
            activeButton.transform.Find("Text").GetComponent<Text>().text = settedButtons;
            if (SettingChanged != null) { SettingChanged(1); }
            chooseSound.Play();
        }

        if (!shiftPressed)
        {
            activeButton = null;
            EventSystem.current.SetSelectedGameObject(null);
            chooseSound.Play();
        }
    }

    public void InvertMouse(Toggle invertMouseToggle)
    {
        mouseInverted = invertMouseToggle.isOn;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Mouse is inverted " + mouseInverted);
    }

    public void SetMouseSensitivity(Slider mouseSensitivitySlider)
    {
        mouseSensitivity = mouseSensitivitySlider.value;
        if (SettingChanged != null) { SettingChanged(1); }
        chooseSound.Play();
        Debug.Log("Mouse sensitivity is setted: " + mouseSensitivity);
    }

    public void ResetControls()
    {
        chooseSound.Play();
        ApplyDefaultSettings();
        Debug.Log("Controls are reseted");
    }

    public void ToggleAutorun(Toggle autorunToggle)
    {
        autorunToggled = autorunToggle.isOn;
        if (SettingChanged != null) { SettingChanged(1); }
        if (autorunWasToggled != null) { autorunWasToggled(autorunToggled); }
        chooseSound.Play();
        Debug.Log("Autorun is toggle " + mouseInverted);
    }

    public void ApplyDefaultSettings()
    {
        mouseSensitivitySlider.value = 50;
        autorunToggle.isOn = false;
        invertMouseToggle.isOn = false;
        castSpellText.text = "LMB";
        interractText.text = "E";
        jumpText.text = "SPACE";
        runText.text = "SHIFT + W";
        shiftSpellText.text = "SHIFT + SHIFT";
        forwardText.text = "W";
        backText.text = "S";
        leftText.text = "A";
        rightText.text = "D";
        inventoryText.text = "I";
        escapeText.text = "ESC";
        SaveSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("mouseSensitivity", mouseSensitivitySlider.value);
        PlayerPrefs.SetInt("autorun", autorunToggle.isOn == true ? 1 : 0);
        PlayerPrefs.SetInt("invertMouse", invertMouseToggle.isOn == true ? 1 : 0);
        PlayerPrefs.SetString("castSpellButtons", castSpellText.text);
        PlayerPrefs.SetString("interractButtons", interractText.text);
        PlayerPrefs.SetString("jumpButtons", jumpText.text);
        PlayerPrefs.SetString("runButtons", runText.text);
        PlayerPrefs.SetString("shiftSpellButtons", shiftSpellText.text);
        PlayerPrefs.SetString("forwardButtons", forwardText.text);
        PlayerPrefs.SetString("backButtons", backText.text);
        PlayerPrefs.SetString("leftButtons", leftText.text);
        PlayerPrefs.SetString("rightButtons", rightText.text);
        PlayerPrefs.SetString("inventoryButtons", inventoryText.text);
        PlayerPrefs.SetString("escapeButtons", escapeText.text);
        chooseSound.Play();
        Debug.Log("all sounds settings was saved");
    }

    void UploadPlayerPrefs()
    {
        mouseSensitivitySlider.value = PlayerPrefs.GetFloat("mouseSensitivity", 50);
        autorunToggle.isOn = PlayerPrefs.GetInt("autorun", 0) == 1 ? true : false;
        invertMouseToggle.isOn = PlayerPrefs.GetInt("invertMouse", 0) == 1 ? true : false;
        castSpellText.text = PlayerPrefs.GetString("castSpellButtons", "LMB");
        interractText.text = PlayerPrefs.GetString("interractButtons", "E");
        jumpText.text = PlayerPrefs.GetString("jumpButtons", "SPACE");
        runText.text = PlayerPrefs.GetString("runButtons", "SHIFT + W");
        shiftSpellText.text = PlayerPrefs.GetString("shiftSpellButtons", "SHIFT + SHIFT");
        forwardText.text = PlayerPrefs.GetString("forwardButtons", "W");
        backText.text = PlayerPrefs.GetString("backButtons", "S");
        leftText.text = PlayerPrefs.GetString("leftButtons", "A");
        rightText.text = PlayerPrefs.GetString("rightButtons", "D");
        inventoryText.text = PlayerPrefs.GetString("inventoryButtons", "I");
        escapeText.text = PlayerPrefs.GetString("escapeButtons", "ESC");
    }
}
