using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsConnector : MonoBehaviour
{
    List<Tuple<Transform, Transform>> OresList = new List<Tuple<Transform, Transform>>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubscribeOnOre(Transform newOre)
    {
        if (newOre.GetComponent<ConnectableResource>() == null)
        {
            Debug.Log("CARE doesn´t have ConnectableResource component attached");
            return;
        }
        newOre.GetComponent<ConnectableResource>().ContactedResource += ConnectTwoOres;
    }

    void ConnectTwoOres(Transform firstOre, Transform secondOre)
    {
        Debug.Log("entering " + firstOre + " " + secondOre);
        //Debug.Log(firstOre.GetComponent<OreCounter>().OreCount);
        //Debug.Log(secondOre.GetComponent<OreCounter>().OreCount);
        if (CheckObjectsEnabled(firstOre, secondOre) && CheckSameMaterial(firstOre, secondOre))
        {
            
            Transform updatedOre = GetBiggestOre(firstOre, secondOre);
            Transform destroyedOre = GetDestroyedOre(firstOre, secondOre, updatedOre);
            UpdateOreCounter(updatedOre, destroyedOre);
            if (destroyedOre == firstOre) { DestroyOre(firstOre, updatedOre); }
            else { DestroyOre(secondOre, updatedOre); }
            
        }
    }

    bool CheckObjectsEnabled(Transform firstOre, Transform secondOre)
    {
        if (firstOre.GetComponent<ConnectableResource>().Enabled && secondOre.GetComponent<ConnectableResource>().Enabled)
        {
            return true;
        }
        else { return false; }
    }

    bool CheckSameMaterial(Transform firstOre, Transform secondOre)
    {
        if (firstOre.GetComponent<GlobalResource>().ID == secondOre.GetComponent<GlobalResource>().ID)
        {
            return true;
        }
        else { return false; }
    }

    void UpdateOreCounter(Transform updatedOre, Transform destroyedOre)
    {
        int destroyedCount = destroyedOre.GetComponent<OreCounter>().OreCount;
        if (!updatedOre.GetComponent<OreCounter>().CounterOn) { updatedOre.GetComponent<OreCounter>().ShowCounter(); }
        //Debug.Log(destroyedCount);
        //Debug.Log(updatedOre.GetComponent<OreCounter>().OreCount);
        updatedOre.GetComponent<OreCounter>().OreCount += destroyedCount;
        //Debug.Log(updatedOre.GetComponent<OreCounter>().OreCount);
    }

    Transform GetBiggestOre(Transform firstOre, Transform secondOre)
    {
        if (firstOre.GetComponent<OreCounter>().OreCount > secondOre.GetComponent<OreCounter>().OreCount) { return firstOre; }
        else { return secondOre; }
    }

    Transform GetDestroyedOre(Transform firstOre, Transform secondOre, Transform updatedOre)
    {
        if (firstOre == updatedOre) { return secondOre; }
        else { return firstOre; }
    }

    void DestroyOre(Transform destroyedOre, Transform connectedOre)
    {

        destroyedOre.GetComponent<ConnectableResource>().ContactedResource -= ConnectTwoOres;
        destroyedOre.GetComponent<ConnectableResource>().TargetConnection = connectedOre;
        destroyedOre.GetComponent<ConnectableResource>().Enabled = false;

        Debug.Log("Destroyed");
    }
}
