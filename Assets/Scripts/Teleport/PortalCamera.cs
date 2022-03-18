using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] Transform mage;
    RaycastHit hit = new RaycastHit();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * 10), Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward * 10), out hit, 10f, targetLayer))
        {
            Debug.Log(hit.transform.parent.GetComponent<SkeletonBehavior>().Activity);
            if (hit.transform.parent.GetComponent<SkeletonBehavior>() != null && hit.transform.parent.GetComponent<SkeletonBehavior>().Activity == "idle")
            {
                Debug.Log("Hello:");
                hit.transform.parent.GetComponent<SkeletonBehavior>().StartChazingPortal(mage);
            }
        }
        
        if (Physics.SphereCast(transform.position, 0.1f, transform.TransformDirection(Vector3.forward * 3), out hit, 3f, clickableLayerMask))
        {
            objectOutliner.StoreVewedObject(hit.transform);
        }
        */
    }
}
