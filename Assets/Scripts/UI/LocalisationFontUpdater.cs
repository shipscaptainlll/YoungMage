using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalisationFontUpdater : MonoBehaviour
{
    [SerializeField] Font defaultFont;
    [SerializeField] Font reserveFont;
    

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    public void UpdateText(Text textComponent)
    {
        if (LocalizationSettings.SelectedLocale.name == "Russian (ru)")
        {
            Debug.Log("current localization is Russian font is " + reserveFont);
            textComponent.font = reserveFont;
            
        } else
        {
            Debug.Log("current localization is Other font is " + defaultFont);
            textComponent.font = defaultFont;
        }
        
    }
}
