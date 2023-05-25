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

    public static string GetLocalizationName(string localizationShortName)
    {
        switch (localizationShortName)
        {
            case "Chinese (Simplified) (zh-Hans)":
                return "Simplified Chinese";
            case "Chinese (Traditional) (zh-Hant)":
                return "Traditional Chinese";
            case "Czech (cs)":
                return "Czech";
            case "English (en)":
                return "English";
            case "French (fr)":
                return "French";
            case "German (de)":
                return "German";
            case "Greek (el)":
                return "Greek";
            case "Italian (it)":
                return "Italian";
            case "Japanese (ja)":
                return "Japanese";
            case "Korean (ko)":
                return "Korean";
            case "Polish (pl)":
                return "Polish";
            case "Portuguese (Brazil) (pt-BR)":
                return "Portuguese-Brazil";
            case "Russian (ru)":
                return "Russian";
            case "Slovak (sk)":
                return "Slovak";
            case "Spanish (Spain) (es-ES)":
                return "Spanish - Spain";
            case "Thai (th)":
                return "Thai";
            case "Turkish (tr)":
                return "Turkish";
            case "Ukrainian (uk)":
                return "Ukrainian";
            default:
                return null;
        }
    }

    public static int GetLocalizationId(string localizationName)
    {
        switch (localizationName)
        {
            case "Chinese (Simplified) (zh-Hans)":
                return 1;
            case "Chinese (Traditional) (zh-Hant)":
                return 2;
            case "Czech (cs)":
                return 3;
            case "English (en)":
                return 4;
            case "French (fr)":
                return 5;
            case "German (de)":
                return 6;
            case "Greek (el)":
                return 7;
            case "Italian (it)":
                return 8;
            case "Japanese (ja)":
                return 9;
            case "Korean (ko)":
                return 10;
            case "Polish (pl)":
                return 11;
            case "Portuguese (Brazil) (pt-BR)":
                return 12;
            case "Russian (ru)":
                return 13;
            case "Slovak (sk)":
                return 14;
            case "Spanish (Spain) (es-ES)":
                return 15;
            case "Thai (th)":
                return 16;
            case "Turkish (tr)":
                return 17;
            case "Ukrainian (uk)":
                return 18;
            default:
                return 0;
        }
    }
}
