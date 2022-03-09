using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmutationCostTaker : MonoBehaviour
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] Transform oreProductsCounters;
    [SerializeField] PotentialProductLibrary potentialProductLibrary;
    List<int> operations = new List<int>();
    int iterationNumber = 1;
    int firstOperationID;
    int secondOperationID;
    int thirdOperationID;
    int fourthOperationID;
    int fifthOperationID;
    int sixthOperationID;

    public event Action ResourcesEndedUp = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        clickManager.EnterClicked += AddToCounters;
        operations.Add(firstOperationID);
        operations.Add(secondOperationID);
        operations.Add(thirdOperationID);
        operations.Add(fourthOperationID);
        operations.Add(fifthOperationID);
        operations.Add(sixthOperationID);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddToCounters()
    {
        foreach (Transform counter in oreProductsCounters)
        {
            counter.GetComponent<ICounter>().AddResource(5);
        }
    }

    void CacheUnitTest()
    {
        
        CheckCost(11);
    }
                                        

    public bool CheckCost(int productID)
    {
        
        foreach (var productResourcesList in potentialProductLibrary.PotentialProducts)
        {
            if (productResourcesList.Key == productID)
            {
                foreach (var resourceID in productResourcesList.Value)
                {
                    //Debug.Log("processing element id number: " + resourceID);
                    foreach (Transform counter in oreProductsCounters)
                    {
                        //Debug.Log("processing counter: " + counter);
                        if (counter.GetComponent<ICounter>().ID == resourceID)
                        {
                            //Debug.Log("counter " + counter + " has " + counter.GetComponent<ICounter>().Count);
                            if (counter.GetComponent<ICounter>().Count > 1)
                            {
                                
                                counter.GetComponent<ICounter>().GetResource(1);
                                SaveOperation(resourceID);
                                
                                //Debug.Log("payed for element id number: " + resourceID);
                            }
                            else
                            {
                                ResetTransmutation();
                                return false;
                            }
                        }
                    }
                }
                FinishTransmutation();
                return true;
            }
            
        }
        return false;
    }

    void SaveOperation(int id)
    {
        operations[iterationNumber - 1] = id;
        //Debug.Log("Added to operation counter hello " + operations[iterationNumber - 1] + " " + id);
        iterationNumber++;
    }

    void FinishTransmutation()
    {
        ResetOperationsData();
        //Debug.Log("Sucessfully payed for everything");
    }

    void ResetTransmutation()
    {
        ReturnCosts();
        ResetOperationsData();
        if (ResourcesEndedUp != null) { ResourcesEndedUp(); }
        //Debug.Log("Unsuccessfull transmutation");
    }

    void ReturnCosts()
    {
        for (int i = 0; i < operations.Count; i++)
        {
            //Debug.Log(i + " " + operations[i]);
            if (operations[i] != 0)
            {
                //Debug.Log(operations[i]);
                foreach (Transform counter in oreProductsCounters)
                {
                    if (counter.GetComponent<ICounter>().ID == operations[i])
                    {
                        counter.GetComponent<ICounter>().AddResource(1);
                        //Debug.Log("returened costs to " + counter);
                    }
                }
            }
        }
    }

    void ResetOperationsData()
    {
        iterationNumber = 1;
        for (int i = 0; i < operations.Count; i++)
        {
            operations[i] = 0;
        }
    }
}
