using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorPortalInstantiator : MonoBehaviour
{
    [SerializeField] Transform portalToInstantiate;
    [SerializeField] DefractorPipeSystem defractorPipeSystem;
    public Transform portalInstance;
    bool portalIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        defractorPipeSystem.ProductLeftPipes += InstantiatePortal;
        //potentialProductAppearance.ObjectCreated += InstantiatePortal;
        //potentialProductAppearance.ObjectTeleported += ClosePortal;
    }

    void InstantiatePortal()
    {
        if (!portalIsActive)
        {
            
            portalIsActive = true;
            portalInstance = Instantiate(portalToInstantiate, portalToInstantiate.position, portalToInstantiate.rotation);
            portalInstance.GetComponent<DefractorPortalOpener>().InitiatePortalOpening();
            portalInstance.GetComponent<DefractorPortalOpener>().PortalClosed += DestroyInstance;


        }
        StopAllCoroutines();
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(10f);
        ClosePortal();
    }

    void ClosePortal()
    {
        if (portalIsActive)
        {
            portalInstance.GetComponent<DefractorPortalOpener>().InitiatePortalClosing();
            //portalInstance = null;
            portalIsActive = false;
        }
    }

    void DestroyInstance()
    {
        Destroy(portalInstance.gameObject);
    }
}
