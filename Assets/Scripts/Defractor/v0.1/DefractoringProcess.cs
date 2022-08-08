using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractoringProcess : MonoBehaviour
{
    [SerializeField] OreSensor oreSensor;
    [SerializeField] CuttingProcess cuttingProcess;
    [SerializeField] DefractorPipeSystem defractorPipeSystem;

    public event Action<int> SentInPipes = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        oreSensor.OreContacted += Start;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Start(int contactedOreId)
    {
        Debug.Log("started cones rotation");
        StartCoroutine(StartDefractoring(contactedOreId));
    }

    IEnumerator StartDefractoring(int contactedOreId)
    {
        Debug.Log("started cones rotation1");
        cuttingProcess.StartConesRotation();
        yield return new WaitForSeconds(1f);
        if (SentInPipes != null) { SentInPipes(contactedOreId); }
        yield return null;
    }
}
