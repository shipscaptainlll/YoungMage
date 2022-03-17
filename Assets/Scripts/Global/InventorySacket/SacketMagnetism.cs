using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacketMagnetism : MonoBehaviour
{
    [SerializeField] ClickManager clickManager;
    [SerializeField] Animator sacketMagnetismAnimation;
    [SerializeField] QuickAccessHandController quickAccessHandController;
    [SerializeField] Transform sacketMagnet;
    [SerializeField] SacketMagnetDatabase sacketMagnetDatabase;
    [SerializeField] LayerMask magneticLayers;
    [SerializeField] Transform sacketPosition;
    [SerializeField] Transform startMovementParticles;
    RaycastHit[] hits;
    bool fieldActivated = false;

    public bool FieldActivated { get { return fieldActivated; } }
    // Start is called before the first frame update
    void Start()
    {
        clickManager.RHolded += CastMagneticField;
        clickManager.RUp += StopMagneticField;
    }

    // Update is called once per frame
    void Update()
    {
        if (fieldActivated)
        {
            MagnetResources();



        }
    }

    void MagnetResources()
    {
        DetectTargets();
        foreach (RaycastHit hit in hits)
        {
            TransferElement(hit);
        }
    }

    void DetectTargets()
    {
        hits = Physics.SphereCastAll(transform.position, 2.5f, transform.TransformDirection(Vector3.forward * 3), 10f, magneticLayers);
    }


    void TransferElement(RaycastHit hit)
    {
        ActivateResourceMagnetism(hit);
        Vector3 difference = hit.transform.transform.position - sacketPosition.position;
        difference.Normalize();
        difference = -difference * Time.deltaTime * 250;
        hit.transform.GetComponent<Rigidbody>().velocity = difference;
    }

    void ActivateResourceMagnetism(RaycastHit hit)
    {
        hit.transform.GetComponent<GlobalResource>().ActivateInventoryMagnetism(startMovementParticles);
    }



    private void OnDrawGizmos()
    {
        
    }

    void CastMagneticField()
    {
        if (quickAccessHandController.CurrentCustomID != 10)
        {
            if (!fieldActivated)
            {
                fieldActivated = true;
                quickAccessHandController.ObjectInHand.GetComponent<MeshRenderer>().enabled = false;
                sacketMagnetismAnimation.Play("SacketMagnetismStart");
                //sacketMagnetDatabase.Active = true;
                //sacketMagnet.gameObject.SetActive(true);

            }
        }
        
        
        
        
    }

    void StopMagneticField()
    {
        fieldActivated = false;
        quickAccessHandController.ObjectInHand.GetComponent<MeshRenderer>().enabled = true;
        sacketMagnetismAnimation.Play("Idle(Empty)");
        //sacketMagnetDatabase.Active = false;
        //sacketMagnetDatabase.ResetLists();
        //sacketMagnet.gameObject.SetActive(false);
    }
}
