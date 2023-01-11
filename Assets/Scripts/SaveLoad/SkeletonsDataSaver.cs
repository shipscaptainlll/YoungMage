using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SkeletonsDataSaver
{
    public static void SaveSkeletonData(Transform skeletonsHolder, Transform oresHolder, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);

        SkeletonData skeletonData = new SkeletonData(skeletonsHolder, oresHolder);

        formatter.Serialize(fileStream, skeletonData);
        fileStream.Close();
    }

    public static SkeletonData LoadSkeletonData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SkeletonData skeletonData = formatter.Deserialize(stream) as SkeletonData;
            stream.Close();

            return skeletonData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
