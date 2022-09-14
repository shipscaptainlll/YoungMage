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
    [SerializeField] LayerMask checkLayer3;
    [SerializeField] LayerMask checkLayer4;
    [SerializeField] LayerMask checkLayer5;
    [SerializeField] CharacterOccupation _characterOccupation;
    [SerializeField] float gravity;
    [SerializeField] Transform caveBulpsHolder;
    bool isAutoRunning;

    [Header("Warp Base Settings")]
    [SerializeField] MiscPanel miscPanel;
    [SerializeField] Transform basePosition;
    Vector3 velocity;

    [Header("Settings Connection")]
    [SerializeField] ControlsPanel controlsPanel;

    [Header("Audio Connection")]
    [SerializeField] SoundManager soundManager;
    AudioSource walkingSound;
    AudioSource woodCreakSound;
    
    AudioSource runningSound;
    AudioSource shiftingSound;
    AudioSource walkingStairsSound;
    AudioSource runningStairsSound;
    AudioSource walkingBedSound;
    AudioSource jumpingBedSound;
    AudioSource doubleJumpingBedSound;
    AudioSource walkingStoneSound;
    AudioSource runningStoneSound;
    AudioSource woodLandNormalSound;
    AudioSource woodLandLoudSound;
    AudioSource stoneLandNormalSound;
    AudioSource stoneLandLoudSound;
    AudioSource startJumpSound;
    AudioSource startDoubleJumpSound;
    AudioSource landedChairSound;
    AudioSource landedTableSound;
    AudioSource exclaimingSound;

    Coroutine delayCreakCoroutine;
    Coroutine delayCaveBulpCoroutine;
    bool creakDelayElapsed;
    bool caveDelayElapsed;
    System.Random rand;

    float checkRadius;
    float stairsCheckRadius;
    bool occupied;
    VisualEffect movementVFX;
    VisualEffect jumpVFX;
    VisualEffect landVFX;
    [SerializeField] ParticleSystem doubleShiftPS;


    bool isWalking = false;
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

    bool jumped;
    bool doubleJumped;
    bool isGrounded;
    bool onStairs;
    bool onStone;
    bool onOtherGround;
    string currentObjectGround;

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
        creakDelayElapsed = true;
        caveDelayElapsed = true;
        rand = new System.Random();
        speed = basicSpeed;
        occupied = false;
        checkRadius = 0.5f;
        stairsCheckRadius = 1.3f;
        movementVFX = transform.Find("VFX").Find("vfxgraph_StylizedSmoke").GetComponent<VisualEffect>();
        jumpVFX = transform.Find("VFX").Find("vfxgraph_StylizedSmokeJump").GetComponent<VisualEffect>();
        landVFX = transform.Find("VFX").Find("vfxgraph_StylizedSmokeLand").GetComponent<VisualEffect>();
        _characterOccupation.CharacterEngagedSomething += PreventMoving;
        _characterOccupation.CharacterDisengagedSomething += EnableMoving;
        miscPanel.WarpBaseRequested += WarpBase;
        controlsPanel.autorunWasToggled += SetAutorun;
        isAutoRunning = controlsPanel.AutorunToggled;
        StartCoroutine(MovingDustSpawner());

        walkingSound = soundManager.FindSound("Walk");
        woodCreakSound = soundManager.FindSound("WoodSkreak");
        runningSound = soundManager.FindSound("Run");
        walkingStairsSound = soundManager.FindSound("StairsWalk");
        runningStairsSound = soundManager.FindSound("StairsRun");
        walkingBedSound = soundManager.FindSound("BedWalk");
        jumpingBedSound = soundManager.FindSound("BedJump");
        doubleJumpingBedSound = soundManager.FindSound("BedDoubleJump");
        walkingStoneSound = soundManager.FindSound("StoneWalk");
        runningStoneSound = soundManager.FindSound("StoneRun");
        woodLandNormalSound = soundManager.FindSound("WoodLandNormal");
        woodLandLoudSound = soundManager.FindSound("WoodLandLoud");
        stoneLandNormalSound = soundManager.FindSound("StoneLandNormal");
        stoneLandLoudSound = soundManager.FindSound("StoneLandLoud");
        startJumpSound = soundManager.FindSound("Jump");
        startDoubleJumpSound = soundManager.FindSound("DoubleJump");
        landedChairSound = soundManager.FindSound("Chair");
        landedTableSound = soundManager.FindSound("TableSlap");
        shiftingSound = soundManager.FindSound("Shift");
        exclaimingSound = soundManager.FindSound("YoungMageExclaiming");
        
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            
        }
        if (!occupied)
            MoveCharacter();
        if (isWalking && !onStone && !onStairs && !onOtherGround && isGrounded) { RandomWoodcreakInitiator(); if (!walkingSound.isPlaying) { walkingSound.Play(); } } else { if (walkingSound.isPlaying) { walkingSound.Stop(); } }
        if (isWalking && onStairs) { if (!walkingStairsSound.isPlaying) { walkingStairsSound.Play(); } } else { if (walkingStairsSound.isPlaying) { walkingStairsSound.Stop(); } }
        if (isRunning && !onStone && !onStairs && !onOtherGround && isGrounded) { if (!runningSound.isPlaying) { runningSound.Play(); } } else { if (runningSound.isPlaying) { runningSound.Stop(); } }
        if (isRunning && onStairs) { if (!runningStairsSound.isPlaying) { runningStairsSound.Play(); } } else { if (runningStairsSound.isPlaying) { runningStairsSound.Stop(); } }
        if (isWalking && onStone) { if (!walkingStoneSound.isPlaying) { walkingStoneSound.Play(); } } else { if (walkingStoneSound.isPlaying) { walkingStoneSound.Stop(); } }
        if (isRunning && onStone) { if (!runningStoneSound.isPlaying) { runningStoneSound.Play(); } } else { if (runningStoneSound.isPlaying) { runningStoneSound.Stop(); } }

        if ((isWalking && onStone) || (isRunning && onStone)) { RandomCavebulpInitiator(); }

        if (isWalking && onOtherGround && currentObjectGround == "Bed") { if (!walkingBedSound.isPlaying) { walkingBedSound.Play(); } } else { if (walkingBedSound.isPlaying) { walkingBedSound.Stop(); } }

    }

    void RandomWoodcreakInitiator()
    {
        if (creakDelayElapsed)
        {
            int randomNumber = rand.Next(1, 10);
            //Debug.Log("Creak random number: " + randomNumber);
            if (randomNumber > 8)
            {
                
                woodCreakSound.Play();
            }
            creakDelayElapsed = false;
            delayCreakCoroutine = StartCoroutine(DelayCreak());
        }
    }

    void RandomCavebulpInitiator()
    {
        if (caveDelayElapsed)
        {
            int randomNumber = rand.Next(1, 10);
            Debug.Log("Creak random number: " + randomNumber);
            if (randomNumber > 7)
            {
                Debug.Log("there: " + randomNumber);
                int randNumberBulps = rand.Next(0, caveBulpsHolder.childCount);
                caveBulpsHolder.GetChild(randNumberBulps).GetComponent<CaveSoundHolder>().PlaySound();
            }
            caveDelayElapsed = false;
            delayCaveBulpCoroutine = StartCoroutine(DelayBulp());
        }
    }

    IEnumerator DelayCreak()
    {
        yield return new WaitForSeconds(10);
        creakDelayElapsed = true;
    }

    IEnumerator DelayBulp()
    {
        yield return new WaitForSeconds(10);
        caveDelayElapsed = true;
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
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(xInput) > 0.2f || Mathf.Abs(zInput) > 0.2f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

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
                shiftingSound.Play();
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



        isGrounded = (Physics.CheckSphere(checkGround.position, checkRadius, checkLayer) || Physics.CheckSphere(checkGround.position, checkRadius, checkLayer2) 
            || Physics.CheckSphere(checkGround.position, stairsCheckRadius, checkLayer3) || Physics.CheckSphere(checkGround.position, checkRadius, checkLayer4)
            || Physics.CheckSphere(checkGround.position, checkRadius, checkLayer5));
        onStairs = (Physics.CheckSphere(checkGround.position, stairsCheckRadius, checkLayer3));
        onStone = (Physics.CheckSphere(checkGround.position, checkRadius, checkLayer4));
        onOtherGround = (Physics.CheckSphere(checkGround.position, checkRadius, checkLayer5));
        if (onOtherGround && currentObjectGround == null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(checkGround.position, checkRadius);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.transform.name == "Bed")
                {
                    currentObjectGround = "Bed";
                    return;
                }
                else
                {
                    currentObjectGround = null;
                }
            }
        }
        if (!onOtherGround && currentObjectGround != null) { currentObjectGround = null; }
        if (isGrounded && !isGroundedOld) 
        {
            landVFX.SendEvent("CharacterLanded");
            landVFX.SetVector3("SphereCenterPosition", landVFX.transform.position);
            Collider[] hitColliders = Physics.OverlapSphere(checkGround.position, checkRadius);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.transform.name == "chair")
                {
                    landedChairSound.Play();
                }
                else if (hitCollider.transform.name == "Table")
                {
                    landedTableSound.Play();
                }
                else if (hitCollider.transform.name == "Table")
                {
                    jumpingBedSound.Play();
                }
            }
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -60f;
            
            if (onStone)
            {
                if (!doubleJumped && jumped) { stoneLandNormalSound.Play(); } else if (doubleJumped) { stoneLandLoudSound.Play(); }
            } else if (!onStone && currentObjectGround == null)
            {
                if (!doubleJumped && jumped) { woodLandNormalSound.Play(); } else if (doubleJumped) { woodLandLoudSound.Play(); }
            } else if (currentObjectGround == "Bed")
            {
                Debug.Log(currentObjectGround);
                if (!doubleJumped && jumped) { jumpingBedSound.Play(); } else if (doubleJumped) { doubleJumpingBedSound.Play(); }
            }
            
            doubleJumped = false;
        }

        if (isGrounded && Input.GetButtonDown("Jump") || (!doubleJumped && !isGrounded && Input.GetButtonDown("Jump")))
        {
            jumps++;
            if (CharacterJumped != null) { CharacterJumped(jumps); }
            jumpVFX.SetVector3("SphereCenterPosition", jumpVFX.transform.position); 
            jumpVFX.SendEvent("CharacterJumped");
            jumped = true;
            //Debug.Log(jumped);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
            if (!isGrounded) { 
                doubleJumped = true;
                doubleJumps++;
                startDoubleJumpSound.Play();
                exclaimingSound.Play();
                if (CharacterDoubleJumped != null) { CharacterDoubleJumped(doubleJumps); }
            } else
            {
                startJumpSound.Play();
            }
        }

        if (isGrounded && velocity.y < 0 && jumped)
        {
            jumped = false;
            //Debug.Log(jumped);
            
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
