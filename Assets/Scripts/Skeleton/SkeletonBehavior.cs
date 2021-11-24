using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour
{
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask checkLayer;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] CharacterController characterController;
    Animator localAnimator;
    Vector3 velocity;
    RaycastHit foundObject = new RaycastHit();
    Transform targetOre;
    float gravity;
    float checkRadius;
    bool isGrounded;
    
    void Start()
    {
        localAnimator = transform.GetComponent<Animator>();
        localAnimator.Play("SkeletonMine");
        gravity = -9.81f;
        checkRadius = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Gravitation();
        Vision();
        ChazeOre(); 
        
    }

    public void AddTarget(Transform targetOre)
    {
        this.targetOre = targetOre;
        Debug.Log(targetOre);
    }

    void ChazeOre()
    {
        if (Vector3.Distance(transform.position, targetOre.position) > 8)
        {
            Vector3 distance = targetOre.position - transform.position;
            distance.Normalize();
            velocity = distance;
            characterController.Move(velocity * Time.deltaTime);
            Debug.Log(Vector3.Distance(transform.position, targetOre.position));
        } else { velocity = new Vector3(0, 0, 0); }
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
}
