using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public static class LocalizationChanger
{
    private static int m_currentLocalizationID = 4;

    public static int CurrentLocalizationID
    {
        get => m_currentLocalizationID;
        set => m_currentLocalizationID = value;
    }

    public static void ApplyLocalization(int localizationId)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localizationId - 1];
        m_currentLocalizationID = localizationId;
    }

    public static string GetLocalizationName(int localizationId)
    {
        switch (localizationId)
        {
            case 1:
                return "Simplified Chinese";
            case 2:
                return "Traditional Chinese";
            case 3:
                return "Czech";
            case 4:
                return "English";
            case 5:
                return "French";
            case 6:
                return "German";
            case 7:
                return "Greek";
            case 8:
                return "Italian";
            case 9:
                return "Japanese";
            case 10:
                return "Korean";
            case 11:
                return "Polish";
            case 12:
                return "Portuguese-Brazil";
            case 13:
                return "Russian";
            case 14:
                return "Slovak";
            case 15:
                return "Spanish - Spain";
            case 16:
                return "Thai";
            case 17:
                return "Turkish";
            case 18:
                return "Ukrainian";
            default:
                return null;
        }
    }
}
