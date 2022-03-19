using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonBehavior : MonoBehaviour
{
    [SerializeField] int ID;
    [SerializeField] bool hasPortal;
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
    Transform castlePosition;
    Animator localAnimator;
    Vector3 velocity;
    RaycastHit foundObject = new RaycastHit();
    Transform targetMage;
    Transform targetOre;
    Transform visibleObject;
    Transform targetPortal;
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
    bool reachedPosition;

    public bool ReachedPosition { get { return reachedPosition; } }
    //cache
    [SerializeField] Transform targetMage1;
    public Transform CastlePosition { set { castlePosition = value; } }
    public event Action<Transform> OriginRotated = delegate { };
    void Start()
    {
        localAnimator = transform.GetComponent<Animator>();

        gravity = -9.81f;
        checkRadius = 0;
        isIdle = true;

        contactManager.GetComponent<ContactManager>().OreDetected += AddTarget;
        //activity = "idle";
        //Debug.Log("Start " + transform);
        connectedTeleport.GetComponent<Teleporter>().TeleportFound += StopGravity;
        if (hasPortal)
        {
            transform.GetComponent<CopycatCreator>().OriginTeleported += StopActivities;
        }
        //castlePosition = transform.parent.parent.Find("CastlePosition");
    }

    public bool IsTeleported
    {
        get
        {
            return isTeleported;
        }
        set
        {
            isTeleported = value;
        }
    }

    public string Activity
    {
        get
        {
            return activity;
        }
        set
        {
            activity = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //-transform.forward equals left
        if (ID == 8)
        {
            //Debug.Log("is teleported " + isTeleported);
            //Debug.Log("activity " + activity);
        }
        //Debug.Log(activity);
        BehaviorManager();
        Vision();
        GoAroundSurroundings();
        //Debug.Log(activity);
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
            case "NearCastle":
                StayNearCastle();
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
            case "ChazePortal":
                ChazePortal();
                break;
            case "ChasingPosition":
                ChazePosition();
                break;
            case "TurningToCastle":
                TurningToCastle();
                break;
        }
    }

    void StayStill()
    {
        localAnimator.Play("SkelIdle");
    }

    void StayNearCastle()
    {
        localAnimator.Play("SkelShakeHand");
    }

    public void StopActivities()
    {
        
        isTeleported = true;
        ResetVelocity();
        activity = "Idle";
        //Debug.Log("StopActivities");
        StartCoroutine(returnGravity());
    }

    IEnumerator returnGravity()
    {
        yield return new WaitForSeconds(0.01f);
        isTeleported = false;
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
            //Debug.Log("ConnectToMage");
        }
    }

    public void ConnectToPosition(Transform position)
    {
        //Debug.Log(activity);
        if (activity != "ChasingPosition")
        {
            activity = "ChasingPosition";
            targetMage = position;
            //Debug.Log("hello");
        }
        //Debug.Log(activity);
    }

    void ChazePosition()
    {
        
        GoTo(targetMage, 4f);
        if (Vector3.Distance(transform.position, targetMage.position) < 4f) { reachedPosition = true; StopActivities(); StayStill(); activity = "TurningToCastle"; }
        TurnAroundTo(targetMage);
    }

    void TurningToCastle()
    {
        //Debug.Log(castlePosition);
        TurnAroundTo(castlePosition);
        StartCoroutine(Delay(1.5f));
    }

    public void StopGravity()
    {
        if (activity == "ChasingMage")
        {
            ResetVelocity();
            activity = "Idle";
            Debug.Log("StopGravity");
        }
        isTeleported = true;
    }

    void ChazeMage()
    {
        GoTo(targetMage, 4f);
        Debug.Log("chasing mage");
        TurnAroundTo(targetMage);
    }

    public void StartChazingPortal(Transform foundPortal)
    {
        targetPortal = foundPortal;
        activity = "ChazePortal";
    }

    public void ChazePortal()
    {
        if (activity == "ChazePortal")
        {
            //Debug.Log("still working");
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
        } else { }
        
        
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
            { 
                transform.Rotate(new Vector3(0, angleLangle / 60, 0));
            }
            else if (Vector3.Cross(transform.forward, distanceAngles).y < 0)
            {
                transform.Rotate(new Vector3(0, -angleLangle / 60, 0));
            }
            //Debug.Log(angleLangle);
            if (Mathf.Abs(angleLangle) <= 60)
            {
                rotatedEnough = true;
            }
        }
    }

    public void GoTo(Transform target, float keptDistance)
    {
        if (Vector3.Distance(transform.position, target.position) > keptDistance)
        {
            //Debug.Log(activity + " " + transform);
            //Debug.Log(Vector3.Distance(transform.position, target.position));
            float distancex = target.position.x - transform.position.x;
            float distancez = target.position.z - transform.position.z;
            Vector3 distance = new Vector3(distancex, 0, distancez);
            //Debug.Log(Vector3.Distance(transform.position, targetMage.position));
            
            distance.Normalize();
            //velocity = distance;
            velocity.x = distance.x;
            velocity.z = distance.z;
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
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * 3), Color.red);
        if (Physics.SphereCast(transform.position, 1.0f, transform.TransformDirection(Vector3.forward * 3), out foundObject, 3f, targetLayerMask)) {
            
        }
    }

    void ResetVelocity()
    {
        velocity = new Vector3(0, 0, 0);
    }

    IEnumerator Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StopActivities(); activity = "NearCastle";
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
