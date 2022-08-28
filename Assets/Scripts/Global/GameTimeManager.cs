using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    
    float secondsInGame;
    float minutesInGame;
    float hoursInGame;
    int restSeconds;
    int restMinutes;

    public int ProgressParameter { get { return restMinutes; } }
    public event Action<int> MinutesIngamePassed = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountOneMinute());
    }

    // Update is called once per frame
    void Update()
    {
        GetTimeIngame();

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("hours: " + hoursInGame + " minutes: " + restMinutes + " seconds: " + restSeconds);
        }
    }

    public void GetTimeIngame()
    {
        secondsInGame = Time.time;
        minutesInGame = (int)(secondsInGame / 60);
        restSeconds = (int) (secondsInGame % 60);
        restMinutes = (int) (minutesInGame % 60);
        hoursInGame = (int)(minutesInGame / 60);
    }

    IEnumerator CountOneMinute()
    {
        yield return new WaitForSeconds(60.5f);
        GetTimeIngame();
        //Debug.Log("Minute passed " + restMinutes);
        if (MinutesIngamePassed != null) { MinutesIngamePassed(restMinutes); }
        if (restMinutes < 5)
        {
            StartCoroutine(CountOneMinute());
        }
    }
}
