using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SavesNamesData
{
    public string[] savesNames;

    public SavesNamesData(SavesNamesData oldSavesNamesData, string oldName, string newName)
    {
        SaveNames(oldSavesNamesData, oldName, newName);
    }

    void SaveNames(SavesNamesData oldSavesNamesData, string oldName, string newName)
    {
        string path = Application.persistentDataPath + "/Saves";

        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
        
        var result = dir.GetDirectories().OrderBy(t => t.LastWriteTime).ToList();

        savesNames = new string[result.Count];
        int indexer = 0;

        if (oldSavesNamesData != null)
        {
            foreach (var element in oldSavesNamesData.savesNames)
            {
                this.savesNames[indexer] = element;
                indexer++;
                if (this.savesNames[indexer] == oldName)
                {
                    this.savesNames[indexer] = newName;
                }
            }
        }
        

        if (indexer != result.Count)
        {
            this.savesNames[indexer] = newName;
        }
    }

}
