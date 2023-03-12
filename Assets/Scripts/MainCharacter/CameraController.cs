using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] PersonMovement personMovement;
    [SerializeField] CameraShake cameraShake;
    [SerializeField] Transform ladder;
    bool tutorialModeActivated;
    float yRotation;
    float xRotation;
    RaycastHit hit = new RaycastHit();
    RaycastHit hitThird = new RaycastHit();
    bool isOnStairs;
    bool upperStairs;

    Vector3 startRotation;
    bool cityRegenerationMode;
    bool introMode;
    bool seeingTutorial;
    
    public bool IntroMode { get { return introMode; } set { introMode = value; } }
    public bool TutorialModeActivated { get { return tutorialModeActivated; } set { tutorialModeActivated = value; } }
    public bool CityRegenerationMode { get { return cityRegenerationMode; } set { startRotation = transform.localRotation.eulerAngles;  cityRegenerationMode = value; yRotation = startRotation.y; xRotation = startRotation.x; } }
    Vector3 StartRotation { get { return startRotation; } set { startRotation = value; } }
    public float YRotation { get { return yRotation; } set { yRotation = value; } }
    public bool IsOnStairs { get { return isOnStairs; } set { isOnStairs = value; } }
    public bool UpperStairs { get { return upperStairs; } set { upperStairs = value; } }
    public bool SeeingTutorial { get { return seeingTutorial; } }
    public RaycastHit ObservedObject
    {
        get
        {
            return hit;
        }
    }
    public RaycastHit HitThird { get { return hitThird; } }
    // Start is called before the first frame update
    void Awake()
    {
        
        StartCoroutine(SeeObject());
        OnDrawGizmosSelected();
    }

    public void ReinitialiseAfterLoading()
    {
        //Debug.Log("was reloaded, but something is off");
        StartCoroutine(SeeObject());
        OnDrawGizmosSelected();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Debug.Log("tutorialModeActivated " + tutorialModeActivated);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * 3), Color.red);
        if (!cursorManager.SomethingOpened && !tutorialModeActivated)
        {
            RotateHead();
            DetectObject();
        }
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            
        }
    }

    void RotateHead()
    {
        if (introMode)
        {
            return;
            
        }
        
        float xRot = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float yRot = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        if (isOnStairs)
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = (ladder.position - transform.position).normalized;

            // The step size is equal to speed times frame time.
            float singleStep = 1 * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            var newDirection = Quaternion.LookRotation(targetDirection);

            
            xRot -= Input.GetAxis("Vertical") * 0.1f * personMovement.Speed;
            transform.RotateAround(ladder.transform.position, Vector3.up, xRot * Time.deltaTime);
            /*
            if (upperStairs)
            {
                xRot += -0.01f * Input.GetAxis("Vertical") * Time.deltaTime * mouseSensitivity * personMovement.Speed;
            } else
            {
                xRot += 0.01f * Input.GetAxis("Vertical") * Time.deltaTime * mouseSensitivity * personMovement.Speed;
            }
            */

        }
       
        
        
        if (!cityRegenerationMode)
        {
            yRotation -= yRot * 2;
            xRotation += xRot;
            yRotation = Mathf.Clamp(yRotation, -90f, 50f);
            //yRotation = Mathf.Clamp(yRotation, -90f, 50f);
            transform.localRotation = Quaternion.Euler(yRotation, 0, 0f);

            characterBody.Rotate(Vector3.up * xRot * 3);
        } else
        {
            yRotation += xRot * 0.25f;
            xRotation -= yRot * 0.25f;
            //Debug.Log(startRotation);
            yRotation = Mathf.Clamp(yRotation, startRotation.y - 6.5f, startRotation.y + 6.5f);
            xRotation = Mathf.Clamp(xRotation, startRotation.x - 6.5f, startRotation.x + 6.5f);
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, startRotation.z);
            //Debug.Log(transform.localRotation.eulerAngles);
        }




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
        //Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(Vector3.forward * contactingRayDistance), 0.1f);
    }

    IEnumerator SeeObject()
    {

        while (true)
        {
            //Debug.Log("working");
            if (Physics.SphereCast(transform.position, 0.1f, transform.TransformDirection(Vector3.forward * contactingRayDistance), out hitThird, contactingRayDistance, clickableLayerMask))
            {
                if (hitThird.transform.gameObject.layer == 12)
                {
                    //Debug.Log(hitThird.transform + "here there");
                    objectOutliner.StoreVewedObject(hitThird.transform);
                    seeingTutorial = false;
                } else if (hitThird.transform.gameObject.layer == 3)
                {
                    objectOutliner.StoreVewedObject(null);
                    seeingTutorial = true;
                    //Debug.Log(hitThird.transform + " found this one");
                }
                
            } else { objectOutliner.StoreVewedObject(null);
                seeingTutorial = false;
            }
            yield return new WaitForSeconds(0.033f);
        }        
    }


}
