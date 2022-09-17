using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MidasPipesTransmission : MonoBehaviour
{
    [SerializeField] MidasConversionProcess midasConversionProcess;
    [SerializeField] PipeElementInstantiator pipeElementInstantiator;
    [SerializeField] ObjectManager objectManager;
    [SerializeField] Transform finalOutlet;
    [SerializeField] Transform transformationVFX;
    Coroutine transformationVFXCoroutine;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource coinInstantiationSound;
    AudioSource handElectricitySound;

    System.Random random;
    // Start is called before the first frame update
    void Start()
    {
        midasConversionProcess.CoinTransportationAccepted += StartMateialTransportation;
        random = new System.Random();
        coinInstantiationSound = soundManager.LocateAudioSource("CoinAppear", finalOutlet);
        handElectricitySound = soundManager.LocateAudioSource("ElectricitySound", transformationVFX);
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
        ManageHandVFX();
        StartCoroutine(CoinTransportation());
    }

    void ManageHandVFX()
    {
        if (transformationVFXCoroutine != null)
        {
            StopCoroutine(transformationVFXCoroutine);
        }
        transformationVFXCoroutine = StartCoroutine(DelayVFXOff());
    }

    IEnumerator DelayVFXOff()
    {
        transformationVFX.gameObject.SetActive(true);
        transformationVFX.GetComponent<VisualEffect>().Play();
        if (!handElectricitySound.isPlaying) { handElectricitySound.Play(); }
        yield return new WaitForSeconds(5f);
        transformationVFX.GetComponent<VisualEffect>().Stop();
        handElectricitySound.Stop();
        transformationVFX.gameObject.SetActive(false);
    }

    IEnumerator CoinTransportation()
    {
        pipeElementInstantiator.InstantiateCoinObject();
        yield return null;
        //LetCoinOut();
    }

    public void LetCoinOut()
    {
        coinInstantiationSound.Play();
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
