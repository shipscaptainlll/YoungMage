using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameTimer : MonoBehaviour
{
    int timeIngame;

    public int TimeIngame { get { return timeIngame; } set { timeIngame = value; } }

    IEnumerator CountTimeIngame() {
        yield return new WaitForSecondsRealtime(1f);
        timeIngame++;
        Debug.Log("time in game " + timeIngame);
    }
}
