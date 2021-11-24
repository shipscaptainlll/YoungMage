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
        //transform.Rotate(0, 1, 0);
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
        if (Vector3.Distance(transform.position, targetOre.position) > 4)
        {
            float distancex = targetOre.position.x - transform.position.x;
            float distancez = targetOre.position.z - transform.position.z;
            Vector3 distance = new Vector3(distancex, 0, distancez);
            distance.Normalize();
            velocity = distance;
            characterController.Move(velocity * Time.deltaTime);

            Vector3 distanceAngles = targetOre.position - transform.position;
            float angle = Mathf.Atan2(distanceAngles.y, distanceAngles.x) * Mathf.Rad2Deg;
            Quaternion lookRotation = Quaternion.LookRotation(distanceAngles);
            Debug.Log(distanceAngles);
            lookRotation.x = 0; lookRotation.z = 0;
            
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Mathf.Clamp01(3.0f * Time.maximumDeltaTime));
            transform.Rotate(new Vector3(0, 90, 0));
        } else { ResetVelocity(); }
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
        if (Vector3.Distance(transform.position, targetMage.position) > 1)
        {
            float distancex = targetMage.position.x - transform.position.x;
            float distancez = targetMage.position.z - transform.position.z;
            Vector3 distance = new Vector3(distancex, 0, distancez);
            distance.Normalize();
            velocity = distance;
            characterController.Move(velocity * Time.deltaTime);

            Vector3 distanceAngles = (targetMage.position - transform.position).normalized;
            float angle = Mathf.Atan2(distanceAngles.y, distanceAngles.x) * Mathf.Rad2Deg;
            Quaternion lookRotation = Quaternion.LookRotation(distanceAngles);
            //Debug.Log(lookRotation);
            //Debug.Log(transform.rotation);
            lookRotation.x = 0; lookRotation.z = 0;
            //transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Mathf.Clamp01(3.0f * Time.maximumDeltaTime));
            if (cache == false)
            {
                cache = true;
                StartCoroutine(helloworld());
            }
            
            if (angle > 0)
            {
                //transform.Rotate(new Vector3(0, -0.1f, 0));
            } else if (angle < 0 ) { transform.Rotate(new Vector3(0, -0.1f, 0)); }
            
        }
        else { ResetVelocity(); }
    }

    IEnumerator helloworld()
    {
        float permit;
        Vector3 distanceAngles = (targetMage.position - transform.position).normalized;
        float angle = Mathf.Atan2(distanceAngles.y, distanceAngles.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.LookRotation(distanceAngles);
        lookRotation.x = 0; lookRotation.z = 0;
        while (Mathf.Abs((lookRotation.y - transform.rotation.y)) >= 0.05f)
        {
            Vector3 distanceAngles1 = (targetMage.position - transform.position).normalized;
            float angle1 = Mathf.Atan2(distanceAngles1.y, distanceAngles1.x) * Mathf.Rad2Deg;
            Quaternion lookRotation1 = Quaternion.LookRotation(distanceAngles1);
            lookRotation1.x = 0; lookRotation1.z = 0;
            
            if (Mathf.Abs(transform.rotation.y - lookRotation1.y) > Mathf.Abs(lookRotation1.y - transform.rotation.y))
            {
                permit = Mathf.Abs((transform.rotation.y - lookRotation1.y) / 200);
                Quaternion newOne = lookRotation1;
                newOne.y = -permit;
                lookRotation1.y += permit;
                transform.rotation *= newOne;
            } else { 
                permit = Mathf.Abs((transform.rotation.y - lookRotation1.y) / 200);
                Quaternion newOne = lookRotation1;
                newOne.y = permit;
                lookRotation1.y -= permit;
                transform.rotation *= newOne;
            }
            
            Debug.Log(permit);
            
            yield return new WaitForSeconds(0.002f);
        }
        
        
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
