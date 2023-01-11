using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public int timeInGame;

    public GameSaveData(Transform ingameTimeHolder)
    {
        GetTime(ingameTimeHolder);
    }

    void GetTime(Transform ingameTimeHolder)
    {
        timeInGame = ingameTimeHolder.GetComponent<IngameTimer>().TimeIngame;
    }
}
