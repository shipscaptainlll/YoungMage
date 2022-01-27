using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonBehavior : MonoBehaviour
{
    [SerializeField] Transform connectedTeleport;
    [SerializeField] Transform checkGround;
    [SerializeField] Transform checkSurrounding;
    [SerializeField] LayerMask checkSurroundingLayer;
    [SerializeField] LayerMask checkGroundLayer;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform contactManager;
    [SerializeField] Transform LassoInvoker;
    [SerializeField] StoneOreCounter stoneOreCounter;
    [SerializeField] MetalOreCounter metalOreCounter;
    [SerializeField] CursedOreCounter cursedOreCounter;
    [SerializeField] EarthstoneOreCounter earthstoneOreCounter;
    [SerializeField] LavastoneOreCounter lavastoneOreCounter;
    [SerializeField] MagicstoneOreCounter magicstoneOreCounter;
    [SerializeField] WaterstoneOreCounter waterstoneOreCounter;
    [SerializeField] WindstoneOreCounter windstoneOreCounter;
    Animator localAnimator;
    Vector3 velocity;
    RaycastHit foundObject = new RaycastHit();
    Transform targetMage;
    Transform targetOre;
    Transform visibleObject;
    float gravity;
    float checkRadius;
    bool isGrounded;
    bool isIdle;
    bool isTeleported = false;
    bool chasingPortal = false;
    bool rotatedEnough = false;
    string activity;
    bool cache = false;
    float speed = 3f;

    //cache
    [SerializeField] Transform targetMage1;

    void Start()
    {
        localAnimator = transform.GetComponent<Animator>();

        gravity = -9.81f;
        checkRadius = 0;
        isIdle = true;

        contactManager.GetComponent<ContactManager>().OreDetected += AddTarget;
        activity = "idle";
        connectedTeleport.GetComponent<Teleporter>().TeleportFound += StopGravity;
    }

    public string Activity
    {
        get
        {
            return activity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //-transform.forward equals left

        BehaviorManager();
        Vision();
        GoAroundSurroundings();
    }


    void BehaviorManager()
    {
        if (!isTeleported)
        {
            Gravitation();
        }
        Vision();
        switch (activity)
        {
            case "Idle":
                StayStill();
                break;
            case "ChasingMage":
                ChazeMage();
                break;
            case "ChasingOre":
                ChazeOre();
                break;
            case "MineOre":
                MineOre();
                break;
        }
    }

    void StayStill()
    {
        localAnimator.Play("SkelIdle");
    }

    public void AddTarget(Transform targetOre)
    {
        if (activity == "ChasingMage")
        {
            this.targetOre = targetOre;
            activity = "ChasingOre";
        }
    }

    void ChazeOre()
    {
        float keptDistance = 2.85f;
        GoTo(targetOre, keptDistance);
        TurnAroundTo(targetOre);
        if (Vector3.Distance(transform.position, targetOre.position) <= keptDistance)
        {
            ResetVelocity();
            activity = "MineOre";
        }
    }

    public void ConnectToMage(Transform foundSkeleton, Transform mage)
    {
        if (activity != "ChasingMage")
        {
            if (transform == foundSkeleton)
            {
                activity = "ChasingMage";
                targetMage = mage;
            }
            else { }
        } else if (activity == "ChasingMage")
        {
            ResetVelocity();
            activity = "Idle";
        }
    }

    public void StopGravity()
    {
        if (activity == "ChasingMage")
        {
            ResetVelocity();
            activity = "Idle";
        }
        isTeleported = true;
    }

    void ChazeMage()
    {
        GoTo(targetMage, 4f);
        TurnAroundTo(targetMage);
    }

    public void ChazePortal(Transform targetPortal)
    {
        Transform neededPortal = targetPortal;
        if (!chasingPortal)
        {
            TurnAroundTo(neededPortal);
            if (rotatedEnough)
            {
                GoTo(targetPortal, 0f);
            }
            //
            
            //chasingPortal = true;
        }
        
    }

    void MineOre()
    {
        TurnAroundTo(targetOre);
        localAnimator.Play("SkelMine");
    }

    void TurnAroundTo(Transform target)
    {
        Vector3 distanceAngles = (target.position - transform.position).normalized;
        float angleLangle = Vector3.Angle(distanceAngles, transform.forward);

        float angle = Mathf.Atan2(distanceAngles.y, distanceAngles.x) * Mathf.Rad2Deg;
        if (angleLangle >= 10)
        {
            if (Vector3.Cross(transform.forward, distanceAngles).y > 0)
            { transform.Rotate(new Vector3(0, angleLangle / 60, 0)); }
            else if (Vector3.Cross(transform.forward, distanceAngles).y < 0)
            {
                transform.Rotate(new Vector3(0, -angleLangle / 60, 0));
            }
            Debug.Log(angleLangle);
            if (Mathf.Abs(angleLangle) <= 60)
            {
                rotatedEnough = true;
            }
            
        }
    }

    void GoTo(Transform target, float keptDistance)
    {
        if (Vector3.Distance(transform.position, target.position) > keptDistance)
        {
            Debug.Log(Vector3.Distance(transform.position, target.position));
            float distancex = target.position.x - transform.position.x;
            float distancez = target.position.z - transform.position.z;
            Vector3 distance = new Vector3(distancex, 0, distancez);
            //Debug.Log(Vector3.Distance(transform.position, targetMage.position));
            distance.Normalize();
            velocity = distance;
            characterController.Move(velocity * Time.deltaTime * speed);
            localAnimator.Play("SkelMove");
        }
        else { ResetVelocity();
            StayStill(); 
        }
    }

    void Gravitation()
    {
        isGrounded = Physics.CheckSphere(checkGround.position, checkRadius, checkGroundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    void GoAroundSurroundings()
    {
        bool isCollided = Physics.CheckSphere(checkSurrounding.position, 4f, checkSurroundingLayer);
        Collider[] hitCollider = Physics.OverlapSphere(checkSurrounding.position, 4f, checkSurroundingLayer);
        if (isCollided == true && hitCollider[0].transform != targetOre)
        {
            Vector3 disctanceVector = checkSurrounding.position - hitCollider[0].transform.position;
            float distanceBetweenX = checkSurrounding.position.x - hitCollider[0].transform.position.x;
            float distanceBetweenZ = checkSurrounding.position.z - hitCollider[0].transform.position.z;
            float distance = Mathf.Sqrt(distanceBetweenX * distanceBetweenX + distanceBetweenZ * distanceBetweenZ);
            float coefficient = 50f / (distance * distance);
            if (coefficient >= 3) { coefficient = 3; }
            if (Vector3.Cross(transform.forward, disctanceVector).y > 0)
            {
                characterController.Move(transform.right * coefficient * Time.deltaTime);
            } else if (Vector3.Cross(transform.forward, disctanceVector).y <= 0)
            {
                characterController.Move(-transform.right * coefficient * Time.deltaTime);
            }
            
        }
    }

    void Vision()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * 3), Color.red);
        if (Physics.SphereCast(transform.position, 1.0f, transform.TransformDirection(Vector3.forward * 3), out foundObject, 3f, targetLayerMask)) {
            
        }
    }

    void ResetVelocity()
    {
        velocity = new Vector3(0, 0, 0);
    }

    void HitOre()
    {
        switch (targetOre.GetComponent<IOre>().Type)
        {
            case "RockOre":
                stoneOreCounter.AddResource(1);
                break;
            case "MetalOre":
                metalOreCounter.AddResource(1);
                break;
            case "CursedOre":
                cursedOreCounter.AddResource(1);
                break;
            case "EarthstoneOre":
                earthstoneOreCounter.AddResource(1);
                break;
            case "LavastoneOre":
                lavastoneOreCounter.AddResource(1);
                break;
            case "MagicstoneOre":
                magicstoneOreCounter.AddResource(1);
                break;
            case "WaterstoneOre":
                waterstoneOreCounter.AddResource(1);
                break;
            case "WindstoneOre":
                windstoneOreCounter.AddResource(1);
                break;
        }
    }
}
