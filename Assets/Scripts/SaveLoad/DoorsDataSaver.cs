using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DoorsDataSaver
{
    public static void SaveDoorsData(DoorsStateMachine doorsStateMachine, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);
        Debug.Log("saved doors state");
        DoorsData doorsData = new DoorsData(doorsStateMachine);

        formatter.Serialize(fileStream, doorsData);
        fileStream.Close();
    }

    public static DoorsData LoadDoorsData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DoorsData doorsData = formatter.Deserialize(stream) as DoorsData;
            stream.Close();

            return doorsData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
