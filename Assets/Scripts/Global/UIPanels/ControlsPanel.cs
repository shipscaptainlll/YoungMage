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
    bool mouseInverted;
    Button activeButton;
    bool shiftPressed;
    bool changingSettings;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void StartEditingControls(Button button)
    {
        changingSettings = true;
        activeButton = button;
    }

    public void SaveSettings(string settedButtons)
    {
        activeButton.transform.Find("Text").GetComponent<Text>().text = settedButtons;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void InvertMouse(Toggle invertMouseToggle)
    {
        mouseInverted = invertMouseToggle.isOn;
        Debug.Log("Mouse is inverted " + mouseInverted);
    }

    public void SetMouseSensitivity(Slider mouseSensitivitySlider)
    {
        mouseSensitivity = mouseSensitivitySlider.value;
        Debug.Log("Mouse sensitivity is setted: " + mouseSensitivity);
    }

    public void ResetControls()
    {
        Debug.Log("Controls are reseted");
    }

    // Update is called once per frame
    void Update()
    {                           
        
    }
}
