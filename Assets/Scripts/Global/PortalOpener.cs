using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOpener : MonoBehaviour
{
    [SerializeField] SkeletonsStack skeletonsStack;
    [SerializeField] Transform housePortalContainer;
    [SerializeField] Transform fieldPortalContainer;
    [SerializeField] Transform VFXContainer;
    [SerializeField] ClickManager clickManager;
    [SerializeField] CopycatCreator teleportationManager;
    [SerializeField] EClickVariations eClickVariations;
    bool portalOpened;
    Transform housePortal;
    Transform fieldPortal;
    bool cycleRunning = false;

    Transform choosenSkeleton;
    System.Random random;
    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        clickManager.EClicked += InitiatePortalOpening;
        housePortal = housePortalContainer.Find("Simple Portal").Find("Visualisation");
        housePortal.localScale = new Vector3(0.1f, 0.1f, housePortal.localScale.z);
        teleportationManager.SkeletonFinallyTeleported += InitiatePortalClosing;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitiatePortalOpening()
    {
        if (!portalOpened)
        {
            portalOpened = true;
            Debug.Log("OpeningPortal");
            if (!cycleRunning && eClickVariations.IsOpeningPortal)
            {
                cycleRunning = true;
                
                EnablePortals();
                ChooseSkeletonInstance();
                ChangePortalPosition();
                StartVFX();
                StartCoroutine(OpenVFX());
                StartCoroutine(OpenPortal());
            }
        } else { portalOpened = false;
            Debug.Log("ClosingPortal");
            if (cycleRunning)
            {
                cycleRunning = false;
                StartCoroutine(ClosePortal());
                StartCoroutine(CloseVFX());
            }
        }
        
    }

    void InitiatePortalClosing()
    {
        if (cycleRunning)
        {
            cycleRunning = false;
            StartCoroutine(ClosePortal());
            StartCoroutine(CloseVFX());
        }
    }
    IEnumerator OpenPortal()
    {
        housePortal.gameObject.SetActive(true);
        housePortal.localScale = new Vector3(0.001f, 0.001f, housePortal.localScale.z);
        yield return new WaitForSeconds(1f);
        
        float elapsed = 0;
        float updateSpeed = 0.15f;
        float startXScale = housePortal.localScale.x;
        float currentXScale;
        float targetXScale = 0.6137492f;
        float startYScale = housePortal.localScale.y;
        float currentYScale;
        float targetYScale = 0.865296f;
        while (elapsed < updateSpeed)
        {
            elapsed += Time.deltaTime;
            currentXScale = Mathf.Lerp(startXScale, targetXScale, elapsed / updateSpeed);
            currentYScale = Mathf.Lerp(startYScale, targetYScale, elapsed / updateSpeed);
            housePortal.localScale = new Vector3(currentXScale, currentYScale, housePortal.localScale.z);
            yield return null;
        }
    }

    IEnumerator ClosePortal()
    {
        yield return new WaitForSeconds(1f);
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
        housePortal.gameObject.SetActive(false);
    }

    IEnumerator CloseVFX()
    {
        yield return new WaitForSeconds(1f);
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
        housePortalContainer.gameObject.SetActive(true);
        fieldPortalContainer.gameObject.SetActive(true);
    }

    void ChooseSkeletonInstance()
    {
        int skeletonID = random.Next(0, skeletonsStack.SkeletonStack.Count);
        
        var skeletons = skeletonsStack.SkeletonStack.ToArray();

        choosenSkeleton = skeletons[skeletonID];

        Debug.Log(choosenSkeleton);
    }

    void ChangePortalPosition()
    {
        fieldPortalContainer.position = choosenSkeleton.position + new Vector3(-4, 1.5f, 0);
    }
}
