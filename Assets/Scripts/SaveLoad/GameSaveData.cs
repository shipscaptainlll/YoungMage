using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public int secondsInGame;
    public string timeInGame;

    public GameSaveData(Transform ingameTimeHolder)
    {
        GetTime(ingameTimeHolder);
    }

    void GetTime(Transform ingameTimeHolder)
    {
        secondsInGame = ingameTimeHolder.GetComponent<IngameTimer>().TimeIngame;
        timeInGame = ingameTimeHolder.GetComponent<IngameTimer>().GetTimeIngame();
        //Debug.Log("Saved game it was " + timeInGame);
    }
}
