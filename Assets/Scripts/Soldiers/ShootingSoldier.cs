using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSoldier : MonoBehaviour
{
    [SerializeField] Animator bowAnimator;
    [SerializeField] Animator soldierAnimator;
    [SerializeField] SkeletonsStack skeletonsStack;
    [SerializeField] Transform shootingStart;
    [SerializeField] Transform arrowAmmo;
    [SerializeField] float arrowSpeed;
    Transform nextTarget;
    System.Random rand;

    public Transform NextTarget { get { return nextTarget; } set { nextTarget = value; } }

    public event Action ShootedTarget = delegate { };
    public event Action TargetsUnavailable = delegate { };
    public event Action TargetLost = delegate { };
    public event Action TargetEliminated = delegate {};
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (!soldierAnimator.GetCurrentAnimatorStateInfo(0).IsName("SoldierFiringBow"))
        {
            if (nextTarget == null) { nextTarget = GetNextTarget(); ConnectToTarget(); }
            //Debug.Log(nextTarget);
            if (nextTarget != null) {
                RotateSoldier();
                SoldierShootAnimation();
            }
        }
    }

    public void ConnectToTarget()
    {
        if (nextTarget != null) { nextTarget.GetComponent<SkeletonHealthDecreaser>().SkeletonUnsubscribed += UnconnectFromTarget; 
        
        }
    }

    public void UnconnectFromTarget()
    {
        if (TargetEliminated != null) { TargetEliminated(); }
        nextTarget.GetComponent<SkeletonHealthDecreaser>().SkeletonUnsubscribed -= UnconnectFromTarget;
        nextTarget = null;
        
    }

    public Transform GetNextTarget()
    {
        if (skeletonsStack.SkeletonsArena.Count > 0)
        {
            Transform targetSkeleton = skeletonsStack.SkeletonsArena[rand.Next(0, skeletonsStack.SkeletonsArena.Count)];
            //Debug.Log(targetSkeleton);
            
            return targetSkeleton;
        }
        if (TargetsUnavailable != null) { TargetsUnavailable(); }
        return null;
    }

    public void SoldierShootAnimation()
    {
        soldierAnimator.CrossFade("SoldierFiringBow", 0.1f);
    }

    public void ShootBow()
    {
        bowAnimator.CrossFade("BowFire", 0.1f);
    }

    public void FireArrow()
    {
        if (nextTarget == null) { return; }
        if (ShootedTarget != null) { ShootedTarget(); }
        Vector2 calculatedVelocities = CalculateVelocities();
        ShootAmmo(calculatedVelocities.x, calculatedVelocities.y);
        
    }

    void RotateSoldier()
    {
        transform.LookAt(nextTarget);
    }

    void ShootAmmo(float xVelocity, float yVelocity)
    {
        Transform newAmmo = Instantiate(arrowAmmo);
        newAmmo.gameObject.SetActive(true);
        newAmmo.GetComponent<ArrowSoldier>().Target = nextTarget;
        newAmmo.GetChild(0).GetComponent<ParticleSystem>().Play();
        //Debug.Log(newAmmo);
        newAmmo.position = shootingStart.position;
        newAmmo.LookAt(nextTarget);
        newAmmo.GetComponent<Rigidbody>().velocity = -transform.up * 1 + transform.forward * xVelocity * 4.5f;
        newAmmo.GetComponent<Rigidbody>().AddTorque(new Vector3(0, 0, -15));
    }

    Vector2 CalculateVelocities()
    {
        Vector2 calculatedVelocities;
        Vector3 initialPosition = shootingStart.position;
        Vector3 finalPosition = nextTarget.position;
        float Distance = Vector3.Distance(initialPosition, finalPosition);
        float gravity = Physics.gravity.y;
        float tangAngle = Mathf.Tan(45 * Mathf.Deg2Rad);
        float height = finalPosition.y - initialPosition.y;
        float xVelocity = Mathf.Sqrt(gravity * Distance * Distance * arrowSpeed / (2f * (height - Distance * tangAngle)));
        float yVelocity = tangAngle * xVelocity;
        calculatedVelocities = new Vector2(xVelocity, yVelocity);
        return calculatedVelocities;
    }
}
