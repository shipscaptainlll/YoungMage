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
    public event Action<Transform> ParallelAmuletChoosen = delegate { };
    public event Action<Transform> NoResourcesLeft = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        potentialProductAppearance.StartedAutomaticTransmutation += ControllAutomaticTransmutation;
        potentialProductAppearance.NoResourcesLeft += StopAutomaticTransmutation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ControllAutomaticTransmutation(Transform amulet)
    {
        if (amulet != null)
        {
            if (usedAmulet != amulet && usedAmulet != null)
            {
                if (ParallelAmuletChoosen != null) { ParallelAmuletChoosen(usedAmulet); }
                usedAmulet = null;
                StopAllCoroutines();
                automaticTransmutation = null;
                
            }
            Debug.Log(automaticTransmutation);
            usedAmulet = amulet;
            automaticTransmutation = StartCoroutine(TransmutateAutomaticly());
            Debug.Log("startedTransmutation");
        }
        else if (amulet == null )
        {
            usedAmulet = null;
            StopAllCoroutines();
            automaticTransmutation = null;
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

    void StopAutomaticTransmutation()
    {
        if (NoResourcesLeft != null) { NoResourcesLeft(usedAmulet); }
        usedAmulet = null;
        StopAllCoroutines();
        automaticTransmutation = null;
    }
}
