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
    [SerializeField] Transform castlePoint;

    bool reachedPosition;
    bool CastleNavroutActive;
    bool lookingOnCastle;
    bool navMeshEnabled;
    bool PotentialpositionsNavroutActive;
    int castleNavpointNumber;
    NavMeshAgent navMeshAgent;
    Transform navigationTarget;

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



    // Start is called before the first frame update
    void Start()
    {
        navMeshEnabled = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        GotoCastle();
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
                    navigationTarget = castlePositionsManager.GetAvailablePosition();
                    CastleNavroutActive = false;
                }

            }

        }
        /*
        if (lookingOnCastle)
        {

            
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
            //if () ;
            float xAngleDiff = transform.rotation.x - targetRotation.x;
            float yAngleDiff = transform.rotation.x - targetRotation.x;
            float zAngleDiff = transform.rotation.x - targetRotation.x;
            Debug.Log(transform + " " + transform.rotation.eulerAngles + " " + transform.localRotation.eulerAngles + " " + targetRotation.eulerAngles );
            //transform.LookAt(castlePoint);
            //lookingOnCastle = false;
        }*/
        if (navMeshEnabled && PotentialpositionsNavroutActive)
        {
            if (navMeshAgent.velocity.magnitude < 0.15f)
            {
                Debug.Log("Reached Position");
                reachedPosition = true;
                lookingOnCastle = true;
                navMeshEnabled = false;
                navMeshAgent.enabled = false;
                StartCoroutine(LookAtCastle());
                PotentialpositionsNavroutActive = false;
            }
        }
    }

    IEnumerator LookAtCastle()
    {
        float elapsed = 0;
        float delay = 5;
        var targetRotation = Quaternion.LookRotation(castlePoint.transform.position - transform.position);
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
