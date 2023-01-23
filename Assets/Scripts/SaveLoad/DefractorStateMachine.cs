using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorStateMachine : MonoBehaviour
{
    [SerializeField] RotatingCones rotatingCones;
    [SerializeField] CuttingProcess cuttingProcess;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetConesRotation()
    {
        Debug.Log("cones rotation saving");
        return rotatingCones.Rotating;
    }

    public void ApplyConesRotation()
    {
        Debug.Log("cones rotation initiated");
        cuttingProcess.StartConesRotation();
    }

    public void StopConesRotation()
    {
        cuttingProcess.StopConesRotation();
    }
}
