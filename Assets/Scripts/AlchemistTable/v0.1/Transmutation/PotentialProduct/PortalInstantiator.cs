using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInstantiator : MonoBehaviour
{
    [SerializeField] Transform portalToInstantiate;
    [SerializeField] PotentialProductAppearance potentialProductAppearance;
    public Transform portalInstance;
    bool portalIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        potentialProductAppearance.ObjectCreated += InstantiatePortal;
        potentialProductAppearance.ObjectTeleported += ClosePortal;
    }

    void InstantiatePortal()
    {
        if (!portalIsActive)
        {
            
            portalIsActive = true;
            portalInstance = Instantiate(portalToInstantiate, portalToInstantiate.position + new Vector3(0, 0.15f, 0), portalToInstantiate.rotation);
            portalInstance.GetComponent<ProductPortalOpener>().InitiatePortalOpening();
            portalInstance.GetComponent<ProductPortalOpener>().PortalClosed += DestroyInstance;


        }
    }

    void ClosePortal()
    {
        if (portalIsActive)
        {
            portalInstance.GetComponent<ProductPortalOpener>().InitiatePortalClosing();
            //portalInstance = null;
            portalIsActive = false;
        }
    }

    void DestroyInstance()
    {
        Destroy(portalInstance.gameObject);
    }
}
