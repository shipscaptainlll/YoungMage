using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class CollectablesDataSaver
{
    public static void SavePlayerData(PersonMovement personMovement, CameraController cameraController, PortalOpener portalOpener, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream fileStream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(personMovement, cameraController, portalOpener);

        formatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    public static PlayerData LoadPlayerData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return playerData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
