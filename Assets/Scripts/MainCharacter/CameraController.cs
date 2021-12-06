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
    [SerializeField] ClickManager ClickManager;
    float yRotation;
    RaycastHit hit = new RaycastHit();

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
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        RotateHead();
        DetectObject();
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

        if (Physics.SphereCast(transform.position, 1.2f, transform.TransformDirection(Vector3.forward * 3), out hit, 100f, currentObjectLayerMask)) {
            //Debug.Log(hit.transform);
        }
    }
}
