using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInstantiator : MonoBehaviour
{
    [Header("Main Part")]
    [SerializeField] Transform portalToInstantiate;
    [SerializeField] PotentialProductAppearance potentialProductAppearance;
    [SerializeField] Transform instantiatedPortalHolder;
    public Transform portalInstance;
    bool portalIsActive = false;

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    AudioSource openPortalSound;
    AudioSource closePortalSound;

    public bool PortalIsActive { get { return portalIsActive; } }
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
            portalInstance.parent = instantiatedPortalHolder;
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

    public void ImmediatePortalOpening()
    {
        StopAllCoroutines();
        portalIsActive = true;
        portalInstance = Instantiate(portalToInstantiate, portalToInstantiate.position + new Vector3(0, 0.15f, 0), portalToInstantiate.rotation);
        portalInstance.parent = instantiatedPortalHolder;
        //portalInstance.localScale = new Vector3(1000.6137492f, 1000.865296f, portalInstance.localScale.z);
        //openPortalSound.Play();
        portalInstance.GetComponent<ProductPortalOpener>().InitiateImmediatePortalOpening();
        portalInstance.GetComponent<ProductPortalOpener>().PortalClosed += DestroyInstance;
    }

    public void ImmediatePortalClosing()
    {
        if (portalIsActive && instantiatedPortalHolder.childCount > 0)
        {
            StopAllCoroutines();
            DestroyImmediate(instantiatedPortalHolder.GetChild(0).GetComponent<ProductPortalOpener>().gameObject);
            portalIsActive = false;
        }
    }

    void DestroyInstance()
    {
        Destroy(portalInstance.gameObject);
    }
}
