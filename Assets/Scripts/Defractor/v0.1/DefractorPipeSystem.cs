using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorPipeSystem : MonoBehaviour
{
    [SerializeField] DefractoringProcess defractoringProcess;
    [SerializeField] ItemsCounterQuests itemsCounterQuests;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] DefractorProductsList defractorProductsList;
    [SerializeField] Transform defractorStartLine;
    [SerializeField] Transform pipeOutlet;
    [SerializeField] Transform productsCountersHolder;
    System.Random random;

    public event Action ProductLeftPipes = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        defractoringProcess.SentInPipes += StartTransferring;
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartTransferring(int customID)
    {
        StartCoroutine(TransferringThroughPipes(customID));
    }

    IEnumerator TransferringThroughPipes(int customID)
    {
        itemsCounterQuests.countDefractedQuest(GetProductId(customID));
        yield return new WaitForSeconds(1f);
        LetFromDefractor(GetObjectModel(GetProductId(customID)));
        yield return new WaitForSeconds(2f);
        LetFromPipes(GetObjectModel(GetProductId(customID)), GetProductId(customID));
        yield return null;
    }

    void LetFromDefractor(GameObject productObject)
    {
        GameObject productInstance = Instantiate(productObject, defractorStartLine.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        SetDefractoringLine(productInstance);
    }

    void LetFromPipes(GameObject productObject, int productID)
    {
        GameObject productInstance = Instantiate(productObject, pipeOutlet.position, Quaternion.Euler(new Vector3(0,0,90)));
        SetOutletLine(productInstance, productID);
        if (ProductLeftPipes != null) { ProductLeftPipes(); }
    }

    int GetProductId(int customID)
    {
        return defractorProductsList.TakeCounter(customID);
    }

    GameObject GetObjectModel(int customID)
    {
        return objectManager.TakeObject(customID);
    }

    void SetDefractoringLine(GameObject productInstance)
    {
        float xTorque = (float)random.Next(-2, 2);
        float yTorque = (float)random.Next(-2, 2);
        float zTorque = (float)random.Next(-2, 2);
        productInstance.AddComponent<Rigidbody>();
        productInstance.GetComponent<Rigidbody>().useGravity = false;
        productInstance.GetComponent<Rigidbody>().AddForce(0, 100, 0);
        productInstance.AddComponent<BoxCollider>();
        productInstance.GetComponent<BoxCollider>().isTrigger = true ;
        productInstance.AddComponent<DefractorProduct>();
        //productInstance.transform.localScale = new Vector3(0.16f, 0.16f, 0.16f);
        productInstance.GetComponent<Rigidbody>().AddTorque(xTorque * 100, yTorque * 100, zTorque * 100);
    }

    void SetOutletLine(GameObject productInstance, int productID)
    {
        float xTorque = (float)random.Next(-2, 2);
        float yTorque = (float)random.Next(-2, 2);
        float zTorque = (float)random.Next(-2, 2);
        productInstance.AddComponent<Rigidbody>();
        //productInstance.transform.localScale = new Vector3(0.16f, 0.16f, 0.16f);
        productInstance.GetComponent<Rigidbody>().AddTorque(xTorque * 100, yTorque * 100, zTorque * 100);
        productInstance.AddComponent<DefractorProduct>();
        productInstance.GetComponent<DefractorProduct>().ID = productID;
        productInstance.GetComponent<DefractorProduct>().CountersHolder = productsCountersHolder;
        productInstance.AddComponent<BoxCollider>();
        productInstance.GetComponent<BoxCollider>().isTrigger = true;
    }
}
