using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GameDataSaver
{
    public static void SaveGameData(Transform ingameTimeHolder, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);

        GameSaveData gameSaveData = new GameSaveData(ingameTimeHolder);

        formatter.Serialize(fileStream, gameSaveData);
        fileStream.Close();
    }

    public static GameSaveData LoadGameData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameSaveData gameSaveData = formatter.Deserialize(stream) as GameSaveData;
            stream.Close();

            return gameSaveData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
