using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingProcess : MonoBehaviour
{
    [SerializeField] RotatingCones rotatingConeFirst;
    [SerializeField] RotatingCones rotatingConeSecond;

    public event Action RotationStarted = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartConesRotation()
    {
        //Debug.Log("hello 1");
        if (RotationStarted != null) { RotationStarted(); }
    }

    public void StartRotationDelayed(float delay)
    {
        rotatingConeFirst.StartRotation(delay);
        rotatingConeSecond.StartRotation(delay);
    }

    public void StartConesSlowingDown(float delay)
    {
        rotatingConeFirst.StopRotation(delay);
        rotatingConeSecond.StopRotation(delay);
    }

    public void StopConesRotation()
    {
        Debug.Log("cones rotation immediately stopped");
        rotatingConeFirst.ImmediateStopRotation();
        rotatingConeSecond.ImmediateStopRotation();
    }
}
