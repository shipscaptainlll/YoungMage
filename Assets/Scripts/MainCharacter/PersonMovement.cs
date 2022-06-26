using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class PersonMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform checkGround;
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    [SerializeField] LayerMask checkLayer;
    [SerializeField] LayerMask checkLayer2;
    [SerializeField] CharacterOccupation _characterOccupation;
    Vector3 velocity;
    [SerializeField] float gravity;
    float checkRadius;
    bool occupied;
    VisualEffect movementVFX;
    VisualEffect jumpVFX;
    VisualEffect landVFX;

    float xInput;
    float zInput;

    bool isGrounded;

    void Start()
    {
        
        occupied = false;
        checkRadius = 0.5f;
        movementVFX = transform.Find("VFX").Find("vfxgraph_StylizedSmoke").GetComponent<VisualEffect>();
        jumpVFX = transform.Find("VFX").Find("vfxgraph_StylizedSmokeJump").GetComponent<VisualEffect>();
        landVFX = transform.Find("VFX").Find("vfxgraph_StylizedSmokeLand").GetComponent<VisualEffect>();
        _characterOccupation.CharacterEngagedSomething += PreventMoving;
        _characterOccupation.CharacterDisengagedSomething += EnableMoving;
        StartCoroutine(MovingDustSpawner());
    }

    void Update()
    {
        if (!occupied)
        MoveCharacter();
    }

    

    void MoveCharacter()
    {
        bool isGroundedOld = isGrounded;
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * xInput + transform.forward * zInput;
        
        characterController.Move(move * speed * Time.deltaTime);

        isGrounded = (Physics.CheckSphere(checkGround.position, checkRadius, checkLayer) || Physics.CheckSphere(checkGround.position, checkRadius, checkLayer2));
        if (isGrounded && !isGroundedOld) {
            landVFX.SendEvent("CharacterLanded"); landVFX.SetVector3("SphereCenterPosition", landVFX.transform.position);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            jumpVFX.SetVector3("SphereCenterPosition", jumpVFX.transform.position); 
            jumpVFX.SendEvent("CharacterJumped");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    IEnumerator MovingDustSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (isGrounded && (Mathf.Abs(xInput) + Math.Abs(zInput)) > 0) { movementVFX.SendEvent("CharacterMoved"); }
        }
    }

    void PreventMoving()
    {
        occupied = true;
    }

    void EnableMoving()
    {
        occupied = false;
    }
}
