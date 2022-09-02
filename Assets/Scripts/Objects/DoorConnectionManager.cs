using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorConnectionManager : MonoBehaviour
{
    [SerializeField] Transform doorsPotentialPositions;
    static List<Transform> actualPositions = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform positon in doorsPotentialPositions)
        {
            actualPositions.Add(positon);
        }
    }

    public static Transform GetPosition()
    {
        if (actualPositions.Count > 0)
        {
            int positionId = Random.Range(0, actualPositions.Count);
            //Debug.Log(positionId);
            Transform newPosition = actualPositions[positionId];
            actualPositions.RemoveAt(positionId);
            return newPosition;
        }
        return null;
    }

    public static void ReturnPosition(Transform position)
    {
        //Debug.Log(actualPositions.Count);
        actualPositions.Add(position);
        //Debug.Log(actualPositions.Count);
    }
}
