using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidasConversionProcess : MonoBehaviour
{
    [SerializeField] MidasCollectorCatcher inCollectorCatcher;
    [SerializeField] MidasCollectorCatcher outCollectorCatcher;
    [SerializeField] MidasResourcesCosts midasResourcesCosts;
    [SerializeField] ParticleSystem transformationPS;
    [SerializeField] AppearanceTransmutationCircle appearanceTransmutationCircle;
    
    Coroutine delayPSCoroutine;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    [SerializeField] Transform waterfallTransform;
    AudioSource waterFallSound;
    AudioSource conjurationStartSound;

    public event Action CoinTransportationAccepted = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        inCollectorCatcher.ResourceEnteredCollector += StartConversion;
        outCollectorCatcher.ResourceEnteredCollector += StartConversion;
        waterFallSound = soundManager.LocateAudioSource("WaterfallOnMetal", waterfallTransform);
        conjurationStartSound = soundManager.LocateAudioSource("ConjurationCircleAppear", transformationPS.transform);
        waterFallSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartConversion(int resourceID)
    {
        ManageTransformationPS();
        
        Debug.Log("Resource entered collector " + resourceID);
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

    void ManageTransformationPS()
    {
        if (delayPSCoroutine != null) { StopCoroutine(delayPSCoroutine); delayPSCoroutine = StartCoroutine(delayPS()); }
        else
        {
            conjurationStartSound.Play();
            delayPSCoroutine = StartCoroutine(delayPS());
            appearanceTransmutationCircle.CircleAppearance();
            //transformationPS.gameObject.SetActive(true);
            //transformationPS.Play();
            
        }
    }

    IEnumerator delayPS()
    {
        yield return new WaitForSeconds(17f);
        appearanceTransmutationCircle.CircleDisappearance();
        //transformationPS.Stop();
        //transformationPS.gameObject.SetActive(false);
        delayPSCoroutine = null;
    }
}
