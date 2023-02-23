using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class CityUpgradeDataSaver
{
    public static void SaveCityUpgradeData(CityUpgradeStateMachine cityUpgradeStateMachine, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        FileStream fileStream = new FileStream(path, FileMode.Create);
        Debug.Log("saved city upgrade");
        CityUpgradeData cityUpgradeData = new CityUpgradeData(cityUpgradeStateMachine);

        formatter.Serialize(fileStream, cityUpgradeData);
        fileStream.Close();
    }

    public static CityUpgradeData LoadCityUpgradeData(string path)
    {

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CityUpgradeData cityUpgradeData = formatter.Deserialize(stream) as CityUpgradeData;
            stream.Close();

            return cityUpgradeData;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
