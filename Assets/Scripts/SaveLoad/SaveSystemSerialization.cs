using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystemSerialization : MonoBehaviour
{
    [SerializeField] PersonMovement mainCharacterScript;
    [SerializeField] CameraController mainCharacterCamera;
    [SerializeField] Transform oresHolder;
    [SerializeField] Transform countersHolder;

    public void SaveProgress()
    {
        string path = GetPath();
        PlayerDataSaver.SavePlayerData(mainCharacterScript, mainCharacterCamera, path);
        OreDataSaver.SaveOreData(oresHolder, path);
        ItemsCounterDataSaver.SaveItemsData(countersHolder, path);
        Debug.Log("game was saved");
    }

    public void LoadProgress(int saveId)
    {
        if (saveId == 1)
        {
            string path = GetPath();
            PlayerDataApplier.ApplyPlayerData(mainCharacterScript, mainCharacterCamera, PlayerDataSaver.LoadPlayerData(path));
            OreDataApplier.ApplyOreData(oresHolder, OreDataSaver.LoadOreData(path));
            ItemsCounterDataApplier.ApplyCountersData(countersHolder, ItemsCounterDataSaver.LoadItemsData(path));
            Debug.Log("game was loaded");
        }
    }

    string GetPath()
    {
        string path = Application.persistentDataPath + "/player.fun";
        return path;
    }
}
