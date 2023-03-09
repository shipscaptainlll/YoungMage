using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SavesNamesDataSaver
{
    public static void SaveSavesNamesData(string path, SavesNamesData loadedSavesNamesData, string oldName, string newName)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);

        SavesNamesData savesNamesData = new SavesNamesData(loadedSavesNamesData, oldName, newName);

        formatter.Serialize(fileStream, savesNamesData);
        fileStream.Close();
    }

    public static SavesNamesData LoadSavesNamesData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream.Length == 0)
            {
                return null;
            }
            SavesNamesData savesNamesData = formatter.Deserialize(stream) as SavesNamesData;
            stream.Close();

            return savesNamesData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
