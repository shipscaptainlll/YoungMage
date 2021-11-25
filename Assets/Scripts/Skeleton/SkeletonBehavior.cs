using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour
{
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask checkLayer;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform contactManager;
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
    string activity;
    bool cache = false;

    //cache
    [SerializeField] Transform targetMage1;

    void Start()
    {
        localAnimator = transform.GetComponent<Animator>();
        localAnimator.Play("SkeletonMine");
        gravity = -9.81f;
        checkRadius = 0;
        isIdle = true;
        contactManager.GetComponent<ContactManager>().SkeletonDetected += ConnectToMage;
        contactManager.GetComponent<ContactManager>().OreDetected += AddTarget;
        activity = "idle";
        Debug.Log(transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        
        BehaviorManager();
        Vision();
    }


    void BehaviorManager()
    {
        Gravitation();
        Vision();
        switch (activity)
        {
            case "Idle":
                break;
            case "ChasingMage":
                ChazeMage();
                break;
            case "ChasingOre":
                ChazeOre();
                break;
            case "GetRoundObject":
                GetRoundObject();
                break;
        }
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
        GoTo(targetOre, 4);
        TurnAroundTo(targetOre);
    }

    void ConnectToMage(Transform foundSkeleton, Transform mage)
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

    void ChazeMage()
    {
        GoTo(targetMage, 1);
        TurnAroundTo(targetMage);
    }

    void TurnAroundTo(Transform target)
    {
        Vector3 distanceAngles = (target.position - transform.position).normalized;
        float angleLangle = Vector3.Angle(distanceAngles, -transform.right);

        float angle = Mathf.Atan2(distanceAngles.y, distanceAngles.x) * Mathf.Rad2Deg;
        if (angleLangle >= 10)
        {
            if (Vector3.Cross(-transform.right, distanceAngles).y > 0)
            { transform.Rotate(new Vector3(0, angleLangle / 60, 0)); }
            else if (Vector3.Cross(-transform.right, distanceAngles).y < 0)
            {
                transform.Rotate(new Vector3(0, -angleLangle / 60, 0));
            }
        }
    }

    void GoTo(Transform target, float keptDistance)
    {
        if (Vector3.Distance(transform.position, targetMage.position) > keptDistance)
        {
            float distancex = target.position.x - transform.position.x;
            float distancez = target.position.z - transform.position.z;
            Vector3 distance = new Vector3(distancex, 0, distancez);
            distance.Normalize();
            velocity = distance;
            characterController.Move(velocity * Time.deltaTime);
        }
        else { ResetVelocity(); }
    }

    void GetRoundObject()
    {

    }

    void Gravitation()
    {
        isGrounded = Physics.CheckSphere(checkGround.position, checkRadius, checkLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    void Vision()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left * 3), Color.red);
        if (Physics.SphereCast(transform.position, 1.0f, transform.TransformDirection(Vector3.left * 3), out foundObject, 3f, targetLayerMask)) {
            Debug.Log(foundObject.transform);
        }
    }

    void ResetVelocity()
    {
        velocity = new Vector3(0, 0, 0);
    }
}
