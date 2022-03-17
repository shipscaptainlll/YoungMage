using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidasPipesTransmission : MonoBehaviour
{
    [SerializeField] MidasConversionProcess midasConversionProcess;
    [SerializeField] PipeElementInstantiator pipeElementInstantiator;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] Transform finalOutlet;

    System.Random random;
    // Start is called before the first frame update
    void Start()
    {
        midasConversionProcess.CoinTransportationAccepted += StartMateialTransportation;
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartMateialTransportation()
    {
        StartCoroutine(MaterialTransportation());
    }

    IEnumerator MaterialTransportation()
    {
        yield return new WaitForSeconds(2f);
        pipeElementInstantiator.InstantiatePipeObject();
        //LetCoinOut();
    }

    public void StartCoinTransportation()
    {
        StartCoroutine(CoinTransportation());
    }

    IEnumerator CoinTransportation()
    {
        pipeElementInstantiator.InstantiateCoinObject();
        yield return null;
        //LetCoinOut();
    }

    public void LetCoinOut()
    {
        float xTorque = (float)random.Next(-2, 2);
        float yTorque = (float)random.Next(-2, 2);
        float zTorque = (float)random.Next(-2, 2);
        Transform createdCoin = Instantiate(objectManager.TakeObject(1).transform, finalOutlet.position, finalOutlet.rotation);
        createdCoin.gameObject.AddComponent<GlobalResource>();
        createdCoin.gameObject.GetComponent<GlobalResource>().ID = 1;
        createdCoin.gameObject.AddComponent<BoxCollider>();
        createdCoin.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        createdCoin.gameObject.AddComponent<Rigidbody>();
        createdCoin.gameObject.GetComponent<Rigidbody>().AddTorque(xTorque * 100, yTorque * 100, zTorque * 100);
    }
}
