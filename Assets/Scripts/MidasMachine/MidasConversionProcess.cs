using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidasConversionProcess : MonoBehaviour
{
    [SerializeField] MidasCollectorCatcher inCollectorCatcher;
    [SerializeField] MidasCollectorCatcher outCollectorCatcher;
    [SerializeField] MidasResourcesCosts midasResourcesCosts;

    public event Action CoinTransportationAccepted = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        inCollectorCatcher.ResourceEnteredCollector += StartConversion;
        outCollectorCatcher.ResourceEnteredCollector += StartConversion;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartConversion(int resourceID)
    {
        int countOfCoins = midasResourcesCosts.GetCost(resourceID);
        StartCoroutine(PushIntoProcess(countOfCoins));
    }


    IEnumerator PushIntoProcess(int count)
    {
        int pushedCount = 0;
        while (pushedCount < count)
        {
            if (CoinTransportationAccepted != null) { CoinTransportationAccepted(); }
            pushedCount++;
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
