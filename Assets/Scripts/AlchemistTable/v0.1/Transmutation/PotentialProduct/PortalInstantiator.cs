using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInstantiator : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] Transform portalToInstantiate;
    [SerializeField] PotentialProductAppearance potentialProductAppearance;
    public Transform portalInstance;
    bool portalIsActive = false;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource openPortalSound;
    AudioSource closePortalSound;

    // Start is called before the first frame update
    void Start()
    {
        potentialProductAppearance.ObjectCreated += InstantiatePortal;
        potentialProductAppearance.ObjectTeleported += ClosePortal;
        openPortalSound = soundManager.LocateAudioSource("PortalOpeningDefractor", transform);
        closePortalSound = soundManager.LocateAudioSource("PortalClosingDefractor", transform);
    }

    void InstantiatePortal()
    {
        if (!portalIsActive)
        {
            
            portalIsActive = true;
            portalInstance = Instantiate(portalToInstantiate, portalToInstantiate.position + new Vector3(0, 0.15f, 0), portalToInstantiate.rotation);
            
            openPortalSound.Play();
            portalInstance.GetComponent<ProductPortalOpener>().InitiatePortalOpening();
            portalInstance.GetComponent<ProductPortalOpener>().PortalClosed += DestroyInstance;
            

        }
    }

    void ClosePortal()
    {
        Debug.Log("what");
        if (portalIsActive)
        {
            closePortalSound.Play();
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
