using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorPipeSystem : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] DefractoringProcess defractoringProcess;
    [SerializeField] ItemsCounterQuests itemsCounterQuests;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] DefractorProductsList defractorProductsList;
    [SerializeField] Transform defractorStartLine;
    [SerializeField] Transform pipeOutlet;
    [SerializeField] Transform productsCountersHolder;
    [SerializeField] Transform defractoringLine;
    [SerializeField] Transform outletLine;
    System.Random random;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource transportationSound;

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
        if (GetProductId(customID) == 0) { return; }
        StartCoroutine(TransferringThroughPipes(customID));
    }

    public void TransferUploadedObjects(int customID, Vector3 position, Quaternion rotation)
    {
        if (GetProductId(customID) == 0) { return; }
        GameObject productInstance = Instantiate(GetObjectModel(GetProductId(customID)), position, rotation);
        productInstance.transform.parent = defractoringLine;
        transportationSound = soundManager.LocateAudioSource("DefractorFlyingObject", productInstance.transform);
        transportationSound.Play();
        SetDefractoringLine(productInstance, customID);
    }

    public void TransferUploadedOutletObjects(int customID, Vector3 position, Quaternion rotation)
    {
        GameObject productInstance = Instantiate(GetObjectModel(GetProductId(customID)), position, rotation);
        productInstance.transform.parent = outletLine;
        transportationSound = soundManager.LocateAudioSource("DefractorFlyingObject", productInstance.transform);
        transportationSound.Play();
        SetOutletLine(productInstance, GetProductId(customID));
    }

    IEnumerator TransferringThroughPipes(int customID)
    {
        itemsCounterQuests.countDefractedQuest(GetProductId(customID));
        yield return new WaitForSeconds(1f);
        LetFromDefractor(GetObjectModel(GetProductId(customID)), customID);
        yield return new WaitForSeconds(2f);
        LetFromPipes(GetObjectModel(GetProductId(customID)), GetProductId(customID));
        yield return null;
    }

    void LetFromDefractor(GameObject productObject, int customID)
    {
        GameObject productInstance = Instantiate(productObject, defractorStartLine.position, Quaternion.Euler(new Vector3(0, 0, 90)));
        productInstance.transform.parent = defractoringLine;
        transportationSound = soundManager.LocateAudioSource("DefractorFlyingObject", productInstance.transform);
        transportationSound.Play();
        SetDefractoringLine(productInstance, customID);
    }

    void LetFromPipes(GameObject productObject, int productID)
    {
        GameObject productInstance = Instantiate(productObject, pipeOutlet.position, Quaternion.Euler(new Vector3(0,0,90)));
        productInstance.transform.parent = outletLine;
        SetOutletLine(productInstance, productID);
        if (ProductLeftPipes != null) { ProductLeftPipes(); }
    }

    public int GetProductId(int customID)
    {
        return defractorProductsList.TakeCounter(customID);
    }

    GameObject GetObjectModel(int customID)
    {
        return objectManager.TakeObject(customID);
    }

    void SetDefractoringLine(GameObject productInstance, int productID)
    {
        float xTorque = (float)random.Next(-2, 2);
        float yTorque = (float)random.Next(-2, 2);
        float zTorque = (float)random.Next(-2, 2);
        productInstance.AddComponent<Rigidbody>();
        productInstance.GetComponent<Rigidbody>().useGravity = false;
        productInstance.GetComponent<Rigidbody>().AddForce(0, 100, 0);
        productInstance.AddComponent<BoxCollider>();
        if (productInstance.GetComponent<DefractorProduct>() == null) { productInstance.AddComponent<DefractorProduct>(); }
        productInstance.GetComponent<DefractorProduct>().ID = productID;
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

    public void ClearDefractorObjects()
    {
        foreach (Transform element in defractoringLine)
        {
            Destroy(element.gameObject);
        }

        foreach (Transform element in outletLine)
        {
            Destroy(element.gameObject);
        }
    }
}
