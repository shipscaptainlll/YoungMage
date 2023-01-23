using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class IngameTimer : MonoBehaviour
{
    int timeIngame;

    public int TimeIngame { get { return timeIngame; } set { timeIngame = value; } }

    private void Start()
    {
        StartCoroutine(CountTimeIngame());
    }

    IEnumerator CountTimeIngame() {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            timeIngame++;
            //Debug.Log("time in game " + timeIngame);
        }
    }

    public string GetTimeIngame()
    {
        StringBuilder timeCache = new StringBuilder();

        timeCache.Append(GetHours());
        timeCache.Append(":");
        timeCache.Append(GetMinutes());
        timeCache.Append(":");
        timeCache.Append(GetSeconds());


        return timeCache.ToString();
    }

    string GetHours()
    {
        int cacheHours = (int)(timeIngame / 3600);
        string stringHours;
        if (cacheHours < 100)
        {
            stringHours = "0" + cacheHours.ToString();
        }
        else if (cacheHours < 10)
        {
            stringHours = "00" + cacheHours.ToString();
        }
        else
        {
            stringHours = cacheHours.ToString();
        }

        return stringHours;
    }

    string GetMinutes()
    {
        int cacheMinutes = (int)(timeIngame / 60);
        string stringMinutes;
        if (cacheMinutes < 10)
        {
            stringMinutes = "0" + cacheMinutes.ToString();
        }
        else
        {
            stringMinutes = cacheMinutes.ToString();
        }
        return stringMinutes;
    }

    string GetSeconds()
    {
        int cacheSeconds = (int)(timeIngame % 60);
        string stringSeconds;
        if (cacheSeconds < 10)
        {
            stringSeconds = "0" + cacheSeconds.ToString();
        }
        else
        {
            stringSeconds = cacheSeconds.ToString();
        }
        return stringSeconds;
    }
}
