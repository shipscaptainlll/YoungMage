using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MidasData
{
    public bool circleShownPS;
    public float[][] inletObjectsPositions;
    public float[][] inletObjectsRotations;
    public int[] inletObjectsIDs;
    public float[][] pipeMaterialsPositions;
    public float[][] pipeMaterialsRotations;
    public float[] pipeMaterialsSeconds;
    public float[][] pipeCoinsPositions;
    public float[][] pipeCoinsRotations;
    public float[] pipeCoinsSeconds;
    public float[] pipeCoinsElapsed;
    public int coinsAmmount;


    public MidasData(MidasStateMachine midasStateMachine)
    {
        GetActivationCircleState(midasStateMachine);
        GetInletLineParameters(midasStateMachine);
        GetMaterialsParameters(midasStateMachine);
        GetCoinsParameters(midasStateMachine);
        GetCoinsAmmount(midasStateMachine);
    }

    void GetActivationCircleState(MidasStateMachine midasStateMachine)
    {
        circleShownPS = midasStateMachine.GetCircleState();
    }

    void GetInletLineParameters(MidasStateMachine midasStateMachine)
    {
        GetInletLinePositions(midasStateMachine);
        GetInletLineRotations(midasStateMachine);
        GetInletLineIDs(midasStateMachine);
    }

    void GetMaterialsParameters(MidasStateMachine midasStateMachine)
    {
        GetMaterialsPositions(midasStateMachine);
        GetMaterialsRotations(midasStateMachine);
        GetMaterialsSeconds(midasStateMachine);
    }

    void GetCoinsParameters(MidasStateMachine midasStateMachine)
    {
        GetCoinsPositions(midasStateMachine);
        GetCoinsRotations(midasStateMachine);
        GetCoinsSeconds(midasStateMachine);
        GetCoinsElapsed(midasStateMachine);
    }

    void GetInletLinePositions(MidasStateMachine midasStateMachine)
    {
        inletObjectsPositions = new float[midasStateMachine.InPool.childCount][];
        int indexer = 0;
        foreach (Transform element in midasStateMachine.InPool)
        {
            float[] position = new float[3];
            position[0] = element.gameObject.transform.position.x;
            position[1] = element.gameObject.transform.position.y;
            position[2] = element.gameObject.transform.position.z;
            inletObjectsPositions[indexer++] = position;
            Debug.Log("saved one inlet line objects positions");
        }
    }

    void GetInletLineRotations(MidasStateMachine midasStateMachine)
    {
        inletObjectsRotations = new float[midasStateMachine.InPool.childCount][];
        int indexer = 0;
        foreach (Transform element in midasStateMachine.InPool)
        {
            float[] rotation = new float[3];
            rotation[0] = element.gameObject.transform.eulerAngles.x;
            rotation[1] = element.gameObject.transform.eulerAngles.y;
            rotation[2] = element.gameObject.transform.eulerAngles.z;
            inletObjectsRotations[indexer++] = rotation;
            Debug.Log("saved inlet objects rotations");
        }
    }

    void GetInletLineIDs(MidasStateMachine midasStateMachine)
    {
        inletObjectsIDs = new int[midasStateMachine.InPool.childCount];
        int indexer = 0;
        foreach (Transform element in midasStateMachine.InPool)
        {
            inletObjectsIDs[indexer++] = element.GetComponent<GlobalResource>().ID;
            Debug.Log("saved inlet line objects ids");
        }
    }

    void GetMaterialsPositions(MidasStateMachine midasStateMachine)
    {
        pipeMaterialsPositions = new float[midasStateMachine.MaterialsPool.childCount][];
        int indexer = 0;
        foreach (Transform element in midasStateMachine.MaterialsPool)
        {
            float[] position = new float[3];
            position[0] = element.gameObject.transform.position.x;
            position[1] = element.gameObject.transform.position.y;
            position[2] = element.gameObject.transform.position.z;
            pipeMaterialsPositions[indexer++] = position;
            Debug.Log("saved pipe material position");
        }
    }

    void GetMaterialsRotations(MidasStateMachine midasStateMachine)
    {
        pipeMaterialsRotations = new float[midasStateMachine.MaterialsPool.childCount][];
        int indexer = 0;
        foreach (Transform element in midasStateMachine.MaterialsPool)
        {
            float[] rotation = new float[3];
            rotation[0] = element.gameObject.transform.eulerAngles.x;
            rotation[1] = element.gameObject.transform.eulerAngles.y;
            rotation[2] = element.gameObject.transform.eulerAngles.z;
            pipeMaterialsRotations[indexer++] = rotation;
            Debug.Log("saved pipe material rotation");
        }
    }

    void GetMaterialsSeconds(MidasStateMachine midasStateMachine)
    {
        pipeMaterialsSeconds = new float[midasStateMachine.MaterialsPool.childCount];
        int indexer = 0;
        foreach (Transform element in midasStateMachine.MaterialsPool)
        {
            pipeMaterialsSeconds[indexer++] = element.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
            Debug.Log("saved pipe material second");
        }
    }

    void GetCoinsPositions(MidasStateMachine midasStateMachine)
    {
        pipeCoinsPositions = new float[midasStateMachine.CoinsPool.childCount][];
        int indexer = 0;
        foreach (Transform element in midasStateMachine.CoinsPool)
        {
            float[] position = new float[3];
            position[0] = element.gameObject.transform.position.x;
            position[1] = element.gameObject.transform.position.y;
            position[2] = element.gameObject.transform.position.z;
            pipeCoinsPositions[indexer++] = position;
            Debug.Log("saved coin material position");
        }
    }

    void GetCoinsRotations(MidasStateMachine midasStateMachine)
    {
        pipeCoinsRotations = new float[midasStateMachine.CoinsPool.childCount][];
        int indexer = 0;
        foreach (Transform element in midasStateMachine.CoinsPool)
        {
            float[] rotation = new float[3];
            rotation[0] = element.gameObject.transform.eulerAngles.x;
            rotation[1] = element.gameObject.transform.eulerAngles.y;
            rotation[2] = element.gameObject.transform.eulerAngles.z;
            pipeCoinsRotations[indexer++] = rotation;
            Debug.Log("saved coin material rotation");
        }
    }

    void GetCoinsSeconds(MidasStateMachine midasStateMachine)
    {
        pipeCoinsSeconds = new float[midasStateMachine.CoinsPool.childCount];
        int indexer = 0;
        foreach (Transform element in midasStateMachine.CoinsPool)
        {
            pipeCoinsSeconds[indexer++] = element.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
            Debug.Log("saved coin material second");
        }
    }

    void GetCoinsElapsed(MidasStateMachine midasStateMachine)
    {
        pipeCoinsSeconds = new float[midasStateMachine.CoinsPool.childCount];
        int indexer = 0;
        foreach (Transform element in midasStateMachine.CoinsPool)
        {
            pipeCoinsSeconds[indexer++] = element.GetComponent<MeshRenderer>().material.GetFloat("_Clip");
            Debug.Log("saved coin material second");
        }
    }

    void GetCoinsAmmount(MidasStateMachine midasStateMachine)
    {
        coinsAmmount = midasStateMachine.GetCoinsCount();
    }

}
