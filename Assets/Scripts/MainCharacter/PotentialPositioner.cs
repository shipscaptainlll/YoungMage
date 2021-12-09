using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentialPositioner : MonoBehaviour
{
    [SerializeField] ContactManager contactManager;
    [SerializeField] CameraController cameraController;
    [SerializeField] Transform skeletonMeshed;
    bool isActive;
    int angle;
    float xRandPos;
    float yRandPos;
    float yHeight;
    float radius;
    bool positionFound;

    System.Random random;
    
    public bool IsActive
    {
        get
        {
            return isActive;
        }
    }

    public event Action<Transform> DetectedPotentialOre = delegate { };
    public event Action UndetectedPotentialOre = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        radius = 1;
        angle = 0;
        yHeight = -0.25f;
        contactManager.PotentialPositionerActivated += Activate;
        contactManager.PotentialPositionerDeactivated += Deactivate;
        isActive = false;
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            PopPotentialPositionCache();
        }
        
    }

    void Activate()
    {
        isActive = true;
    }

    void Deactivate()
    {
        isActive = false;
        skeletonMeshed.gameObject.SetActive(false);
    }

    void PopPotentialPositionCache()
    {

        if (cameraController.ObservedObject.transform != null && cameraController.ObservedObject.transform.GetComponent<IOre>() != null)
        {
            
            skeletonMeshed.gameObject.SetActive(true);
            if (skeletonMeshed.GetComponent<MeshedSkeleton>().IsGrounded)
            {
                yHeight -= 0.1f;
            }
            if (skeletonMeshed.GetComponent<MeshedSkeleton>().IsGrounded1)
            {
                yHeight += 0.1f;
            }
            
            TurnAroundTo(cameraController.ObservedObject.transform, skeletonMeshed);
            skeletonMeshed.position = cameraController.ObservedObject.transform.Find("SkeletonPosition").position + new Vector3(0,0,0);
            Debug.Log("hello");
        }
        else
        {
            yHeight = -0.25f;
        }
    }

    void PopPotentialPosition()
    {
        
        if (cameraController.ObservedObject.transform != null && cameraController.ObservedObject.transform.GetComponent<IOre>() != null)
        {
            if (DetectedPotentialOre != null)
            {
                DetectedPotentialOre(cameraController.ObservedObject.transform);
            }
            skeletonMeshed.gameObject.SetActive(true);
            RandomizePosition();
            if (skeletonMeshed.GetComponent<MeshedSkeleton>().IsGrounded)
            {
                yHeight -= 0.1f;
            }
            if (skeletonMeshed.GetComponent<MeshedSkeleton>().IsGrounded1)
            {
                yHeight += 0.1f;
            }
            if (skeletonMeshed.GetComponent<MeshedSkeleton>().IsCollisionTargetOre)
            {
                radius += 0.05f;
            }
            skeletonMeshed.position = cameraController.ObservedObject.transform.position + cameraController.ObservedObject.transform.forward + new Vector3(xRandPos, yHeight, yRandPos);
            TurnAroundTo(cameraController.ObservedObject.transform, skeletonMeshed);
            
            
        } else
        {
            radius = 0.5f;
            yHeight = -0.25f;
            if (UndetectedPotentialOre != null)
            {
                UndetectedPotentialOre();
            }
        }
    }

    void RandomizePosition()
    {
        
        angle++;
        if (angle > 360)
        {
            angle = 0;
        }
        xRandPos = radius * Mathf.Cos(angle * Mathf.PI / 180);
        yRandPos = Mathf.Sqrt(radius * radius - xRandPos * xRandPos);
        
        if (angle > 180)
        {
            yRandPos = -yRandPos;
        }
    }

    void TurnAroundTo(Transform target, Transform skeleton)
    {
        Vector3 distanceAngles = (target.position - skeleton.position).normalized;
        float angleLangle = Vector3.Angle(distanceAngles, -skeleton.right);

        float angle = Mathf.Atan2(distanceAngles.y, distanceAngles.x) * Mathf.Rad2Deg;
        if (angleLangle >= 90 && Mathf.Abs((angleLangle - 90)) > 25)
        {
            skeleton.Rotate(new Vector3(0, 0, 22.5f));
        }
        else if (angleLangle <= 90)
        {
            skeleton.Rotate(new Vector3(0, 0, -22.5f));
        }
        if (angleLangle >= 90 && Mathf.Abs((angleLangle - 90)) > 15)
        {
            skeleton.Rotate(new Vector3(0, 0, 12.5f));
        }
        else if (angleLangle <= 90)
        {
            skeleton.Rotate(new Vector3(0, 0, -12.5f));
        }
        if (angleLangle >= 90 && Mathf.Abs((angleLangle - 90)) > 5)
        {
            skeleton.Rotate(new Vector3(0, 0, 2.5f));
        }
        else if (angleLangle <= 90)
        {
            skeleton.Rotate(new Vector3(0, 0, -2.5f));
        }
    }
}
