using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshedSkeleton : MonoBehaviour
{
    [SerializeField] PotentialPositioner potentialPositioner;
    [SerializeField] Transform checkGround;
    [SerializeField] Transform checkGround1;
    [SerializeField] LayerMask checkGroundLayer;
    Vector3 velocity;
    float gravity;
    float checkRadius;
    bool isGrounded;
    bool isGrounded1;
    ControllerColliderHit controllerColliderHit;
    Transform targetOre;

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
    }

    public bool IsGrounded1
    {
        get
        {
            return isGrounded1;
        }
    }

    public bool IsCollisionTargetOre
    {
        get
        {
            if (controllerColliderHit != null && controllerColliderHit.collider.GetComponent<IOre>() != null && controllerColliderHit.collider.transform == targetOre)
            {
                return true;
            } else { return false; }
            
        }
    }

    public bool IsCollisionOre
    {
        get
        {
            if (controllerColliderHit != null && controllerColliderHit.collider.GetComponent<IOre>() != null && controllerColliderHit.collider.transform != targetOre)
            {
                return true;
            }
            else { return false; }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        potentialPositioner.DetectedPotentialOre += AssignTargetOre;
        potentialPositioner.UndetectedPotentialOre += UnassignTargetOre;
        gravity = -0.08f;
        checkRadius = 0.21f;
        targetOre = null;
    }

    // Update is called once per frame
    void Update()
    {
        Gravitation();
    }
    
    void AssignTargetOre(Transform targetOre)
    {
        this.targetOre = targetOre;
    }
    void UnassignTargetOre()
    {
        this.targetOre = null;
    }

    void Gravitation()
    {
        isGrounded = Physics.CheckSphere(checkGround.position, checkRadius, checkGroundLayer);
        isGrounded1 = Physics.CheckSphere(checkGround1.position, checkRadius + 1.0f, checkGroundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        if (isGrounded1)
        {
            transform.position += new Vector3(0, 0.1f, 0);
        }
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            transform.position += new Vector3(0, velocity.y, 0);
        }
        transform.GetComponent<CharacterController>().Move(new Vector3(0.001f, 0, 0));
        //transform.position *= velocity.y;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        controllerColliderHit = hit;
        if (hit.collider.transform != null && hit.collider.GetComponent<IOre>() != null)
        {
        }
    }
}
