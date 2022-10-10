using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldiersInstantiator : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] Transform prefabsHolder;
    [SerializeField] Transform potentialPositionsHolder;
    [SerializeField] Transform targetRotation;
    [SerializeField] Transform instantiatedSoldiersHolder;
    List<Transform> potentialSoldiers = new List<Transform>();
    List<Transform> instantiatedSoldiers = new List<Transform>();

    System.Random randomOperator;

    // Start is called before the first frame update
    void Start()
    {
        randomOperator = new System.Random();
        InitializePrefabs();
        PlaceSoldiers();
    }

    void PlaceSoldiers()
    {
        foreach (Transform potentialPositon in potentialPositionsHolder)
        {
            Transform newSoldier = InstantiateRandomSoldier();
            SaveSoldier(newSoldier);
            PositionSoldier(newSoldier, potentialPositon, targetRotation);
        }
    }

    Transform InstantiateRandomSoldier()
    {
        Transform newSoldier = Instantiate(potentialSoldiers[randomOperator.Next(0, potentialSoldiers.Count)]);
        newSoldier.gameObject.SetActive(true);
        return newSoldier;
    }

    void PositionSoldier(Transform soldier, Transform endPosition, Transform targetRotation)
    {
        soldier.position = endPosition.position;
        soldier.LookAt(targetRotation);
    }

    void SaveSoldier(Transform soldier)
    {
        soldier.parent = instantiatedSoldiersHolder;
        instantiatedSoldiers.Add(soldier);
    }

    void InitializePrefabs()
    {
        foreach(Transform soldier in prefabsHolder)
        {
            potentialSoldiers.Add(soldier);
        }
    }
}
