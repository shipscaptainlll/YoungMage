using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.VFX;

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
    [SerializeField] Transform player;
    [SerializeField] Transform CastleNavroutHolder;
    [SerializeField] CastlePositionsManager castlePositionsManager;
    [SerializeField] ContactedSkeletonsCounter contactedSkeletonsCounter;
    [SerializeField] DestroyedSkeletonsCounter destroyedSkeletonsCounter;
    [SerializeField] Transform playerTransform;
    [SerializeField] CameraShake cameraShake;
    [SerializeField] Transform necklessParticleSystem;
    [SerializeField] Transform letersParticleSystem;
    [SerializeField] SkeletonNecklessBehavior skeletonNecklessBehavior;
    [SerializeField] LayerMask checkLayer1;
    [SerializeField] LayerMask checkLayer2;
    [SerializeField] LayerMask checkLayer3;
    [SerializeField] float checkGroundRadius;
    [SerializeField] float stairsCheckRadius;
    [SerializeField] float speed;
    [SerializeField] SkeletonsStack skeletonsStack;
    [SerializeField] bool beingTested;
    [SerializeField] bool homeVersion;
    [SerializeField] Transform doorsHolder;
    [SerializeField] Transform fracturedSkeletonsHolder;
    [SerializeField] Transform castleGates;
    Transform attachedCopycat;
    Transform occupiedArenaPosition;
    Transform fracturedSkeleton;
    bool onGround;
    bool onStairs;
    bool onStone;
    VisualEffect movementVFX;
    [SerializeField] bool isSmallSkeleton;
    [SerializeField] bool isBigSkeleton;
    [SerializeField] bool isLizardSkeleton;
    bool isCrouching;
    bool isConnectedToMage;

    public float Speed { get { return speed; } set { speed = value;
            navMeshAgent.speed = speed; } }
    public bool IsCrouching { get { return isCrouching; } set { isCrouching = value; isMoving = false; } }
    public Transform AttachedCopycat { get { return attachedCopycat; } set { attachedCopycat = value; } }
    public Transform FracturedSkeleton { get { return fracturedSkeleton; } set { fracturedSkeleton = value; } }
    public bool IsSmallSkeleton { get { return isSmallSkeleton; } }
    public bool IsBigSkeleton { get { return isBigSkeleton; } }
    public bool IsLizardSkeleton { get { return isLizardSkeleton; } }

    [Header("VFX conjuration")]
    [SerializeField] ParticleSystem conjurationVFX;
    [SerializeField] ParticleSystem hitEffectVFX;
    Material upperPartMaterial;
    Material lowerPartMaterial;
    static int countConjuredSkeletons;
    static int countSmallSkeletons;
    static int countBigSkeletons;
    static int countLizardSkeletons;
    bool wasOnceConjured;

    [Header("VFX unconjuration")]
    Transform unusedCounter;
    Text unusedCounterText;
    static int countDestroyedSkeletons;
    static int destroyedSmallSkeletons;
    static int destroyedBigSkeletons;
    static int destroyedLizardSkeletons;
    bool isConjured;
    bool beingUnconjured;
    Coroutine unconjuration;
    Coroutine electricityOverload;
    [SerializeField] Transform overloadingElectricity;
    VisualEffect overloadingVFXElectricity;
    [SerializeField] GameObject destructionParticleSystem;
    [SerializeField] GameObject destroyedSkeleton;
    SkinnedMeshRenderer upperPartSkinnedrenderer;
    SkinnedMeshRenderer lowerPartSkinnedrenderer;
    SkinnedMeshRenderer bodySkinnedrenderer;

    [Header("Castle Nav")]
    List<Transform> CastleNavrout = new List<Transform>();

    [Header("Sounds Manager")]
    [SerializeField] SoundManager soundManager;
    [SerializeField] Transform portalTransform;
    AudioSource walkingGroundSound;
    AudioSource walkingStairsSound;
    AudioSource walkingStoneSound;
    AudioSource destroySkeletonSound;
    AudioSource goingThroughPortal;

    bool goingPortalPlayed;


    bool castleNavroutActive;
    bool PotentialpositionsNavroutActive;
    int castleNavpointNumber;
    NavMeshAgent navMeshAgent;
    Transform navigationTarget;
    bool isMoving = false;
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
    bool hittingCastle = false;
    bool connectedToPortal = false;
    Transform connectedCatapult;

    bool reachedPosition;
    Coroutine movingVFXCoroutine;
    Coroutine changeColorCounter;
    Coroutine changeScaleCounter;
    Coroutine turningToCastleCoroutine;

    int connectedObjects;
    bool isConnectedHands;
    bool isConnectedLeggings;
    bool isConnectedArmor;
    bool isConnectedShoes;
    bool isConnectedHelm;
    bool isConnectedGloves;
    bool isConnectedBracers;

    bool firstConnectedNotified;
    bool secondConnectedNotified;
    bool thirdConnectedNotified;
    bool fourthConnectedNotified;
    bool fifthConnectedNotified;
    bool sixthConnectedNotified;

    public bool IsConjured { get { return isConjured; } set { isConjured = value; } }
    public bool BeingUnconjured { get { return beingUnconjured; } }
    public bool CastleNavroutActive { get { return castleNavroutActive; } set { castleNavroutActive = value; } }
    public int CastleNavpointNumber { get { return castleNavpointNumber; } set { castleNavpointNumber = value; } }
    public bool HittingCastle { get { return hittingCastle; } set { hittingCastle = value; } }
    public bool ConnectedToPortal { get { return connectedToPortal; } set { connectedToPortal = value; } }
    public Transform ConnectedCatapult { get { return connectedCatapult; } set { connectedCatapult = value; } }
    public int ConnectedObjects { get { return connectedObjects; } set { connectedObjects = value; } }
    public bool IsConnectedHands { get { return isConnectedHands; } set { isConnectedHands = value; NotifyObjectConnected(); } }
    public bool IsConnectedLeggings { get { return isConnectedLeggings; } set { isConnectedLeggings = value; NotifyObjectConnected(); } }
    public bool IsConnectedArmor { get { return isConnectedArmor; } set { isConnectedArmor = value; NotifyObjectConnected(); } }
    public bool IsConnectedShoes { get { return isConnectedShoes; } set { isConnectedShoes = value; NotifyObjectConnected(); } }
    public bool IsConnectedHelm { get { return isConnectedHelm; } set { isConnectedHelm = value; NotifyObjectConnected(); } }
    public bool IsConnectedGloves { get { return isConnectedGloves; } set { isConnectedGloves = value; NotifyObjectConnected(); } }
    public bool IsConnectedBracers { get { return isConnectedBracers; } set { isConnectedBracers = value; NotifyObjectConnected(); } }

    public int ProgressParameter { get { return countConjuredSkeletons; } }
    public int ProgressParameterSecond { get { return countSmallSkeletons; } }
    public int ProgressParameterThird { get { return countBigSkeletons; } }
    public int ProgressParameterFourth { get { return countLizardSkeletons; } }
    public int CountDestroyedSkeletons { get { return countDestroyedSkeletons; } }
    public int DestroyedSmallSkeletons { get { return destroyedSmallSkeletons; } }
    public int DestroyedBigSkeletons { get { return destroyedBigSkeletons; } }
    public int DestroyedLizardSkeletons { get { return destroyedLizardSkeletons; } }
    public bool ReachedPosition { get { return reachedPosition; } }
    public AudioSource WalkingGroundSound { get { return walkingGroundSound; } }

    public Transform OccupiedArenaPositions { get { return occupiedArenaPosition; } }
    //cache
    [SerializeField] Transform targetMage1;
    public Transform CastlePosition { set { castlePosition = value; } }
    public event Action<Transform> OriginRotated = delegate { };
    public event Action OreHitted = delegate { };
    public event Action ObjectConnected = delegate { };
    public event Action ReachedCastle = delegate { };

    System.Random rand;
    void Start()
    {
        InstantiateSounds();

        rand = new System.Random();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.avoidancePriority = rand.Next(1, 10);
        if (transform.Find("VFX") != null) {
            //Debug.Log(transform.Find("VFX") + " " + transform);
            //Debug.Log(transform.Find("VFX").Find("vfxgraph_StylizedSmoke"));
            movementVFX = transform.Find("VFX").Find("vfxgraph_StylizedSmoke").GetComponent<VisualEffect>();
            movingVFXCoroutine = StartCoroutine(MovingDustSpawner()); }
        
        localAnimator = transform.GetComponent<Animator>();
        upperPartMaterial = transform.Find("MiddlePart.002").GetComponent<SkinnedMeshRenderer>().material;
        lowerPartMaterial = transform.Find("OuterPart.002").GetComponent<SkinnedMeshRenderer>().material;
        upperPartSkinnedrenderer = transform.Find("MiddlePart.002").GetComponent<SkinnedMeshRenderer>();
        lowerPartSkinnedrenderer = transform.Find("OuterPart.002").GetComponent<SkinnedMeshRenderer>();
        bodySkinnedrenderer = transform.Find("Icosphere.014").GetComponent<SkinnedMeshRenderer>();
        overloadingVFXElectricity = overloadingElectricity.GetComponent<VisualEffect>();

        gravity = -9.81f;
        checkRadius = 0;
        isIdle = true;

        if (transform.Find("Timer").Find("TimerCanvas") != null)
        {
            //Debug.Log("Found it on " + transform);
            unusedCounter = transform.Find("Timer").Find("TimerCanvas");
            unusedCounterText = unusedCounter.Find("Text").GetComponent<Text>();
        }
        /*
        if (transform.GetComponent<SmallSkeleton>() != null)
        {
            isSmallSkeleton = true;
        } else if (transform.GetComponent<BigSkeleton>() != null)
        {
            isBigSkeleton = true;
        } else if (transform.GetComponent<LizardSkeleton>() != null)
        {
            isLizardSkeleton = true;
        }
        */
        //contactManager.GetComponent<ContactManager>().OreDetected += AddTarget;
        //contactManager.GetComponent<ContactManager>().MinesDoorDetected += AddTarget;
        //contactManager.GetComponent<ContactManager>().ObjectOverloaded += ShowEmotion;
        //connectedTeleport.GetComponent<Teleporter>().TeleportFound += StopGravity;
        if (hasPortal)
        {
            //transform.GetComponent<CopycatCreator>().OriginTeleported += StopActivities;
        }
        if (!homeVersion && !hittingCastle) { GotoCastle(); }
        if (homeVersion && !isConjured) { navigationTarget = null; }
        
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

    public Transform NavigationTarget
    {
        get
        {
            return navigationTarget;
        }
        set
        {
            //Debug.Log("hello there");
            Debug.Log("skeleton get conjured to " + navigationTarget);
            //Debug.Log(value);
            if (transform == null)
            {
                //Debug.Log("was null");
                return;
            }
            //Debug.Log(transform);
            //Debug.Log(value + " " + transform);
            isConjured = true;
            if (navigationTarget != null && navigationTarget.GetComponent<PortalCamera>() != null)
            {
                if (hittingCastle) { hittingCastle = false; }
                connectedToPortal = true;
                StartCoroutine(CountDistancePortal());
            }
            if (navigationTarget != null && navigationTarget.GetComponent<IOre>() != null)
            {
                navigationTarget.GetComponent<NavMeshObstacle>().enabled = true;
            }
            if (value != null && value.GetComponent<IOre>() != null)
            {
                //Debug.Log("Connected to the ore");
                value.GetComponent<OreMiningManager>().ConnectedSkeleton = this;
            } else if (value != null && value.parent.name == "SkeletonPositions")
            {
                //Debug.Log("Connected to the main door");
                value.parent.parent.GetComponent<DoorTacklingManager>().ConnectedSkeleton = this;
            }
            if (navigationTarget != null && navigationTarget.GetComponent<IOre>() != null && value.GetComponent<IOre>() == null)
            {
                //Debug.Log("Disconnected from the ore");
                navigationTarget.GetComponent<OreMiningManager>().ConnectedSkeleton = null;
            } else if (navigationTarget != null && navigationTarget.parent.name == "SkeletonPositions" && value != null && value.parent.name != "SkeletonPositions")
            {
                //Debug.Log("Disconnected from the ore");
                
                navigationTarget.parent.parent.GetComponent<DoorConnectionManager>().ReturnPosition(navigationTarget);
                navigationTarget.parent.parent.GetComponent<DoorTacklingManager>().ConnectedSkeleton = null;
            } 
            
            if (navigationTarget == null && value != null && value.GetComponent<PersonMovement>() != null)
            {
                value.GetComponent<PersonMovement>().SkeletonAttached = true;
                isConnectedToMage = true;
                if (contactManager.GetComponent<ContactManager>().ContactedSkeleton != transform)
                {
                    contactManager.GetComponent<ContactManager>().ContactedSkeleton = transform;
                }
                Debug.Log("Hello thereee1");
            } else if (navigationTarget != null && navigationTarget.GetComponent<PersonMovement>() != null && value == null)
            {
                navigationTarget.GetComponent<PersonMovement>().SkeletonAttached = false;
                isConnectedToMage = false;
                Debug.Log("Hello thereee2");
            }
            
            
            if (!wasOnceConjured) {
                //Debug.Log("hello there");
                wasOnceConjured = true;
                skeletonNecklessBehavior.ActivateConjurationNeckless();
                countConjuredSkeletons++;
                
                if (isSmallSkeleton)
                {
                    //Debug.Log("hello there");
                    countSmallSkeletons++;
                    contactedSkeletonsCounter.CountContactedSkeleton(this.transform);
                } else if (isBigSkeleton)
                {
                    //Debug.Log("hello there");
                    countBigSkeletons++;
                    contactedSkeletonsCounter.CountContactedSkeleton(this.transform);
                } else if (isLizardSkeleton)
                {
                    //Debug.Log("hello there");
                    countLizardSkeletons++;
                    contactedSkeletonsCounter.CountContactedSkeleton(this.transform);
                }
            }
            navigationTarget = value;
            //Debug.Log(value + " " + transform);
            UpdateAnimation();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (cameraShake != null)
            {
                ShakePlayerCamera();
            }
        }

        //Debug.Log(localAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShootSkeleton"));

        if (navigationTarget != null) { navMeshAgent.destination = navigationTarget.position; }
        if (castleNavroutActive)
        {
            
            
            if (!navMeshAgent.pathPending && ((navMeshAgent.remainingDistance - navMeshAgent.stoppingDistance) < 1))
            {
                castleNavpointNumber++;
                if (CastleNavrout.Count > (castleNavpointNumber))
                {

                    navigationTarget = CastleNavrout[castleNavpointNumber];
                } else
                {

                    PotentialpositionsNavroutActive = true;
                    hittingCastle = true;
                    navigationTarget = castlePositionsManager.GetAvailablePosition();
                    occupiedArenaPosition = navigationTarget;
                    //Debug.Log("got this position " + occupiedArenaPosition);
                    castleNavroutActive = false;
                }
                
            }
            
        }
        if (PotentialpositionsNavroutActive)
        {
            
            if (navMeshAgent.velocity.magnitude < 0.15f)
            {
                //Debug.Log("Reached Position");
                if (ReachedCastle != null) { ReachedCastle(); }
                skeletonsStack.SaveSkeletonArena(transform);
                reachedPosition = true;
                PotentialpositionsNavroutActive = false;
                
            } 
        }
        
        CheckIfStopped();
        if (isConjured) { CheckIfUnused(); }

    }

    void ManageSounds()
    {
        
        if (onGround) { if (!walkingGroundSound.isPlaying) { walkingGroundSound.Play(); } } else { if (walkingGroundSound.isPlaying) { walkingGroundSound.Stop(); } }
        if (onStairs) { if (!walkingStairsSound.isPlaying) { walkingStairsSound.Play(); } } else { if (walkingStairsSound.isPlaying) { walkingStairsSound.Stop(); } }
        if (onStone) { if (!walkingStoneSound.isPlaying) { walkingStoneSound.Play(); } } else { if (walkingStoneSound.isPlaying) { walkingStoneSound.Stop(); } }
    }
    
    void TurnOffSounds()
    {
        if (walkingGroundSound != null) { walkingGroundSound.Stop(); }
        if (walkingGroundSound != null) { walkingStairsSound.Stop(); }
        if (walkingGroundSound != null) { walkingStoneSound.Stop(); }
    }

    void CheckGroundType()
    {
        onGround = (Physics.CheckSphere(checkGround.position, checkGroundRadius, checkLayer1));
        onStairs = (Physics.CheckSphere(checkGround.position, stairsCheckRadius, checkLayer2));
        onStone = (Physics.CheckSphere(checkGround.position, checkGroundRadius, checkLayer3));
    }

    void NotifyObjectConnected()
    {
        CountConnectedObjects();
        if (connectedObjects == 1 && !firstConnectedNotified)
        {
            firstConnectedNotified = true;
            SkeletonObjectQuests.CountFirstConnected();
        } else if (connectedObjects == 2 && !secondConnectedNotified)
        {
            secondConnectedNotified = true;
            SkeletonObjectQuests.CountSecondConnected();
        } else if (connectedObjects == 3 && !thirdConnectedNotified)
        {
            thirdConnectedNotified = true;
            SkeletonObjectQuests.CountThirdConnected();
        } else if (connectedObjects == 4 && !fourthConnectedNotified)
        {
            fourthConnectedNotified = true;
            SkeletonObjectQuests.CountFourthConnected();
        } else if (connectedObjects == 5 && !fifthConnectedNotified)
        {
            fifthConnectedNotified = true;
            SkeletonObjectQuests.CountFifthConnected();
        } else if (connectedObjects == 6 && !sixthConnectedNotified)
        {
            sixthConnectedNotified = true;
            SkeletonObjectQuests.CountSixthConnected();
        }
        if (ObjectConnected != null) { ObjectConnected(); }
    }

    void CountConnectedObjects()
    {
        connectedObjects++;
    }

    

    void GotoCastle()
    {
        if (CastleNavroutHolder != null)
        {
            CastleNavrout = new List<Transform>();
            foreach (Transform point in CastleNavroutHolder)
            {
                CastleNavrout.Add(point);
            }
            castleNavroutActive = true;
            
            if (castleNavpointNumber < 1)
            {
                castleNavpointNumber = 0;
            }
            navigationTarget = CastleNavrout[castleNavpointNumber];
        }
    }

    public void UploadCastleRouteNumber(int narvoutNumber)
    {
        //Debug.Log("hello2");
        CastleNavrout = new List<Transform>();
        if (CastleNavroutHolder != null)
        {
            //Debug.Log("hello3");
            foreach (Transform point in CastleNavroutHolder)
            {
                CastleNavrout.Add(point);
            }
            castleNavroutActive = true;
            castleNavpointNumber = narvoutNumber;
            if (castleNavpointNumber < 1)
            {
                castleNavpointNumber = 0;
            }
            navigationTarget = CastleNavrout[castleNavpointNumber];
            //Debug.Log("current uploaded navpoitn " + castleNavpointNumber);
        }
    }

    public void UploadCastleHitting()
    {
        PotentialpositionsNavroutActive = true;
        hittingCastle = true;
        navigationTarget = castlePositionsManager.GetAvailablePosition();
        occupiedArenaPosition = navigationTarget;
        Debug.Log("1 got this position " + occupiedArenaPosition);
        castleNavroutActive = false;
    }

    void CheckIfStopped()
    {
        
        if (navMeshAgent.velocity.magnitude < 0.15f && isMoving)
        {
            if (hittingCastle)
            {
                isMoving = false;
                localAnimator.CrossFade("ShootSkeleton", 0.1f);
                turningToCastleCoroutine = StartCoroutine(TurningToCastleIE());
                //Debug.Log("Transitioned heere");
                return;
            }
            

            //Debug.Log(navigationTarget.name);
            //Debug.Log(navigationTarget.parent.name == "SkeletonPositions");
            if (navigationTarget != null && navigationTarget.GetComponent<IOre>() == null && navigationTarget.parent.name != "SkeletonPositions")
            {
                isMoving = false;
                localAnimator.CrossFade("SkelIdle", 0.1f);
            } else if ((navigationTarget != null && navigationTarget.GetComponent<IOre>() != null) ||
                (navigationTarget != null && navigationTarget.parent.name == "SkeletonPositions"))
            {
                isMoving = false;
                localAnimator.CrossFade("SkelMine", 0.1f);
            }
            TurnOffSounds();


        } else if (navMeshAgent.velocity.magnitude > 0.15f && !isMoving)
        {
            isMoving = true;
            if (isCrouching) { localAnimator.CrossFade("CrouchSmallSkeleton", 0.1f); }
            else { localAnimator.CrossFade("SkelMove", 0.1f);

            }
            
        }
        else if (navMeshAgent.velocity.magnitude > 0.15f && isMoving)
        {
            CheckGroundType();
            ManageSounds();
        }
    }

    void UpdateAnimation()
    {
        //Debug.Log(navigationTarget);
        if (navigationTarget != null && navigationTarget.GetComponent<IOre>() == null && navigationTarget.parent.name != "SkeletonPositions")
        {
            localAnimator.CrossFade("SkelIdle", 0.1f);
        }
        else if ((navigationTarget != null && navigationTarget.GetComponent<IOre>() != null) ||
          (navigationTarget != null && navigationTarget.parent.name == "SkeletonPositions"))
        {
            localAnimator.CrossFade("SkelMine", 0.1f);
        }
        if (navigationTarget == null)
        {
            localAnimator.CrossFade("SkelIdle", 0.1f);
        }
    }

    void CheckIfUnused()
    {
        if (navigationTarget == null && !beingUnconjured)
        {
            Debug.Log("StartingUnconjuration");
            unconjuration = StartCoroutine(Unconjure());
            electricityOverload = StartCoroutine(OverloadBeforeDestruction());
            skeletonNecklessBehavior.ActivateDestructionMode();
            beingUnconjured = true;
        }
        if (navigationTarget != null && beingUnconjured)
        {
            
            StopCoroutine(changeColorCounter);
            StopCoroutine(changeScaleCounter);
            StopCoroutine(unconjuration);
            StopCoroutine(electricityOverload);
            StabilizeElectricity();
            skeletonNecklessBehavior.ActivateNormalMode();
            overloadingElectricity.gameObject.SetActive(false);
            beingUnconjured = false;
            unusedCounter.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            Debug.Log("Active again");
        }
    }

    public void DestroyManually()
    {
        StartCoroutine(DestroySkeleton());
    }

    public void DestroyCatapult()
    {
        if (connectedCatapult != null)
        {
            if (connectedCatapult.GetChild(0).Find("OreHealth").GetComponent<CatapultHealthDecreaser>() != null) { connectedCatapult.GetChild(0).Find("OreHealth").GetComponent<CatapultHealthDecreaser>().DestroyCatapult(); }
            if (connectedCatapult.GetChild(0).Find("OreHealth").GetComponent<ObjectHealthDecreaser>() != null) { connectedCatapult.GetChild(0).Find("OreHealth").GetComponent<ObjectHealthDecreaser>().DestroyCatapult(); }
        }
    }

    IEnumerator Unconjure()
    {
        //Debug.Log("BeingDeconjured");
        unusedCounter.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        int elapsed = 6;
        int lifetimeSkeleton = 1;
        changeColorCounter = StartCoroutine(ChangeColorText());
        while (elapsed > lifetimeSkeleton)
        {
            elapsed--;
            unusedCounterText.text = elapsed.ToString();
            changeScaleCounter = StartCoroutine(RescaleText());
            
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(DestroySkeleton());
        yield return null;
    }
    IEnumerator RescaleText()
    {
        float elapsed = 0;
        int lifetimeSkeleton = 1;
        float currentScale;
        RectTransform counterTransform = unusedCounterText.rectTransform;
        counterTransform.localScale = new Vector3(0.1f, 0.1f, 1);
        var counterScale = unusedCounterText.rectTransform.localScale.x;
        while (elapsed < lifetimeSkeleton)
        {
            elapsed += Time.deltaTime;
            currentScale = Mathf.Lerp(counterScale * 0.5f, counterScale, elapsed / lifetimeSkeleton);
            counterTransform.localScale = new Vector3(currentScale, currentScale, currentScale);
            yield return null;
        }
        counterTransform.localScale = new Vector3(counterScale, counterScale, counterScale);
        yield return null;
    }

    IEnumerator ChangeColorText()
    {
        float elapsed = 0;
        int lifetimeSkeleton = 5;
        Color currentColor = new Color(1, 0, 0);
        unusedCounterText.color = currentColor;
        Color counterColor = unusedCounterText.color;
        currentColor = counterColor;
        float currentRedValue;
        float currentGreenValue;
        while (elapsed < lifetimeSkeleton)
        {
            elapsed += Time.deltaTime;
            currentRedValue = Mathf.Lerp(0.5f, 0, elapsed / lifetimeSkeleton);
            currentGreenValue = Mathf.Lerp(0, 1, elapsed / lifetimeSkeleton);
            currentColor = new Color(currentGreenValue, currentRedValue, 0);
            unusedCounterText.color = currentColor;
            yield return null;
        }
        currentColor = new Color(1, 0, 0);
        unusedCounterText.color = currentColor;
        //Debug.Log(currentColor);
        //Debug.Log(counterColor);
        yield return null;
    }

    IEnumerator OverloadBeforeDestruction()
    {
        yield return new WaitForSeconds(1.5f);
        overloadingElectricity.gameObject.SetActive(true);
        
        overloadingVFXElectricity.Play();


        float expired = 0;
        float processTime = 3.5f;
        float currentElectricitySpeed = 0.1f;
        float currentElectricityThickness = 0.51f;
        float currentElectricityVFXSpeed = 1f;
        float targetElectricitySpeed = 15f;
        float targetElectricityThickness = 1f;
        float targetElectricityVFXSpeed = 20f;
        float currentElectricityHSV;
        float currentFirstMeshHSV;
        float currentSecondMeshHSV;
        Color overloadingElectricityColor = overloadingVFXElectricity.GetVector4("Color");
        Color overloadingMeshFirstColor = lowerPartMaterial.GetColor("Color_");
        Color overloadingMeshSecondColor = upperPartMaterial.GetColor("Color_");
        float hElectricity, sElectricity, vElectricity;
        float hMeshFirst, sMeshFirst, vMeshFirst;
        float hMeshSecond, sMeshSecond, vMeshSecond;
        Color.RGBToHSV(overloadingElectricityColor, out hElectricity, out sElectricity, out vElectricity);
        Color.RGBToHSV(overloadingMeshFirstColor, out hMeshFirst, out sMeshFirst, out vMeshFirst);
        Color.RGBToHSV(overloadingMeshSecondColor, out hMeshSecond, out sMeshSecond, out vMeshSecond);

        while (expired < processTime)
        {
            expired += Time.deltaTime;
            currentElectricitySpeed = Mathf.Lerp(0.1f, targetElectricitySpeed, expired / processTime);
            currentElectricityThickness = Mathf.Lerp(0.51f, targetElectricityThickness, expired / processTime);
            currentElectricityVFXSpeed = Mathf.Lerp(1f, targetElectricityVFXSpeed, expired / processTime);
            currentElectricityHSV = Mathf.Lerp(hElectricity, 1f, expired / processTime);
            currentFirstMeshHSV = Mathf.Lerp(hMeshFirst, 1f, expired / processTime);
            currentSecondMeshHSV = Mathf.Lerp(hMeshSecond, 1f, expired / processTime);
            overloadingElectricityColor = Color.HSVToRGB(currentElectricityHSV, sElectricity, vElectricity);
            overloadingMeshFirstColor = Color.HSVToRGB(currentFirstMeshHSV, sMeshFirst, vMeshFirst);
            overloadingMeshSecondColor = Color.HSVToRGB(currentSecondMeshHSV, sMeshSecond, vMeshSecond);
            lowerPartMaterial.SetVector("ElecricityWidth_", new Vector2(currentElectricitySpeed, -0.05f));
            upperPartMaterial.SetVector("ElecricityWidth_", new Vector2(currentElectricitySpeed, -0.05f));
            overloadingVFXElectricity.SetVector4("Color", overloadingElectricityColor);
            lowerPartMaterial.SetColor("Color_", overloadingMeshFirstColor);
            upperPartMaterial.SetColor("Color_", overloadingMeshSecondColor);
            lowerPartMaterial.SetFloat("Thickness_", currentElectricityThickness);
            upperPartMaterial.SetFloat("Thickness_", currentElectricityThickness);
            overloadingVFXElectricity.SetFloat("Speed", currentElectricityVFXSpeed);
            yield return null;
        }
        lowerPartMaterial.SetVector("ElecricityWidth_", new Vector2(targetElectricitySpeed, -0.05f));
        upperPartMaterial.SetVector("ElecricityWidth_", new Vector2(targetElectricitySpeed, -0.05f));
        lowerPartMaterial.SetFloat("Thickness_", targetElectricityThickness);
        upperPartMaterial.SetFloat("Thickness_", targetElectricityThickness);
        overloadingVFXElectricity.SetFloat("Speed", targetElectricityVFXSpeed);
        overloadingElectricity.gameObject.SetActive(false);
        skeletonNecklessBehavior.NecklessParticleSystem.gameObject.SetActive(false);
        skeletonNecklessBehavior.LettersParticleSystem.gameObject.SetActive(false);
        yield return null;
    }

    void StabilizeElectricity()
    {
        float normalElectricitySpeed = 0.1f;
        float normalElectricityThickness = 0.51f;
        float normalElectricityVFXSpeed = 1f;
        lowerPartMaterial.SetVector("ElecricityWidth_", new Vector2(normalElectricitySpeed, -0.05f));
        upperPartMaterial.SetVector("ElecricityWidth_", new Vector2(normalElectricitySpeed, -0.05f));
        lowerPartMaterial.SetFloat("Thickness_", normalElectricityThickness);
        upperPartMaterial.SetFloat("Thickness_", normalElectricityThickness);
        Color overloadingElectricityColor = overloadingVFXElectricity.GetVector4("Color");
        Color overloadingMeshFirstColor = lowerPartMaterial.GetColor("Color_");
        Color overloadingMeshSecondColor = upperPartMaterial.GetColor("Color_");
        float hElectricity, sElectricity, vElectricity;
        float hMeshFirst, sMeshFirst, vMeshFirst;
        float hMeshSecond, sMeshSecond, vMeshSecond;
        Color.RGBToHSV(overloadingElectricityColor, out hElectricity, out sElectricity, out vElectricity);
        Color.RGBToHSV(overloadingMeshFirstColor, out hMeshFirst, out sMeshFirst, out vMeshFirst);
        Color.RGBToHSV(overloadingMeshSecondColor, out hMeshSecond, out sMeshSecond, out vMeshSecond);
        overloadingElectricityColor = Color.HSVToRGB(0.69f, sElectricity, vElectricity);
        overloadingMeshFirstColor = Color.HSVToRGB(0.69f, sMeshFirst, vMeshFirst);
        overloadingMeshSecondColor = Color.HSVToRGB(0.69f, sMeshSecond, vMeshSecond);
        overloadingVFXElectricity.SetVector4("Color", overloadingElectricityColor);
        lowerPartMaterial.SetColor("Color_", overloadingMeshFirstColor);
        upperPartMaterial.SetColor("Color_", overloadingMeshSecondColor);
        overloadingElectricity.GetComponent<VisualEffect>().SetFloat("Speed", normalElectricityVFXSpeed);
    }

    IEnumerator DestroySkeleton()
    {

        if (hittingCastle) { 
            //Debug.Log("5 hitti");
        }
        destructionParticleSystem.SetActive(true);
        destructionParticleSystem.GetComponent<ParticleSystem>().Play();
        //Debug.Log("DestroyedSkeleton");
        ShakePlayerCamera();
        //lowerPartSkinnedrenderer.gameObject.SetActive(false);
        //upperPartSkinnedrenderer.gameObject.SetActive(false);
        bodySkinnedrenderer.gameObject.SetActive(false);
        GameObject instantiatedDestroyedSkeleton = Instantiate(destroyedSkeleton, transform.position, transform.rotation);
        fracturedSkeleton = instantiatedDestroyedSkeleton.transform;
        fracturedSkeleton.gameObject.AddComponent<AutoDestroyObject>();
        fracturedSkeleton.gameObject.GetComponent<AutoDestroyObject>().TimeDestroy = 3f;
        fracturedSkeleton.gameObject.GetComponent<AutoDestroyObject>().InitiateDestruction();
        fracturedSkeleton.parent = fracturedSkeletonsHolder;
        StopCoroutine(movingVFXCoroutine);
        destroySkeletonSound = soundManager.LocateAudioSource("SkeletonBlast", instantiatedDestroyedSkeleton.transform);
        destroySkeletonSound.Play();
        unusedCounter.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        Destroy(walkingGroundSound);
        Destroy(walkingStoneSound);
        Destroy(walkingStairsSound);
        yield return new WaitForSeconds(1.5f);
        foreach (Transform bone in instantiatedDestroyedSkeleton.transform)
        {
            if (bone.GetComponent<SphereCollider>() != null) { bone.GetComponent<SphereCollider>().isTrigger = true; }
        }
        yield return new WaitForSeconds(1.5f);
        countDestroyedSkeletons++;
        if (isSmallSkeleton)
        {
            destroyedSmallSkeletons++;
            destroyedSkeletonsCounter.CountDestroyedSkeleton(this.transform);
        } else if (isSmallSkeleton)
        {
            destroyedBigSkeletons++;
            destroyedSkeletonsCounter.CountDestroyedSkeleton(this.transform);
        } else if (isSmallSkeleton)
        {
            destroyedBigSkeletons++;
            destroyedSkeletonsCounter.CountDestroyedSkeleton(this.transform);
        }
        
        //DestroyImmediate(instantiatedDestroyedSkeleton);
        Destroy(gameObject);
        yield return null;
    }

    public void DestroyUploadSkeleton()
    {
        UnsubscribeBeforeDestruction();
        bodySkinnedrenderer.gameObject.SetActive(false);
        unusedCounter.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        Destroy(walkingGroundSound);
        Destroy(walkingStoneSound);
        Destroy(walkingStairsSound);
        DestroyImmediate(gameObject);
    }

    void ShakePlayerCamera()
    {
        //Debug.Log(Vector3.Distance(playerTransform.position, this.transform.position));
        if (Vector3.Distance(playerTransform.position, this.transform.position) < 6
            && !cameraShake.Activated) {
            cameraShake.ShakeCamera(0.75f, 0.065f * (1/Vector3.Distance(playerTransform.position, this.transform.position)) * 18);
        }
    }

    IEnumerator MovingDustSpawner()
    {
        while (true)
        {
            //Debug.Log("Hello started there");
            yield return new WaitForSeconds(0.2f);
            if (localAnimator.GetCurrentAnimatorStateInfo(0).IsName("SkelMove")) { movementVFX.SendEvent("CharacterMoved"); }
        }
    }

    public void StartChazingPortal(Transform foundPortal)
    {
        transform.GetComponent<CopycatCreator>().enabled = true;
        StartCoroutine(BecomeCojured(foundPortal));
        StartCoroutine(CountDistancePortal());
    }

    IEnumerator BecomeCojured(Transform foundPortal)
    {
        conjurationVFX.gameObject.SetActive(true);
        conjurationVFX.Play();


        float expired = 0;
        float processTime = 0.15f;
        float currentElectricityValue = 0;
        while (expired < processTime)
        {
            expired += Time.deltaTime;
            currentElectricityValue = Mathf.Lerp(0, 1, expired / processTime);
            lowerPartMaterial.SetFloat("_ElectricityValue", currentElectricityValue);
            upperPartMaterial.SetFloat("_ElectricityValue", currentElectricityValue);
            yield return null;
        }
        hitEffectVFX.gameObject.SetActive(true);
        hitEffectVFX.Play();
        lowerPartMaterial.SetFloat("_ElectricityValue", 1);
        upperPartMaterial.SetFloat("_ElectricityValue", 1);
        isConjured = true;
        yield return new WaitForSeconds(0.5f);
        navigationTarget = foundPortal;
        yield return null;

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
        localAnimator.CrossFade("SkelIdle", 0.1f);
    }

    void StayNearCastle()
    {
        if (!localAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShootSkeleton")) { localAnimator.CrossFade("ShootSkeleton", 0.1f); }
        
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
        Debug.Log(navigationTarget);
        //Debug.Log(navigationTarget.GetComponent<PersonMovement>() != null);
        if (navigationTarget != null && navigationTarget.GetComponent<PersonMovement>() != null)
        {
            navigationTarget.GetComponent<PersonMovement>().SkeletonAttached = false;

            if (targetOre.GetComponent<NavMeshObstacle>() != null)
            {
                targetOre.GetComponent<NavMeshObstacle>().enabled = false;
            }
            
            if (targetOre.transform.GetComponent<DoorTacklingManager>() != null)
            {
                int doorIndex = targetOre.transform.GetComponent<DoorTacklingManager>().DoorLevel;
                //Debug.Log("found door hand index " + doorIndex);
                DoorConnectionManager targetConnectionManager = null;
                foreach (Transform door in doorsHolder)
                {
                    //Debug.Log("searched door hand index " + door.GetComponent<DoorTacklingManager>().DoorLevel);
                    if (door.GetComponent<DoorTacklingManager>().DoorLevel == doorIndex)
                    {
                        targetConnectionManager = door.GetComponent<DoorConnectionManager>();
                        break;
                    }
                    
                }
                NavigationTarget = targetConnectionManager.GetPosition();
            } else
            {
                NavigationTarget = targetOre;
            }
            //Debug.Log(navigationTarget);

        } else { 
            Debug.Log("Skeleton is not connected to anything "); }
    }

    public void AddTarget(DoorConnectionManager targetDoors)
    {
        NavigationTarget = targetDoors.GetPosition();

    }

    public void ShowEmotion()
    {
        if (navigationTarget.GetComponent<PersonMovement>() != null)
        {
            transform.GetComponent<SkeletonEmotionsShower>().ShowConfusion();
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
        //Debug.Log("chasing mage");
        TurnAroundTo(targetMage);
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
        localAnimator.CrossFade("SkelMine", 0.1f);
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
            localAnimator.CrossFade("SkelMove", 0.1f);
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
        bool isCollided = Physics.CheckSphere(checkSurrounding.position, 3f, checkSurroundingLayer);
        Collider[] hitCollider = Physics.OverlapSphere(checkSurrounding.position, 3f, checkSurroundingLayer);
        if (isCollided == true && hitCollider[0].transform != targetOre)
        {
            Debug.Log(hitCollider[0].transform);
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

    public void HitOre()
    {
        if (OreHitted != null)
        {
            OreHitted();
        }
    }

    /*
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
    */

    public void SubscribeAfterInstantiation()
    {
        contactManager.GetComponent<ContactManager>().OreDetected += AddTarget;
        contactManager.GetComponent<ContactManager>().MinesDoorDetected += AddTarget;
        contactManager.GetComponent<ContactManager>().ObjectOverloaded += ShowEmotion;
        connectedTeleport.GetComponent<Teleporter>().TeleportFound += StopGravity;
        transform.GetComponent<CopycatCreator>().OriginTeleported += StopActivities;
        localAnimator = transform.GetComponent<Animator>();
    }

    public void UnsubscribeBeforeDestruction()
    {
        //Debug.Log("hello there11");
        
        contactManager.GetComponent<ContactManager>().OreDetected -= AddTarget;
        contactManager.GetComponent<ContactManager>().MinesDoorDetected -= AddTarget;
        contactManager.GetComponent<ContactManager>().ObjectOverloaded -= ShowEmotion;
        connectedTeleport.GetComponent<Teleporter>().TeleportFound -= StopGravity;
        transform.GetComponent<CopycatCreator>().OriginTeleported -= StopActivities;
    }

    public void InstantiateSounds()
    {
        walkingGroundSound = soundManager.LocateAudioSource("SkeletonWalkGround", transform);
        walkingStairsSound = soundManager.LocateAudioSource("SkeletonWalkStairs", transform);
        walkingStoneSound = soundManager.LocateAudioSource("SkeletonWalkStone", transform);
    }

    public void ThroughPortalSound()
    {
        goingPortalPlayed = true;
        goingThroughPortal = soundManager.LocateAudioSource("GoingThroughPortal", portalTransform);
        goingThroughPortal.Play();
        TurnOffSounds();
    }

    IEnumerator CountDistancePortal()
    {
        bool portalActivated = false;
        while (true)
        {
            var currentDistance = Vector3.Distance(transform.position, navigationTarget.position);
            if (currentDistance < 5) { portalActivated = true; }
            if (currentDistance < 4.5f && portalActivated && !goingPortalPlayed) { ThroughPortalSound(); }
            //Debug.Log(currentDistance);
            yield return null;
        }
        
    }

    IEnumerator TurningToCastleIE()
    {
        float expired = 0;
        float targetTime = 3;
        while (expired < targetTime)
        {
            expired += Time.deltaTime;
            TurnAroundTo(castleGates);
            yield return null;
        }
        
    }
}
