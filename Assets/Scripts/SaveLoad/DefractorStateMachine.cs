using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorStateMachine : MonoBehaviour
{
    [SerializeField] RotatingCones rotatingCones;
    [SerializeField] CuttingProcess cuttingProcess;
    [SerializeField] DefractorPipeSystem defractorPipeSystem;
    [SerializeField] Transform defractoringLine;
    [SerializeField] Transform outletLine;
    [SerializeField] DefractorPortalInstantiator defractorPortalInstantiator;
    [SerializeField] DefractorPortalOpener defractorPortalOpener;
    [SerializeField] AppearanceTransmutationCircle appearanceTransmutationCircle;

    public Transform DefractoringLine { get { return defractoringLine; } }
    public Transform OutletLine { get { return outletLine; } }

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
        //Debug.Log("cones rotation saving");
        return rotatingCones.Rotating;
    }

    public float GetRotationProgress()
    {
        //Debug.Log("elapsed was " + rotatingCones.Elapsed);
        return rotatingCones.Elapsed;
    }

    public bool GetConesSlowingDown()
    {
        //Debug.Log("cones slowing down saving");
        return rotatingCones.SlowingDown;
    }

    public void ApplyConesRotation(float delay)
    {
        Debug.Log("cones rotation initiated");
        cuttingProcess.StartRotationDelayed(delay);
    }

    public void ApplyConesSlowingDown(float delay)
    {
        Debug.Log("cones slowing down initiated");
        cuttingProcess.StartConesSlowingDown(delay);
    }

    public void ApplyDefractoringObjects(DefractorData defractorData)
    {
        int indexer = 0;
        while (indexer < defractorData.defractorObjectsPositions.Length)
        {
            Vector3 objectPosition = new Vector3(defractorData.defractorObjectsPositions[indexer][0], defractorData.defractorObjectsPositions[indexer][1], defractorData.defractorObjectsPositions[indexer][2]);
            Vector3 objectRotation = new Vector3(defractorData.defractorObjectsRotations[indexer][0], defractorData.defractorObjectsRotations[indexer][1], defractorData.defractorObjectsRotations[indexer][2]);
            defractorPipeSystem.TransferUploadedObjects(defractorData.defractorObjectsIDs[indexer], objectPosition, Quaternion.Euler(objectRotation));
            indexer++;
        }
    }

    public void ApplyOutletObjects(DefractorData defractorData)
    {
        int indexer = 0;
        while (indexer < defractorData.outletObjectsPositions.Length)
        {
            Vector3 objectPosition = new Vector3(defractorData.outletObjectsPositions[indexer][0], defractorData.outletObjectsPositions[indexer][1], defractorData.outletObjectsPositions[indexer][2]);
            Vector3 objectRotation = new Vector3(defractorData.outletObjectsRotations[indexer][0], defractorData.outletObjectsRotations[indexer][1], defractorData.outletObjectsRotations[indexer][2]);
            defractorPipeSystem.TransferUploadedOutletObjects(defractorData.defractorObjectsIDs[indexer], objectPosition, Quaternion.Euler(objectRotation));
            indexer++;
        }
    }
    
    public void StopConesRotation()
    {
        cuttingProcess.StopConesRotation();
    }

    public bool GetCirclePS()
    {
        Debug.Log("circle was shown " + rotatingCones.CircleShown);
        return rotatingCones.CircleShown;
    }

    public void StartCirclePS()
    {
        rotatingCones.ImmediateActivateRotationPS();
    }

    public void StopCirclePS()
    {
        rotatingCones.ImmediateDeactivateRotationPS();
    }

    public bool GetCatchingCircleState()
    {
        return appearanceTransmutationCircle.CircleShown;
    }

    public void ShowCatchingCircle()
    {
        appearanceTransmutationCircle.ImmediateCircleDisappearance();
        appearanceTransmutationCircle.CircleAppearance();
    }

    public void HideCatchingCircle()
    {
        appearanceTransmutationCircle.ImmediateCircleDisappearance();
    }

    public bool GetCatchingPortalState()
    {
        Debug.Log("portal is currently on " + defractorPortalInstantiator.PortalShown);
        return defractorPortalInstantiator.PortalShown;
    }

    public float GetCatchingPortalElapsed()
    {
        return defractorPortalOpener.PortalElapsed;
    }

    public void OpenCatchingPortal(float uploadedElapsed)
    {
        
        defractorPortalInstantiator.InstantiatePortal();
    }

    public void CloseCatchingPortal(float uploadedElapsed)
    {
        defractorPortalInstantiator.UploadedClosePortal();
    }

    public void ClearDefractorObjectsState()
    {
        defractorPipeSystem.ClearDefractorObjects();
    }
}
