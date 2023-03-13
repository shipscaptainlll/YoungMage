using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPortalOpener : MonoBehaviour
{
    //[SerializeField] Transform portalContainer;
    [SerializeField] Transform VFXContainer;
    [SerializeField] PotentialProductAppearance potentialProductAppearance;
    Transform housePortal;
    Transform houseVFX;
    bool cycleRunning = false;

    public event Action PortalClosed = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        houseVFX = transform.Find("Simple Portal").Find("Visualisation").Find("ShockWave (1)");
        if (housePortal.localScale.x > 0.5f)
        {
            return;
        }
        housePortal.localScale = new Vector3(0.1f, 0.1f, housePortal.localScale.z);
    }

    public void InitiatePortalOpening()
    {
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        houseVFX = transform.Find("Simple Portal").Find("Visualisation").Find("ShockWave (1)");
        if (!cycleRunning)
        {
            cycleRunning = true;
            EnablePortals();
            StartVFX();
            StartCoroutine(OpenPortal());
        }
    }

    public void InitiateImmediatePortalOpening()
    {
        StopAllCoroutines();
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        houseVFX = transform.Find("Simple Portal").Find("Visualisation").Find("ShockWave (1)");
        cycleRunning = true;
        EnablePortals();
        StartVFX();
        housePortal.GetComponent<MeshRenderer>().enabled = true;

        housePortal.localScale = new Vector3(0.6137492f, 0.865296f, housePortal.localScale.z);
    }

    public void InitiatePortalClosing()
    {
        Debug.Log("Here");
        if (cycleRunning)
        {
            cycleRunning = false;
            Debug.Log("here 2");
            StartCoroutine(ClosePortal());
            Debug.Log("here 3");
            StartCoroutine(CloseVFX());
        }
    }
    IEnumerator OpenPortal()
    {
        housePortal = transform.Find("Simple Portal").Find("Visualisation");
        houseVFX = transform.Find("Simple Portal").Find("Visualisation").Find("ShockWave (1)");
        housePortal.GetComponent<MeshRenderer>().enabled = true;
        housePortal.localScale = new Vector3(0.001f, 0.001f, housePortal.localScale.z);
        
        
        float elapsed = 0;
        float updateSpeed = 0.0015f;
        float startXScale = housePortal.localScale.x;
        float currentXScale;
        float targetXScale = 1.146875f;
        float startYScale = housePortal.localScale.y;
        float currentYScale;
        float targetYScale = 1.146875f;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            currentXScale = Mathf.Lerp(startXScale, targetXScale, elapsed / updateSpeed);
            currentYScale = Mathf.Lerp(startYScale, targetYScale, elapsed / updateSpeed);
            housePortal.localScale = new Vector3(currentXScale, currentYScale, housePortal.localScale.z);
            houseVFX.localScale = new Vector3(currentYScale, currentXScale, houseVFX.localScale.z);
            yield return null;
        }
        housePortal.localScale = new Vector3(0.897f, 0.897f, housePortal.localScale.z);
        housePortal.localScale = new Vector3(1.146875f, 1.146875f, housePortal.localScale.z);
    }

    IEnumerator ClosePortal()
    {
        float elapsed = 0;
        float updateSpeed = 0.15f;
        float startXScale = 1.146875f;
        float currentXScale;
        float targetXScale = 0.01f;
        float startYScale = 1.146875f;
        float currentYScale;
        float targetYScale = 0.01f;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            currentXScale = Mathf.Lerp(startXScale, targetXScale, elapsed / updateSpeed);
            currentYScale = Mathf.Lerp(startYScale, targetYScale, elapsed / updateSpeed);
            houseVFX.localScale = new Vector3(currentYScale, currentXScale, currentXScale);
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
