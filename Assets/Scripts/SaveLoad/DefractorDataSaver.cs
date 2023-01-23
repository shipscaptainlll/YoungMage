using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class DefractorDataSaver
{
    public static void SaveDefractorData(DefractorStateMachine defractorStateMachine, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);

        DefractorData defractorData = new DefractorData(defractorStateMachine);

        formatter.Serialize(fileStream, defractorData);
        fileStream.Close();
    }

    public static DefractorData LoadDefractorData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DefractorData defractorData = formatter.Deserialize(stream) as DefractorData;
            stream.Close();

            return defractorData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
