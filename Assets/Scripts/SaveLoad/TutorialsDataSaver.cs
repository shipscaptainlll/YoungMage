using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class TutorialsDataSaver
{
    public static void SaveTutorialsData(TutorialsInstantiator tutorialsInstantiator, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);
        Debug.Log("saved tutorials data");
        TutorialsData tutorialsData = new TutorialsData(tutorialsInstantiator);

        formatter.Serialize(fileStream, tutorialsData);
        fileStream.Close();
    }

    public static TutorialsData LoadTutorialsData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            TutorialsData tutorialsData = formatter.Deserialize(stream) as TutorialsData;
            stream.Close();

            return tutorialsData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
