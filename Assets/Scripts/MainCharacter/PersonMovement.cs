using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class PersonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform checkGround;
    [SerializeField] float basicSpeed;
    float speed;
    [SerializeField] float jumpHeight;
    [SerializeField] LayerMask checkLayer;
    [SerializeField] LayerMask checkLayer2;
    [SerializeField] CharacterOccupation _characterOccupation;
    [SerializeField] float gravity;
    bool isAutoRunning;

    [Header("Warp Base Settings")]
    [SerializeField] MiscPanel miscPanel;
    [SerializeField] Transform basePosition;
    Vector3 velocity;

    [Header("Settings Connection")]
    [SerializeField] ControlsPanel controlsPanel;

    float checkRadius;
    bool occupied;
    VisualEffect movementVFX;
    VisualEffect jumpVFX;
    VisualEffect landVFX;
    [SerializeField] ParticleSystem doubleShiftPS;


    bool isWalking = true;
    bool isRunning;
    float keyPushedLength;
    bool isShifting;
    bool doubleshiftCooldowned;
    float floatSteps;
    int steps;
    int jumps;
    int doubleJumps;
    int shifts;

    float floatRunningSteps;
    int runningSteps;

    float xInput;
    float zInput;

    bool doubleJumped;
    bool isGrounded;

    float timeShiftPressed;

    public float Speed { get { return speed; } }
    public bool IsAutoRunning { get { return isAutoRunning; } set { isAutoRunning = value; } }
    public int ProgressParameter { get { return steps; } }
    public int ProgressParameterSecond { get { return runningSteps; } }
    public int ProgressParameterThird { get { return shifts; } }
    public int ProgressParameterFourth { get { return jumps; } }
    public int ProgressParameterFifth { get { return doubleJumps; } }

    
    
    public event Action<int> CharacterStepMade = delegate { };
    public event Action<int> CharacterRunnedStep = delegate { };
    public event Action<int> CharacterShifted = delegate { };
    public event Action<int> CharacterJumped = delegate { };
    public event Action<int> CharacterDoubleJumped = delegate { };
    void Start()
    {
        speed = basicSpeed;
        occupied = false;
        checkRadius = 0.5f;
        movementVFX = transform.Find("VFX").Find("vfxgraph_StylizedSmoke").GetComponent<VisualEffect>();
        jumpVFX = transform.Find("VFX").Find("vfxgraph_StylizedSmokeJump").GetComponent<VisualEffect>();
        landVFX = transform.Find("VFX").Find("vfxgraph_StylizedSmokeLand").GetComponent<VisualEffect>();
        _characterOccupation.CharacterEngagedSomething += PreventMoving;
        _characterOccupation.CharacterDisengagedSomething += EnableMoving;
        miscPanel.WarpBaseRequested += WarpBase;
        controlsPanel.autorunWasToggled += SetAutorun;
        isAutoRunning = controlsPanel.AutorunToggled;
        StartCoroutine(MovingDustSpawner());
    }

    void LateUpdate()
    {
        if (!occupied)
            MoveCharacter();
    }

    IEnumerator DoubleShiftTimer()
    {
        yield return new WaitForSeconds(0.12f);
        doubleShiftPS.Stop();
        yield return new WaitForSeconds(0.20f);
        
        isShifting = false;
        speed = basicSpeed * 1;
        
        
        //doubleShiftPS.gameObject.SetActive(false);
    }

    IEnumerator CooldownDoubleshift()
    {
        doubleshiftCooldowned = true;
        yield return new WaitForSeconds(0.45f);
        doubleshiftCooldowned = false;
    }

    void MoveCharacter()
    {
        if ((!isShifting && Input.GetKey(KeyCode.LeftShift)) || isAutoRunning && !isShifting)
        {
            keyPushedLength += Time.deltaTime;
            {
                if (keyPushedLength > 0.25f)
                {
                    //Debug.Log("pressed shift");
                    speed = basicSpeed * 1.45f;
                    isWalking = false; isRunning = true;
                }
            }
        }
        if (!isShifting && Input.GetKeyUp(KeyCode.LeftShift) && !isAutoRunning)
        {
            speed = basicSpeed * 1;
            isWalking = true; isRunning = false;
            keyPushedLength = 0;
        }
        bool isGroundedOld = isGrounded;
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        if (!doubleshiftCooldowned && Input.GetKeyDown(KeyCode.LeftShift))
        {
            var timeCurrentPressed = Time.time;
            if (timeCurrentPressed - timeShiftPressed < 0.30f)
            {
                doubleShiftPS.gameObject.SetActive(true);
                doubleShiftPS.Play();
                speed = basicSpeed * 4.55f;
                isShifting = true;
                shifts++;
                if (CharacterShifted != null) { CharacterShifted(shifts); }
                StartCoroutine(DoubleShiftTimer());
                StartCoroutine(CooldownDoubleshift());
            }
            timeShiftPressed = Time.time;
        }

        if (isWalking)
        {
            floatSteps += (Mathf.Abs(zInput) * Time.deltaTime * 2) + (Mathf.Abs(xInput) * Time.deltaTime * 2);
            int newSteps = (int)floatSteps;
            if (steps != newSteps)
            {
                steps = newSteps;
                if (CharacterStepMade != null) { CharacterStepMade(steps); }
            }
        }

        if (isRunning)
        {
            floatRunningSteps += (Mathf.Abs(zInput) * Time.deltaTime * 2) + (Mathf.Abs(xInput) * Time.deltaTime * 2);
            int newSteps = (int)floatRunningSteps;
            if (runningSteps != newSteps)
            {
                runningSteps = newSteps;
                if (CharacterRunnedStep != null) { CharacterRunnedStep(runningSteps); }
            }
        }

        Vector3 move = transform.right * xInput + transform.forward * zInput;

        Vector3 impact = Vector3.Lerp(move, Vector3.zero, 5 * Time.deltaTime);
        characterController.Move(move * speed * Time.deltaTime);



        isGrounded = (Physics.CheckSphere(checkGround.position, checkRadius, checkLayer) || Physics.CheckSphere(checkGround.position, checkRadius, checkLayer2));
        if (isGrounded && !isGroundedOld) {
            landVFX.SendEvent("CharacterLanded");
            landVFX.SetVector3("SphereCenterPosition", landVFX.transform.position);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            doubleJumped = false;
        }

        if (isGrounded && Input.GetButtonDown("Jump") || (!doubleJumped && !isGrounded && Input.GetButtonDown("Jump")))
        {
            jumps++;
            if (CharacterJumped != null) { CharacterJumped(jumps); }
            jumpVFX.SetVector3("SphereCenterPosition", jumpVFX.transform.position); 
            jumpVFX.SendEvent("CharacterJumped");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            if (!isGrounded) { 
                doubleJumped = true;
                doubleJumps++;
                if (CharacterDoubleJumped != null) { CharacterDoubleJumped(doubleJumps); }
            }
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

    void SetAutorun(bool newAutorunStatus)
    {
        isAutoRunning = newAutorunStatus;
    }

    void PreventMoving()
    {
        occupied = true;
    }

    void EnableMoving()
    {
        occupied = false;
    }

    void WarpBase()
    {
        characterController.enabled = false;
        transform.position = basePosition.position;
        transform.rotation = basePosition.rotation;
        characterController.enabled = true;
        Debug.Log("Warped back to base");
    }
}
