using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanel : MonoBehaviour
{
    [SerializeField] SaveSystemSerialization saveSystemSerialization;
    [SerializeField] SavePanel savePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            saveSystemSerialization.LoadProgress(1);
        }
    }

    public void LoadGame(Transform buttonTransform)
    {
        string loadText = buttonTransform.Find("Content").Find("SaveNumber").Find("Text").GetComponent<Text>().text;
        string loadNumber = Regex.Match(loadText, @"\d+").Value;
        int index = Int32.Parse(loadNumber);
        savePanel.FindSaveElement(index);
        saveSystemSerialization.LoadProgress(index);
        //Debug.Log("was loaded + " + index);
    }

    public void LoadLastGame()
    {
        savePanel.AutoLoad();
    }
}
