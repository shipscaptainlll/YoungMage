using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultFire : MonoBehaviour
{
    [SerializeField] Transform catapultAmmo;
    [SerializeField] Transform shootingStart;
    Animator catapultAnimator;
    CatapultMovement catapultMovement;
    CastleHealthDecreaser castleHealthDecreaser;
    bool isFiring;

    public CastleHealthDecreaser CastleHealthDecreaser { get { return castleHealthDecreaser; } set { castleHealthDecreaser = value; } }
    // Start is called before the first frame update
    void Start()
    {
        catapultMovement = transform.parent.parent.GetComponent<CatapultMovement>();
        catapultAnimator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (!isFiring && !catapultMovement.ChangingTarget)
        {
            isFiring = true;
            catapultAnimator.Play("CatapultFire");
            Vector2 calculatedVelocities = CalculateVelocities();
            ShootAmmo(calculatedVelocities.x, calculatedVelocities.y);
            catapultMovement.ChooseNewTarget();
        }
        
    }

    void ShootAmmo(float xVelocity, float yVelocity)
    {
        Transform newAmmo = Instantiate(catapultAmmo);
        newAmmo.position = shootingStart.position;
        newAmmo.GetComponent<Rigidbody>().velocity = transform.forward * yVelocity + -transform.right * xVelocity;
        newAmmo.GetComponent<CatapultAmmo>().CastleHealthDecreaser = castleHealthDecreaser;
    }

    Vector2 CalculateVelocities()
    {
        Vector2 calculatedVelocities;
        Vector3 initialPosition = shootingStart.position;
        Vector3 finalPosition = catapultMovement.FireTarget.position;
        float Distance = Vector3.Distance(initialPosition, finalPosition);
        float gravity = Physics.gravity.y;
        float tangAngle = Mathf.Tan(45 * Mathf.Deg2Rad);
        float height = finalPosition.y - initialPosition.y;
        float xVelocity = Mathf.Sqrt(gravity * Distance * Distance / (2f * (height - Distance * tangAngle)));
        float yVelocity = tangAngle * xVelocity;
        calculatedVelocities = new Vector2(xVelocity, yVelocity);
        return calculatedVelocities;
    }


    public void CheckNotFiring()
    {
        isFiring = false;
    }
}
