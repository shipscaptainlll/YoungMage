using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPateronLink()
    {
        Application.OpenURL("https://www.patreon.com/shipscaptain1234/");
    }

    public void OpenInstagramLink()
    {
        Application.OpenURL("https://www.instagram.com/shipscaptain1234/");
    }
}
