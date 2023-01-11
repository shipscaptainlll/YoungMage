using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDataApplier
{
    static Transform timeHolder;
    static GameSaveData gameDataLoaded;

    public static void ApplyGameData(Transform timeHolder, GameSaveData gameSaveData)
    {
        UpdateData(timeHolder, gameSaveData);
        UpdateGameData(timeHolder, gameSaveData);
        DisconnectData();
    }

    static void UpdateData(Transform timeHolder, GameSaveData gameSaveData)
    {
        timeHolder = timeHolder;
        gameDataLoaded = gameSaveData;
    }

    static void DisconnectData()
    {
        timeHolder = null;
        gameDataLoaded = null;
    }

    static void UpdateGameData(Transform timeHolder, GameSaveData gameSaveData)
    {
        timeHolder.GetComponent<IngameTimer>().TimeIngame = gameSaveData.timeInGame;
        Debug.Log("time in game " + gameSaveData.timeInGame);
    }
}
