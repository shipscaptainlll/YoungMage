using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorPortalOpener : MonoBehaviour
{
    //[SerializeField] Transform portalContainer;
    [Header("Main Part")]
    [SerializeField] AppearanceTransmutationCircle appearanceTransmutationCircle;
    [SerializeField] Transform VFXContainer;
    Transform housePortal;
    Transform houseVFX;
    bool cycleRunning = false;
    float portalElapsed;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    [SerializeField] Transform crystalsSoundSource;
    [SerializeField] float crystalsDistance;
    AudioSource conjurationAppearSound;
    AudioSource crystalsTurnOn;
    AudioSource crystalsWorking;
    AudioSource crystalsTurnOff;
    AudioSource openPortalSound;
    AudioSource closePortalSound;

    public float PortalElapsed { get { return portalElapsed; } }
    public bool CycleRunning { get { return cycleRunning; } }       
    public event Action PortalClosed = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        houseVFX = transform.Find("Simple Portal").Find("Visualisation").Find("ShockWave (1)");
        //housePortal.localScale = new Vector3(0.1f, 0.1f, housePortal.localScale.z);
    }

    public void InstantiateSouds()
    {
        conjurationAppearSound = soundManager.LocateAudioSource("ConjurationCircleAppear", VFXContainer);

        crystalsTurnOn = soundManager.LocateAudioSource("CrystalActivation", crystalsSoundSource);
        crystalsWorking = soundManager.LocateAudioSource("CrystalWorking", crystalsSoundSource);
        crystalsWorking.maxDistance = crystalsDistance;
        crystalsTurnOff = soundManager.LocateAudioSource("CrystalsDeactivating", crystalsSoundSource);
        openPortalSound = soundManager.LocateAudioSource("PortalOpeningDefractor", crystalsSoundSource);
        closePortalSound = soundManager.LocateAudioSource("PortalClosingDefractor", crystalsSoundSource);
    }

    public void InitiatePortalOpening()
    {
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        houseVFX = transform.Find("Simple Portal").Find("Visualisation").Find("ShockWave (1)");
        if (!cycleRunning)
        {

            Debug.Log("portal opeened");
            
            cycleRunning = true;
            EnablePortals();
            StartCrystalsSound();
            StartPS();
            StartCoroutine(OpenPortal());
        }
    }

    public void ImediatePortalClosing()
    {
        if (cycleRunning)
        {
            EndCrystalsSound();
            cycleRunning = false;
            housePortal.localScale = new Vector3(0.01f, 0.01f, housePortal.localScale.z);
            housePortal.GetComponent<MeshRenderer>().enabled = false;
            housePortal.gameObject.SetActive(false);

        }
    }
    public void UploadedPortalClosing(float uploadedElapsed)
    {
        //EndCrystalsSound();
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        houseVFX = transform.Find("Simple Portal").Find("Visualisation").Find("ShockWave (1)");
        EnablePortals();
        cycleRunning = false;
        StartCoroutine(ClosePortal(uploadedElapsed));
    }

    public void UploadedPortalOpening(float uploadedElapsed)
    {
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        houseVFX = transform.Find("Simple Portal").Find("Visualisation").Find("ShockWave (1)");
        cycleRunning = true;
        EnablePortals();
        StartCrystalsSound();
        //StartPS();
        StartCoroutine(OpenPortal(uploadedElapsed));
    }

    public void InitiatePortalClosing()
    {
        if (cycleRunning)
        {
            EndCrystalsSound();
            cycleRunning = false;
            //StopAllCoroutines();
            StartCoroutine(ClosePortal());
            //StartCoroutine(CloseVFX());
            
            //housePortal.gameObject.SetActive(false);
            appearanceTransmutationCircle.CircleDisappearance();
            //VFXContainer.gameObject.SetActive(false);
        }
    }
    IEnumerator OpenPortal()
    {
        openPortalSound.Play();
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        houseVFX = transform.Find("Simple Portal").Find("Visualisation").Find("ShockWave (1)");
        housePortal.GetComponent<MeshRenderer>().enabled = true;
        housePortal.localScale = new Vector3(0.001f, 0.001f, housePortal.localScale.z);


        portalElapsed = 0;
        float updateSpeed = 1.0015f;
        float startXScale = housePortal.localScale.x;
        float currentXScale;
        float targetXScale = 1.146875f;
        float startYScale = housePortal.localScale.y;
        float currentYScale;
        float targetYScale = 1.146875f;
        while (portalElapsed < updateSpeed)
        {
            portalElapsed += Time.deltaTime;
            currentXScale = Mathf.Lerp(startXScale, targetXScale, portalElapsed / updateSpeed);
            currentYScale = Mathf.Lerp(startYScale, targetYScale, portalElapsed / updateSpeed);
            housePortal.localScale = new Vector3(currentXScale, currentYScale, housePortal.localScale.z);
            houseVFX.localScale = new Vector3(currentYScale, currentXScale, houseVFX.localScale.z);
            Debug.Log("currentXScale: " + currentXScale + " currentYScale: " + currentYScale);
            yield return null;
        }
        housePortal.localScale = new Vector3(1.146875f, 1.146875f, housePortal.localScale.z);
        houseVFX.localScale = new Vector3(0.897f, 0.897f, 0.897f);
        //portalElapsed = 0;
    }

    IEnumerator OpenPortal(float uploadedElapsed)
    {
        openPortalSound.Play();
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        houseVFX = transform.Find("Simple Portal").Find("Visualisation").Find("ShockWave (1)");
        housePortal.GetComponent<MeshRenderer>().enabled = true;
        housePortal.localScale = new Vector3(0.001f, 0.001f, housePortal.localScale.z);


        portalElapsed = uploadedElapsed;
        float updateSpeed = 1.0015f;
        float startXScale = housePortal.localScale.x;
        float currentXScale;
        float targetXScale = 1.146875f;
        float startYScale = housePortal.localScale.y;
        float currentYScale;
        float targetYScale = 1.146875f;
        while (portalElapsed < updateSpeed)
        {
            portalElapsed += Time.deltaTime;
            currentXScale = Mathf.Lerp(startXScale, targetXScale, portalElapsed / updateSpeed);
            currentYScale = Mathf.Lerp(startYScale, targetYScale, portalElapsed / updateSpeed);
            houseVFX.localScale = new Vector3(currentYScale, currentXScale, currentXScale);
            housePortal.localScale = new Vector3(currentXScale, currentYScale, currentXScale);
            yield return null;
        }
        housePortal.localScale = new Vector3(1.146875f, 1.146875f, housePortal.localScale.z);
        //portalElapsed = 0;
    }

    IEnumerator ClosePortal()
    {
        closePortalSound.Play();
        portalElapsed = 0;
        float updateSpeed = 1.15f;
        float startXScale = 1.146875f;
        float currentXScale;
        float targetXScale = 0.01f;
        float startYScale = 1.146875f;
        float currentYScale;
        float targetYScale = 0.01f;
        while (portalElapsed < updateSpeed)
        {
            portalElapsed += Time.deltaTime;
            currentXScale = Mathf.Lerp(startXScale, targetXScale, portalElapsed / updateSpeed);
            currentYScale = Mathf.Lerp(startYScale, targetYScale, portalElapsed / updateSpeed);
            Debug.Log("currentXScale: " + currentXScale + " currentYScale: " + currentYScale + " portal elapsed: " + portalElapsed);
            houseVFX.localScale = new Vector3(currentYScale, currentXScale, currentXScale);
            housePortal.localScale = new Vector3(currentXScale, currentYScale, housePortal.localScale.z);
            yield return null;
        }
        housePortal.GetComponent<MeshRenderer>().enabled = false;
        PortalClosed();
        housePortal.gameObject.SetActive(false);
        portalElapsed = 0;
    }

    IEnumerator ClosePortal(float uploadedElapsed)
    {
        if (closePortalSound != null) { closePortalSound.Play(); }
        portalElapsed = uploadedElapsed;
        float updateSpeed = 1.15f;
        float startXScale = 1.146875f;
        float currentXScale;
        float targetXScale = 0.01f;
        float startYScale = 1.146875f;
        float currentYScale;
        float targetYScale = 0.01f;
        while (portalElapsed < updateSpeed)
        {
            portalElapsed += Time.deltaTime;
            currentXScale = Mathf.Lerp(startXScale, targetXScale, portalElapsed / updateSpeed);
            currentYScale = Mathf.Lerp(startYScale, targetYScale, portalElapsed / updateSpeed);
            houseVFX.localScale = new Vector3(currentYScale, currentXScale, currentXScale);
            housePortal.localScale = new Vector3(currentXScale, currentYScale, housePortal.localScale.z);
            yield return null;
        }
        housePortal.GetComponent<MeshRenderer>().enabled = false;
        //PortalClosed();
        housePortal.gameObject.SetActive(false);
        //portalElapsed = 0;
    }

    IEnumerator CloseVFX()
    {
        float elapsed = 0;
        float updateSpeed = 0.15f;
        float startXScale = VFXContainer.localScale.x;
        float currentXScale;
        float targetXScale = 0.01f;
        float startYScale = VFXContainer.localScale.y;
        float currentYScale;
        float targetYScale = 0.01f;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            currentXScale = Mathf.Lerp(startXScale, targetXScale, elapsed / updateSpeed);
            currentYScale = Mathf.Lerp(startYScale, targetYScale, elapsed / updateSpeed);
            VFXContainer.localScale = new Vector3(currentXScale, currentYScale, VFXContainer.localScale.z);
            yield return null;
        }
        VFXContainer.gameObject.SetActive(false);
    }

    IEnumerator OpenVFX()
    {
        VFXContainer.localScale = new Vector3(0.001f, 0.001f, VFXContainer.localScale.z);
        float elapsed = 0;
        float updateSpeed = 0.15f;
        float startXScale = VFXContainer.localScale.x;
        float currentXScale;
        float targetXScale = 0.702593f;
        float startYScale = VFXContainer.localScale.y;
        float currentYScale;
        float targetYScale = 0.4141574f;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            currentXScale = Mathf.Lerp(startXScale, targetXScale, elapsed / updateSpeed);
            currentYScale = Mathf.Lerp(startYScale, targetYScale, elapsed / updateSpeed);
            VFXContainer.localScale = new Vector3(currentXScale, currentYScale, VFXContainer.localScale.z);
            yield return null;
        }
    }

    void StartPS()
    {
        appearanceTransmutationCircle.CircleAppearance();
        //VFXContainer.gameObject.SetActive(true);
        //VFXContainer.GetComponent<ParticleSystem>().Play();
        conjurationAppearSound.Play();
    }

    void EndCrystalsSound()
    {
        crystalsWorking.Stop();
        crystalsTurnOff.Play();
    }

    void StartCrystalsSound()
    {
        crystalsTurnOn.Play();
        crystalsWorking.Play();

    }

    void EnablePortals()
    {
        transform.gameObject.SetActive(true);
    }
}
