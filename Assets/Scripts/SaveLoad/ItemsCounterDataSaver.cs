using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class ItemsCounterDataSaver
{
    public static void SaveItemsData(Transform countersHolder, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream fileStream = new FileStream(path, FileMode.Append);

        ItemsCounterData itemsCounterData = new ItemsCounterData(countersHolder);

        formatter.Serialize(fileStream, itemsCounterData);
        fileStream.Close();
    }

    public static ItemsCounterData LoadItemsData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ItemsCounterData itemsCounterData = formatter.Deserialize(stream) as ItemsCounterData;
            stream.Close();

            return itemsCounterData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
