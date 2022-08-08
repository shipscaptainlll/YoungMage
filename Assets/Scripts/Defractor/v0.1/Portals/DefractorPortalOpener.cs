using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorPortalOpener : MonoBehaviour
{
    //[SerializeField] Transform portalContainer;
    [SerializeField] Transform VFXContainer;
    Transform housePortal;
    bool cycleRunning = false;

    public event Action PortalClosed = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        //housePortal.localScale = new Vector3(0.1f, 0.1f, housePortal.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiatePortalOpening()
    {
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        if (!cycleRunning)
        {
            cycleRunning = true;
            EnablePortals();
            StartVFX();
            //StartCoroutine(OpenPortal());
        }
    }

    public void InitiatePortalClosing()
    {
        if (cycleRunning)
        {
            cycleRunning = false;
            //StartCoroutine(ClosePortal());
            //StartCoroutine(CloseVFX());
            PortalClosed();
            housePortal.gameObject.SetActive(false);
            VFXContainer.gameObject.SetActive(false);
        }
    }
    IEnumerator OpenPortal()
    {
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        housePortal.GetComponent<MeshRenderer>().enabled = true;
        housePortal.localScale = new Vector3(0.001f, 0.001f, housePortal.localScale.z);
        
        
        float elapsed = 0;
        float updateSpeed = 0.0015f;
        float startXScale = housePortal.localScale.x;
        float currentXScale;
        float targetXScale = 1000.6137492f;
        float startYScale = housePortal.localScale.y;
        float currentYScale;
        float targetYScale = 1000.865296f;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            currentXScale = Mathf.Lerp(startXScale, targetXScale, elapsed / updateSpeed);
            currentYScale = Mathf.Lerp(startYScale, targetYScale, elapsed / updateSpeed);
            housePortal.localScale = new Vector3(currentXScale, currentYScale, housePortal.localScale.z);
            yield return null;
        }
        housePortal.localScale = new Vector3(1.146875f, 1.090437f, housePortal.localScale.z);
    }

    IEnumerator ClosePortal()
    {
        float elapsed = 0;
        float updateSpeed = 0.15f;
        float startXScale = housePortal.localScale.x;
        float currentXScale;
        float targetXScale = 0.01f;
        float startYScale = housePortal.localScale.y;
        float currentYScale;
        float targetYScale = 0.01f;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            currentXScale = Mathf.Lerp(startXScale, targetXScale, elapsed / updateSpeed);
            currentYScale = Mathf.Lerp(startYScale, targetYScale, elapsed / updateSpeed);
            housePortal.localScale = new Vector3(currentXScale, currentYScale, housePortal.localScale.z);
            yield return null;
        }
        housePortal.GetComponent<MeshRenderer>().enabled = false;
        PortalClosed();
        housePortal.gameObject.SetActive(false);
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

    void StartVFX()
    {
        VFXContainer.gameObject.SetActive(true);
    }

    void EnablePortals()
    {
        transform.gameObject.SetActive(true);
    }
}
