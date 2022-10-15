using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatapultMovement : MonoBehaviour
{
    [Header("Castle Nav")]
    List<Transform> CastleNavrout = new List<Transform>();
    [SerializeField] Transform CastleNavroutHolder;
    [SerializeField] CastlePositionsManager castlePositionsManager;
    [SerializeField] Transform potentialTargetsPositions;
    [SerializeField] Transform castlePoint;

    bool reachedPosition;
    bool CastleNavroutActive;
    bool lookingOnCastle;
    bool navMeshEnabled;
    bool PotentialpositionsNavroutActive;
    bool changingTarget;
    int castleNavpointNumber;
    NavMeshAgent navMeshAgent;
    Transform navigationTarget;
    Transform fireTarget;
    Transform connectedSkeleton;

    System.Random rand;
    public Transform NavigationTarget
    {
        get
        {
            return navigationTarget;
        }
        set
        {
            navigationTarget = value;
        }
    }

    public bool ChangingTarget {  get { return changingTarget; } }
    public Transform FireTarget { get { return fireTarget; } }
    public Transform ConnectedSkeleton { get { return connectedSkeleton; } set { connectedSkeleton = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
        /*
        
        //Debug.Log(potentialTargetsPositions);
        //Debug.Log(potentialTargetsPositions.childCount);
        int randomIndex = rand.Next(0, (potentialTargetsPositions.childCount - 1));
        //Debug.Log(randomIndex);
        
        GotoCastle();
        */
    }

    public void InstantiationSetUp()
    {
        rand = new System.Random();
        navMeshEnabled = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.avoidancePriority = rand.Next(30, 50);
    }

    public void SubscribeOnSkeleton(Transform skeleton)
    {
        ConnectedSkeleton = skeleton;
        navigationTarget = skeleton;
        skeleton.GetComponent<SkeletonBehavior>().ReachedCastle += PlaceOnCastle;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(navMeshAgent);
        if (navMeshEnabled && navigationTarget != null) { navMeshAgent.destination = navigationTarget.position; }
        if (navMeshEnabled && CastleNavroutActive)
        {


            if (!navMeshAgent.pathPending && ((navMeshAgent.remainingDistance - navMeshAgent.stoppingDistance) < 1))
            {
                castleNavpointNumber++;
                if (CastleNavrout.Count > (castleNavpointNumber))
                {
                    navigationTarget = CastleNavrout[castleNavpointNumber];
                }
                else
                {
                    PotentialpositionsNavroutActive = true;
                    //navigationTarget = castlePositionsManager.GetAvailablePosition();
                    CastleNavroutActive = false;
                }

            }

        }
        
        if (navMeshEnabled && PotentialpositionsNavroutActive)
        {
            if (navMeshAgent.velocity.magnitude < 0.15f)
            {
                //Debug.Log("Reached Position");
                reachedPosition = true;
                lookingOnCastle = true;
                navMeshEnabled = false;
                navMeshAgent.enabled = false;
                StartCoroutine(LookAtCastle(ChooseCurrentTarget(), 5));
                PotentialpositionsNavroutActive = false;
            }
        }
    }

    void PlaceOnCastle()
    {
        PotentialpositionsNavroutActive = true;
        navigationTarget = connectedSkeleton.GetComponent<SkeletonBehavior>().OccupiedArenaPositions.GetChild(0);
        CastleNavroutActive = false;
        Debug.Log(navigationTarget);
    }

    public void ChooseNewTarget()
    {
        changingTarget = true;
        StartCoroutine(LookAtCastle(ChooseCurrentTarget(), 5));
        
    }

    Transform ChooseCurrentTarget()
    {
        //Debug.Log(potentialTargetsPositions);
        //Debug.Log(potentialTargetsPositions.childCount);
        int randomIndex = rand.Next(0, (potentialTargetsPositions.childCount - 1));
        
        return potentialTargetsPositions.GetChild(randomIndex);
    }

    IEnumerator LookAtCastle(Transform targetPoint, float delay)
    {
        fireTarget = targetPoint;
        yield return new WaitForSeconds(1);
        float elapsed = 0;
        var targetRotation = Quaternion.LookRotation(targetPoint.transform.position - transform.position);
        Quaternion startRotation = transform.rotation;
        Quaternion currentRotation;
        while (elapsed < delay)
        {
            elapsed += Time.deltaTime;
            currentRotation = Quaternion.Lerp(startRotation, targetRotation, elapsed/delay);
            transform.rotation = currentRotation;
            yield return null;
        }
        //transform.rotation = targetRotation;
        changingTarget = false;
        yield return null;
    }

    void GotoCastle()
    {
        foreach (Transform point in CastleNavroutHolder)
        {
            CastleNavrout.Add(point);
        }
        CastleNavroutActive = true;
        navigationTarget = CastleNavrout[0];
        castleNavpointNumber = 0;
    }
    

}
