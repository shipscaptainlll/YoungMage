using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class InventoryDataSaver
{
    public static void SaveInventoryData(Transform mainInventoryHolder, Transform innerQuickaccessHolder, Transform outerQuickaccessHolder, QuickAccessHandController quickAccessHandController, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Append);

        InventoryData inventoryData = new InventoryData(mainInventoryHolder, innerQuickaccessHolder, outerQuickaccessHolder, quickAccessHandController);

        formatter.Serialize(fileStream, inventoryData);
        fileStream.Close();
    }

    public static InventoryData LoadInventoryData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryData inventoryData = formatter.Deserialize(stream) as InventoryData;
            stream.Close();

            return inventoryData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
