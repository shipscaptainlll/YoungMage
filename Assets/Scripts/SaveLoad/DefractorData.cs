using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DefractorData
{
    public bool conesRotating;
    public float rotationProgress;
    public bool conesSlowingDown;
    public bool circleShownPS;
    public float[][] defractorObjectsPositions;
    public float[][] defractorObjectsRotations;
    public int[] defractorObjectsIDs;
    public float[][] outletObjectsPositions;
    public float[][] outletObjectsRotations;
    public int[] outletObjectsIDs;
    public bool catchCircleShown;
    public bool catchPortalShown;
    public float catchPortalElapsed;

    public DefractorData(DefractorStateMachine defractorStateMachine)
    {
        GetActivationCircleState(defractorStateMachine);
        GetConesRotationState(defractorStateMachine);
        GetDefractoringLineParameters(defractorStateMachine);
        GetOutletLineParameters(defractorStateMachine);
        GetCatchCircleState(defractorStateMachine);
        GetCatchPortalState(defractorStateMachine);
    }

    void GetConesRotationState(DefractorStateMachine defractorStateMachine)
    {
        conesRotating = defractorStateMachine.GetConesRotation();
        conesSlowingDown = defractorStateMachine.GetConesSlowingDown();
        rotationProgress = defractorStateMachine.GetRotationProgress();
    }

    void GetActivationCircleState(DefractorStateMachine defractorStateMachine)
    {
        circleShownPS = defractorStateMachine.GetCirclePS();
    }

    void GetDefractoringLineParameters(DefractorStateMachine defractorStateMachine)
    {
        GetDefractoringLinePositions(defractorStateMachine);
        GetDefractoringLineRotations(defractorStateMachine);
        GetDefractoringLineIDs(defractorStateMachine);
    }

    void GetOutletLineParameters(DefractorStateMachine defractorStateMachine)
    {
        GetOutleLinePositions(defractorStateMachine);
        GetOutletLineRotations(defractorStateMachine);
        GetOutletLineIDs(defractorStateMachine);
    }

    void GetDefractoringLinePositions(DefractorStateMachine defractorStateMachine)
    {
        defractorObjectsPositions = new float[defractorStateMachine.DefractoringLine.childCount][];
        int indexer = 0;
        foreach (Transform element in defractorStateMachine.DefractoringLine)
        {
            float[] position = new float[3];
            position[0] = element.gameObject.transform.position.x;
            position[1] = element.gameObject.transform.position.y;
            position[2] = element.gameObject.transform.position.z;
            defractorObjectsPositions[indexer++] = position;
            Debug.Log("saved one defractoring line objects positions");
        }
    }

    void GetDefractoringLineRotations(DefractorStateMachine defractorStateMachine)
    {
        defractorObjectsRotations = new float[defractorStateMachine.DefractoringLine.childCount][];
        int indexer = 0;
        foreach (Transform element in defractorStateMachine.DefractoringLine)
        {
            float[] rotation = new float[3];
            rotation[0] = element.gameObject.transform.eulerAngles.x;
            rotation[1] = element.gameObject.transform.eulerAngles.y;
            rotation[2] = element.gameObject.transform.eulerAngles.z;
            defractorObjectsRotations[indexer++] = rotation;
            Debug.Log("saved defractoring line objects rotations");
        }
    }

    void GetDefractoringLineIDs(DefractorStateMachine defractorStateMachine)
    {
        defractorObjectsIDs = new int[defractorStateMachine.DefractoringLine.childCount];
        int indexer = 0;
        foreach (Transform element in defractorStateMachine.DefractoringLine)
        {
            defractorObjectsIDs[indexer++] = element.GetComponent<DefractorProduct>().ID;
            Debug.Log("saved one defractoring line objects ids");
        }
    }

    void GetOutleLinePositions(DefractorStateMachine defractorStateMachine)
    {
        outletObjectsPositions = new float[defractorStateMachine.OutletLine.childCount][];
        int indexer = 0;
        foreach (Transform element in defractorStateMachine.OutletLine)
        {
            float[] position = new float[3];
            position[0] = element.gameObject.transform.position.x;
            position[1] = element.gameObject.transform.position.y;
            position[2] = element.gameObject.transform.position.z;
            outletObjectsPositions[indexer++] = position;
            Debug.Log("saved one outlet line objects positions");
        }
    }

    void GetOutletLineRotations(DefractorStateMachine defractorStateMachine)
    {
        outletObjectsRotations = new float[defractorStateMachine.OutletLine.childCount][];
        int indexer = 0;
        foreach (Transform element in defractorStateMachine.OutletLine)
        {
            float[] rotation = new float[3];
            rotation[0] = element.gameObject.transform.eulerAngles.x;
            rotation[1] = element.gameObject.transform.eulerAngles.y;
            rotation[2] = element.gameObject.transform.eulerAngles.z;
            outletObjectsRotations[indexer++] = rotation;
            Debug.Log("saved outlet line objects rotations");
        }
    }

    void GetOutletLineIDs(DefractorStateMachine defractorStateMachine)
    {
        outletObjectsIDs = new int[defractorStateMachine.OutletLine.childCount];
        int indexer = 0;
        foreach (Transform element in defractorStateMachine.OutletLine)
        {
            outletObjectsIDs[indexer++] = element.GetComponent<DefractorProduct>().ID;
            Debug.Log("saved one outlet line objects ids");
        }
    }

    void GetCatchCircleState(DefractorStateMachine defractorStateMachine)
    {
        catchCircleShown = defractorStateMachine.GetCatchingCircleState();
    }

    void GetCatchPortalState(DefractorStateMachine defractorStateMachine)
    {
        catchPortalShown = defractorStateMachine.GetCatchingPortalState();
        catchPortalElapsed = defractorStateMachine.GetCatchingPortalElapsed();
    }

}
