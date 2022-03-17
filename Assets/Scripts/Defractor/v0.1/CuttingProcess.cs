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
}
