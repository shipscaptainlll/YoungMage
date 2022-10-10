using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlePositionsManager : MonoBehaviour
{
    public List<Transform> castlePotentialPositions = new List<Transform>();
    public List<Transform> castleOccupiedPositions = new List<Transform>();

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
        
        castleOccupiedPositions.Add(returnedPosition);
        castlePotentialPositions.Remove(returnedPosition);
        Debug.Log(castlePotentialPositions.Count);
        return returnedPosition;

    }

    public void RegeneratePositions(Transform returnedPosition)
    {
        returnedPosition.gameObject.SetActive(true);
        castlePotentialPositions.Add(returnedPosition);
    }
}
