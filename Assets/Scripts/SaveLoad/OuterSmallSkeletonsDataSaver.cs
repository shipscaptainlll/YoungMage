using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class OuterSmallSkeletonsDataSaver
{
    public static void SaveSkeletonData(Transform skeletonsHolder, SkeletonArenaInstantiator skeletonArenaInstantiator, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);

        OuterSmallSkeletonData outerSmallSkeletonData = new OuterSmallSkeletonData(skeletonsHolder, skeletonArenaInstantiator);

        formatter.Serialize(fileStream, outerSmallSkeletonData);
        fileStream.Close();
    }

    public static OuterSmallSkeletonData LoadSkeletonData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OuterSmallSkeletonData outerSmallSkeletonData = formatter.Deserialize(stream) as OuterSmallSkeletonData;
            stream.Close();

            return outerSmallSkeletonData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
