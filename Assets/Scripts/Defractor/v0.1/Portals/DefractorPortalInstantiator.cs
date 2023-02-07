using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefractorPortalInstantiator : MonoBehaviour
{
    [SerializeField] Transform portalToInstantiate;
    [SerializeField] DefractorPipeSystem defractorPipeSystem;
    [SerializeField] Transform portalsHolder;
    public Transform portalInstance;
    bool portalIsActive = false;
    bool portalShown;


    public bool PortalShown { get { return portalShown; } }


    // Start is called before the first frame update
    void Start()
    {
        defractorPipeSystem.ProductLeftPipes += InstantiatePortal;
        //potentialProductAppearance.ObjectCreated += InstantiatePortal;
        //potentialProductAppearance.ObjectTeleported += ClosePortal;
    }

    public void InstantiatePortal()
    {
        portalShown = true;
        if (!portalIsActive)
        {
            
            portalIsActive = true;
            portalInstance = Instantiate(portalToInstantiate, portalToInstantiate.position, portalToInstantiate.rotation);
            portalInstance.parent = portalsHolder;
            portalInstance.GetComponent<DefractorPortalOpener>().InstantiateSouds();
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

    public void UploadedClosePortal()
    {
        if (portalsHolder.childCount > 0)
        {
            portalIsActive = true;
            portalInstance = portalsHolder.GetChild(0);
            portalInstance.GetComponent<DefractorPortalOpener>().InitiatePortalClosing();
            //portalInstance = null;
            portalIsActive = false;
        }
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
        portalShown = false;
        Destroy(portalInstance.gameObject);
    }
}
