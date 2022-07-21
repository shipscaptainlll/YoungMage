using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlePositionsManager : MonoBehaviour
{
    List<Transform> castlePotentialPositions = new List<Transform>();

    public List<Transform> CastlePotentialPositions
    {
        get
        {
            return castlePotentialPositions;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform potentialPosition in transform)
        {
            castlePotentialPositions.Add(potentialPosition);
        }
    }

    public Transform GetAvailablePosition()
    {
        Transform returnedPosition = castlePotentialPositions[0];
        
        castlePotentialPositions.Remove(returnedPosition);
        return returnedPosition;

    }
}
