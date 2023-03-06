using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class OuterLizardSkeletonsDataSaver
{
    public static void SaveSkeletonData(Transform skeletonsHolder, CrossbowCatapultArenaInstantiator crossbowCatapultArenaInstantiator, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);

        OuterLizardSkeletonData outerLizardSkeletonData = new OuterLizardSkeletonData(skeletonsHolder, crossbowCatapultArenaInstantiator);

        formatter.Serialize(fileStream, outerLizardSkeletonData);
        fileStream.Close();
    }

    public static OuterLizardSkeletonData LoadSkeletonData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            OuterLizardSkeletonData outerLizardSkeletonData = formatter.Deserialize(stream) as OuterLizardSkeletonData;
            stream.Close();

            return outerLizardSkeletonData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
