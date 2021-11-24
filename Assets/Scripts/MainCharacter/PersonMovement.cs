using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform checkGround;
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    [SerializeField] LayerMask checkLayer;
    [SerializeField] LayerMask checkLayer2;
    Vector3 velocity;
    float gravity;
    float checkRadius;

    bool isGrounded;
    void Start()
    {
        gravity = -9.81f;
        checkRadius = 0.5f;
    }

    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        isGrounded = (Physics.CheckSphere(checkGround.position, checkRadius, checkLayer) || Physics.CheckSphere(checkGround.position, checkRadius, checkLayer2));

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}
