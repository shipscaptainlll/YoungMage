using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class OuterBigSkeletonsDataSaver
{
    public static void SaveSkeletonData(Transform skeletonsHolder, CatapultArenaInstantiator catapultArenaInstantiator, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);

        OuterBigSkeletonData outerBigSkeletonData = new OuterBigSkeletonData(skeletonsHolder, catapultArenaInstantiator);

        formatter.Serialize(fileStream, outerBigSkeletonData);
        fileStream.Close();
    }

    public static OuterBigSkeletonData LoadSkeletonData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OuterBigSkeletonData outerBigSkeletonData = formatter.Deserialize(stream) as OuterBigSkeletonData;
            stream.Close();

            return outerBigSkeletonData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
