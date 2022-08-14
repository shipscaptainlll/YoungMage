using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class OreDataSaver
{
    public static void SaveOreData(Transform oresHolder, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream fileStream = new FileStream(path, FileMode.Append);

        OreData oreData = new OreData(oresHolder);

        formatter.Serialize(fileStream, oreData);
        fileStream.Close();
    }

    public static OreData LoadOreData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OreData oreData = formatter.Deserialize(stream) as OreData;
            stream.Close();

            return oreData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
