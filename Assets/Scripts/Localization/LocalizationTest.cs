using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        } else if (Input.GetKeyDown(KeyCode.P))
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
        } else if (Input.GetKeyDown(KeyCode.W))
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[3];
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[4];
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[5];
        } else if (Input.GetKeyDown(KeyCode.T))
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[6];
        } else if (Input.GetKeyDown(KeyCode.Y))
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[7];
        } 
    }
}
