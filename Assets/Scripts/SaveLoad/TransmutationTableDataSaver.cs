using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class transmutationTableDataSaver
{
    public static void SaveTransmutationTableData(TransmutationTableStateMachine transmutationTableStateMachine, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);

        TransmutationTableData transmutationTableData = new TransmutationTableData(transmutationTableStateMachine);

        formatter.Serialize(fileStream, transmutationTableData);
        fileStream.Close();
    }

    public static TransmutationTableData LoadTransmutationTableData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            TransmutationTableData transmutationTableData = formatter.Deserialize(stream) as TransmutationTableData;
            stream.Close();

            return transmutationTableData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
