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
    VisualEffect movementVFX;
    bool isSmallSkeleton;
    bool isBigSkeleton;
    bool isLizardSkeleton;

    [Header("VFX conjuration")]
    [SerializeField] ParticleSystem conjurationVFX;
    [SerializeField] ParticleSystem hitEffectVFX;
    Material upperPartMaterial;
    Material lowerPartMaterial;
    static int countConjuredSkeletons;
    static int countSmallSkeletons;
    bool wasOnceConjured;

    [Header("VFX unconjuration")]
    Transform unusedCounter;
    Text unusedCounterText;
    static int countDestroyedSkeletons;
    static int destroyedSmallSkeletons;
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

    [Header("Castle Nav")]
    List <Transform> CastleNavrout = new List<Transform>();
    

    bool CastleNavroutActive;
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
    float speed = 9f;
    bool reachedPosition;
    Coroutine changeColorCounter;
    Coroutine changeScaleCounter;

    public int ProgressParameter { get { return countConjuredSkeletons; } }
    public int ProgressParameterSecond { get { return countSmallSkeletons; } }
    public int CountDestroyedSkeletons { get { return countDestroyedSkeletons; } }
    public int DestroyedSmallSkeletons { get { return destroyedSmallSkeletons; } }
    public bool ReachedPosition { get { return reachedPosition; } }
    //cache
    [SerializeField] Transform targetMage1;
    public Transform CastlePosition { set { castlePosition = value; } }
    public event Action<Transform> OriginRotated = delegate { };
    public event Action OreHitted = delegate { };
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (transform.Find("VFX") != null) {
            Debug.Log(transform.Find("VFX") + " " + transform);
            Debug.Log(transform.Find("VFX").Find("vfxgraph_StylizedSmoke"));
            movementVFX = transform.Find("VFX").Find("vfxgraph_StylizedSmoke").GetComponent<VisualEffect>(); 
            StartCoroutine(MovingDustSpawner()); }
        
        localAnimator = transform.GetComponent<Animator>();
        upperPartMaterial = transform.Find("MiddlePart.002").GetComponent<SkinnedMeshRenderer>().material;
        lowerPartMaterial = transform.Find("OuterPart.002").GetComponent<SkinnedMeshRenderer>().material;
        upperPartSkinnedrenderer = transform.Find("MiddlePart.002").GetComponent<SkinnedMeshRenderer>();
        lowerPartSkinnedrenderer = transform.Find("OuterPart.002").GetComponent<SkinnedMeshRenderer>();
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
        if (transform.GetComponent<SmallSkeleton>() != null)
        {
            isSmallSkeleton = true;
        }
        contactManager.GetComponent<ContactManager>().OreDetected += AddTarget;
        connectedTeleport.GetComponent<Teleporter>().TeleportFound += StopGravity;
        if (hasPortal)
        {
            transform.GetComponent<CopycatCreator>().OriginTeleported += StopActivities;
        }
        GotoCastle();
        
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
            isConjured = true;
            if (navigationTarget != null && navigationTarget.GetComponent<IOre>() != null)
            {
                navigationTarget.GetComponent<NavMeshObstacle>().enabled = true;
            }
            if (value != null && value.GetComponent<IOre>() != null)
            {
                Debug.Log("Connected to the ore");
                value.GetComponent<OreMiningManager>().ConnectedSkeleton = this;
            }
            if (navigationTarget != null && navigationTarget.GetComponent<IOre>() != null && value.GetComponent<IOre>() == null)
            {
                Debug.Log("Disconnected from the ore");
                navigationTarget.GetComponent<OreMiningManager>().ConnectedSkeleton = null;
            }
            if (!wasOnceConjured) {
                //Debug.Log("hello there");
                wasOnceConjured = true;
                countConjuredSkeletons++;
                
                if (isSmallSkeleton)
                {
                    //Debug.Log("hello there");
                    countSmallSkeletons++;
                    contactedSkeletonsCounter.CountContactedSkeleton(this.transform);
                }
            }
            navigationTarget = value;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (navigationTarget != null) { navMeshAgent.destination = navigationTarget.position; }
        if (CastleNavroutActive)
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
                    navigationTarget = castlePositionsManager.GetAvailablePosition();
                    CastleNavroutActive = false;
                }
                
            }
            
        }
        if (PotentialpositionsNavroutActive)
        {
            if (navMeshAgent.velocity.magnitude < 0.15f)
            {
                Debug.Log("Reached Position");
                reachedPosition = true;
                PotentialpositionsNavroutActive = false;
            }
        }
        CheckIfStopped();
        if (isConjured) { CheckIfUnused(); }
    }

    void GotoCastle()
    {
        foreach(Transform point in CastleNavroutHolder)
        {
            CastleNavrout.Add(point);
        }
        CastleNavroutActive = true;
        navigationTarget = CastleNavrout[0];
        castleNavpointNumber = 0;
    }

    void CheckIfStopped()
    {
        
        if (navMeshAgent.velocity.magnitude < 0.15f && isMoving)
        {
            if (navigationTarget != null && navigationTarget.GetComponent<IOre>() == null)
            {
                isMoving = false;
                localAnimator.Play("SkelIdle");
            } else if (navigationTarget != null && navigationTarget.GetComponent<IOre>() != null)
            {
                isMoving = false;
                localAnimator.Play("SkelMine");
            }
            
        } else if (navMeshAgent.velocity.magnitude > 0.15f && !isMoving)
        {
            isMoving = true;
            localAnimator.Play("SkelMove");
        }
        
    }

    void CheckIfUnused()
    {
        if (navigationTarget == null && !beingUnconjured)
        {
            unconjuration = StartCoroutine(Unconjure());
            electricityOverload = StartCoroutine(OverloadBeforeDestruction());
            beingUnconjured = true;
        }
        if (navigationTarget != null && beingUnconjured)
        {
            StopCoroutine(changeColorCounter);
            StopCoroutine(changeScaleCounter);
            StopCoroutine(unconjuration);
            StopCoroutine(electricityOverload);
            StabilizeElectricity();
            overloadingElectricity.gameObject.SetActive(false);
            beingUnconjured = false;
            unusedCounter.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            Debug.Log("Active again");
        }
    }

    IEnumerator Unconjure()
    {
        Debug.Log("BeingDeconjured");
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
        Debug.Log(currentColor);
        Debug.Log(counterColor);
        yield return null;
    }

    IEnumerator OverloadBeforeDestruction()
    {
        overloadingElectricity.gameObject.SetActive(true);
        
        overloadingVFXElectricity.Play();

        float expired = 0;
        float processTime = 5f;
        float currentElectricitySpeed = 0.1f;
        float currentElectricityThickness = 0.51f;
        float currentElectricityVFXSpeed = 1f;
        float targetElectricitySpeed = 15f;
        float targetElectricityThickness = 1f;
        float targetElectricityVFXSpeed = 20f;
        while (expired < processTime)
        {
            expired += Time.deltaTime;
            currentElectricitySpeed = Mathf.Lerp(0.1f, targetElectricitySpeed, expired / processTime);
            currentElectricityThickness = Mathf.Lerp(0.51f, targetElectricityThickness, expired / processTime);
            currentElectricityVFXSpeed = Mathf.Lerp(1f, targetElectricityVFXSpeed, expired / processTime);
            lowerPartMaterial.SetVector("ElecricityWidth_", new Vector2(currentElectricitySpeed, -0.05f));
            upperPartMaterial.SetVector("ElecricityWidth_", new Vector2(currentElectricitySpeed, -0.05f));
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
        overloadingElectricity.GetComponent<VisualEffect>().SetFloat("Speed", normalElectricityVFXSpeed);
    }

    IEnumerator DestroySkeleton()
    {
        

        destructionParticleSystem.SetActive(true);
        destructionParticleSystem.GetComponent<ParticleSystem>().Play();
        Debug.Log("DestroyedSkeleton");
        lowerPartSkinnedrenderer.gameObject.SetActive(false);
        upperPartSkinnedrenderer.gameObject.SetActive(false);
        GameObject instantiatedDestroyedSkeleton = Instantiate(destroyedSkeleton, transform.position, transform.rotation);
        unusedCounter.gameObject.GetComponent<CanvasGroup>().alpha = 0;
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
        }
        Destroy(instantiatedDestroyedSkeleton);
        Destroy(gameObject);
        yield return null;
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
        StartCoroutine(BecomeCojured(foundPortal));
        
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
        if (navigationTarget.GetComponent<PersonMovement>() != null)
        {
            if (targetOre.GetComponent<NavMeshObstacle>() != null)
            {
                targetOre.GetComponent<NavMeshObstacle>().enabled = false;
            }
            
            NavigationTarget = targetOre;
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

    void HitOre()
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
}
