using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform characterBody;
    [SerializeField] float mouseSensitivity;
    [SerializeField] LayerMask currentObjectLayerMask;
    [SerializeField] LayerMask clickableLayerMask;
    [SerializeField] ClickManager ClickManager;
    [SerializeField] CursorManager cursorManager;
    [SerializeField] ObjectOutliner objectOutliner;
    [SerializeField] float contactingRayDistance;
    float yRotation;
    RaycastHit hit = new RaycastHit();

    public float YRotation { get { return yRotation; } }
    public RaycastHit ObservedObject
    {
        get
        {
            return hit;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        OnDrawGizmosSelected();
        StartCoroutine(SeeObject());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * 3), Color.red);
        if (!cursorManager.SomethingOpened)
        {
            RotateHead();
            DetectObject();
        }
        //
    }

    void RotateHead()
    {
        float xRot = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float yRot = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        yRotation -= yRot * 2;
        yRotation = Mathf.Clamp(yRotation, -90f, 50f);

        transform.localRotation = Quaternion.Euler(yRotation, 0, 0f);

        characterBody.Rotate(Vector3.up * xRot * 3);
    }

    

    void DetectObject()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * 3), Color.red);
        if (Physics.SphereCast(transform.position, 0.1f, transform.TransformDirection(Vector3.forward * contactingRayDistance), out hit, contactingRayDistance, currentObjectLayerMask))
        {
            
        }
        
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Debug.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward * contactingRayDistance));
        Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(Vector3.forward * contactingRayDistance), 0.1f);
    }

    IEnumerator SeeObject()
    {
        while (true)
        {
            if (Physics.SphereCast(transform.position, 0.1f, transform.TransformDirection(Vector3.forward * contactingRayDistance), out hit, contactingRayDistance, clickableLayerMask))
            {
                objectOutliner.StoreVewedObject(hit.transform); 
            } else { objectOutliner.StoreVewedObject(null);  }
            yield return new WaitForSeconds(0.033f);
        }        
    }
}
