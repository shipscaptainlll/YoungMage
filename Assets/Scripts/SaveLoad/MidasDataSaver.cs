using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class MidasDataSaver
{
    public static void SaveMidasData(MidasStateMachine midasStateMachine, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);
        Debug.Log("saved mida");
        MidasData midasData = new MidasData(midasStateMachine);

        formatter.Serialize(fileStream, midasData);
        fileStream.Close();
    }

    public static MidasData LoadMidasData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MidasData midasData = formatter.Deserialize(stream) as MidasData;
            stream.Close();

            return midasData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
