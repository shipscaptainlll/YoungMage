using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveNameChanger : MonoBehaviour
{

    public void SaveName(string oldName, string newName)
    {
        SavesNamesData savesNamesData = SavesNamesDataSaver.LoadSavesNamesData(Application.persistentDataPath + "/SaveNames");
        SavesNamesDataSaver.SaveSavesNamesData(Application.persistentDataPath + "/SaveNames", savesNamesData, oldName, newName);
    }

    public string GetLastSaveName()
    {
        SavesNamesData savesNamesData = SavesNamesDataSaver.LoadSavesNamesData(Application.persistentDataPath + "/SaveNames");
        return savesNamesData.savesNames[savesNamesData.savesNames.Length - 1];
    }
}
