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
        StartCoroutine(SeeObject());
    }

    // Update is called once per frame
    void Update()
    {
        if (!cursorManager.SomethingOpened)
        {
            RotateHead();
            //DetectObject();
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
        if (Physics.SphereCast(transform.position, 0.1f, transform.TransformDirection(Vector3.forward * 3), out hit, 3f, clickableLayerMask))
        {
            
        }

    }

    IEnumerator SeeObject()
    {
        while (true)
        {
            if (Physics.SphereCast(transform.position, 0.1f, transform.TransformDirection(Vector3.forward * 3), out hit, 3f, clickableLayerMask))
            {
                objectOutliner.StoreVewedObject(hit.transform); 
            } else { objectOutliner.StoreVewedObject(null);  }
            yield return new WaitForSeconds(0.033f);
        }        
    }
}
