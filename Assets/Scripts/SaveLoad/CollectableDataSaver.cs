using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class CollectableDataSaver
{
    public static void SaveCollectableData(Transform collectableHolder, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);

        CollectableData collectableData = new CollectableData(collectableHolder);

        formatter.Serialize(fileStream, collectableData);
        fileStream.Close();
    }

    public static CollectableData LoadCollectableData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CollectableData collectableData = formatter.Deserialize(stream) as CollectableData;
            stream.Close();

            return collectableData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
