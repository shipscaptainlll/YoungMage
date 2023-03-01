using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorConnectionManager : MonoBehaviour
{
    [SerializeField] Transform doorsPotentialPositions;
    List<Transform> actualPositions = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform positon in doorsPotentialPositions)
        {
            actualPositions.Add(positon);
        }
    }

    public Transform GetPosition()
    {
        Debug.Log("was connected to doors number " + transform.name);
        Debug.Log("number of left positions " + actualPositions.Count);
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

    public void ResetPositions()
    {
        actualPositions = new List<Transform>();
        foreach (Transform positon in doorsPotentialPositions)
        {
            actualPositions.Add(positon);
        }
    }

    public void ReturnPosition(Transform position)
    {
        //Debug.Log(actualPositions.Count);
        actualPositions.Add(position);
        //Debug.Log(actualPositions.Count);
    }
}
