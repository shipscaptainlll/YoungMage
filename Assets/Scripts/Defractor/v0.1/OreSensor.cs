using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreSensor : MonoBehaviour
{
    public event Action OreContactedLearningTutorial = delegate { };
    public event Action<int> OreContacted = delegate { };
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DefractorResource>() != null)
        {
            Debug.Log("contacted " + other.transform);
            if (OreContactedLearningTutorial != null) { OreContactedLearningTutorial(); }
            if (OreContacted != null) { OreContacted(other.GetComponent<DefractorResource>().ID); }
        }
    }
}
