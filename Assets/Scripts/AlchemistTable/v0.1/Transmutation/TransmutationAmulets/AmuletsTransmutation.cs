using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletsTransmutation : MonoBehaviour
{
    [SerializeField] PotentialProductAppearance potentialProductAppearance;
    Coroutine automaticTransmutation;
    Transform usedAmulet;

    public event Action<Transform>  AutomaticTransmutationContinue = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        potentialProductAppearance.StartedAutomaticTransmutation += ControllAutomaticTransmutation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ControllAutomaticTransmutation(Transform amulet)
    {
        if (amulet != null)
        {
            usedAmulet = amulet;
            automaticTransmutation = StartCoroutine(TransmutateAutomaticly());
            Debug.Log("startedTransmutation");
        }
        else if (amulet == null)
        {
            usedAmulet = null;
            StopCoroutine(automaticTransmutation);
            Debug.Log("stopedTransmutation");
        }
    }

    IEnumerator TransmutateAutomaticly()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            Debug.Log(usedAmulet);
            AutomaticTransmutationContinue(usedAmulet);
            Debug.Log("transmutated");
        }
        
    }
}
